using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace EF6Ninja.Crawler.Extractors
{
    public class TechFirstContentExtractor:IContentExtractor
    {

        public IList<KeyValuePair<string, string>> ExtractContent(string content, string pageUrl)
        {

            HtmlDocument doc = new HtmlDocument();

            IList<KeyValuePair<string, string>> extractedData = new List<KeyValuePair<string, string>>();

            doc.LoadHtml(content);

            HtmlNode testModel = doc.DocumentNode.QuerySelector(".prodtxtinf");

            if (testModel == null) return extractedData;

            extractedData.Add(new KeyValuePair<string, string>("Model", testModel.InnerText));

            extractedData.Add(new KeyValuePair<string, string>("ContentUrl", pageUrl));

            //Get Image Path
            HtmlNode imagePath = doc.DocumentNode.QuerySelector(".TAC");

            if (imagePath == null) return extractedData;

            foreach (var imageNode in imagePath.SelectNodes(".//img"))
            {
                string value = imageNode.GetAttributeValue("src", " ");

                extractedData.Add(new KeyValuePair<string, string>("Image", value));
                break;
            }

            IList<HtmlNode> sections = doc.DocumentNode.QuerySelectorAll(".comtitv");

            foreach (HtmlNode section in sections)
            {
                HtmlNode node = section.SelectNodes("following-sibling::div[1]").FirstOrDefault();

                foreach (HtmlNode tr in node.SelectNodes("./table//tr"))
                {
                    HtmlNode td = tr.SelectSingleNode("./td");
                    HtmlNode th = tr.SelectSingleNode("./th");

                    extractedData.Add(new KeyValuePair<string, string>(PropertyNameCreator(th.InnerText, section.InnerText), td.InnerText));
                }

            }

            return extractedData;
        }

        private static string PropertyNameCreator(string propertyText, string categoryText)
        {
            string result = string.Empty;

            propertyText = Regex.Replace(propertyText, "[^0-9a-zA-Z]+", "");
            categoryText = Regex.Replace(categoryText, "[^0-9a-zA-Z]+", "");

            return string.Format("{0}{1}{2}", categoryText, "_", propertyText);
        }
    }
}
