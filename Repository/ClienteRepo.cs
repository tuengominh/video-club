using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Helper;
using VideoClub.Model;

namespace VideoClub.Repository
{
    public class ClienteRepo
    {
        private DBHelper dBHelper;

        public ClienteRepo()
        {
            this.dBHelper = new DBHelper(SchemaUtil.DB_NAME);
        }

        public List<Cliente> getTodos()
        {
            List<Cliente> list = getClientesConQuery(new SQLBuilder().from(SchemaUtil.CLIENTE_TABLE_NAME).build());
            if (list?.Any() != true)
            {
                throw new Exception("No hay clientes en la base de datos.");
            }
            return list;
        }

        public Cliente getClienteConID(int id)
        {
            List<Cliente> list = getClientesConQuery(new SQLBuilder().from(SchemaUtil.CLIENTE_TABLE_NAME).where(SchemaUtil.CLIENTE_KEY_ID + "=" + id).build());
            if (list?.Any() != true)
            {
                throw new Exception("No existe esta id.");
            }
            return list[0];
        }

        public Cliente getClienteConNIF(string nif)
        {
            List<Cliente> list = getClientesConQuery(new SQLBuilder().from(SchemaUtil.CLIENTE_TABLE_NAME).where(SchemaUtil.CLIENTE_KEY_NIF, "=", nif).build());
            if (list?.Any() != true)
            {
                throw new Exception("No existe cliente con este NIF.");
            }
            return list[0];
        }

        public List<Cliente> getClientesConEstado(ClienteEstado estado)
        {
            List<Cliente> list = getClientesConQuery(new SQLBuilder().from(SchemaUtil.CLIENTE_TABLE_NAME).where(SchemaUtil.CLIENTE_KEY_ESTADO, "=", estado.ToString()).build());
            if (list?.Any() != true)
            {
                throw new Exception("No hay clientes con estado " + estado.ToString() + " en la base de datos.");
            }
            return list;
        }

        public int anadirCliente(Cliente c)
        {
            if (c == null)
            {
                throw new Exception("El cliente es nulo.");
            }

            string sql = "INSERT INTO " + SchemaUtil.CLIENTE_TABLE_NAME + "(" 
                    + SchemaUtil.CLIENTE_KEY_NOMBRE + ","
                    + SchemaUtil.CLIENTE_KEY_APELLIDOS + ","
                    + SchemaUtil.CLIENTE_KEY_DIRECCION + ","
                    + SchemaUtil.CLIENTE_KEY_NIF + ","
                    + SchemaUtil.CLIENTE_KEY_TELEFONO + ","
                    + SchemaUtil.CLIENTE_KEY_EMAIL + ","
                    + SchemaUtil.CLIENTE_KEY_FECHA + ","
                    + SchemaUtil.CLIENTE_KEY_ESTADO + ","
                    + SchemaUtil.CLIENTE_KEY_VIP
                    + " )" + "VALUES" + " (" 
                    + "'" + c.nombre + "',"
                    + "'" + c.apellidos + "',"
                    + "'" + c.direccion + "',"
                    + "'" + c._nif + "',"
                    + "'" + c.telefono + "',"
                    + "'" + c.email + "',"
                    + "'" + c.fechaDeAlta.ToString("yyyy-MM-dd") + "',"
                    + "'" + c.estado.ToString() + "',";
            if (c.VIP == true)
            {
                sql += 1 + ");";
            }
            else
            {
                sql += 0 + ");";
            }

            Console.WriteLine(sql);

            this.dBHelper.ejecutar(sql);

            return getTodos().Last().clienteCodigo;
        }

        public void actualizarCliente(Cliente c, int id)
        {
            if (c == null)
            {
                throw new Exception("El cliente es nulo.");
            }

            string sql = "INSERT OR REPLACE INTO " + SchemaUtil.CLIENTE_TABLE_NAME + "(" 
                + SchemaUtil.CLIENTE_KEY_ID + ","
                + SchemaUtil.CLIENTE_KEY_NOMBRE + ","
                + SchemaUtil.CLIENTE_KEY_APELLIDOS + ","
                + SchemaUtil.CLIENTE_KEY_DIRECCION + ","
                + SchemaUtil.CLIENTE_KEY_NIF + ","
                + SchemaUtil.CLIENTE_KEY_TELEFONO + ","
                + SchemaUtil.CLIENTE_KEY_EMAIL + ","
                + SchemaUtil.CLIENTE_KEY_FECHA + ","
                + SchemaUtil.CLIENTE_KEY_ESTADO + ","
                + SchemaUtil.CLIENTE_KEY_VIP
                + " )" + "VALUES" + " (" 
                + id + ","
                + "'" + c.nombre + "',"
                + "'" + c.apellidos + "',"
                + "'" + c.direccion + "',"
                + "'" + c._nif + "',"
                + "'" + c.telefono + "',"
                + "'" + c.email + "',"
                + "'" + c.fechaDeAlta.ToString("yyyy-MM-dd") + "',"
                + "'" + c.estado.ToString() + "',";
            if (c.VIP == true)
            {
                sql += 1 + ");";
            }
            else
            {
                sql += 0 + ");";
            }

            Console.WriteLine(sql);

            this.dBHelper.ejecutar(sql);
        }

        private List<Cliente> getClientesConQuery(string query)
        {
            List<Cliente> list = new List<Cliente>();

            DataTable dataTable = this.dBHelper.leer(query);
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(getClienteDeRow(row));
            }

            return list;
        }

        private Cliente getClienteDeRow(DataRow row)
        {
            Cliente c = new Cliente();

            c.clienteCodigo = ValidationUtil.NumeroInteger(row[SchemaUtil.CLIENTE_KEY_ID].ToString());
            c.nombre = row[SchemaUtil.CLIENTE_KEY_NOMBRE].ToString();
            c.apellidos = row[SchemaUtil.CLIENTE_KEY_APELLIDOS].ToString();
            c.direccion = row[SchemaUtil.CLIENTE_KEY_DIRECCION].ToString();
            c.nif = ValidationUtil.NIF(row[SchemaUtil.CLIENTE_KEY_NIF].ToString());
            c.telefono = ValidationUtil.Telefono(row[SchemaUtil.CLIENTE_KEY_TELEFONO].ToString());
            c.email = ValidationUtil.Email(row[SchemaUtil.CLIENTE_KEY_EMAIL].ToString());
            c.fechaDeAlta = ValidationUtil.Fecha(row[SchemaUtil.CLIENTE_KEY_FECHA].ToString());

            string tempEstado = row[SchemaUtil.CLIENTE_KEY_ESTADO].ToString();
            if (tempEstado == "Activo")
            {
                c.estado = ClienteEstado.Activo;
            }
            else if (tempEstado == "Bloqueado")
            {
                c.estado = ClienteEstado.Bloqueado;
            }
            
            if (ValidationUtil.NumeroInteger(row[SchemaUtil.CLIENTE_KEY_VIP].ToString()) == 1)
            {
                c.VIP = true;
            }
            else
            {
                c.VIP = false;
            }

            return c;
        }
    }
}
