using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace fantasy.Classes
{
    public class GetFixtures
    {
        public List<Fixture> fixtures = new List<Fixture>();
        public async Task AllFixtures()
        {
            int day = 12;
            int month = 9;
            int year = 2020;
            bool continueRunning = true;
            while (continueRunning)
            {
                try
                {
                    var scrape = await this.Scrape(year.ToString(), month.ToString(), day.ToString());
                }
                catch (System.Exception)
                {
                    
                }
                if(day < 31)
                {
                    day++;
                } else {
                    day = 1;
                    if(month < 12)
                    {
                        month++;
                    } else {
                        month = 1;
                        year++;
                    }
                    Console.WriteLine(day + "/" + month + "/" + year + " - " + this.fixtures.Count + " fixtures logged");
                }
                if(day > 31 && month > 8 && year > 2021 || this.fixtures.Count == 380){
                    continueRunning = false;
                }
            }
        }
        public async Task<string> Scrape(string year, string month, string day)
        {
            if(month.Length == 1){
                month = "0" + month;
            }
            if(day.Length == 1){
                day = "0" + day;
            }
            string url = "https://www.goal.com/en/premier-league/fixtures-results/"+year+"-"+month+"-"+day+"/2kwbbcootiqqgmrzs6o5inle5";

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);
            var homeTeams = pageDocument.DocumentNode.SelectNodes("(//div[contains(@class,'match-row')]//td[contains(@class, 'team-home')]//span)");
            var awayTeams = pageDocument.DocumentNode.SelectNodes("(//div[contains(@class,'match-row')]//td[contains(@class, 'team-away')]//span)");
            int i = 0;
            while (i < homeTeams.Count)
            {
                DateTime date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                Fixture fixture = new Fixture(date, homeTeams[i].InnerText, awayTeams[i].InnerText);
                this.fixtures.Add(fixture);
                i++;
            }
            return "OK";
        }

    }
}