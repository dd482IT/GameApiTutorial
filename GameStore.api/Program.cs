using GameStore.api.Entities;

const string GetGamesEndpointName = "GetGame";

// List of temp database for testing
List<Game> games = new()
{
    new Game()
    {
       id = 1,
       Name = "Minecraft",
       Genre = "Creative",
       Price = 24.99M,
       ReleaseDate = new DateTime(1992,2,1),
       ImageURI = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fimagepng.org%2Fwp-content%2Fuploads%2F2017%2F08%2Fminecraft-icone-icon.png&f=1&nofb=1&ipt=6060e6c31609eac72143fd375e91cbffef99199705e4a97edc1d0308f0740b92&ipo=images"
    },
    new Game()
    {
       id = 2,
       Name = "Diablo 4",
       Genre = "Roleplay",
       Price = 59.99M,
       ReleaseDate = new DateTime(2092,2,1),
       ImageURI = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fimagepng.org%2Fwp-content%2Fuploads%2F2017%2F08%2Fminecraft-icone-icon.png&f=1&nofb=1&ipt=6060e6c31609eac72143fd375e91cbffef99199705e4a97edc1d0308f0740b92&ipo=images"
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Input Validation



var group = app.MapGroup("/");
group.MapGet("/games/{id}", (int id) =>{


}

);

// Endpoint
app.MapGet("/games", () => games);
// Endpoint by ID, if ID does not exist, returns null, error 200OK, which is wrong. 
group.MapGet("/games/{id}", (int id) => {

    Game? game = games.Find(game => game.id == id);
    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
}).WithName(GetGamesEndpointName);

//Create EndPoint
group.MapGet("/games", (Game game) =>
{
    game.id = games.Max(game => game.id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGamesEndpointName, new { id = game.id }, game);
});

//Update Endpoint
group.MapGet("/games/{id}", (int id, Game updatedGame) =>
{
    Game? existingGame = games.Find(game => game.id == id);

    // If a result is not found, we should always  create the resource if the resource is not found
    // However, when using SQL, SQL generates the ID possibly generating out of order ids 
    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;
    existingGame.ImageURI = updatedGame.ImageURI;

    return Results.NoContent(); 
});

//Delete Endpoint
group.MapGet("/games/{id}", (int id) =>
{
    Game? existingGame = games.Find(game => game.id == id);

    // If a result is not found, we should always  create the resource if the resource is not found
    // However, when using SQL, SQL generates the ID possibly generating out of order ids 
    if (existingGame is not null)
    {
        games.Remove(existingGame);
    }

    return Results.NoContent();
});

//Currently using group level (Route Group)
//Previously used end point level app.MapGet.  

app.Run();


