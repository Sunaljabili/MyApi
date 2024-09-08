using MyApi.DTOs.CommonDtos;
using MyApi.Models;

namespace MyApi.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<ResponseDto> CreateProductAsync(Product product);
}