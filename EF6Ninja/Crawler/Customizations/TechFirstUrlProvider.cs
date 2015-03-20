using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6Ninja.Crawler.Extractors
{
    public class TechFirstUrlProvider:IContentUrlProvider
    {
        IList<string> urlList = new List<string>();

        public IList<string> CrawerUrlList()
        {
            for (int i = 1; i < 200; i++)
            {
                urlList.Add(string.Format("http://tech.firstpost.com/product/laptops/list-30-page-{0}.html", i));
            }

            return urlList;
        }
    }
}
