﻿using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SeatController : ControllerBase
    {
        private readonly ISeatServices _seatServices;

        public SeatController(ISeatServices seatServices)
        {
            _seatServices = seatServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeat(int pageSize=1,int pageNumber=5)
        {
            var listSeat = await _seatServices.GetAll(pageSize,pageNumber);
            return Ok(listSeat);
        }
        [HttpGet("GetAllSeatRoom")]
        public async Task<IActionResult> GetAllSeatRoom(int idRoom)
        {
            var listSeat = await _seatServices.GetAllInRoom(idRoom);
            return Ok(listSeat);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewSeat(Request_Seat request)
        {
            var seat = await _seatServices.AddNewSeat(request);
            return Ok(seat);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSeat(int id)
        {
            var Seat = await _seatServices.UpdateSeat(id);
            return Ok(Seat);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var delete = await _seatServices.DeleteSeat(id);
            return Ok(delete);
        }
    }
}