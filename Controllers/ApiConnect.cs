using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using fantasy.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace fantasy.Controllers
{
    public class ApiConnect
    {
        private static string url = "https://fantasy.premierleague.com/api/bootstrap-static/";

        public async Task<MyTeam> Connect(List<Fixture> fixtures){
            HttpRequestMessage request = new HttpRequestMessage(){
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            var response = await Program.client.SendAsync(request);
            var output = response.Content.ReadAsStringAsync().Result;
            object dec = JsonConvert.DeserializeObject(output);
            JObject obj = JObject.Parse(dec.ToString());
            
            var events = obj["events"];
            var game_settings = obj["game_settings"];
            var phases = obj["phases"];
            var teams = JsonConvert.DeserializeObject<List<Team>>(JsonConvert.SerializeObject(obj["teams"]));
            var total_players = obj["total_players"];
            var players = JsonConvert.DeserializeObject<List<Player>>(JsonConvert.SerializeObject(obj["elements"]));
            var element_stats = obj["element_stats"];
            var element_types = obj["element_types"];

            var gameWeek = 1;
            var start = DateTime.Parse(events[gameWeek-1]["deadline_time"].ToString()).AddDays(-1);
            var end = DateTime.Parse(events[gameWeek]["deadline_time"].ToString()).AddDays(-1);
            Console.WriteLine(end);
            List<Fixture> gameweekFixtures = fixtures.Where(Fixture => Fixture.Date > start && Fixture.Date < end).ToList();
            Console.WriteLine(JsonConvert.SerializeObject(gameweekFixtures));


            // Console.WriteLine(element_types);

            foreach (var player in players)
            {
                foreach (var type in element_types)
                {
                    if (type["id"].ToString() == player.element_type.ToString())
                    {
                        player.position = type["singular_name_short"].ToString();
                        continue;
                    }
                }
                foreach (var team in teams)
                {
                    if (team.id == player.team)
                    {
                        player.teamName = team.name;
                    }
                }
            }
            
            MyTeam myTeam = new MyTeam();
            myTeam.players = new List<Player>();
            myTeam.starters = new List<Player>();
            myTeam.Generate(players, gameweekFixtures, teams);
            await myTeam.Existing(players);
            myTeam.BestStarters();
            return myTeam;
        }

    }
}