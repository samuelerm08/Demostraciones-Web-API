using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPIPubs.Models;

namespace WebAPIPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly pubsContext context;

        public PublisherController(pubsContext context)
        {
            this.context = context;
        }

        // Get
        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> GetClinica()
        {
            return context.Publishers.ToList();
        }

        //Get por Id
        [HttpGet("{id}")]
        public ActionResult<Publisher> GetByID(string id)
        {
            Publisher publisher = (from m in context.Publishers
                                   where id == m.PubId
                                   select m).SingleOrDefault();
            return publisher;
        }

        //Put                        
        [HttpPut("{id}")]
        public ActionResult Put(string id, Publisher p)
        {
            if (id != p.PubId)
            {
                return BadRequest();
            }

            context.Entry(p).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //Post
        [HttpPost]
        public ActionResult Post(Publisher p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Publishers.Add(p);
            context.SaveChanges();
            return Ok();
        }

        //Delete
        [HttpDelete("{id}")]
        public ActionResult<Publisher> Delete(string id)
        {
            var publisher = (from m in context.Publishers
                             where m.PubId == id
                             select m).SingleOrDefault();

            if (publisher == null)
            {
                return NotFound();
            }

            context.Publishers.Remove(publisher);
            context.SaveChanges();

            return publisher;
        }
    }
}
