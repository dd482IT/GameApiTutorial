using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.api.Entities;

namespace GameStore.api.Repositories
{
    public class InMemGamesRepository : IGamesRepository
    {
        private const int Year = 2092;


        // List of temp database for testing
        private readonly List<Game> games = new()
        {
            new Game()
            {
               Id = 1,
               Name = "Minecraft",
               Genre = "Creative",
               Price = 24.99M,
               ReleaseDate = new DateTime(1992,2,1),
               ImageURI = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fimagepng.org%2Fwp-content%2Fuploads%2F2017%2F08%2Fminecraft-icone-icon.png&f=1&nofb=1&ipt=6060e6c31609eac72143fd375e91cbffef99199705e4a97edc1d0308f0740b92&ipo=images"
            },
            new Game()
            {
               Id = 2,
               Name = "Diablo 4",
               Genre = "Roleplay",
               Price = 59.99M,
               ReleaseDate = new DateTime(Year,2,1),
               ImageURI = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fimagepng.org%2Fwp-content%2Fuploads%2F2017%2F08%2Fminecraft-icone-icon.png&f=1&nofb=1&ipt=6060e6c31609eac72143fd375e91cbffef99199705e4a97edc1d0308f0740b92&ipo=images"
            }
        };

        public IEnumerable<Game> GetAll()
        {
            return games;
        }

        // If game not found, return a null value
        public Game? Get(int Id)
        {
            return games.Find(game => game.Id == Id);
        }

        public void Create(Game game)
        {
            game.Id = games.Max(game => game.Id) + 1;
            games.Add(game);
        }

        public void Update(Game UpdatedGame)
        {
            var index = games.FindIndex(game => game.Id == UpdatedGame.Id);
            games[index] = UpdatedGame;
        }

        public void Delete(int Id)
        {
            var index = games.FindIndex(game => game.Id == Id);
            games.RemoveAt(index);
        }
    }
}

