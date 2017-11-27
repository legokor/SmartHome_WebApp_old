﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartHome_WebApp.Models;
using SmartHome_WebApp.Data;

namespace SmartHome_WebApp.Controllers
{
    [Authorize]
    public class MeasurementController : Controller
    {
        private RepositoryService _repository;

        public MeasurementController(RepositoryService repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody] DataSample measurement)
        {
            //Check if the model is valid
            if(!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            if (!await _repository.DataSamples.Add(measurement))
            {
                //If the saving fails returns internal server error
                return StatusCode(500);
            }

            return new OkResult();
        }
    }
}