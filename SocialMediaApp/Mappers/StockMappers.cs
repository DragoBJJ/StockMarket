using SocialMediaApp.DTO.Stock;
using SocialMediaApp.Models;

namespace SocialMediaApp.Mappers;



public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
        };
    }
    public static Stock ToStockFromCreateDto(this CreateStockDto stockModel)
    {
        return new Stock
        {
            Symbol = stockModel.Symbol,
            LastDiv = stockModel.LastDiv,
            CompanyName = stockModel.CompanyName,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
        };
    }
}
