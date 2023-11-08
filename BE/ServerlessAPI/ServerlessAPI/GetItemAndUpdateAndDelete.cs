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
    public class GetItemAndUpdateAndDelete
    {
        private readonly AppDbContext _dbContext;

        public GetItemAndUpdateAndDelete(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [FunctionName("GetItemAndUpdateAndDelete")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "put", "delete", Route = "items/{id}")] 
            HttpRequest req, int id)
        {

            if (req.Method == HttpMethods.Get)
            {
                var item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
                if (item == null) return new NotFoundResult();

                return new OkObjectResult(item);
            } else if (req.Method == HttpMethods.Put)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var item = JsonConvert.DeserializeObject<Item>(requestBody);
                item.Id = id;
                _dbContext.Items.Update(item);
                await _dbContext.SaveChangesAsync();
                return new OkObjectResult(item);
            }
            else
            {
                var item = await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
                if (item == null) return new NotFoundResult();

                _dbContext.Items.Remove(item);
                await _dbContext.SaveChangesAsync();
                return new NoContentResult();
            }
        }
    }
}
