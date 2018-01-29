using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaptorENEL_V._1._0.Models
{
    [Table("incidencias_notificacion_municipio", Schema = "public")]
    public class Notificacionmunicipio
    {
        [Key]
        public int id { get; set; }

    }
}