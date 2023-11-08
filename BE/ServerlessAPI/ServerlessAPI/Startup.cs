using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:FunctionsStartup(typeof(ServerlessAPI.Startup))]

namespace ServerlessAPI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("DBConnectionSrting");
            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));
        }
    }
}
