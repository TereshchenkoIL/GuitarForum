using System;
using System.Threading.Tasks;
using Contracts;
using Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BaseApiController
    {
        public CategoriesController(IServiceManager serviceManager) : base(serviceManager)
        {
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await ServiceManager.CategoryService.GetAllAsync();

            return Ok(categories);
        }
        [Authorize]
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(Guid categoryId)
        {
            var category = await ServiceManager.CategoryService.GetByIdAsync(categoryId);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            await ServiceManager.CategoryService.CreateAsync(categoryDto);

            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            await ServiceManager.CategoryService.UpdateAsync(categoryDto);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await ServiceManager.CategoryService.DeleteAsync(id);

            return Ok();
        }
    }
}