using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.ModelDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]/[action]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _bookrepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApiResponse _response;
        public BooksController(IBooksRepository bookreo, 
            IUnitOfWork unitofwork, IMapper mapper)
        {
            _bookrepo = bookreo;
            _unitOfWork = unitofwork;   
            _response = new ApiResponse();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _bookrepo.Get();
            if (model == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Errors = new List<string>()
                {
                    "No record found"
                };
                return NotFound(_response);
            }
            else
            {
                _response.StatusCode=System.Net.HttpStatusCode.OK;
                _response.Result = model;
                return Ok(_response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _bookrepo.Get(id);
            if(result == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Errors = new List<string>()
                {
                    "No record found against the id:" +id
                };
                return NotFound(_response);
            }
            _response.StatusCode=System.Net.HttpStatusCode.OK;
            _response.Result=result;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BooksDTO modelDto)
        {
            if(modelDto == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Errors = new List<string>()
                {
                    "Empty model"
                };
                return BadRequest(_response);
            }
            var model = _mapper.Map<Books>(modelDto);
            _unitOfWork.BooksRepository.Create(model);
            var rowchanges = await _unitOfWork.SaveChanges();
            if(rowchanges>0)
            {
                _response.StatusCode= System.Net.HttpStatusCode.OK;
                _response.Result = "Record saved successfully";
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Errors = new List<string>()
                {
                    "Could not save the record"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BooksDTO modelDto)
        {
            if (modelDto == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Errors = new List<string>()
                {
                    "Empty model"
                };
                return BadRequest(_response);
            }
            var model = _mapper.Map<Books>(modelDto);
            _unitOfWork.BooksRepository.Update(model);
            var rowchanges = await _unitOfWork.SaveChanges();
            if (rowchanges > 0)
            {
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = "Record updated successfully";
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Errors = new List<string>()
                {
                    "Could not update the record"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Delete(int bookId)
        {
            if(bookId <= 0)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Errors = new List<string>()
                {
                    "Bad request"
                };
                return BadRequest(_response);
            }
            var model = await _bookrepo.Get(bookId);
            if(model == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Errors = new List<string>()
                {
                    "Record for id : " + bookId + " not found"
                };
                return NotFound(_response);
            }
            _bookrepo.Delete(model);
            var rowchanges = await _unitOfWork.SaveChanges();
            if(rowchanges>0)
            {
                _response.StatusCode=System.Net.HttpStatusCode.OK;
                _response.Result = "Record deleted";
                return Ok(_response);
            }
            _response.StatusCode = System.Net.HttpStatusCode.InternalServerError;            
            _response.IsSuccess=false;
            _response.Errors = new List<string>()
            {
                "Internal server error"
            };

            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }
}
