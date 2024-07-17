using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;

namespace MandatApp
{
    public static class RetrieveWebPage
    {
        public static async Task<bool> GetRetrievedPage(int pageNumber)
        {
            // URL of the web page you want to retrieve
            string url = $"https://mandat.uzbmb.uz/?pageNumber={pageNumber}&pageSize=10&s4subject=Matematika&s5subject=Chet%20tili&edLangId=1&lang=uz";

            // Call the method to get the web page content
            string content = await GetWebPageContentAsync(url);
            string surname = "OBLO";
            string name = "R. R.";

            if (content.Contains(surname))
            {
                Console.WriteLine(content);
                if(content.Contains(name))
                {
                    Console.WriteLine("--------------------------------------------------TOPDIK--------------------------------------------------------------------------");
                    Console.WriteLine(content);
                    return true;
                }
            }

            // Display the content
            //Console.WriteLine(content);
            return false;
        }

        static async Task<string> GetWebPageContentAsync(string url)
        {
            // Create an HttpClient instance
            using HttpClient client = new HttpClient();

            try
            {
                /// Send a GET request to the specified URL
                HttpResponseMessage response = await client.GetAsync(url);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string pageContent = await response.Content.ReadAsStringAsync();

                // Parse and extract the desired part of the page
                string extractedContent = ExtractContent(pageContent);
                return extractedContent;
            }
            catch (HttpRequestException e)
            {
                // Handle the exception (e.g., display an error message)
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

        static string ExtractContent(string html)
        {
            // Load the HTML document
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            // Use XPath or other selectors to find the desired part of the page
            // Example: Extract content inside a div with a specific class
            HtmlNode node = document.DocumentNode.SelectSingleNode("//div[@class='card-body table-responsive']");

            // Return the inner text of the node
            return node != null ? node.InnerText : "Desired content not found.";
        }

    }
}
