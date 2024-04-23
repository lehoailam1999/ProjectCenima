using Application.Payload.Response;
using Application.Service.IServices;
using Azure;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeServices _services;
        private readonly IBaseRepositories<User> _baseRepositories;

        public IncomeController(IIncomeServices services, IBaseRepositories<User> baseRepositories)
        {
            _services = services;
            _baseRepositories = baseRepositories;
        }

        [HttpGet("Income")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Income(DateTime startAt, DateTime endAt)
        {
            var list = await _services.GetIncome(startAt, endAt);
            return Ok(list);
        }
        [HttpGet("GetFoodHighLight")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetFoodHighLight(int itemAmout)
        {
            var list = await _services.GetFoodHighLight(itemAmout);
            return Ok(list);
        }
    }
}
