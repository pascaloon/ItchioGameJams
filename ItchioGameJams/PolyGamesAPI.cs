using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ItchioGameJams
{
    class PolyGamesAPI
    {
        static private string PolyGamesURL = "http://polygames.polymtl.ca";
        //static private string PolyGamesURL = "http://localhost:3001";
        static private string CreateGameURL = $"{PolyGamesURL}/api/projects/create";

        private String Context = "";

        public PolyGamesAPI(string context)
        {
            Context = context;
        }

        public void CreateGames(List<Game> games)
        {
            games.ForEach(CreateGame);
            //CreateGame(games[0]);
        }

        public void CreateGame(Game game)
        {

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(CreateGameURL);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = $"{{\"gameData\": {{\"title\": \"{game.Title}\", \"thumbnail\": \"{game.UID}.png\", \"context\": \"{Context}\", \"externalPage\": \"{game.PageURL}\"}}, \"thumbnailData\": \"{ImageToBase64(game.ResizedImagePath)}\" }}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
                Console.WriteLine("Sending: " + json);
            }

            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            sr.ReadToEnd();
            Thread.Sleep(1000);
        }

        private string ImageToBase64(string filepath)
        {
            using (Image image = Image.FromFile(filepath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

    }
}
