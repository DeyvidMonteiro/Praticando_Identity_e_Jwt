using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiIdentityJwt.Entities;
using WebApiIdentityJwt.Repository;

namespace WebApiIdentityJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class productsController : ControllerBase
    {
        private readonly InterfaceProduct _interfaceProduct;

        public productsController(InterfaceProduct interfaceProduct)
        {
            _interfaceProduct = interfaceProduct;
        }

        [HttpGet("/api/List")]
        [Produces("application/json")]
        public async Task<object> List()
        {
            return await _interfaceProduct.List();
        }

        [HttpGet("/api/GetEntitybyId")]
        [Produces("application/json")]
        public async Task<object> GetEntitybyId(int id)
        {
            return await _interfaceProduct.GetEntitybyId(id);
        }

        [HttpPost("/api/Add")]
        [Produces("application/json")]
        public async Task<object> Add(ProductModel productModel)
        {
            try
            {
                return await _interfaceProduct.Add(productModel);
            }
            catch (Exception ERROR)
            {

            }

            return Task.FromResult("OK");
        }

        [HttpPut("/api/Update")]
        [Produces("application/json")]
        public async Task<object> Update(ProductModel productModel)
        {
            try
            {
                return await _interfaceProduct.Update(productModel);
            }
            catch (Exception ERROR)
            {

            }

            return Task.FromResult("OK");
        }

        [HttpDelete("/api/Delete")]
        [Produces("application/json")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var product = await _interfaceProduct.GetEntitybyId(id);
                await _interfaceProduct.Delete(product);
            }
            catch (Exception ERROR)
            {
                return false;
            }

            return Task.FromResult("OK");
        }


    }
}
