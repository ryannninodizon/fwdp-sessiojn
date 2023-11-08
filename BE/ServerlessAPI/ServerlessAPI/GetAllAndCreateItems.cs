using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ServerlessAPI
{
    public class GetAllAndCreateItems
    {
        private readonly AppDbContext _dbContext;

        public GetAllAndCreateItems(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("GetAllAndCreateItems")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "items")] 
                HttpRequest req
            )
        {
            if (req.Method == HttpMethods.Post)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var item = JsonConvert.DeserializeObject<Item>(requestBody);
                _dbContext.Items.Add(item);
                await _dbContext.SaveChangesAsync();
                return new OkObjectResult(item);
            }

            var items = await _dbContext.Items.ToListAsync();
            return new OkObjectResult(items);
        }

    }
}
