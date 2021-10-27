using APIWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;
using WebAPI2.Repositories;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private ICategoryRepository _categoryRepository;

        public CategoryController(EcommerceContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategorys();
            if (categories != null)
            {
                return new JsonResult(categories);
            }
            return NotFound("Not Category");
        }
    }
}
