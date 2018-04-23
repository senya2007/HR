using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewMe_Client
{

    public interface IGitHubApi
    {
        [Get("/visitors/count")]
        Task<int> FetchUserAsync(string player);

        [Get("/add")]
        Task AddHumanVisitors(string player, int count);

        [Delete("/visitors/count")]
        Task DeleteVisitorsCount(string player);
    }


    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            IGitHubApi api = RestClient.For<IGitHubApi>("http://localhost:60404");
                        
            await api.DeleteVisitorsCount("player1");

            var list = new List<Task>();
            for(int i = 0; i < 10; i++)
            {                
                list.Add(Task.Run(async () => 
                {                    
                    await api.AddHumanVisitors("player1", i);
                }));
            }
            var res = await api.FetchUserAsync("player1");
            Console.WriteLine(res);
        }
    }
}
