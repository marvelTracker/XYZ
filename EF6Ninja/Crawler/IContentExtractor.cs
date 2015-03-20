using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6Ninja.Crawler
{
   public interface IContentExtractor
   {
       IList<KeyValuePair<string, string>> ExtractContent(string content, string pageUrl);
   }
}
