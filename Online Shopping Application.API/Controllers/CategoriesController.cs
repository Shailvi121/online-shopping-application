using AutoMapper;
using Online_Shopping_Application.API.Services;
using Online_Shopping_Application.API.ViewModel;
using System.Diagnostics.Eventing.Reader;

namespace Online_Shopping_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly JWTServices _jwtService;
        private readonly ICategory _category;
        private readonly IMapper _mapper;

        public CategoriesController(JWTServices jwtService, ICategory category, IMapper mapper)
        {
            _jwtService = jwtService;
            _category = category;
            _mapper = mapper;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _category.GetAll();
            return Ok(categories);
        }


        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _category.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }



        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CategoryViewModel categoryViewModel)
        {

            if (categoryViewModel != null)
            {

                var categoryView = _mapper.Map<Category>(categoryViewModel);
                await _category.Create(categoryView);
                return Ok();
            }
            else
            {
                return StatusCode(500, new { Message = "An error occurred.", StatusCode = 500 });
            }
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryViewModel categoryViewModel)
        {
            if (categoryViewModel != null)
            {
                var category = _mapper.Map<Category>(categoryViewModel);
                category.ID= id;
                await _category.Update(id, category);
                
                return Ok();
            }
            else
            {
                return StatusCode(500, new { Message = "An error occurred", StatusCode = 500 });
            }
        }



        [HttpDelete("Delete/{id}")]





        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                await _category.Delete(id);

                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "This should not be deleted. Invalid ID." });
            }
        }
    }
}
