using VuongLeTrung.Exercise02.Context;
using VuongLeTrung.Exercise02.DTOs;
using VuongLeTrung.Exercise02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VuongLeTrung.Exercise02.Context;
using VuongLeTrung.Exercise02.DTOs;
using VuongLeTrung.Exercise02.Models;

namespace VuongLeTrung.Exercise02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly Exercise02Context _db;

        public ProductController(Exercise02Context db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<ProductDTO> Get()
        {
            var products = _db.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category) // Kết hợp với thông tin từ bảng Category
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    SalePrice = p.SalePrice,
                    BuyingPrice = p.BuyingPrice,
                    Quantity = p.Quantity,
                    ShortDescription = p.ShortDescription,
                    ProductDescription = p.ProductDescription,
                    //Published = p.Published,
                    Note = p.Note,
                    ProductCategoryIds = p.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                    ProductCategoryNames = p.ProductCategories.Select(pc => pc.Category.CategoryName).ToList(), // Lấy ra tên các danh mục sản phẩm
                    ProductTagsNames = p.ProductTags.Select(pc => pc.Tag.TagName).ToList() // Lấy ra tên các danh mục sản phẩm
                }).ToList();

            return products;
        }
        [HttpGet("Tag/{tagName}")]
        public IEnumerable<ProductDTO> Get(string tagName)
        {
            var products = _db.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(P => P.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.ProductTags.Any(pt => pt.Tag.TagName == tagName))
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    SalePrice = p.SalePrice,
                    BuyingPrice = p.BuyingPrice,
                    ProductCategoryIds = p.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                    ProductCategoryNames = p.ProductCategories.Select(pc => pc.Category.CategoryName).ToList() // Lấy ra tên các danh mục sản phẩm
                }).ToList();

            return products;
        }
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(Guid id)
        {
            var product = _db.Products
.Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductTags) // Include ProductTags để lấy thông tin tags
                .ThenInclude(pt => pt.Tag)   // Include thông tin của Tag
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                SalePrice = product.SalePrice,
                BuyingPrice = product.BuyingPrice,
                Quantity = product.Quantity,
                ShortDescription = product.ShortDescription,
                ProductDescription = product.ProductDescription,
                //Published = product.Published,
                Note = product.Note,
                ProductCategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                ProductCategoryNames = product.ProductCategories?.Select(pc => pc.Category.CategoryName).ToList() ?? new List<string>(),
                ProductTagIds = product.ProductTags.Select(pc => pc.TagId).ToList(),
                ProductTagsNames = product.ProductTags?.Select(pt => pt.Tag.TagName).ToList() ?? new List<string>()
            };

            return productDTO;
        }





        [HttpPost]
        public ActionResult<ProductDTO> Post([FromBody] ProductDTO productDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                ProductName = productDTO.ProductName,
                SalePrice = productDTO.SalePrice,
                BuyingPrice = productDTO.BuyingPrice,
                Quantity = productDTO.Quantity,
                ShortDescription = productDTO.ShortDescription,
                ProductDescription = productDTO.ProductDescription,
                //Published = productDTO.Published,
                Note = productDTO.Note,
                CreatedAt = DateTime.Now,
            };

            _db.Products.Add(product);
            _db.SaveChanges();
            productDTO.Id = product.Id;

            foreach (var categoryId in productDTO.ProductCategoryIds)
            {
                var category = _db.Categories.Find(categoryId);
                if (category != null)
                {
                    var productCategory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        ProductId = product.Id // Sử dụng product.Id được tạo trước đó
                    };

                    _db.ProductCategories.Add(productCategory);
                }
                else
                {
                    return BadRequest($"Category with id {categoryId} does not exist.");
                }
            }
            foreach (var tagName in productDTO.ProductTagsNames)
            {
                var tag = _db.Tags.FirstOrDefault(t => t.TagName == tagName);
                if (tag != null)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tag.Id,
                        ProductId = product.Id
                    };
                    _db.ProductTags.Add(productTag);
                }
                else
                {
                    return BadRequest($"Tag with name '{tagName}' does not exist.");
                }
            }

            _db.SaveChanges(); // Lưu các product categories vào cơ sở dữ liệu

            return CreatedAtAction(nameof(Get), new { id = product.Id }, productDTO);
        }



        [HttpPut("{id}")]
        public ActionResult<ProductDTO> Put(Guid id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest("Id in the URL does not match the Id in the body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = _db.Products.Find(id);

            if (existingProduct == null)
            {
                return NotFound($"Product with Id {id} not found.");
            }

            existingProduct.ProductName = productDTO.ProductName;
            existingProduct.SalePrice = productDTO.SalePrice;
            existingProduct.BuyingPrice = productDTO.BuyingPrice;
            existingProduct.Quantity = productDTO.Quantity;
            existingProduct.ShortDescription = productDTO.ShortDescription;
            existingProduct.ProductDescription = productDTO.ProductDescription;
            //existingProduct.Published = productDTO.Published;
            existingProduct.Note = productDTO.Note;

            // Xóa tất cả các ProductCategories của sản phẩm hiện tại
            var existingProductCategories = _db.ProductCategories.Where(pc => pc.ProductId == id);
            _db.ProductCategories.RemoveRange(existingProductCategories);

            // Thêm các ProductCategories mới từ ProductDTO
            foreach (var categoryId in productDTO.ProductCategoryIds)
            {
                var category = _db.Categories.Find(categoryId);
                if (category != null)
                {
                    var productCategory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        ProductId = id
                    };

                    _db.ProductCategories.Add(productCategory);
                }
                else
                {
                    return BadRequest($"Category with id {categoryId} does not exist.");
                }
            }

            // Xóa tất cả các ProductTags của sản phẩm hiện tại
            var existingProductTags = _db.ProductTags.Where(pt => pt.ProductId == id);
            _db.ProductTags.RemoveRange(existingProductTags);

            // Thêm các ProductTags mới từ ProductDTO
            foreach (var tagName in productDTO.ProductTagsNames)
            {
                var tag = _db.Tags.FirstOrDefault(t => t.TagName == tagName);
                if (tag != null)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tag.Id,
                        ProductId = id
                    };
                    _db.ProductTags.Add(productTag);
                }
                else
                {
                    return BadRequest($"Tag with name '{tagName}' does not exist.");
                }
            }

            _db.SaveChanges();

            return Ok(productDTO);
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = _db.Products
                  .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(P => P.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .Include(p => p.Galleries)
                .FirstOrDefault(e => e.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Xóa tất cả các danh mục sản phẩm liên quan đến sản phẩm
            _db.ProductCategories.RemoveRange(product.ProductCategories);
            _db.Galleries.RemoveRange(product.Galleries);
            _db.ProductTags.RemoveRange(product.ProductTags);
            // Xóa sản phẩm
            _db.Products.Remove(product);
            _db.SaveChanges();

            return NoContent();
        }

    }
}