using GameStore.api.Endpoints; // needed to use MapGamesEndPoints()
using GameStore.api.Repositories;
using GameStore.api.Entities;


var builder = WebApplication.CreateBuilder(args);
//Register Instance of Repository
//AddScoped adds a new instance of the repoistory each POST request
//builder.Services.AddScoped<IGamesRepository, InMemGamesRepository>();

// Uses the same instances of the Repoistory for each interaction
builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

var app = builder.Build();

app.MapGamesEndPoints();
app.Run();


