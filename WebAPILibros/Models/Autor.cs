﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPILibros.Models
{
    [Table("Autor")]
    public class Autor
    {
        [Key]
        public int IdAutor { get; set; }        
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Nombre { get; set; }       
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Apellido { get; set; }
        [Range(18, 110, ErrorMessage = "Solo se permiten edades entre 18 y 110 años")]
        public int Edad { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
