using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Priority.Abstractions.Services;
using Priority.Data.Dto;
using Priority.Data.Models;

namespace Priority.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityTaskController : ControllerBase
    {
        private readonly IPriorityTaskService _priorityTaskService;
        private readonly IMapper _mapper;

        public PriorityTaskController(IPriorityTaskService priorityTaskService, IMapper mapper)
        {
            _mapper = mapper;
            _priorityTaskService = priorityTaskService;
        }

        [HttpPost]
        public IActionResult Create([FromBody]PriorityTaskModifyDto request)
        {
            var entity = _mapper.Map<PriorityTask>(request);
            var id = _priorityTaskService.Create(entity).Id;
            return Ok(id);
        }

        [HttpPut]
        public IActionResult Update([FromBody]PriorityTaskModifyDto request)
        {
            var entity = _mapper.Map<PriorityTask>(request);
            _priorityTaskService.Update(entity);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int id)
        {
            _priorityTaskService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_priorityTaskService.GetAll());
        }

        [HttpGet("all-with-number")]
        public IActionResult GetAllWithNumber()
        {
            return Ok(_priorityTaskService.GetAllWithNumber());
        }
    }
}