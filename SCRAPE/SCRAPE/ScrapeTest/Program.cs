using SCRAPE;
using System;
using System.Net;

namespace ScrapeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ScrapeTest! Enter URL: ");
            string url = Console.ReadLine();
            Page page = new Page(url);
            //Console.WriteLine(page.Print());
            PageNode first = page.GetRoot();
            PageNode[] found = first.FindChildren("href");
            //Console.WriteLine(found.Get());
            string[] links = new string[found.Length];
            for (int i = 0; i < found.Length; i++)
            {
                links[i] = found[i].GetLink();
            }
            links = PageNode.GoogleSearchResultsLinks(links);
            //for (int i = 0; i < links.Length; i++) links[i] = WebUtility.UrlDecode(links[i]);
        }
    }
}
