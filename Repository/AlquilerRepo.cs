using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VideoClub.Helper;
using VideoClub.Model;

namespace VideoClub.Repository
{
    public class AlquilerRepo
    {
        private DBHelper dBHelper;

        public AlquilerRepo()
        {
            this.dBHelper = new DBHelper(SchemaUtil.DB_NAME);
        }

        public List<Alquiler> getTodos()
        {
            List<Alquiler> list = getAlquileresConQuery(new SQLBuilder().from(SchemaUtil.ALQUILER_TABLE_NAME).build());
            if (list?.Any() != true)
            {
                throw new Exception("No hay alquileres en la base de datos.");
            }
            return list;
        }

        public Alquiler getAlquilerConID(int id)
        {
            List<Alquiler> list = getAlquileresConQuery(new SQLBuilder().from(SchemaUtil.ALQUILER_TABLE_NAME).where(SchemaUtil.ALQUILER_KEY_ID + "=" + id).build());
            if (list?.Any() != true)
            {
                throw new Exception("No existe esta id.");
            }
            return list[0];
        }

        public List<Alquiler> getAlquileresConProductoID(int id)
        {
            List<Alquiler> list = getAlquileresConQuery(new SQLBuilder().from(SchemaUtil.ALQUILER_TABLE_NAME).where(SchemaUtil.ALQUILER_KEY_PRODUCTO_ID + "=" + id).build());
            if (list?.Any() != true)
            {
                throw new Exception("Este producto no ha sido alquilado o no existe.");
            }
            return list;
        }

        public List<Alquiler> getAlquileresConClienteID(int id)
        {
            List<Alquiler> list = getAlquileresConQuery(new SQLBuilder().from(SchemaUtil.ALQUILER_TABLE_NAME).where(SchemaUtil.ALQUILER_KEY_CLIENTE_ID + "=" + id).build());
            if (list?.Any() != true)
            {
                throw new Exception("Este cliente no ha realizado ningun alquiler o no existe.");
            }
            return list;
        }

        public int anadirAlquiler(Alquiler al)
        {
            if (al == null)
            {
                throw new Exception("El alquiler es nulo.");
            }

            string sql = "INSERT INTO " +  SchemaUtil.ALQUILER_TABLE_NAME + "("
                + SchemaUtil.ALQUILER_KEY_CLIENTE_ID + ","
                + SchemaUtil.ALQUILER_KEY_PRODUCTO_ID + ","
                + SchemaUtil.ALQUILER_KEY_FECHA + ","
                + SchemaUtil.ALQUILER_KEY_TIEMPO + ","
                + SchemaUtil.ALQUILER_KEY_DESCUENTO + ","
                + SchemaUtil.ALQUILER_KEY_PAGO + ","
                + SchemaUtil.ALQUILER_KEY_ESTADO
                + ") " + " VALUES" + "(" 
                + al.clienteCodigo + ","
                + al.productoCodigo + ","
                + "'" + al.fechaDeAlquiler.ToString("yyyy-MM-dd") + "',"
                + al.tiempoDeAlquiler + ","
                + (decimal) al.descuento + ","
                + (decimal) al.totalPago + ","
                + "'" + al.estado.ToString() + "');";

            Console.WriteLine(sql);

            this.dBHelper.ejecutar(sql);

            return getTodos().Last().alquilerCodigo;
        }

        public void actualizarAlquiler(Alquiler al, int id)
        {
            if (al == null)
            {
                throw new Exception("El alquiler es nulo.");
            }

            string sql = "INSERT OR REPLACE INTO " + SchemaUtil.ALQUILER_TABLE_NAME + "(" 
                + SchemaUtil.ALQUILER_KEY_ID + "," 
                + SchemaUtil.ALQUILER_KEY_CLIENTE_ID + ","
                + SchemaUtil.ALQUILER_KEY_PRODUCTO_ID + ","
                + SchemaUtil.ALQUILER_KEY_FECHA + ","
                + SchemaUtil.ALQUILER_KEY_TIEMPO + ","
                + SchemaUtil.ALQUILER_KEY_DESCUENTO + ","
                + SchemaUtil.ALQUILER_KEY_PAGO + ","
                + SchemaUtil.ALQUILER_KEY_ESTADO
                + ") " + "VALUES" + " (" 
                + id + "," 
                + al.clienteCodigo + ","
                + al.productoCodigo + ","
                + "'" + al.fechaDeAlquiler.ToString("yyyy-MM-dd") + "',"
                + al.tiempoDeAlquiler + ","
                + (decimal)al.descuento + ","
                + (decimal)al.totalPago + ","
                + "'" + al.estado.ToString() + "');";

            Console.WriteLine(sql);

            this.dBHelper.ejecutar(sql);
        }

        private List<Alquiler> getAlquileresConQuery(string query)
        {
            List<Alquiler> alquileresList = new List<Alquiler>();

            DataTable dataTable = this.dBHelper.leer(query);
            foreach (DataRow row in dataTable.Rows)
            {
                alquileresList.Add(getAlquilerDeRow(row));
            }

            return alquileresList;
        }

        private Alquiler getAlquilerDeRow(DataRow row)
        {
            Alquiler al = new Alquiler();

            al.alquilerCodigo = ValidationUtil.NumeroInteger(row[SchemaUtil.ALQUILER_KEY_ID].ToString());
            al.clienteCodigo = ValidationUtil.NumeroInteger(row[SchemaUtil.ALQUILER_KEY_CLIENTE_ID].ToString());
            al.productoCodigo = ValidationUtil.NumeroInteger(row[SchemaUtil.ALQUILER_KEY_PRODUCTO_ID].ToString());
            al.fechaDeAlquiler = ValidationUtil.Fecha(row[SchemaUtil.ALQUILER_KEY_FECHA].ToString());
            al.tiempoDeAlquiler = ValidationUtil.NumeroInteger(row[SchemaUtil.ALQUILER_KEY_TIEMPO].ToString());
            al.descuento = ValidationUtil.NumeroDouble(row[SchemaUtil.ALQUILER_KEY_DESCUENTO].ToString());
            al.totalPago = ValidationUtil.NumeroDouble(row[SchemaUtil.ALQUILER_KEY_PAGO].ToString());

            string tempEstado = row[SchemaUtil.ALQUILER_KEY_ESTADO].ToString();
            if (tempEstado == "Alquilado")
            {
                al.estado = AlquilerEstado.Alquilado;
            } else if (tempEstado == "Regresado")
            {
                al.estado = AlquilerEstado.Regresado;
            } else if (tempEstado == "Extraviado")
            {
                al.estado = AlquilerEstado.Extraviado;
            }

            return al;
        }
    }
}
