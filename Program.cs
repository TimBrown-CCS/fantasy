using System;
using System.Net.Http;
using System.Threading.Tasks;
using fantasy.Classes;
using fantasy.Controllers;
using Newtonsoft.Json;

namespace fantasy
{
    class Program
    {
        public static HttpClient client = new HttpClient();
        public static string cookie = "pl_profile=eyJzIjogIld6SXNNVFkzTWpJM09EZGQ6MWtJN3R1OkxyclZ3S0lwNHBVaTNTTno3UmNwSnBCNmV5byIsICJ1IjogeyJpZCI6IDE2NzIyNzg3LCAiZm4iOiAiVGltIiwgImxuIjogIkJyb3duIiwgImZjIjogM319; csrftoken=ArE8KVyt9hPVDbZLwdNU2qhjSsCfTL5JF1FWh8MFA2x4kmDgFjnbEyMjvJ9A2QP1; sessionid=.eJyrVopPLC3JiC8tTi2Kz0xRslIyNDM3MjK3MFfSQZZKSkzOTs0DyRfkpBXk6IFk9AJ8QoFyxcHB_o5ALqqGjMTiDKBqS0MTy8S0VHNjI7OUlFTzFENjw1QzY1MLQ0uzZAPDVEMDCxOL1DRDS6VaAHt0K_M:1kI7tv:6TCHDRmp0u5i3DsP7wdqBWPpo2o";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Get fixtures?");
            var fixtures = Console.ReadLine();
            GetFixtures getFixtures = new GetFixtures();
            if (fixtures.ToLower() == "yes" || fixtures.ToLower() == "true")
            {
                Console.WriteLine("Yes!");
            }
            Console.WriteLine("Getting fixtures");
            await getFixtures.AllFixtures();
            Console.WriteLine("Picking team");
            ApiConnect apiConnect = new ApiConnect();
            MyTeam myTeam = await apiConnect.Connect(getFixtures.fixtures);
            Console.WriteLine("Value: " + myTeam.value + ",000");
            Console.WriteLine("Players: " + myTeam.players.Count);
            if(myTeam.players.Count < 15){
                throw new SystemException("Not enough players selected");
            }
            Console.WriteLine("\r\nFull Team");

            foreach (var player in myTeam.players)
            {
                Console.WriteLine(player.web_name + " - " + player.teamName + " - " + player.position + " - " + player.potential);
            }
            Console.WriteLine("\r\nStarters");
            foreach (var player in myTeam.starters)
            {
                Console.WriteLine(player.web_name + " - " + player.teamName + " - " + player.position);
            }
            Console.WriteLine("\r\nCaptain: " + myTeam.captain);
            Console.WriteLine("Vice Captain: " + myTeam.viceCaptain);
            // Console.WriteLine(JsonConvert.SerializeObject(myTeam));
        }
    }
}
 