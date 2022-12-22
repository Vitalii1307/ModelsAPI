using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsApi.Data;
using ModelsApi.Models;

namespace ModelsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchesController : Controller
    {
        private readonly WatchesAPIDbContext dbContext;
        
        public WatchesController(WatchesAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetWatches()
        {
            return Ok(await dbContext.Watches.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWatch([FromRoute] Guid id)
        {
            var watch = await dbContext.Watches.FindAsync(id);
            
            if (watch == null)
            {
                return NotFound();
            }

            return Ok(watch);
        }

        [HttpPost]
        public async Task<IActionResult> AddWatches(AddWatchRequest addWatchRequest)
        {
            var watch = new Watch()
            {
                Id = Guid.NewGuid(),
                Name = addWatchRequest.Name,
                CompanyName = addWatchRequest.CompanyName,
                Price = addWatchRequest.Price,
            };

            await dbContext.Watches.AddAsync(watch);
            await dbContext.SaveChangesAsync();

            return Ok(watch);
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWatch([FromRoute] Guid id, UpdateWatchRequest updateWatchRequest)
        {
            var watch = await dbContext.Watches.FindAsync(id);

            if (watch != null)
            {
                watch.Name = updateWatchRequest.Name;
                watch.CompanyName = updateWatchRequest.CompanyName;
                watch.Price = updateWatchRequest.Price;

                await dbContext.SaveChangesAsync();

                return Ok(watch);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeteteWatch([FromRoute] Guid id)
        {
            var watch = await dbContext.Watches.FindAsync(id);

            if (watch != null) 
            {
                dbContext.Remove(watch);
                await dbContext.SaveChangesAsync();
                return Ok(watch);
            }
            return NotFound();
        }
    }
}
