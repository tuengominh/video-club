using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Helper;
using VideoClub.Model;
using VideoClub.Repository;

namespace VideoClub.Service
{
    public class ClienteService
    {
        public const int VIP_DIA_COMO_SOCIO = 365;
        public const int VIP_ALQUILERES = 60;

        private ClienteRepo clienteRepo;
        private AlquilerRepo alquilerRepo;

        public ClienteService()
        {
            this.clienteRepo = new ClienteRepo();
            this.alquilerRepo = new AlquilerRepo();
        }

        public void checkVIP()
        {
            foreach (Cliente c in mostrarTodos())
            {
                List<Alquiler> alquileres = alquilerRepo.getAlquileresConClienteID(c.clienteCodigo);
                TimeSpan dateSpan = DateTime.Now - c.fechaDeAlta;
                if (dateSpan.Days >= VIP_DIA_COMO_SOCIO && alquileres.Count >= VIP_ALQUILERES)
                {
                    c.VIP = true;
                }
                actualizarCliente(c, c.clienteCodigo);
            }
        }

        public List<Cliente> mostrarTodos()
        {
            return clienteRepo.getTodos();
        }

        public List<Cliente> mostrarInactivos()
        {
            return getClientesConEstado(ClienteEstado.Bloqueado);
        }

        public int anadirCliente(Cliente c)
        {
            return clienteRepo.anadirCliente(c);
        }

        public void actualizarCliente(Cliente c, int id)
        {
            clienteRepo.actualizarCliente(c, id);
        }

        public Cliente getClienteConID(int id)
        {
            return clienteRepo.getClienteConID(id);
        }

        public Cliente getClienteConNIF(string nif)
        {
            return clienteRepo.getClienteConNIF(nif);
        }

        public List<Cliente> getClientesConEstado(ClienteEstado estado)
        {
            return clienteRepo.getClientesConEstado(estado);
        }
    }
}
