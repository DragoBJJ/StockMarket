﻿using System;
using SocialMediaApp.DTO.Stock;
using SocialMediaApp.Helpers;
using SocialMediaApp.Models;



public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
    Task<int?> DeleteAsync(int id);

    Task<bool> StockExists(int id);
}