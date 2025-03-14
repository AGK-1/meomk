using System.Net.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using sqli_samko.Models;

namespace sqli_samko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContraController : ControllerBase
    {
        private readonly AppDb _context;

        public ContraController(AppDb context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cars>>> Getcars()
        {
            var rest = await _context.Carss.ToListAsync();
            return rest;
        }

        [HttpPost]
        public async Task<ActionResult<Cars>> PotsCar(Cars newCars)
        {
            var addCar = _context.Carss.Add(newCars);
            await _context.SaveChangesAsync();
            return Ok("OK ey bro");

        }

    }



}