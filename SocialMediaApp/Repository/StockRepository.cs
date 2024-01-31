using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.Models;
using SocialMediaApp.DTO.Stock;
using SocialMediaApp.Helpers;

namespace SocialMediaApp.Repository
{
    public class StockRepository(ApplicationDBContext context) : IStockRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            try

            {
 
                await _context.Stock.AddAsync(stockModel);
                await _context.SaveChangesAsync();

                return stockModel;

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");

            }
        }

        public async Task<int?> DeleteAsync(int Id)
        {
            try
            {
                var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == Id);

                if (stock == null) return null;

                _context.Stock.Remove(stock);

                await _context.SaveChangesAsync();

                return stock.Id;

            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            try
            {
                var stocks = _context.Stock.Include(c => c.Comments).AsQueryable();

                if (!string.IsNullOrWhiteSpace(query.CompanyName))
                {
                    stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
                }

                if (!string.IsNullOrEmpty(query.Symbol))
                {
                    stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));

                }

                if (!string.IsNullOrEmpty(query.SortBy))
                {
                    if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                    {
                        stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                    }
                }

                var skipNumber = (query.PageNumber - 1) * query.PageSize;

                return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Stock?> GetByIdAsync(int Id)
        {
                try
                    {
                        var stock = await _context.Stock.FindAsync(Id);
                        return stock ?? null;
                    }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public Task<bool> StockExists(int id)
        {
             try
            {
                 return this._context.Stock.AnyAsync(x => x.Id == id);    
            }catch (Exception ex)
            {
                 throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Stock?> UpdateAsync(int Id, UpdateStockDto stockDto)
        {
            try
                {
                    var stockModel = await _context.Stock.FirstOrDefaultAsync(s => s.Id == Id);

                    if (stockModel == null) return null;

                        stockModel.Symbol = stockDto.Symbol;
                        stockModel.CompanyName = stockDto.CompanyName;
                        stockModel.Purchase = stockDto.Purchase;
                        stockModel.LastDiv = stockDto.LastDiv;
                        stockModel.Industry = stockDto.Industry;
                        stockModel.MarketCap = stockDto.MarketCap;

                        await _context.SaveChangesAsync();

                    return stockModel;

                }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }

        }
    }
}
