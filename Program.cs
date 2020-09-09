using System;
using System.Net.Http;
using fantasy.Classes;
using fantasy.Controllers;
using Newtonsoft.Json;

namespace fantasy
{
    class Program
    {
        public static HttpClient client = new HttpClient();
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            ApiConnect apiConnect = new ApiConnect();
            MyTeam myTeam = await apiConnect.Connect();
            Console.WriteLine("Value: " + myTeam.value + ",000");
            Console.WriteLine("\r\nFull Team");
            foreach (var player in myTeam.players)
            {
                Console.WriteLine(player.second_name + " - " + player.teamName + " - " + player.position);
            }
            Console.WriteLine("\r\nStarters");
            foreach (var player in myTeam.starters)
            {
                Console.WriteLine(player.second_name + " - " + player.teamName + " - " + player.position);
            }
            Console.WriteLine("\r\nCaptain: " + myTeam.captain);
            Console.WriteLine("Vice Captain: " + myTeam.viceCaptain);
            // Console.WriteLine(JsonConvert.SerializeObject(myTeam));
        }
    }
}
 