using backend;
using backend.Errors;
using backend.RouteActions;

const string frontendUrl = "http://localhost:3000";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.LoadDependencies();

builder.Services.AddProblemDetails();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15d);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins(frontendUrl)
            .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE", "PATCH")
            .WithHeaders("Content-Type", "Authorization")
        );
});

var app = builder.Build();
app.UseExceptionHandler(errorApp => errorApp.Run(ErrorConfigurator.ConfigureResponseErrors));
app.Services.SeedInitialData();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseSession();
app.UseCors("AllowFrontend");

var root = app.MapGroup("/api");

var desks = root.MapGroup("/desks");
desks.MapGet("/", DeskActions.GetAllAsync);
desks.MapGet("/{id:long}", DeskActions.GetByIdAsync);

app.Run();