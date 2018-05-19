using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewMe.DAL
{
    public class InitializeDB
    {
        public static void Init()
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.Database.Delete();
                }
                catch
                {
                    //Ignore
                }
                context.Database.Create();
                context.Database.Initialize(true);
                context.Stores.Add(new Models.Store
                {
                    Name = "player1"
                });
            }
        }
    }
}