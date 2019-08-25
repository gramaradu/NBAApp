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
        const string imageSearchTemplate = @"https://www.google.com/search?q={0}&tbm=isch&site=imghp";
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
            catch (Exception)
            {
                throw new BadImageFormatException("Something went wrong with the image search. Please try again later");
            }
        }
    }
}
