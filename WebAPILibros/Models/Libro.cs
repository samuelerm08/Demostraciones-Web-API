using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPILibros.Models
{
    [Table("Libro")]
    public class Libro
    {        
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Titulo { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Descripcion { get; set; }
        public int IdAutor { get; set; }

        [ForeignKey("IdAutor")]
        public Autor Autor { get; set; }
    }
}
