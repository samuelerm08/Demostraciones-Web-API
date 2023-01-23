using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebAPILibros.Context;
using WebAPILibros.Models;

namespace WebAPILibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : Controller
    {
        //INYECCION DE DEPENDENCIA -> INIT
        private readonly DbLibrosContext context;
        public LibroController(DbLibrosContext context)
        {
            this.context = context;
        }

        //SELECT *
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.ToList();
        }

        //SELECT BY AUTHORID
        [HttpGet("libros/{autorId}")]
        public ActionResult<IEnumerable<Libro>> Get(int autorId)
        {
            List<Libro> libros = (from l in context.Libros
                          where l.IdAutor == autorId
                          select l).ToList();
            return libros;
        }

        //SELECT BY ID
        [HttpGet("{id}")]
        public ActionResult<Libro> GetById(int id)
        {
            var libro = (from l in context.Libros
                         where l.Id == id
                         select l).SingleOrDefault();
            return libro;
        }

        //INSERT
        [HttpPost]
        public ActionResult Post(Libro l)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Libros.Add(l);
            context.SaveChanges();
            return Ok();
        }

        //UPDATE
        //PUT api/libreria/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, Libro l)
        {
            if (id != l.Id)
            {
                return BadRequest();
            }

            context.Entry(l).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //DELETE
        //DELETE api/libreria/{id}
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = (from l in context.Libros
                         where l.Id == id
                         select l).SingleOrDefault();

            if (libro == null)
            {
                return NotFound();
            }

            context.Libros.Remove(libro);
            context.SaveChanges();

            return libro;
        }
    }
}
