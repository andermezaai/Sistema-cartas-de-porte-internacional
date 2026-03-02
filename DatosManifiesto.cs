using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Documentos
{
    public class DatosManifiesto
    {
        public string CodigoManifiesto { get; set; }
        public decimal NumeroCodigo { get; set; }
        public string CertificadoIdoneidad { get; set; }
        public string PermisoPrestacion { get; set; }
        public string MarcaVehiculo { get; set; }
        public string AnioFabricacionVehiculo { get; set; }
        public string PlacaVehiculo { get; set; }
        public string ChasisVehiculo { get; set; }
        public string CertificadosHabilitacion { get; set; }
        public string MarcaUnidadCarga { get; set; }
        public string AnioFabricacionUnidadCarga { get; set; }
        public string PlacaUnidadCarga { get; set; }
        public string ChasisUnidadCarga { get; set; }
        public string CertificadoHabilitacion2 { get; set; }
        public string NombreConductorPrincipal { get; set; }
        public string DocIdentidadConductorPrincipal { get; set; }
        public string NacionalidadConductorPrincipal { get; set; }
        public string LicenciaConductorPrincipal { get; set; }
        public string LicenciaTripulanteTerrestreConductorPrincipal { get; set; }
        public string NombreConductorAuxiliar { get; set; }
        public string DocIdentidadConductorAuxiliar { get; set; }
        public string NacionalidadConductorAuxiliar { get; set; }
        public string LicenciaConductorAuxiliar { get; set; }
        public string LibretaTripulanteTerrestreConductorAuxiliar { get; set; }
        public string LugarCarga { get; set; }
        public string LugarDescarga { get; set; }
        public Boolean APeligrosa { get; set; }
        public Boolean BSustanciaQuimica { get; set; }
        public Boolean CPerecible { get; set; }
        public Boolean DOtra { get; set; }
        public String DOtraTexto { get; set; }
        public string NumeroIdentificacionContenedores { get; set; }
        public string NumeroPrecintosAduaneros { get; set; }
        public string NroCartaPorte { get; set; }
        public string DescripcionMercancias { get; set; }
        public string CantidadBultos { get; set; }
        public string ClaseMarcaBultos { get; set; }
        public string PesoBruto { get; set; }
        public string PesoNeto { get; set; }
        public string TreintayTres { get; set; }
        public string PrecioMercancias { get; set; }
        public string AduanaCruceFrontera { get; set; }
        public string AduanaDestino { get; set; }
        public string FechaEmision { get; set; }        
    }
}
