using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Helper;

namespace VideoClub.Model
{
    public class Pelicula : Producto
    {
        public Tipo tipo { get; set; } = Tipo.Pelicula;

        public int duracion { get; set; }

        public int _anoProduction;
        public int anoProduccion { get { return this._anoProduction; } set { this._anoProduction = ValidationUtil.Ano(value); } }

        public int _anoCompra;
        public int anoCompra { get { return this._anoCompra; } set { this._anoCompra = ValidationUtil.Ano(value); } }
    }
}
