using System;
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

            group.MapGet("/", () => repository.GetAll());


            // Endpoint by ID, if ID does not exist, returns null, error 200OK, which is wrong. 
            group.MapGet("/games/{id}", (int id) => {

                Game? game = repository.Get(id);
                return game is not null ? Results.Ok(game) : Results.NotFound();
                
            }).WithName(GetGamesEndpointName);

            //Create EndPoint
            group.MapPost("/games", (Game game) =>
            {
                repository.Create(game);
                return Results.CreatedAtRoute(GetGamesEndpointName, new { id = game.id }, game);
            });

            //Update Endpoint
            group.MapPut("/games/{id}", (int id, Game updatedGame) =>
            {
                Game? existingGame = repository.Get(id);

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

                repository.Update(updatedGame);

                return Results.NoContent();
            });

            //Delete Endpoint
            group.MapGet("/games/{id}", (int id) =>
            {
                Game? existingGame = repository.Get(id);

                // If a result is not found, we should always  create the resource if the resource is not found
                // However, when using SQL, SQL generates the ID possibly generating out of order ids 
                if (existingGame is not null)
                {
                    repository.Delete(id);
                }

                return Results.NoContent();
            });

            // We are using RouteGroupBuilder, therefore you return group in case you would like to make continous calls. 
            return group;
         
        }

    }
}

