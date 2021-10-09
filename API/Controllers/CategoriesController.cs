using System;
using System.Threading.Tasks;
using Contracts;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        public CategoriesController(IServiceManager serviceManager) : base(serviceManager)
        {
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await ServiceManager.CategoryService.GetAllAsync();

            return Ok(categories);
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