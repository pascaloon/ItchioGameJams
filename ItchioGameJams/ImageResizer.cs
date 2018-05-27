using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItchioGameJams
{
    class ImageResizer
    {
        static private string ResizerHelperPath = "cs_imgResizer.exe";
        static private string ResizerHelperArgs (string raw, string resized) 
            => $"{raw} {resized} 400 200";

        public void ResizeGamesImages(List<Game> games)
        {
            if (!Directory.Exists(Game.ResizedImageFolder))
                Directory.CreateDirectory(Game.ResizedImageFolder);

            foreach (var game in games)
            {
                Process.Start(
                    new ProcessStartInfo(ResizerHelperPath, ResizerHelperArgs(game.RawImagePath, game.ResizedImagePath))
                    );
            }
        }
    }
}
