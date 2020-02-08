using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Model
{
    public enum Tipo { Pelicula, VideoJuegos }

    public class Producto
    {
        public int productCodigo { get; set; }

        public string titulo { get; set; }

        public int _noDeCopias;
        public int noDeCopias
        {
            get { return this._noDeCopias; }
            set { this._noDeCopias = value; }
        }

        public void anadirCopias(int cantidad)
        {
            this._noDeCopias += cantidad;
            this._disponible = _noDeCopias - _extraviado - _alquilado;
        }

        public int _disponible;
        public int disponible
        {
            get { return this._disponible; }
            set { this._disponible = value; }
        }

        public int _alquilado = 0;
        public int alquilado
        {
            get { return this._alquilado; }
            set { this._alquilado = value; }
        }

        public int _extraviado = 0;
        public int extraviado
        {
            get { return this._extraviado; }
            set { this._extraviado = value; }
        }
        public void anadirAlquiler()
        {
            _alquilado++;
            _disponible = _noDeCopias - _extraviado - _alquilado;
        }
        public void regresarAlquiler()
        {
            _alquilado--;
            _disponible = _noDeCopias - _extraviado - _alquilado;
        }

        public void informarExtraviado()
        {
            _extraviado++;
            _alquilado--;
            _disponible = _noDeCopias - _extraviado - _alquilado;
        }

        public void regresarExtraviado()
        {
            _extraviado--;
            _disponible = _noDeCopias - _extraviado - _alquilado;
        }
    }
}
