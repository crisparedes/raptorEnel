using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RaptorENEL_V._1._0.Models
{
    
    [Table("cundinamarca100_candidato", Schema = "public")]
    public class Candidato
    {
        [Key]
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Sucursal")]
        [Range(1,Int32.MaxValue, ErrorMessage ="Debe ingresar un numero mayor a 0")]
        public int sucursal { get; set; }

        [StringLength(10, ErrorMessage = " La solicitud no puede tener mas de 10 caracteres")]
        [Display(Name = "Solicitud")]
        public String solicitud { get; set; }
       
        [StringLength(75, ErrorMessage = " El municipio no puede tener mas de 75 caracteres")]
        [Display(Name = "Municipio")]
        [Required(ErrorMessage = "Debe ingresar un municipio")]
        public String municipio { get; set; }

        [StringLength(200, ErrorMessage = " La dirección no puede tener mas de 200 caracteres")]
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Debe ingresar una dirección")]
        public String direccion { get; set; }

        [StringLength(150, ErrorMessage = " El propietario no puede tener mas de 150 caracteres")]
        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "Debe ingresar un propietario")]
        public String propietario { get; set; }

        [StringLength(25, ErrorMessage = " La zona no puede tener mas de 25 caracteres")]
        [Display(Name = "Zona")]
        [Required(ErrorMessage = "Debe ingresar una zona")]
        public String zona { get; set; }

        [StringLength(25, ErrorMessage = " La subzona no puede tener mas de 25 caracteres")]
        [Display(Name = "Subzona")]
        [Required(ErrorMessage = "Debe ingresar una subzona")]
        public String subzona { get; set; }

        [Display(Name = "Latitud")]
        public double latitud { get; set; }

        [Display(Name = "Longitud")]
        public double longitud { get; set; }

        [StringLength(50, ErrorMessage = " El estado no puede tener mas de 50 caracteres")]
        [Display(Name = "Estado")]       
        public String estado { get; set; }
        
    }
}