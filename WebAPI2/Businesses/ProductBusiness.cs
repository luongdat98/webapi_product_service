using APIWeb.Dtos;
using APIWeb.Repositories;
using System.Collections.Generic;
using System.Linq;
using WebAPI2.Helpers;
using WebAPI2.Models;

namespace APIWeb.Businesses
{
    public class ProductBusiness : IProductRepository
    {
        private readonly EcommerceContext _ecommerceDBContext;

        public ProductBusiness(EcommerceContext ecommerceDBContext)
        {
            _ecommerceDBContext = ecommerceDBContext;
        }

        public PageList<Product> GetAllProducts(ProductPaging productPaging)
        {
            var products = _ecommerceDBContext.Products.ToList().AsQueryable();
            return PageList<Product>.ToPageList(products, productPaging.PageNumber
                                               , productPaging.PageSize);
        }

        public PageList<Product> GetAllProductsBySearch(ProductPaging productPaging, string search)
        {
            var products = _ecommerceDBContext.Products.Where(p => p.NameProduct.Contains(search.Trim())).ToList().AsQueryable();
            return PageList<Product>.ToPageList(products, productPaging.PageNumber
                                               , productPaging.PageSize);
        }

        public PageList<Product> GetProductByFilterCategory(ProductPaging productPaging, int idCategory)
        {
            var products = _ecommerceDBContext.Products
                            .Where(p => p.CategoryId == idCategory).ToList().AsQueryable();
                return PageList<Product>.ToPageList(products, productPaging.PageNumber
                                               , productPaging.PageSize);
        }

        public PageList<Product> GetAllProductsBySearchAndFilter(ProductPaging productPaging, string search,
                                                        int idCategory)
        {
            var products = _ecommerceDBContext.Products.Where(p => p.NameProduct.Contains(search.Trim()))
                            .Where(p => p.CategoryId == idCategory)
                            .ToList().AsQueryable();
            return PageList<Product>.ToPageList(products, productPaging.PageNumber
                                               , productPaging.PageSize);
        }


        public ProductEditDto GetProductById(int idProduct)
        {
            var product = _ecommerceDBContext.Products.Where(p => p.ProductId == idProduct).FirstOrDefault();
            ProductEditDto productEdit = new ProductEditDto
            {
                NameProduct = product.NameProduct,
                Description = product.Description,
                ReleaseDate = product.ReleaseDate,
                DiscontinuedDate = product.DiscontinuedDate,
                Rating = product.Rating,
                Price = product.Price,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId
            };

            return productEdit;

        }

        public Product AddProduct(ProductAddDto product)
        {
            using (EcommerceContext entityObject = new EcommerceContext())
            {
                Product item = new Product
                {
                    NameProduct = product.NameProduct,
                    Description = product.Description,
                    ReleaseDate = product.ReleaseDate,
                    DiscontinuedDate = product.DiscontinuedDate,
                    Rating = product.Rating,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    SupplierId = product.SupplierId
                };
                entityObject.Products.Add(item);
                entityObject.SaveChanges();
                return item;
            }
        }

        public Product UpdateProduct(ProductDto item)
        {
            Product product = _ecommerceDBContext.Products.Where(p => p.ProductId == item.ProductId).FirstOrDefault();

            if (product != null)
            {
                product.NameProduct = item.NameProduct;
                product.Description = item.Description;
                product.ReleaseDate = item.ReleaseDate;
                product.DiscontinuedDate = item.DiscontinuedDate;
                product.Rating = item.Rating;
                product.Price = item.Price;
                product.CategoryId = item.CategoryId;
                product.SupplierId = item.SupplierId;

                _ecommerceDBContext.SaveChanges();
            }
            return product;

        }

        public void DeleteProduct(int idProduct)
        {
            Product item = _ecommerceDBContext.Products.Where(p => p.ProductId == idProduct).FirstOrDefault();
            if (item != null)
            {
                _ecommerceDBContext.Remove(item);
                _ecommerceDBContext.SaveChanges();
            }
        }

        

        

    }
}
