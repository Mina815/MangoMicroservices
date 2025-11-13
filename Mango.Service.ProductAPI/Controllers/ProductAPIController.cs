using Azure;
using Mango.Service.ProductAPI.Models.Dto;
using Mango.Service.ProductAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Mango.Service.ProductAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace Mango.Service.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        private readonly ILogger<ProductAPIController> _logger;
        public ProductAPIController(AppDbContext db, IMapper mapper, ILogger<ProductAPIController> logger)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
            _logger = logger;
        }
        [HttpGet]
        public ResponseDto GetProducts()
        {
            try
            {
                _logger.LogInformation("Getting products from DB...");
                var objList = _db.Products.ToList();
                var x = _mapper.Map<List<ProductDto>>(objList);
                _response.Result = x;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting products from Database ---  {ex.InnerException} ---- {ex.StackTrace} ");
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto GetProductById(int id)
        {
            try
            {
                var obj = _db.Products.First(u => u.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting product with id = {id} from Database ---  {ex.InnerException} ---- {ex.StackTrace} ");
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ResponseDto CreateProduct([FromBody] ProductDto ProductDto)
        {
            try
            {
                var obj = _mapper.Map<Product>(ProductDto);
                _db.Products.Add(obj);
                _db.SaveChanges();

                if (ProductDto.Image != null)
                {

                    string fileName = obj.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;

                    //I have added the if condition to remove the any image with same name if that exist in the folder by any change
                    var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    FileInfo file = new FileInfo(directoryLocation);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        ProductDto.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    obj.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    obj.ImageLocalPath = filePath;
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }
                _db.Products.Update(obj);
                _db.SaveChanges();
                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error In create product ---  {ex.InnerException} ---- {ex.StackTrace} ");
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public ResponseDto UpdateProduct([FromBody] ProductDto ProductDto)
        {
            try
            {
                var obj = _mapper.Map<Product>(ProductDto);
                _db.Products.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error update product ---  {ex.InnerException} ---- {ex.StackTrace} ");
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public ResponseDto DeleteProductById(int id)
        {
            try
            {
                var obj = _db.Products.First(u => u.ProductId == id);
                _db.Products.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in delete product ---  {ex.InnerException} ---- {ex.StackTrace} ");
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
