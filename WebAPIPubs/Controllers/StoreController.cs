using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPIPubs.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;
        }

        // Get
        [HttpGet]
        public ActionResult<IEnumerable<Store>> Get()
        {
            return context.Stores.ToList();
        }

        //Get por Id
        [HttpGet("{id}")]
        public ActionResult<Store> GetByID(string id)
        {
            Store store = (from s in context.Stores
                           where id == s.StorId
                           select s).SingleOrDefault();
            return store;
        }

        //Put                        
        [HttpPut("{id}")]
        public ActionResult Put(string id, Store s)
        {
            if (id != s.StorId)
            {
                return BadRequest();
            }

            context.Entry(s).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //Post
        [HttpPost]
        public ActionResult Post(Store s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Stores.Add(s);
            context.SaveChanges();
            return Ok();
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult<Store> Delete(string id)
        {
            Store store = (from s in context.Stores
                         where s.StorId == id
                         select s).SingleOrDefault();

            if (store == null)
            {
                return NotFound();
            }

            context.Stores.Remove(store);
            context.SaveChanges();

            return store;
        }
    }
}
