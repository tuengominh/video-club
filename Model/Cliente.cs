using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Helper;

namespace VideoClub.Model
{
    public enum ClienteEstado { Activo, Bloqueado }

    public class Cliente
    {
        public int clienteCodigo { get; set; }

        public string nombre { get; set; }

        public string apellidos { get; set; }

        public string direccion { get; set; }

        public string _nif;
        public string nif { get { return this._nif; } set { this._nif = ValidationUtil.NIF(value); } }

        public string _telefono;
        public string telefono { get { return this._telefono; } set { this._telefono = ValidationUtil.Telefono(value); } }

        public string _email;
        public string email { get { return this._email; } set { this._email = ValidationUtil.Email(value); } }

        public DateTime fechaDeAlta { get; set; }

        public ClienteEstado estado { get; set; } = ClienteEstado.Activo;

        public bool VIP { get; set; } = false;
    }
}
