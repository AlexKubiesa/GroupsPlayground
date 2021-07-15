using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupsPlayground.Persistence;
using GroupsPlayground.Persistence.Groups;
using GroupsPlayground.Persistence.Quests;

namespace GroupsPlayground.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (DatabaseSession session = new())
            {
                session.RegenerateDatabase();
            }

            using (QuestsSession session = new())
            {
                session.EnsureReferenceData();
                session.SaveChanges();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
