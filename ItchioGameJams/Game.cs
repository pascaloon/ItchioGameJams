using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ItchioGameJams
{
    class Game
    {
        static public string RawImageFolder = "ImagesRaw";
        static public string ResizedImageFolder = "ImagesResized";

        public String Title { get; set; }
        public String ImageURL { get; set; }
        public String UID { get; set; }
        public String RawImagePath => $"{RawImageFolder}/{UID}.png";
        public String ResizedImagePath => $"{ResizedImageFolder}/{UID}.png";
        public String PageURL { get; set; }
    }


    class GameImagesDownloader
    {
        public void DownloadImages(List<Game> games)
        {
            WebClient wc = new WebClient();
            if (!Directory.Exists(Game.RawImageFolder))
                Directory.CreateDirectory(Game.RawImageFolder);

            foreach (var game in games)
            {
                game.UID = Guid.NewGuid().ToString();
                wc.DownloadFile(game.ImageURL, game.RawImagePath);
            }

        }
    }
}
