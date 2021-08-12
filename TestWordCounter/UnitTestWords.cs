using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WordCounter;
using Xunit;

namespace TestWordCounter
{
    public class UnitTestWords
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public UnitTestWords()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Theory]
        [InlineData("")]
        [InlineData("Hello world Hello, all hello europe world")]
        [InlineData("All")]
        public async Task WordsCount(string text)
        {
            try
            {
                HttpContent cont = new StringContent(string.Format("txtar={0}", text), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                // Act
                var response = await _client.PostAsync("/Home/WordsList", cont);
                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(responseString);
                List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='table']")
            .Descendants("tr")
            .Where(tr => tr.Elements("td").Count() > 1)
            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
            .ToList();

                if (text == "All" || text == "")
                {
                    Assert.True(table.Count == 0);
                }
                else
                {
                    Assert.True(table.Count == 3);
                    Assert.Equal("1|Hello|3|***", string.Join('|', table[0]));
                    Assert.Equal("2|world|2|**", string.Join('|', table[1]));
                    Assert.Equal("3|europe|1|*", string.Join('|', table[2]));
                }
            }
            catch(Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }
    }
}
