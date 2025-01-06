namespace web_api.Controllers
{
    using System.Text.Json;
    using Marten;
    using Microsoft.AspNetCore.Mvc;
    using web_api.dto;

    [ApiController]
    [Route("api/[controller]")]
    public class KeyValueController : ControllerBase
    {
        private readonly IDocumentStore _documentStore;

        public KeyValueController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        // POST: api/KeyValue
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate( [FromBody] KeyValue data)
        {
            using var session = _documentStore.LightweightSession();
           // var record = new KeyValue { Key = data.key, Data = data };
            session.Store(data);
            await session.SaveChangesAsync();
            return Ok(new { Message = "Data stored successfully", Key = data.Key });
        }

        // GET: api/KeyValue/{key}
        [HttpGet("{key}")]
        public async Task<IActionResult> Get([FromRoute] string key)
        {
            using var session = _documentStore.QuerySession();
            var record = await session.Query<KeyValue>().FirstOrDefaultAsync(x => x.Key == key);

            if (record == null)
            {
                return NotFound(new { Message = "Key not found", Key = key });
            }

            if (record.Data == null)
            {
                return NotFound(new { Message = "Key found, but no data available", Key = key });
            }



            Console.WriteLine(JsonSerializer.Serialize(record.Data));

            
          
            Console.WriteLine("record is : " + record);
            Console.WriteLine("key is : " + record.Key);
            Console.WriteLine("data is : " + record.Data);




            // For other object types
            return Ok(record.Data);


        }


        // DELETE: api/KeyValue/{key}
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            using var session = _documentStore.LightweightSession();
            var record = await session.Query<KeyValue>().FirstOrDefaultAsync(x => x.Key == key);

            //var records = await session.Query<KeyValue>().Where(x => x.Key == key).ToListAsync();

            if (record == null)
            {
                return NotFound(new { Message = "Key not found", Key = key });
            }

            session.Delete(record);
            await session.SaveChangesAsync();
            return Ok(new { Message = "Key deleted successfully", Key = key });
        }
    }

}
