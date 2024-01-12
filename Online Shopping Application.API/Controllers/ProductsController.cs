using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Online_Shopping_Application.API.Services;
using Online_Shopping_Application.API.ViewModel;
using AutoMapper;
using System;

namespace Online_Shopping_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JWTServices _jwtService;
        private readonly IProduct _product;
        private readonly IMapper _mapper;

        public ProductsController(JWTServices jwtService, IProduct product, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _jwtService = jwtService;
            _product = product;
            _mapper = mapper;
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _product.GetProductsAsync(id);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductViewModel productViewModel)
        {
            try
            {
                if (productViewModel == null)
                {
                    return BadRequest("Product object is null");
                }
                
                var productEntity = _mapper.Map<Product>(productViewModel);

                var createdProduct = await _product.CreateAsync(productEntity);

           
                var createdProductViewModel = _mapper.Map<ProductViewModel>(createdProduct);

                return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProductViewModel);
            }
            catch (Exception ex)
            {
             
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
