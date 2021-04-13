using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressoNewApi.Data;
using ExpressoNewApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpressoNewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private ExpressoDbContext _expressoDbContext;

        public ReservationsController(ExpressoDbContext expressoDbContext)
        {
            _expressoDbContext = expressoDbContext;
        }

        // POST api/reservations
        [HttpPost]
        public IActionResult Post([FromBody] Reservation reservation)
        {
            _expressoDbContext.Reservations.Add(reservation);
            _expressoDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}