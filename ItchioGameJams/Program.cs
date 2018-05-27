using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ItchioGameJams
{
    

    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://itch.io/jam/creative-jam-11/entries";
            var context = "5b09f8e0fb6fc0292d6ef7bf";

            Console.WriteLine("Fetching games...");
            ItchioCrawler itchioCrawler = new ItchioCrawler();
            var games = itchioCrawler.GetGamesFromGameJam(url);
            if (games == null || games.Count == 0)
            {
                Console.WriteLine("No games. Finishing...");
                return;
            }

            Console.WriteLine("Downloading images...");
            GameImagesDownloader gameImagesDownloader = new GameImagesDownloader();
            gameImagesDownloader.DownloadImages(games);

            Console.WriteLine("Resizing images...");
            ImageResizer imageResizer = new ImageResizer();
            imageResizer.ResizeGamesImages(games);

            Console.WriteLine("Creating games...");
            PolyGamesAPI polyGamesAPI = new PolyGamesAPI(context);
            polyGamesAPI.CreateGames(games);

            Console.WriteLine("Done.");
            Console.ReadLine();

        }
    }
}
