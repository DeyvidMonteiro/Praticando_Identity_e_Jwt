using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApiIdentityJwt.Context;
using WebApiIdentityJwt.Entities;

namespace WebApiIdentityJwt.Repository;

public class RepositoryProduct : InterfaceProduct
{
    private readonly DbContextOptions<AppDbContext> _context;

    public RepositoryProduct(DbContextOptions<AppDbContext> context)
    {
        _context = context;
    }

    public async Task<ProductModel> Add(ProductModel objeto)
    {
        using (var data = new AppDbContext(_context))
        {
            await data.Set<ProductModel>().AddAsync(objeto);
            await data.SaveChangesAsync();

            return objeto;
        }
    }

    public async Task Delete(ProductModel objeto)
    {
        using (var data = new AppDbContext(_context))
        {
            data.Set<ProductModel>().Remove(objeto);
            await data.SaveChangesAsync();
        }
    }

    public async Task<ProductModel> GetEntitybyId(int Id)
    {
        using (var data = new AppDbContext(_context))
        {
            var produto = await data.Set<ProductModel>().FindAsync(Id);

            if (produto == null)
                return null;

            return produto;
        }
    }

    public async Task<List<ProductModel>> List()
    {
        using (var data = new AppDbContext(_context))
        {
            return await data.Set<ProductModel>().ToListAsync();
        }
    }

    public async Task<ProductModel> Update(ProductModel objeto)
    {
        using (var data = new AppDbContext(_context))
        {
            data.Set<ProductModel>().Update(objeto);
            await data.SaveChangesAsync();

            return objeto;
        }
    }
}
