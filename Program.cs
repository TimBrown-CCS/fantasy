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
        static async Task Main(string[] args)
        {
            GetFixtures getFixtures = new GetFixtures();
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
            Console.WriteLine(JsonConvert.SerializeObject(myTeam));
        }
    }
}
 