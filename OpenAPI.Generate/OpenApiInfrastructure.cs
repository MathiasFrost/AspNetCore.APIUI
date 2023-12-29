using System.Reflection;
using System.Text.Json;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using OpenAPI.Generate.Models;
using Path = OpenAPI.Generate.Models.Path;

namespace OpenAPI.Generate;

/// <summary> TODOC </summary>
public static class OpenApiInfrastructure
{
    /// <summary> TODOC </summary>
    private static bool _hasCompiled;

    /// <summary> TODOC </summary>
    [PublicAPI]
    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        return app.Use(static async (context, next) =>
        {
            string normalizedPath = context.Request.Path.ToString().TrimEnd('/');
            if (normalizedPath.Contains(".."))
            {
                await next();
                return;
            }

            switch (normalizedPath)
            {
                case "/openapi":
                    await RouteOpenApi(context);
                    break;
                case "/openapi/ui":
                    await RouteOpenApiUi(context);
                    break;
                default:
                    if (normalizedPath.StartsWith("/openapi/ui/_app")) await RouteOpenApiUiApp(context, next);
                    else await next();
                    break;
            }
        });
    }

    /// <summary> TODOC </summary>
    private static async Task RouteOpenApiUiApp(HttpContext context, Func<Task> next)
    {
        string file = context.Request.Path.ToString().Replace("/openapi/ui/", String.Empty);
        string dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

        IEnumerable<string> parts = new[] { dir }.Concat(from part in file.Split('/') select part);
        string path = System.IO.Path.Combine(parts.ToArray());

        string[] files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
        if (files.All(s => s != path))
        {
            await next();
            return;
        }

        if (path.EndsWith(".js")) context.Response.ContentType = "text/javascript";
        if (path.EndsWith(".json")) context.Response.ContentType = "application/json";
        if (path.EndsWith(".css")) context.Response.ContentType = "text/css";

        string content = await File.ReadAllTextAsync(path, context.RequestAborted);
        if (content.Contains("$PUBLIC_BACKEND_URL$")) content = content.Replace("$PUBLIC_BACKEND_URL$", $"{context.Request.Scheme}://{context.Request.Host}/");
        await context.Response.WriteAsync(content, context.RequestAborted);
    }

    /// <summary> TODOC </summary>
    private static async Task RouteOpenApiUi(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "index.html");
        string content = await File.ReadAllTextAsync(path, context.RequestAborted);
        await context.Response.WriteAsync(content, context.RequestAborted);
    }

    /// <summary> TODOC </summary>
    private static async Task RouteOpenApi(HttpContext context)
    {
        string? executing = Assembly.GetExecutingAssembly().GetName().Name;
        string? filePath = null;
        if (executing != null)
        {
            var name = $"{executing}.OpenAPI.json";
            filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name);
            if (!_hasCompiled && File.Exists(filePath))
            {
                _hasCompiled = true;
                string content = await File.ReadAllTextAsync(filePath);
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(content, context.RequestAborted);
                return;
            }
        }

        var res = new OpenApiDocument {
            OpenApi = "3.0.1",
            Info = new Info { Title = AppDomain.CurrentDomain.FriendlyName, Version = "1.0" },
            Paths = new Dictionary<string, Dictionary<string, Path>>()
        };

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly assembly in assemblies)
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types.OfType<Type>().ToArray();
            }

            // Iterate through each type in the assembly
            foreach (Type controller in types)
            {
                // HTTP REST Controllers
                // Check if the type inherits from ControllerBase and has the ApiController attribute
                if (!controller.IsSubclassOf(typeof(ControllerBase)) || !Attribute.IsDefined(controller, typeof(ApiControllerAttribute))) continue;

                // Retrieve the Route attribute if it exists
                var routeAttr = controller.GetCustomAttribute<RouteAttribute>();
                if (routeAttr == null) continue;

                IEnumerable<MethodInfo> methods = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (MethodInfo method in methods)
                {
                    if (method.GetCustomAttributes().FirstOrDefault(static attribute => attribute is HttpMethodAttribute) is not
                        HttpMethodAttribute httpMethod) continue;

                    string endpoint = OpenApiTypeHelper.GetEndpoint(controller, routeAttr.Template, method, httpMethod.Template);

                    Path path = OpenApiTypeHelper.GetPath(controller, routeAttr.Template, method, httpMethod.Template);

                    string m = httpMethod.HttpMethods.First().ToLower();
                    if (res.Paths.TryGetValue(endpoint, out Dictionary<string, Path>? value)) value.Add(m, path);
                    else res.Paths.Add(endpoint, new Dictionary<string, Path> { { m, path } });
                }

                // WebSocket Hubs
                // if (type.IsSubclassOf(typeof(Hub<>)))
                // {
                // builder.AppendLine($"Hub: {type.Name}");
                // }
            }
        }

        res.Components = OpenApiTypeHelper.Components;
        if (filePath != null) await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(res, new JsonSerializerOptions { WriteIndented = true }));
        await context.Response.WriteAsJsonAsync(res, context.RequestAborted);
    }
}