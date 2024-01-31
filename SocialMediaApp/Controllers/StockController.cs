using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Data;
using SocialMediaApp.DTO.Stock;
using SocialMediaApp.Helpers;
using SocialMediaApp.Mappers;
using SocialMediaApp.Models;


namespace SocialMediaApp.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(IStockRepository stockRepo) : ControllerBase
    {
        private readonly IStockRepository _stockRepo = stockRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
             var stocks = await this._stockRepo.GetAllAsync(query);

            if (stocks == null) return NotFound();

            var stocksDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocksDto); 
        }

        [HttpGet("{id:int}")]
        public  async Task<IActionResult> GetById([FromRoute] int id) {

            var stock =  await this._stockRepo.GetByIdAsync(id);

            if (stock == null) return NotFound(); 
            
            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        { 

             var stockModel = stockDto.ToStockFromCreateDto();
             var stock = await this._stockRepo.CreateAsync(stockModel);

            if (stock == null) return NotFound();

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
        {
          var stock = await this._stockRepo.UpdateAsync(id, stockDto); 
            
          if(stock == null) return NotFound();
          return Ok(stock);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
               var stockId = await _stockRepo.DeleteAsync(id);
            if(stockId == null) return NotFound();  
            return NoContent();
        }

    }
}
