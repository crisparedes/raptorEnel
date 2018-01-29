using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaptorENEL_V._1._0.Models
{
    [Table("incidencias_notificacion", Schema = "public")]
    public class Notificacion
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        [Display(Name = "Activo")]
        public bool activo { get; set; } = true;

        [Display(Name = "Usuario")]
        public int usuario_id { get; set; }
        public virtual User User { get; set; }
    }
}