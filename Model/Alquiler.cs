using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Model
{
    public enum AlquilerEstado { Alquilado, Regresado, Extraviado }

    public class Alquiler
    {
        public int alquilerCodigo { get; set; }

        public int clienteCodigo { get; set; }

        public int productoCodigo { get; set; }

        public DateTime fechaDeAlquiler { get; set; }

        public int tiempoDeAlquiler { get; set; } = 1;

        public double descuento { get; set; } = 0.0;

        public double totalPago { get; set; } = 0.0;

        public AlquilerEstado estado { get; set; } = AlquilerEstado.Alquilado;
    }
}
