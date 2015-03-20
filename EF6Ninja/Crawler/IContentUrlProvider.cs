using System.Collections.Generic;

namespace EF6Ninja.Crawler
{
    public interface IContentUrlProvider
    {
        IList<string> CrawerUrlList();
    }
}
