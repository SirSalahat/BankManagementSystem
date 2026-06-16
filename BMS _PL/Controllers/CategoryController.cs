using BMS_BLL.Services.Interfaces;
using BMS_DALL.Classes;
using BMS_DALL.DTOs.Requests;
using BMS_DALL.DTOs.Responses;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMS__PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService _service)
        {
            service = _service;
        }
        [HttpPost("Add")]
        public IActionResult Add(CategoryRequest request)
        {
            var result = service.Add(request.Adapt<Category>());
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            service.GetAll();
            return Ok(service.GetAll());

        }
        [HttpGet("GetById")]

        public IActionResult GetById(int id)
        {
            var result = service.GetById(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public IActionResult Update(int id,CategoryRequest request)
        {

            var result = service.Update(request.Adapt<Category>());
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var result = service.Delete(id);
            return Ok(result);
        }
    }
}
