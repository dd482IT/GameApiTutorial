using GameStore.api.Endpoints; // needed to use MapGamesEndPoints()
using GameStore.api.Entities;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndPoints();
app.Run();


