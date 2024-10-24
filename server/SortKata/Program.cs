using Microsoft.OpenApi.Models;
using SortKata.Domain.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Add CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowSpecificOrigins",
        policy => {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

// Add services to the container.
builder.Services.AddScoped(typeof(ISortService<>), typeof(SortService<>));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo() {
        Title = "Sort Kata API",
        Version = "v1",
        Description = "Simple service to sorting items"
    });
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
