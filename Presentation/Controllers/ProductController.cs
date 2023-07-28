using Application.Dtos.Product;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductQueryService _queryService;
        private readonly IProductCommandService _commandService;
        private readonly IMapper _mapper;

        public ProductController(IProductCommandService commandService,IProductQueryService queryService,IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _queryService = queryService;
            _commandService = commandService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,string? userId = null)
        {
         
            var products = await _queryService.GetAllProductsAsync(pageNumber,pageSize, userId);
            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _queryService.GetProductByIdAsync(id);
            if(product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreationDto productDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(productDto);

            if(product == null)
                return BadRequest(ModelState);

            await _commandService.AddProductAsync(product);

            return Ok(product.Id);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id,ProductCreationDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var IsProductExist = await _queryService.IsProductExistAsync(id);
            if (IsProductExist == false)
                return BadRequest();

            //Check if the LoggedInUser is the same as Manufactor or not
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var product = await _queryService.GetProductByIdAsync(id);

            if (email != product.ManufactorEmail)
                return BadRequest();

            await _commandService.UpdateProductAsync(id, productDto);
            
            return NoContent();
        }

        [HttpPatch("id")]
        public async Task<IActionResult> PartialUpdateProduct(string id, JsonPatchDocument<ProductCreationDto> patchDocument)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var IsProductExist = await _queryService.IsProductExistAsync(id);
            if (IsProductExist == false)
                return BadRequest();

            //Check if the LoggedInUser is the same as Manufactor or not
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var product = await _queryService.GetProductByIdAsync(id);

            if (email != product.ManufactorEmail)
                return BadRequest();

            //need this variable for "ApplyTo" function
            //store old values in it becuse first we checked the all actions if returns correct we mapp it to previous
            ProductCreationDto TestingProduct = _mapper.Map<ProductCreationDto>(product);

            patchDocument.ApplyTo(TestingProduct,ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            //Validate New Values by modelState
            if (!TryValidateModel(TestingProduct))
                return BadRequest(ModelState);

            await _commandService.UpdateProductAsync(id, TestingProduct);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var IsProductExist = await _queryService.IsProductExistAsync(id);
            if (IsProductExist == false)
                return BadRequest();

            //Check if the LoggedInUser is the same as Manufactor or not
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var product = await _queryService.GetProductByIdAsync(id);

            if (email != product.ManufactorEmail)
                return BadRequest();

            await _commandService.DeleteProduct(id);
            return NoContent();
        }
    }
}
