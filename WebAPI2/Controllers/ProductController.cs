using APIWeb.Businesses;
using APIWeb.Dtos;
using APIWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;

namespace APIWeb.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private IProductRepository _productRepository;

        public ProductController(EcommerceContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        //[Route("search")]
        //[HttpGet]
        //public IActionResult GetAllProduct([FromQuery] ProductPaging productPaging, string searchProduct = null, int idCategory = 0)
        //{
        //    if (idCategory > 0 && !String.IsNullOrEmpty(searchProduct))
        //    {
        //        var productFilterWithSearch = _productRepository.GetAllProductsBySearchAndFilter(productPaging, searchProduct, idCategory);
        //        var metadata = new
        //        {
        //            productFilterWithSearch.TotalCount,
        //            productFilterWithSearch.PageSize,
        //            productFilterWithSearch.CurrentPage,
        //            productFilterWithSearch.HasNext,
        //            productFilterWithSearch.HasPrevious
        //        };
        //        Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata));
        //        return new JsonResult(productFilterWithSearch);
        //    }
        //    else if (idCategory > 0 && String.IsNullOrEmpty(searchProduct))
        //    {
        //        var productFilterWithoutSearch = _productRepository.GetProductByFilterCategory(productPaging, idCategory);
        //        var metadata = new
        //        {
        //            productFilterWithoutSearch.TotalCount,
        //            productFilterWithoutSearch.PageSize,
        //            productFilterWithoutSearch.CurrentPage,
        //            productFilterWithoutSearch.HasNext,
        //            productFilterWithoutSearch.HasPrevious
        //        };
        //        Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata));
        //        return new JsonResult(productFilterWithoutSearch);
        //    }
        //    else if (idCategory == 0 && string.IsNullOrEmpty(searchProduct))
        //    {
        //        var products = _productRepository.GetAllProducts(productPaging);
        //        var metadata = new
        //        {
        //            products.TotalCount,
        //            products.PageSize,
        //            products.CurrentPage,
        //            products.HasNext,
        //            products.HasPrevious
        //        };

        //        Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata));
        //        return new JsonResult(products);
        //    }

        //    else if(idCategory == 0 && !string.IsNullOrEmpty(searchProduct))
        //    {
        //        var productSearch = _productRepository.GetAllProductsBySearch(productPaging, searchProduct);

        //            var metadata2 = new
        //            {
        //                productSearch.TotalCount,
        //                productSearch.PageSize,
        //                productSearch.CurrentPage,
        //                productSearch.HasNext,
        //                productSearch.HasPrevious
        //            };

        //            Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata2));
        //            return new JsonResult(productSearch);
        //    }
        //    else
        //    {
        //        return NotFound("Not Product");
        //    }
        //}

        [Route("search")]
        [HttpGet]
        public IActionResult GetAllProduct([FromQuery] ProductPaging productPaging, string searchProduct = null, int idCategory = 0)
        {
            if (idCategory > 0 && !String.IsNullOrEmpty(searchProduct))
            {
                var productFilterWithSearch = _productRepository.GetAllProductsBySearchAndFilter(productPaging, searchProduct, idCategory);
                var metadata = new
                {
                    productFilterWithSearch.TotalCount,
                    productFilterWithSearch.PageSize,
                    productFilterWithSearch.CurrentPage,
                    productFilterWithSearch.HasNext,
                    productFilterWithSearch.HasPrevious
                };
                ProductDataToClientDto productDataFilterWithSearch = new ProductDataToClientDto
                {
                    Items = productFilterWithSearch,
                    Meta = metadata
                };
                Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata));
                return new JsonResult(productDataFilterWithSearch);
            }
            else if (idCategory > 0 && String.IsNullOrEmpty(searchProduct))
            {
                var productFilterWithoutSearch = _productRepository.GetProductByFilterCategory(productPaging, idCategory);
                var metadata1 = new
                {
                    productFilterWithoutSearch.TotalCount,
                    productFilterWithoutSearch.PageSize,
                    productFilterWithoutSearch.CurrentPage,
                    productFilterWithoutSearch.HasNext,
                    productFilterWithoutSearch.HasPrevious
                };
                ProductDataToClientDto productDataFilterWithoutSearch = new ProductDataToClientDto
                {
                    Items = productFilterWithoutSearch,
                    Meta = metadata1
                };
                Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata1));
                return new JsonResult(productDataFilterWithoutSearch);
            }
            else if (idCategory == 0 && string.IsNullOrEmpty(searchProduct))
            {
                var products = _productRepository.GetAllProducts(productPaging);
                var metadata2 = new
                {
                    products.TotalCount,
                    products.PageSize,
                    products.CurrentPage,
                    products.HasNext,
                    products.HasPrevious
                };
                ProductDataToClientDto productDataPaging = new ProductDataToClientDto
                {
                    Items = products,
                    Meta = metadata2
                };

                Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata2));
                return new JsonResult(productDataPaging);
            }
            else if (idCategory == 0 && !string.IsNullOrEmpty(searchProduct))
            {
                var productSearch = _productRepository.GetAllProductsBySearch(productPaging, searchProduct);

                var metadata3 = new
                {
                    productSearch.TotalCount,
                    productSearch.PageSize,
                    productSearch.CurrentPage,
                    productSearch.HasNext,
                    productSearch.HasPrevious
                };

                ProductDataToClientDto productDataSearch = new ProductDataToClientDto
                {
                    Items = productSearch,
                    Meta = metadata3
                };
                Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata3));
                return new JsonResult(productDataSearch);
            }
            else
            {
                return NotFound("Not Product");
            }
        }


        [Route("id")]
            [HttpGet]
            public IActionResult GetProductByIdProduct(int id)
            {
                var product = _productRepository.GetProductById(id);
                if (product != null)
                {
                    return new JsonResult(product);
                }
                return NotFound($"Product with Id product: {id} was not found");
            }

            [Route("addproduct")]
            [HttpPost]
            public IActionResult AddProduct(ProductAddDto product)
            {
                var item = _productRepository.AddProduct(product);
                if (item != null)
                {
                    return new JsonResult("Add product successful");
                }
                else
                {
                    return NotFound($"Add Product: {item.NameProduct} is fail");
                }
            }

            //[Route("editproduct/{id}")]
            [HttpPut]
            public IActionResult EditProduct(ProductDto product)
            {
                var item = _productRepository.UpdateProduct(product);
                if (item != null)
                {
                    return new JsonResult("Update product successful");

                }
                else
                {
                    return NotFound($"Edit Product: {item.NameProduct} is fail");
                }
            }

            // Cách 2:
            [Route("{id}")]
            [HttpDelete]
            //[HttpDelete("{id}")]
            public IActionResult DeleteProduct(int id)
            {
                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    _productRepository.DeleteProduct(id);
                    return new JsonResult("Delete Successful");
                }
                return NotFound($"Delete Product: {id} is fail");
        }      
    }

    // Cách 1
    //[Route("{id}")]
    //[HttpDelete]
    ////[HttpDelete("{id}")]
    //public IActionResult DeleteProduct(int productId)
    //{
    //    if (!string.IsNullOrEmpty(productId.ToString()))
    //    {
    //        _productRepository.DeleteProduct(productId);
    //        return new JsonResult("Delete Successful");
    //    }
    //    return NotFound($"Delete Product: {productId} is fail");
    //}

    //[Route("filter")]
    //[HttpGet]
    //public IActionResult GetProductByFilterCategory(int id)
    //{
    //    if (!string.IsNullOrEmpty(id.ToString()))
    //    {
    //        var products = _productRepository.GetProductByFilterCategory(id);
    //        return new JsonResult(products);
    //    }
    //    else
    //    {
    //        return NotFound("Not Product");
    //    }
    //}
}
