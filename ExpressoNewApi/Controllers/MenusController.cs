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

        // GET api/menus/submenu/2
        [HttpGet("submenu/{id}")]
        public IActionResult GetSubMenu(int id)
        {
            var subMenu = _expressoDbContext.Menus
                            .Where(menu => menu.SubMenus.Any(sub => sub.Id == id))
                            .Select(menu => new
                            {
                                Id = menu.SubMenus.FirstOrDefault(sub => sub.Id == id).Id,
                                Name = menu.SubMenus.FirstOrDefault(sub => sub.Id == id).Name,
                                Description = menu.SubMenus.FirstOrDefault(sub => sub.Id == id).Description,
                                Price = menu.SubMenus.FirstOrDefault(sub => sub.Id == id).Price,
                                Image = menu.SubMenus.FirstOrDefault(sub => sub.Id == id).Image,
                                MenuId = menu.SubMenus.FirstOrDefault(sub => sub.Id == id).MenuId,
                                MenuName = menu.Name,
                            }).FirstOrDefault();

            if (subMenu == null)
            {
                return NotFound();
            }
            return Ok(subMenu);
        }
    }
}