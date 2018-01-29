using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaptorENEL_V._1._0.Models
{
    [Table("cundinamarca100_imagencandidato", Schema = "public")]
    public class ImagenCandidato
    {

        [Key]
        public int id { get; set; }

        [Display(Name = "Solicitud")]
        [Required(ErrorMessage = " Debe ingresar una solicitud")]
        [StringLength(10, ErrorMessage = " La solicitud no puede tener mas de 10 caracteres")]
        public String solicitud { get; set; }

        [Display(Name = "Observaciones")]
        [Required(ErrorMessage = " Debe ingresar el campo")]
        public String observaciones { get; set; }

        [Display(Name = "Imagen")]
        public String imagen { get; set; } = "";

        [Display(Name = "Fecha Creación")]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase PostedFile { get; set; }

        [NotMapped]
        public bool limpiar { get; set; } = false;
    }
}