using System.Threading;
using Microsoft.EntityFrameworkCore;
using WebAPILibros.Models;

namespace WebAPILibros.Context
{
    public class DbLibrosContext : DbContext
    {
        //Constructor para EFCore
        public DbLibrosContext(DbContextOptions<DbLibrosContext> options) : base(options){}

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
    }
}
