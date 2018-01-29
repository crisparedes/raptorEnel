using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaptorENEL_V._1._0.Models
{

    [Table("cundinamarca100_ubala", Schema = "public")]
    public class Ubala
    {

        public List<SelectListItem> getStateAvailable()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem
            { Text = "Sin servicio", Value = "S" });
            Lista.Add(new SelectListItem
            { Text = "Conectado", Value = "C" });
            Lista.Add(new SelectListItem
            { Text = "Sin servicio - red existente codensa", Value = "R" });
            Lista.Add(new SelectListItem
            { Text = "Sin servicio - red construida tercero", Value = "T" });
            Lista.Add(new SelectListItem
            { Text = "Con servicio (medidor)", Value = "M" });
            Lista.Add(new SelectListItem
            { Text = "Servicio directo - red codensa", Value = "J" });
            Lista.Add(new SelectListItem
            { Text = "Servicio directo - red terceros", Value = "K" });
            return Lista;
        }

        public List<SelectListItem> getNetworkAvailable()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem
            { Text = "Red trenzada", Value = "T" });
            Lista.Add(new SelectListItem
            { Text = "Red abierta", Value = "A" });
            return Lista;
        }

        public List<SelectListItem> getDwellingAvailable()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem
            { Text = "Construida", Value = "CT" });
            Lista.Add(new SelectListItem
            { Text = "En construcción", Value = "EC" });
            Lista.Add(new SelectListItem
            { Text = "Lote baldío", Value = "LB" });
            return Lista;
        }

        public List<SelectListItem> getServiceAvailable()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem
            { Text = "Residencial", Value = "R" });
            Lista.Add(new SelectListItem
            { Text = "Comercial", Value = "C" });
            Lista.Add(new SelectListItem
            { Text = "Industrial", Value = "I" });
            return Lista;
        }

        public List<SelectListItem> getChargeType()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem
            { Text = "Monofásico", Value = "M" });
            Lista.Add(new SelectListItem
            { Text = "Bifásico", Value = "B" });
            Lista.Add(new SelectListItem
            { Text = "Trifásico", Value = "T" });
            return Lista;
        }

        public List<SelectListItem> getCoverageType()
        {
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem
            { Text = "Ninguna", Value = "N" });
            Lista.Add(new SelectListItem
            { Text = "Claro", Value = "C" });
            Lista.Add(new SelectListItem
            { Text = "Movistar", Value = "M" });
            Lista.Add(new SelectListItem
            { Text = "Tigo", Value = "T" });
            return Lista;
        }

        [Key]
        [Required(ErrorMessage = " Debe ingresar un código")]
        [Display(Name = "Código")]
        [StringLength(10, ErrorMessage = " El código no pueden tener mas de 10 caracteres")]
        public String factibilidad { get; set; }

        [Display(Name = "Estado del predio")]
        public String estado_predio { get; set; }

        [StringLength(50, ErrorMessage = " El municipio no pueden tener mas de 50 caracteres")]
        [Display(Name = "Municipio")]
        public String municipio { get; set; }

        [StringLength(150, ErrorMessage = " El nombre del propietario no pueden tener mas de 150 caracteres")]
        [Display(Name = "Nombre de propietario")]
        [Required(ErrorMessage = "Debe ingresar un nombre de propietario")]
        public String nombre_propietario { get; set; }

        [StringLength(50, ErrorMessage = " El documento no pueden tener mas de 50 caracteres")]
        [Display(Name = "Documento")]
        [Required(ErrorMessage = "Debe ingresar un documento")]
        public String documento { get; set; }

        [StringLength(50, ErrorMessage = " El número contacto no pueden tener mas de 50 caracteres")]
        [Display(Name = "Número contacto")]
        [Required(ErrorMessage = "Debe ingresar un número de contacto")]
        public String numero_contacto { get; set; } = "";

        [Display(Name = "Latitud predio")]
        [Required(ErrorMessage = "Debe ingresar la latitud del predio")]
        public double latitud_pre { get; set; }

        [Display(Name = "Longitud predio")]
        [Required(ErrorMessage = "Debe ingresar la longitud del predio")]
        public double longitud_pre { get; set; }

        [StringLength(50, ErrorMessage = " El código conexión no pueden tener mas de 50 caracteres")]
        [Display(Name = "Código conexión")]
        [Required(ErrorMessage = "Debe ingresar un código de conexión")]
        public String codigo_conexion { get; set; }

        [Display(Name = "Latitud conexión")]
        [Required(ErrorMessage = "Debe ingresar la latitud de la conexión")]
        public double latitud_con { get; set; }

        [Display(Name = "Longitud conexión")]
        [Required(ErrorMessage = "Debe ingresar la longitud de la conexión")]
        public double longitud_con { get; set; }

        [Display(Name = "Tipo de red")]
        public String tipo_red { get; set; }

        [StringLength(25, ErrorMessage = " El mantenimiento red no pueden tener mas de 25 caracteres")]
        [Display(Name = "Mantenimiento red")]
        public String mantenimiento_red { get; set; }

        [Display(Name = "Mantenimiento")]
        public String mantenimiento { get; set; }

        [StringLength(25, ErrorMessage = " El Centro de distribuición no pueden tener mas de 25 caracteres")]
        [Display(Name = "Centro de distribuición")]
        [Required(ErrorMessage = "Debe ingresar un centro de distribuición")]
        public String cdt { get; set; }

        [Display(Name = "Estado de vivienda")]
        public String estado_vivienda { get; set; }

        [Display(Name = "Obs adecuacion i")]
        [Required(ErrorMessage = "Debe ingresar el dato")]
        public String obs_adecuacion_i { get; set; }

        [Display(Name = "Obs adecuacion e")]
        [Required(ErrorMessage = "Debe ingresar el dato")]
        public String obs_adecuacion_e { get; set; }

        [Display(Name = "Tipo de servicio")]
        public String tipo_servicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        [Range(0, 999999.9, ErrorMessage = "La cantidad debe estar entre el rango de 0 y 999999.9")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Debe ingresar una cantidad")]
        public Decimal carga { get; set; } = 0;

        [Display(Name = "Tipo carga")]
        public String tipo_carga { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "El calibre debe ser mayor a 0")]
        [Display(Name = "Calibre")]
        [Required(ErrorMessage = "Debe ingresar un calibre")]
        public int calibre { get; set; }

        [Display(Name = "Documentación")]
        [Required(ErrorMessage = "Debe ingresar la documentación")]
        public String documentacion { get; set; }

        [Display(Name = "Observaciones")]
        [Required(ErrorMessage = "Debe ingresar las observaciones")]
        public String observaciones { get; set; }

        [Display(Name = "Usuario")]
        public int usuario_id { get; set; }
        public virtual User User { get; set; }


        [StringLength(4, ErrorMessage = " El Centro de distribuición no pueden tener mas de 4 caracteres")]
        [Display(Name = "Servicio directo")]
        [Required(ErrorMessage = "Debe ingresar un servicio directo")]
        public String servicio_directo { get; set; } = "N/A";


        [Display(Name = "Fecha creación")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime? fecha { get; set; } = null;



        [Display(Name = "Cobertura")]
        public String cobertura { get; set; }

        [Display(Name = "Imagen")]
        public String imagen { get; set; } = "";

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase PostedFile { get; set; }

        [NotMapped]
        public bool limpiar { get; set; } = false;

        [Display(Name = "Distancia")]
        [Required(ErrorMessage = "Debe seleccionar una distancia")]
        public double distancia { get; set; }        

        [Display(Name = "Fecha Envío")]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;

    }
}