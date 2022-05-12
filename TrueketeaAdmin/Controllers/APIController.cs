using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TrueketeaAdmin.Models;
using TrueketeaAdmin.Services.DB;
using TrueketeaAdmin.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrueketeaAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private DBContext dBContext = Startup.dbContext;

        // GET: api/<APIController>
        [HttpGet]
        public IEnumerable<Login_Users> Get()
        {

            LoginViewModel lg = new LoginViewModel(dBContext);
            lg.LoadAllUsers();

            var rng = new Random();
            return Enumerable.Range(1, lg.AppUsersDT.Rows.Count).Select(index => new Login_Users
            {
                UserId = (decimal)lg.AppUsersDT.Rows[index-1][0],
                RolId = (decimal)lg.AppUsersDT.Rows[index-1][1],
                UserName = (string)lg.AppUsersDT.Rows[index-1][2],
                UserEmail = (string)lg.AppUsersDT.Rows[index-1][3],
                Password = (string)lg.AppUsersDT.Rows[index-1][4],
                LastConnection = (string)lg.AppUsersDT.Rows[index-1][5],
                Device = (string)lg.AppUsersDT.Rows[index-1][6],
                Active = (decimal)lg.AppUsersDT.Rows[index-1][7],
                Photo = (string)lg.AppUsersDT.Rows[index-1][8]
            })
            .ToArray();


        }

        // GET api/<APIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<APIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<APIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<APIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
