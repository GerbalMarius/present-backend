using backend;
using backend.Errors;
using backend.Filters;
using backend.Models;
using backend.Models.DTO;
using backend.RouteActions;

const string frontendUrl = "http://localhost:3000";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.LoadDependencies();

builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins(frontendUrl)
            .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE", "PATCH")
            .WithHeaders("Content-Type", "Authorization")
            .AllowCredentials()
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

var root = app.MapGroup("/api")
                               .AddEndpointFilter<ValidationFilter>();

var reservations = root.MapGroup("/reservations");

reservations.MapPost("/", ReservationActions.CreateAsync)
        .WithName("CreateReservation")
        .Produces<ReservationData>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
        .ProducesProblem(StatusCodes.Status404NotFound);

var desks = root.MapGroup("/desks");
desks.MapGet("/", DeskActions.GetAllAsync)
        .WithName("GetAllDesks")
        .Produces<List<DeskData>>();

desks.MapGet("/{id:long}", DeskActions.GetByIdAsync)
        .WithName("GetDeskById")
        .Produces<DeskData>()
        .ProducesProblem(StatusCodes.Status404NotFound);

var users = root.MapGroup("/users");

users.MapGet("/{id:long}/reservations", UserActions.GetReservationDataByUserAsync);

users.MapGet("/me", UserActions.GetCurrentUserAsync)
    .WithName("GetCurrentUser")
    .Produces<UserData>();

app.Run();