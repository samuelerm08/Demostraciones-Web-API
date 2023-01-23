using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebAPILibros.Context;
using WebAPILibros.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPILibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        //INYECCION DE DEPENDENCIA -> INIT
        private readonly DbLibrosContext context;

        public AutorController(DbLibrosContext context)
        {
            this.context = context;
        }

        //SELECT
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autores.ToList();
        }

        //SELECT BY AGE
        [HttpGet("listado/{edad}")]
        public ActionResult<IEnumerable<Autor>> Get(int edad)
        {
            List<Autor> autores = (from a in context.Autores
                                   where edad == a.Edad
                                   select a).ToList();

            return autores;
        }

        //SELECT BY ID
        [HttpGet("{id}")]
        public ActionResult<Autor> GetById(int id)
        {
            var autor = (from a in context.Autores
                         where a.IdAutor == id
                         select a).SingleOrDefault();
            return autor;
        }



        //INSERT
        [HttpPost]
        public ActionResult Post(Autor a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Autores.Add(a);
            context.SaveChanges();
            return Ok();
        }

        //UPDATE
        //PUT api/autor/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, Autor a)
        {
            if (id != a.IdAutor)
            {
                return BadRequest();
            }

            context.Entry(a).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //DELETE
        //DELETE api/autor/{id}
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = (from a in context.Autores
                         where a.IdAutor == id
                         select a).SingleOrDefault();

            if (autor == null)
            {
                return NotFound();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();
            
            return autor;
        }
    }
}
