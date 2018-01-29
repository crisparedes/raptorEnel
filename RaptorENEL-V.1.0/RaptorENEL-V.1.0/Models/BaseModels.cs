using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace RaptorENEL_V._1._0.Models
{
    [Table("basicas_base", Schema = "public")]
    public class Base
    {

        public List<SelectListItem> mapTipo()
        {
            
                List<SelectListItem> ListaTipo = new List<SelectListItem>();
                ListaTipo.Add(new SelectListItem
                {
                    Text = "Anomalía",
                    Value = "A"

                });
                ListaTipo.Add(new SelectListItem
                {
                    Text = "Tipo Reporte",
                    Value = "R"

                });

                return ListaTipo;
            
        }


        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = " Debe ingresar un código")]
        [StringLength(10, ErrorMessage = " La código no pueden tener mas de 10 caracteres")]
        public String codigo { get; set; }

        [Required(ErrorMessage = " Debe ingresar una descripción")]
        [StringLength(50, ErrorMessage = " La descripción no pueden tener mas de 50 caracteres")]
        [Display(Name = "Descripción")]
        public String descripcion { get; set; }

        [Required(ErrorMessage = " Debe ingresar un proceso")]
        [Display(Name = "Proceso")]
        [StringLength(20, ErrorMessage = " El proceso no pueden tener mas de 20 caracteres")]
        public String proceso { get; set; } = "N/A";

        [Display(Name = "Tipo")]
        public String tipo { get; set; }

        [Display(Name = "Activo")]
        public bool activo { get; set; } = true;

        [Display(Name = "Fecha Creación")]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        public ICollection<Hurto> Hurto {get; set; }



    }
}