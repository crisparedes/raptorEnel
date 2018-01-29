using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace RaptorENEL_V._1._0.Models
{
    [Table("hurtos_hurto", Schema = "public")]
    public class Hurto
    {
        [Key]
        [Required(ErrorMessage = " Debe ingresar un código")]
        [Display(Name = "Código pedido")]
        [StringLength(14, ErrorMessage = " El código no pueden tener mas de 14 caracteres")]
        public String codigo { get; set; }

        [Display(Name = "Tipo reporte")]
        public String tipo_reporte_id { get; set; }
        [ForeignKey("tipo_reporte_id")]
        [InverseProperty("Hurto")]
        public virtual Base tipo_reporte { get; set; }


        [Display(Name = "Referencia")]
        public bool nse_referencia { get; set; } = false;

        [Required(ErrorMessage = " Debe ingresar un Nse")]
        [Display(Name = "Nse")]
        [StringLength(10, ErrorMessage = " El código no pueden tener mas de 10 caracteres")]
        public String nse { get; set; }

        [Display(Name = "Medidor")]
        public int medidor { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        [Range(0, 999999999.9, ErrorMessage = "La lectura debe estar entre el rango de 0 y 999999999.9")]
        [Display(Name = "Lectura")]
        [Required(ErrorMessage = "Debe ingresar una lectura")]
        public Decimal lectura { get; set; } = 0;

        [Display(Name = "Anomalía")]
        public String anomalia_id { get; set; }
        [ForeignKey("anomalia_id")]
        public virtual Base anomalia { get; set; }

        [Display(Name = "Imagen")]
        public String imagen { get; set; } = "";

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase PostedFile { get; set; }

        [Display(Name = "Usuario")]
        public int usuario_id { get; set; }
        public virtual User User { get; set; }

        [Required(ErrorMessage = " Debe ingresar un ciclo")]
        [Display(Name = "Ciclo")]
        public int ciclo { get; set; }

        [Required(ErrorMessage = " Debe ingresar un grupo")]
        [Display(Name = "Grupo")]
        public int grupo { get; set; }

        [Display(Name = "Fecha creación")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime? fecha { get; set; } = null;

        [Display(Name = "Observacion")]
        [Required(ErrorMessage = "Debe ingresar las observación")]
        public String observacion { get; set; }

        [Display(Name = "Fecha Envío")]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;

        [Display(Name = "Procesado")]
        public bool procesado { get; set; } = false;

        [Display(Name = "Finalizado")]
        public bool finalizado { get; set; } = false;

        [Display(Name = "Locación")]
        public String geo_nse { get; set; }

        [NotMapped]
        public float latitud { get; set; }

        [NotMapped]
        public float longitud { get; set; }

        [NotMapped]
        public bool limpiar { get; set; } = false;
    }
}