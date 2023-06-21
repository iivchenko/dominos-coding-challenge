using CodingChallenge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddWebApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseApiKeyAuthentication()
    .UseAppExceptionHandling()
    .UseOutputCache()
    .UseAuthorization();

app.MapControllers();

// iivc comment:
// This is not very good practice and better to have EF Migrations in place 
// but I opt to this piece of code make things easy to run from stractch 
// without manual DB creation and to save some development time.
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    context.SaveChanges();
}

app.Run();
