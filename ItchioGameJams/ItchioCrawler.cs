using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItchioGameJams
{
    class ItchioCrawler
    {
        public List<Game> GetGamesFromGameJam(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var gamesGrid = doc.DocumentNode.SelectNodes("//div[@class='game_grid_widget browse_game_grid']")?.FirstOrDefault();
            if (gamesGrid == null)
            {
                Console.WriteLine("HTML FORMAT ERROR: Could not find '//div[@class='game_grid_widget browse_game_grid']'");
                return null;
            }

            List<Game> games = new List<Game>();

            foreach (var gameNode in gamesGrid.ChildNodes)
            {

                var titleNode = gameNode.SelectSingleNode(".//a[@class='title game_link']");
                string gameTitle = titleNode.InnerText;
                Console.WriteLine($"Title: {gameTitle}");
                

                var imgNode = gameNode.SelectSingleNode(".//div[@class='game_thumb']");
                string gameImgUrl = imgNode.Attributes["data-background_image"]?.Value;
                Console.WriteLine($"Image URL: {gameImgUrl}");

                string gamePageUrl = imgNode.ParentNode.Attributes["href"].Value;
                Console.WriteLine($"Page URL: {gamePageUrl}");

                if (String.IsNullOrEmpty(gameTitle) || String.IsNullOrEmpty(gameImgUrl) || String.IsNullOrEmpty(gamePageUrl))
                {
                    Console.WriteLine($"INVALID GAME: {gameTitle}");
                    continue;
                }

                games.Add(new Game()
                {
                    Title = gameTitle,
                    ImageURL = gameImgUrl,
                    PageURL = "https://itch.io" + gamePageUrl
                });

                Console.WriteLine();

            }

            Console.WriteLine("Done.");

            return games;
        }
    }
}
