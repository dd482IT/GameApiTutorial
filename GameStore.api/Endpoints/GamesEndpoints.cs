using System;
using GameStore.api.Dtos;
using GameStore.api.Entities;
using GameStore.api.Repositories;

namespace GameStore.api.Endpoints
{
	public static class GamesEndpoints
	{
        const string GetGamesEndpointName = "GetGame";
        
        public static RouteGroupBuilder MapGamesEndPoints(this IEndpointRouteBuilder routes)
		{
            InMemGamesRepository repository = new();

            var group = routes.MapGroup("/games").WithParameterValidation();
            // Instantiate instance


            // Get All games
            group.MapGet("/", (IGamesRepository repository)
                   => repository.GetAll().Select(game => game.AsDto()));


            // Endpoint by Id, if Id does not exist, returns null, error 200OK, which is wrong
            group.MapGet("/games/{Id}", (IGamesRepository repository, int Id) => {

                Game? game = repository.Get(Id);
                return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
                
            }).WithName(GetGamesEndpointName);

            // Create EndPoint
            // Originally we were requesting the game entity, now we request a GameDTO
            group.MapPost("/games", (IGamesRepository repository, CreateGameDto gameDto) =>
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageURI = gameDto.ImageURI

                };

                repository.Create(game);
                return Results.CreatedAtRoute(GetGamesEndpointName, new { Id = game.Id }, game);
            });

            //Update Endpoint
            // Update from entity to Dto Object
            group.MapPut("/games/{Id}", (IGamesRepository repository, int Id, UpdateGameDto updatedGame) =>
            {
                Game? existingGame = repository.Get(Id);

                // If a result is not found, we should always  create the resource if the resource is not found
                // However, when using SQL, SQL generates the Id possibly generating out of order Ids 
                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updatedGame.Name;
                existingGame.Genre = updatedGame.Genre;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.ImageURI = updatedGame.ImageURI;

                repository.Update(existingGame);

                return Results.NoContent();
            });

            //Delete Endpoint
            group.MapGet("/games/{Id}", (IGamesRepository repository, int Id) =>
            {
                Game? existingGame = repository.Get(Id);

                // If a result is not found, we should always  create the resource if the resource is not found
                // However, when using SQL, SQL generates the Id possibly generating out of order Ids 
                if (existingGame is not null)
                {
                    repository.Delete(Id);
                }

                return Results.NoContent();
            });

            // We are using RouteGroupBuilder, therefore you return group in case you would like to make continous calls. 
            return group;
         
        }
    }
}

