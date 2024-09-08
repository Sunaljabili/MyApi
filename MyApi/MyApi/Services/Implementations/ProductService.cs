using Microsoft.EntityFrameworkCore;
using MyApi.Contexts;
using MyApi.DTOs.CommonDtos;
using MyApi.Models;
using MyApi.Services.Interfaces;
using System.Net;

namespace MyApi.Services.Implementations;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto> CreateProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return new ResponseDto
        {
            StatusCode = (int)HttpStatusCode.Created,
            Message = "Product successfully created"
        };
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }
}