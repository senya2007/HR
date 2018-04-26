using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewMe_Client
{
    public interface IStoreApi
    {
        [Get("/visitors/count")]
        Task<int> FetchCountAsync(string player);

        [Get("/add")]
        Task AddHumanVisitorsAsync(string player, int count);

        [Delete("/visitors/count")]
        Task ResetVisitorsCountAsync(string player);
    }

    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
            Console.WriteLine("Press any key for exit...");
            Console.ReadLine();
        }

        static async Task MainAsync(string[] args)
        {
            IStoreApi api = RestClient.For<IStoreApi>("http://localhost:60404");

            const int count = 10;
            int expectedValue = Enumerable.Range(0, count).Sum();

            const string storeName = "player1"; 

            await api.ResetVisitorsCountAsync(storeName);

            var list = new List<Task>();
            for(int i = 0; i < count; i++)
            {
                int value = i;
                list.Add(Task.Run(async () => 
                {                    
                    await api.AddHumanVisitorsAsync(storeName, value);
                }));
            }
            
            Task.WaitAll(list.ToArray());

            var result = await api.FetchCountAsync(storeName);

            if (result == expectedValue)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed!"); 
            }
        }
    }
}
