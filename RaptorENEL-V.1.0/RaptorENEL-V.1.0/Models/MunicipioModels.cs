using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaptorENEL_V._1._0.Models
{
    [Table("basicas_municipio", Schema = "public")]
    public class Municipio
    {
        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Required(ErrorMessage = " Debe ingresar una descripción")]
        [StringLength(75, ErrorMessage = " La descripción no pueden tener mas de 150 caracteres")]
        [Display(Name = "Descripción")]
        public String descripcion { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = " Debe ingresar una zona")]
        [StringLength(25, ErrorMessage = " La zona no pueden tener mas de 25 caracteres")]
        [Display(Name = "Zona UOC")]
        public String zona_uoc { get; set; }

        [Display(Name = "Activo")]
        public bool activo { get; set; } = true;


    }
}