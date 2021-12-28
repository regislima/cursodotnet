using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain.Models;
using api.Domain.Services;
using api.Resources;
using api.Util.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/[controller]")]
    [Authorize()]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IEnumerable<ProductResource>> ListAsync()
        {
            var products = await _productService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            
            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ProductResource> GetByIdAsync(int id)
        {
            var product = await _productService.FindByIdAsync(id);
            var resource = _mapper.Map<Product, ProductResource>(product);

            return resource;
        }

        [HttpGet("GetbyName/{name}")]
        public async Task<IEnumerable<ProductResource>> GetByNameAsync(string name)
        {
            var products = await _productService.FindByNameAsync(name);
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var product = _mapper.Map<ProductResource, Product>(resource);
            var result = await _productService.SaveAsync(product);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var ProductResource = _mapper.Map<Product, ProductResource>(result.Answer);

            return Ok(ProductResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ProductResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var product = _mapper.Map<ProductResource, Product>(resource);
            var result = await _productService.UpdateAsync(id, product);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var ProductResource = _mapper.Map<Product, ProductResource>(result.Answer);

            return Ok(ProductResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var ProductResource = _mapper.Map<Product, ProductResource>(result.Answer);

            return Ok(ProductResource);
        }
    }
}