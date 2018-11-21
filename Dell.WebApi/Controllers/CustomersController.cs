using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dell.Data.Entities;
using Dell.Data.EntityDB;
using Microsoft.AspNetCore.Mvc;

namespace Dell.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        public CustomersDB CustomersRepo { get; set; }

        public CustomersController(Data.EntityDB.CustomersDB customersDB )
        {
            CustomersRepo = customersDB;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult result = null;
            try
            {
                var rez = CustomersRepo.ReadAll();

                if (rez == null)
                    result = new NotFoundResult();
                else
                    result = new OkObjectResult(rez);
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(ex);
            }

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            IActionResult result = null;
            try
            {
                var rez = CustomersRepo.ReadByID(id);

                if (rez == null)
                    result = new NotFoundResult();
                else
                    result = new OkObjectResult(rez);
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(ex);
            }

            return result;
        }

        [HttpGet("byEmail/{email}")]
        public IActionResult GetbyEmail(string email)
        {
            IActionResult result = null;
            try
            {
                var rez = CustomersRepo.ReadByEmail(email);

                if (rez == null)
                    result = new NotFoundResult();
                else
                    result = new OkObjectResult(rez);
            }
            catch (Exception ex)
            {
                result = new BadRequestObjectResult(ex);
            }

            return result;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Data.Entities.Customer entity)
        {
            IActionResult result = null;

            if (entity != null)
            {
                try
                {
                    var rez = CustomersRepo.Insert(entity);

                    if (rez <= 0) 
                        result = new BadRequestResult();
                    else
                    {
                        var add = CustomersRepo.ReadByID(entity.ID);
                        result = new OkObjectResult(add);
                    }
                }
                catch(Exception ex)
                {
                    result = new BadRequestObjectResult(ex);
                }
            }
            return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Customer entity)
        {
            IActionResult result = null;

            if (entity != null)
            {
                try
                {
                    var rez = CustomersRepo.Update(entity);

                    if (rez <= 0)
                        result = new BadRequestResult();
                    else
                    {
                        var add = CustomersRepo.ReadByID(entity.ID);
                        result = new OkObjectResult(add);
                    }
                }
                catch (Exception ex)
                {
                    result = new BadRequestObjectResult(ex);
                }
            }
            return result;
        }

    }
}
