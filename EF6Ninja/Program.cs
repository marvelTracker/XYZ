using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Abot.Crawler;
using Abot.Poco;
using EF6Ninja.Crawler;
using EF6Ninja.Crawler.Extractors;
using EntityFrameworkNinja;
using EntityFrameworkNinja.Model;
using HibernatingRhinos.Profiler.Appender.EntityFramework;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using EF6Ninja.Repository;
using EF6Ninja.Model;


namespace EF6Ninja
{
    class Program
    {
        public  static  List<string> urlList = new List<string>();


        static void Main(string[] args)
        {
            Console.WriteLine( "Ninja Started");

           // EntityFrameworkProfiler.Initialize();

           // AddNinjas();

            // LargeSearch();

           // ParseHtml();

            //WebCrawer(args);

           // WebCrawer();

            ImageDownloader();

            Console.WriteLine("Ninja Finished");
            Console.ReadLine();
        }

        private static void AddNinjas()
        {
            NinjaContext ninjaContext = new NinjaContext();

            for (int i = 0; i < 40000; i++)
            {
                Ninja ninja = new Ninja
                {
                    Description = "Ninja Description" + i,
                    Level = i.ToString(),
                    NinjaId = i
                };

                ninjaContext.Ninjas.Add(ninja);
            }

            ninjaContext.SaveChanges();
        }

        private static void LargeSearch()
        {
            var list = new List<int>();

            for (int i = 0; i < 40000; i++)
            {
                list.Add(i);
            }

            NinjaContext ninjaContext = new NinjaContext();

            var results = ninjaContext.Ninjas.Where(n => list.Contains(n.NinjaId)).ToList();

            foreach (Ninja result in results)
            {
                Console.WriteLine("Results Descriptions :" + result.Description);
            }

        }
       
        private static void WebCrawer(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            PrintDisclaimer();

            Uri uriToCrawl = null;


            for (int i = 1; i < 200; i++)
            {
                uriToCrawl = new Uri(string.Format("http://tech.firstpost.com/product/laptops/list-30-page-{0}.html", i));

                IWebCrawler crawler;

                //Uncomment only one of the following to see that instance in action
                crawler = GetDefaultWebCrawler();
                //crawler = GetManuallyConfiguredWebCrawler();
                //crawler = GetCustomBehaviorUsingLambdaWebCrawler();

                //Subscribe to any of these asynchronous events, there are also sychronous versions of each.
                //This is where you process data about specific events of the crawl
                crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
                crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
                crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
                crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

                //Start the crawl
                //This is a synchronous call
                CrawlResult result = crawler.Crawl(uriToCrawl);

                //Now go view the log.txt file that is in the same directory as this executable. It has
                //all the statements that you were trying to read in the console window :).
                //Not enough data being logged? Change the app.config file's log4net log level from "INFO" TO "DEBUG"

            }

            PrintDisclaimer();

            Console.ReadLine();
        }

        private static void WebCrawer()
        {
            log4net.Config.XmlConfigurator.Configure();
            PrintDisclaimer();

            Uri uriToCrawl = null;

            IContentUrlProvider urlProvider = new TechFirstUrlProvider();

            foreach (string url in urlProvider.CrawerUrlList())
            {
                uriToCrawl = new Uri(url);

                IWebCrawler crawler;

                //Uncomment only one of the following to see that instance in action
                crawler = GetDefaultWebCrawler();
                //crawler = GetManuallyConfiguredWebCrawler();
                //crawler = GetCustomBehaviorUsingLambdaWebCrawler();

                //Subscribe to any of these asynchronous events, there are also sychronous versions of each.
                //This is where you process data about specific events of the crawl
                crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
                crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
                crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
                crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

                //Start the crawl
                //This is a synchronous call
                CrawlResult result = crawler.Crawl(uriToCrawl);

                //Now go view the log.txt file that is in the same directory as this executable. It has
                //all the statements that you were trying to read in the console window :).
                //Not enough data being logged? Change the app.config file's log4net log level from "INFO" TO "DEBUG"

            }

            PrintDisclaimer();

            Console.ReadLine();
        }

        private static IWebCrawler GetDefaultWebCrawler()
        {
            return new PoliteWebCrawler(null, new TechFirstDecisionMaker(), null, null, null, null, null, null, null);
        }

        private static IWebCrawler GetManuallyConfiguredWebCrawler()
        {
            //Create a config object manually
            CrawlConfiguration config = new CrawlConfiguration();
            config.CrawlTimeoutSeconds = 0;
            config.DownloadableContentTypes = "text/html, text/plain";
            config.IsExternalPageCrawlingEnabled = false;
            config.IsExternalPageLinksCrawlingEnabled = false;
            config.IsRespectRobotsDotTextEnabled = false;
            config.IsUriRecrawlingEnabled = false;
            config.MaxConcurrentThreads = 10;
            config.MaxPagesToCrawl = 10;
            config.MaxPagesToCrawlPerDomain = 0;
            config.MinCrawlDelayPerDomainMilliSeconds = 1000;

            //Add you own values without modifying Abot's source code.
            //These are accessible in CrawlContext.CrawlConfuration.ConfigurationException object throughout the crawl
            config.ConfigurationExtensions.Add("Somekey1", "SomeValue1");
            config.ConfigurationExtensions.Add("Somekey2", "SomeValue2");

            //Initialize the crawler with custom configuration created above.
            //This override the app.config file values
            return new PoliteWebCrawler(config, null, null, null, null, null, null, null, null);
        }

