using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressoNewApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpressoNewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private ExpressoDbContext _expressoDbContext;

        public MenusController(ExpressoDbContext expressoDbContext)
        {
            _expressoDbContext = expressoDbContext;
        }

        // GET api/menus
        [HttpGet]
        public IActionResult Get()
        {
            var menus = _expressoDbContext.Menus.Include("SubMenus");
            return Ok(menus);
        }

        // GET api/menus/5
        [HttpGet("{id}")]
        public IActionResult GetMenu(int id)
        {
            var menu = _expressoDbContext.Menus.Include("SubMenus").FirstOrDefault(m => m.Id == id);
            if(menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }
    }
}