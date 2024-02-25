using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessesController : ControllerBase {
        IProcessService _processManager;

        public ProcessesController(IProcessService processManager) {
            _processManager = processManager;
        }

        [HttpGet("get")]
        public IActionResult Get(int id) {
            var result = _processManager.Get(id);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll() {
            var result = _processManager.GetAll();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetails")]
        public IActionResult GetDetails() {
            var result = _processManager.GetDetails();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(Process process) {
            var result = _processManager.Add(process);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Process process) {
            var result = _processManager.Delete(process);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Process process) {
            var result = _processManager.Update(process);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
