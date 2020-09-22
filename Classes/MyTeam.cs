using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace fantasy.Classes
{
    public class MyTeam
    {
        public int value { get; set; }
        public List<Player> players { get; set; }
        public List<Player> starters { get; set; }
        public int GKP { get; set; }
        public int DEF { get; set; }
        public int MID { get; set; }
        public int FWD { get; set; }
        public string captain { get; set; }
        public string viceCaptain { get; set; }

        public async Task<MyTeam> Existing(List<Player> players)
        {
            var cookie = Program.cookie;
            var url = "https://fantasy.premierleague.com/api/my-team/2683770/";
            HttpRequestMessage request = new HttpRequestMessage(){
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Cookie", cookie);
            var response = await Program.client.SendAsync(request);
            var output = response.Content.ReadAsStringAsync().Result;
            object dec = JsonConvert.DeserializeObject(output);
            JObject obj = JObject.Parse(dec.ToString());
            Transfer transfer = JsonConvert.DeserializeObject<Transfer>(obj["transfers"].ToString());
            Console.WriteLine(JsonConvert.SerializeObject(transfer));
            /*
            Sort by potential
            While transfer.made < transfer.limit -> find best replacement for lowest player in same position within budget
            Budget = transfer.bank + player.value
            */
            return null;
        }
        
        public void Generate(List<Player> players)
        {
            players.Sort(delegate(Player x, Player y)
            {
                return y.value_season.CompareTo(x.value_season);
            });
            // 2 GKP
            // 5 DEF
            // 5 MID
            // 3 FWD
            this.GKP = 0;
            this.DEF = 0;
            this.MID = 0;
            this.FWD = 0;
            foreach (var player in players)
            {
                if (this.players.Count < 15){
                    if (player.position == "GKP" && this.GKP < 2 && (this.value + player.now_cost) <= 1000){
                        this.GKP++;
                        this.value += player.now_cost;
                        this.players.Add(player);
                    } else if (player.position == "DEF" && this.DEF < 5 && (this.value + player.now_cost) <= 1000)
                    {
                        this.DEF++;
                        this.value += player.now_cost;
                        this.players.Add(player);
                    } else if (player.position == "MID" && this.MID < 5 && (this.value + player.now_cost) <= 1000)
                    {
                        this.MID++;
                        this.value += player.now_cost;
                        this.players.Add(player);
                    } else if (player.position == "FWD" && this.FWD < 3 && (this.value + player.now_cost) <= 1000)
                    {
                        this.FWD++;
                        this.value += player.now_cost;
                        this.players.Add(player);
                    }
                } else {
                    if (player.position == "GKP" && this.GKP < 2 && (this.value + player.now_cost) == 1000){
                        this.GKP++;
                        this.value += player.now_cost;
                        this.players.Add(player);
                    } else if (player.position == "DEF" && this.DEF < 5 && (this.value + player.now_cost) == 1000)
                    {
                        this.DEF++;
                        this.value += player.now_cost;
                        this.players.Add(player);
                    } else if (player.position == "MID" && this.MID < 5 && (this.value + player.now_cost) == 1000)
                    {
                        this.MID++;
                        this.value += player.now_cost;
                        this.players.Add(player);
                    } else if (player.position == "FWD" && this.FWD < 3 && (this.value + player.now_cost) == 1000)
                    {
                        this.FWD++;
                        this.value += player.now_cost;
                        this.players.Add(player);
                    }
                }
                if (this.players.Count == 15){
                    break;
                }
            }
        }

        public string BestStarters()
        {
            this.players.Sort(delegate(Player x, Player y)
            {
                return y.total_points.CompareTo(x.total_points);
            });
            this.GKP = 0; // 1
            this.DEF = 0; // 3 - 5
            this.MID = 0; // 2 - 5
            this.FWD = 0; // 1 - 3

            int i = 0;

            foreach (var player in this.players)
            {
                if (i == 0){
                    this.captain = player.second_name;
                } else if (i == 1) {
                    this.viceCaptain = player.second_name;
                }
                i++;
                if ((this.starters.Count < 11)){
                    if (player.position == "GKP" && this.GKP < 1){
                        this.GKP++;
                        this.starters.Add(player);
                    } else if (player.position == "DEF" && this.DEF < 5)
                    {
                        this.DEF++;
                        this.starters.Add(player);
                    } else if (player.position == "MID" && this.MID < 5)
                    {
                        this.MID++;
                        this.starters.Add(player);
                    } else if (player.position == "FWD" && this.FWD < 3)
                    {
                        this.FWD++;
                        this.starters.Add(player);
                    }
                }
            }
            this.starters.Sort(delegate(Player x, Player y)
            {
                return y.element_type.CompareTo(x.element_type);
            });
            return null;
        }
    }
}