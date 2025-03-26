using WebApiIdentityJwt.Entities;

namespace WebApiIdentityJwt.Repository
{
    public interface InterfaceProduct
    {
        Task<ProductModel> Add(ProductModel objeto);
        Task<ProductModel> Update(ProductModel objeto);
        Task Delete(ProductModel objeto);
        Task<ProductModel> GetEntitybyId(int Id);
        Task<List<ProductModel>> List();
    }
}
