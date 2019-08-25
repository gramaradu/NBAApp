using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NBA.Utils.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NBA.Utils.Images
{
    public static class ImageProvider
    {
        public class ar
        {
            public Dictionary<string, string> keyValuePairs { get; set; }
        }

        static JObject json = new JObject();
        const string imageSearchTemplate = @"https://www.google.com/search?q={0}&tbm=isch&site=imghp";
        const string randomImageApi = @"https://picsum.photos/200/300?random=1";
        public static string GetImage( string searchTerms)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerms))
                {
                    throw new BadImageFormatException("Something went wrong with the image search. Please try again later");
                }

                using (WebClient client = new WebClient())
                {
                    var result = client.DownloadString(String.Format(imageSearchTemplate, new object[] { searchTerms }));

                    if (!result.Contains("images_table"))
                    {
                        throw new BadImageFormatException("Something went wrong with the image search. Please try again later");
                    }

                    var doc = new HtmlAgilityPack.HtmlDocument();

                    doc.LoadHtml(result);

                    var imageList = from tables in doc.DocumentNode.Descendants("table")
                                    from img in tables.Descendants("img")
                                    where tables.Attributes["class"] != null
                                    && tables.Attributes["class"].Value == "images_table"
                                    && img.Attributes["src"] != null
                                    && img.Attributes["src"].Value.Contains("images?")
                                    select img;

                    string Url = imageList.First().Attributes["src"].Value;
                    return Url;
                }
            }
            catch (Exception ex)
            {
                throw new BadImageFormatException("Something went wrong with the image search. Please try again later");
            }
            //    var x = ImageProvider.WriteTODisk(searchTerms);
            //return string.Empty;
        }

        public static void WriteCache(List<Player> players)
        {
            JObject json = new JObject();
            StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cache.json");
            string j = sr.ReadToEnd();

            var items = JsonConvert.DeserializeObject(j);
            var cache = items.ToString();

            foreach (var player in players)
            {
                 var searchTerms = $"{player.First_Name} {player.Last_name} {player.Team.Full_name}";

                if (!items.ToString().Contains(searchTerms))
                {
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            var result = client.DownloadString(String.Format(imageSearchTemplate, new object[] { searchTerms }));

                            if (!result.Contains("images_table"))
                            {
                                throw new BadImageFormatException("Something went wrong with the image search. Please try again later");
                            }

                            var doc = new HtmlAgilityPack.HtmlDocument();

                            doc.LoadHtml(result);

                            var imageList = from tables in doc.DocumentNode.Descendants("table")
                                            from img in tables.Descendants("img")
                                            where tables.Attributes["class"] != null
                                            && tables.Attributes["class"].Value == "images_table"
                                            && img.Attributes["src"] != null
                                            && img.Attributes["src"].Value.Contains("images?")
                                            select img;

                            string Url = imageList.First().Attributes["src"].Value;
                            if (!json.ContainsKey(searchTerms))
                            {
                                json.Add(searchTerms, Url);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cache1.json", json.ToString());
                        }
                    }
                }
            }
            System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cache1.json", json.ToString());

        }
        public static JObject WriteTODisk(string searchTerms)
        {
            StreamReader sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cache.json");
            string j = sr.ReadToEnd();

            var items = JsonConvert.DeserializeObject(j);


            if (string.IsNullOrEmpty(searchTerms))
            {
                throw new BadImageFormatException("Something went wrong with the image search. Please try again later");
            }

            if (!items.ToString().Contains(searchTerms))
            {
                using (WebClient client = new WebClient())
                {
                    var result = client.DownloadString(String.Format(imageSearchTemplate, new object[] { searchTerms }));

                    if (!result.Contains("images_table"))
                    {
                        throw new BadImageFormatException("Something went wrong with the image search. Please try again later");
                    }

                    var doc = new HtmlAgilityPack.HtmlDocument();

                    doc.LoadHtml(result);

                    var imageList = from tables in doc.DocumentNode.Descendants("table")
                                    from img in tables.Descendants("img")
                                    where tables.Attributes["class"] != null
                                    && tables.Attributes["class"].Value == "images_table"
                                    && img.Attributes["src"] != null
                                    && img.Attributes["src"].Value.Contains("images?")
                                    select img;

                    string Url = imageList.First().Attributes["src"].Value;
                    if (!json.ContainsKey(searchTerms))
                    {
                        json.Add(searchTerms, Url);
                    }


                }
            }

            
            return json;
        }
    }
}