        private static IWebCrawler GetCustomBehaviorUsingLambdaWebCrawler()
        {
            IWebCrawler crawler = GetDefaultWebCrawler();

            //Register a lambda expression that will make Abot not crawl any url that has the word "ghost" in it.
            //For example http://a.com/ghost, would not get crawled if the link were found during the crawl.
            //If you set the log4net log level to "DEBUG" you will see a log message when any page is not allowed to be crawled.
            //NOTE: This is lambda is run after the regular ICrawlDecsionMaker.ShouldCrawlPage method is run.
            crawler.ShouldCrawlPage((pageToCrawl, crawlContext) =>
            {
                if (pageToCrawl.Uri.AbsoluteUri.Contains("ghost"))
                    return new CrawlDecision { Allow = false, Reason = "Scared of ghosts" };

                return new CrawlDecision { Allow = true };
            });

            //Register a lambda expression that will tell Abot to not download the page content for any page after 5th.
            //Abot will still make the http request but will not read the raw content from the stream
            //NOTE: This lambda is run after the regular ICrawlDecsionMaker.ShouldDownloadPageContent method is run
            crawler.ShouldDownloadPageContent((crawledPage, crawlContext) =>
            {
                if (crawlContext.CrawledCount >= 5)
                    return new CrawlDecision { Allow = false, Reason = "We already downloaded the raw page content for 5 pages" };

                return new CrawlDecision { Allow = true };
            });

            //Register a lambda expression that will tell Abot to not crawl links on any page that is not internal to the root uri.
            //NOTE: This lambda is run after the regular ICrawlDecsionMaker.ShouldCrawlPageLinks method is run
            crawler.ShouldCrawlPageLinks((crawledPage, crawlContext) =>
            {
                if (!crawledPage.IsInternal)
                    return new CrawlDecision { Allow = false, Reason = "We dont crawl links of external pages" };

                return new CrawlDecision { Allow = true };
            });

            return crawler;
        }

        private static Uri GetSiteToCrawl(string[] args)
        {
            string userInput = "";
            if (args.Length < 1)
            {
                System.Console.WriteLine("Please enter ABSOLUTE url to crawl:");
                userInput = System.Console.ReadLine();
            }
            else
            {
                userInput = args[0];
            }

            if (string.IsNullOrWhiteSpace(userInput))
                throw new ApplicationException("Site url to crawl is as a required parameter");

            return new Uri(userInput);
        }

        private static void PrintDisclaimer()
        {
            PrintAttentionText("The demo is configured to only crawl a total of 10 pages and will wait 1 second in between http requests. This is to avoid getting you blocked by your isp or the sites you are trying to crawl. You can change these values in the app.config or Abot.Console.exe.config file.");
        }

        private static void PrintAttentionText(string text)
        {
            ConsoleColor originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = originalColor;
        }

        private static void PrintWarnText(string text)
        {
            ConsoleColor originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = originalColor;
        }

        static void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            //Process data
        }

        static void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            //Process data
            CrawledPage crawledPage = e.CrawledPage;

            if (urlList.Contains(crawledPage.ParentUri + crawledPage.Uri.AbsoluteUri))
            {
                PrintWarnText("Duplicate Page :" + crawledPage.ParentUri + crawledPage.Uri.AbsoluteUri);
                return;
            }

            urlList.Add(crawledPage.ParentUri + crawledPage.Uri.AbsoluteUri);

            PrintAttentionText(crawledPage.Uri.AbsolutePath);

            //ParseHtml(crawledPage.Content.Text, crawledPage.ParentUri + crawledPage.Uri.AbsoluteUri);

            IContentExtractor extractor = new TechFirstContentExtractor();
            var extractedContent = extractor.ExtractContent(crawledPage.Content.Text, crawledPage.ParentUri + crawledPage.Uri.AbsoluteUri);
            
            IContentExtractorRepository repository = new LaptopMetadataRepository();
            repository.Save(extractedContent);
        }

        static void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            //Process data
        }

        static void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            //Process data
        }

        private static void ImageDownloader()
        {
            try
            {
                using (NinjaContext context = new NinjaContext())
                {
                    var LaptopList = context.LaptopMetaDataList;

                    foreach (var laptopMetaData in LaptopList)
                    {
                        string id = Guid.NewGuid().ToString();
                        string localFilename = string.Format(@"c:\Gallery\{0}.jpg", id );

                        //if(!IsImageExist(laptopMetaData.Image))continue;

                        using (WebClient client = new WebClient())
                        {
                            PrintAttentionText("Downloading...." + laptopMetaData.Image);

                            try
                            {
                                client.DownloadFile(laptopMetaData.Image, localFilename);
                            }
                            catch (Exception ex)
                            {
                                PrintWarnText(ex.Message + ex.StackTrace);
                            }
                        }

                        laptopMetaData.Image = localFilename;
                        
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
               PrintWarnText(ex.Message + ex.StackTrace);
            }
           
        }

        private static bool IsImageExist(string url)
        {

            bool isValid = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1200; // miliseconds
            request.Method = "HEAD";

            PrintAttentionText("Checking file exisits in started");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            PrintAttentionText("Checking file exisits in End");

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
