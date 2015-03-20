using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abot.Core;
using Abot.Poco;

namespace EF6Ninja.Crawler
{
    public class TechFirstDecisionMaker:ICrawlDecisionMaker
    {
        public CrawlDecision ShouldCrawlPage(PageToCrawl pageToCrawl, CrawlContext crawlContext)
        {
            if(pageToCrawl.Uri.AbsoluteUri.Contains("laptops"))
                return new CrawlDecision { Allow = true , Reason = "Allow other contents" };

            return new CrawlDecision { Allow = false, Reason = "Allow other contents" };
        }

        public CrawlDecision ShouldCrawlPageLinks(CrawledPage crawledPage, CrawlContext crawlContext)
        {
            return new CrawlDecision { Allow = true, Reason = "Only download raw page content for .com tlds" };
        }

        public CrawlDecision ShouldDownloadPageContent(CrawledPage crawledPage, CrawlContext crawlContext)
        {
            return new CrawlDecision { Allow = true, Reason = "Only download raw page content for .com tlds" };
        }

        public CrawlDecision ShouldRecrawlPage(CrawledPage crawledPage, CrawlContext crawlContext)
        {
            return new CrawlDecision { Allow = false, Reason = "One time craw only" };
        }
    }
}
