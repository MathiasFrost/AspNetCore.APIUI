using OpenAPI.Generate;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(static options => options.AddDefaultPolicy(static policyBuilder =>
{
    policyBuilder.AllowAnyMethod();
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyOrigin();
}));
builder.Services.AddControllers();

WebApplication app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
app.UseOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();