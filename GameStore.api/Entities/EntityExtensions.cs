using GameStore.api.Dtos;
using System;

namespace GameStore.api.Entities;

public static class EntityExtensions
{
	//extending game entity (extension method, game entity to dto)
	public static GameDto AsDto(this Game game)
	{
		// Can use autoMapper (mapping framework) here
            return new GameDto(game.Id, game.Name, game.Genre, game.Price, game.ReleaseDate, game.ImageURI);
	}
}

