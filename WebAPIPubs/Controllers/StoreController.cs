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

        //GetByName
        [HttpGet("names/{name}")]
        public ActionResult<Store> GetByName(string name)
        {
            Store store = (from s in context.Stores
                           where name == s.StorName
                           select s).SingleOrDefault();
            return store;
        }

        //GetByZip
        [HttpGet("zip/{zip}")]
        public ActionResult<IEnumerable<Store>> GetByZip(string zip)
        {
            List<Store> stores = (from s in context.Stores
                                  where zip == s.Zip
                                  select s).ToList();
            return stores;
        }

        //GetByCityState
        [HttpGet("state/{state}")]
        public ActionResult<IEnumerable<Store>> GetByCityState(string state)
        {
            List<Store> stores = (from s in context.Stores
                                  where state == s.State
                                  select s).ToList();
            return stores;
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
        [HttpDelete("{idStor}")]
        public ActionResult<Store> Delete(string idStor)
        {
            Store store = (from s in context.Stores
                           where idStor == s.StorId
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
