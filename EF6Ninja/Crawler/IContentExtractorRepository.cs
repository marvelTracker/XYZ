using System.Collections.Generic;

namespace EF6Ninja.Crawler
{
    public interface IContentExtractorRepository
    {
        void Save(IList<KeyValuePair<string, string>> content);
    }
}
