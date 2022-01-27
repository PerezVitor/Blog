using Microsoft.AspNetCore.Mvc;
using Blog.Data;
using Blog.ViewModel;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Blog.Extensions;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BlogDataContext dbContext;
        public CategoryController([FromServices] BlogDataContext dbContext)
            => this.dbContext = dbContext;

        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var categories = await dbContext.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Erro interno no Servidor"));
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            try
            {
                var category = await dbContext
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return BadRequest(new ResultViewModel<Category>("Conteúdo não encotrado"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Erro interno no Servidor"));
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                Category category = model;

                await dbContext.Categories.AddAsync(category);
                await dbContext.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Erro interno no Servidor"));
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                var category = await dbContext
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return BadRequest(new ResultViewModel<Category>("Conteúdo não encotrado"));

                category.Name = model.Name;
                category.Slug = model.Slug.ToLower();

                dbContext.Categories.Update(category);
                await dbContext.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Erro interno no Servidor"));
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id)
        {
            try
            {
                var category = await dbContext
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return BadRequest(new ResultViewModel<Category>("Conteúdo não encotrado"));

                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("Erro interno no Servidor"));
            }
        }
    }
}
