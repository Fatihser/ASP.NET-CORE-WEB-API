using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]

    //Ok(200), NotFound(404), NoContent(204), Created(201), BadRequest(400)
    public class ProductsController:ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result= await _productRepository.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data= await _productRepository.GetByIdAsync(id);
            if (data==null)
            {
                return NotFound(id);
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var addedProduct = await _productRepository.CreateAsync(product);
            return Created(string.Empty, addedProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var chackProduct = await _productRepository.GetByIdAsync(product.Id);
            if (chackProduct==null)
            {
                return NotFound(product.Id);
            }
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var chackProduct = await _productRepository.GetByIdAsync(id);
            if (chackProduct == null)
            {
                return NotFound(id);
            }
            await _productRepository.RemoveAsync(id);
            return NoContent();
        }

        //api/products/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm]IFormFile formFile)
        {
            var newName = Guid.NewGuid() + "." + Path.GetExtension(formFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newName);
            var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return Created(string.Empty, formFile);
        }

        ////////////////
        
        [HttpGet("[action]")]
        public IActionResult Test([FromServices] IDummyRepository dummyRepository)
        {
            return Ok(dummyRepository.GetName());
        }

    }
}
