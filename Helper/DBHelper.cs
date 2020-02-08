using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Helper
{
    class DBHelper
    {
        private string path;

        public DBHelper(string path)
        {
            this.path = path;
        }

        public void ejecutar(string statement)
        {
            SQLiteConnection conn = crearConnection();
            SQLiteCommand sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = statement;
            sqlite_cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable leer(string query)
        {
            SQLiteConnection conn = crearConnection();
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public SQLiteConnection crearConnection()
        {
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + this.path);
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return sqlite_conn;
        }

        public void crearSampleDatabase()
        {
            if (!File.Exists(this.path))
            {
                SQLiteConnection.CreateFile(this.path);
                ejecutar(SchemaUtil.PRODUCTO_CREATE_STATEMENT);
                ejecutar(SchemaUtil.CLIENTE_CREATE_STATEMENT);
                ejecutar(SchemaUtil.ALQUILER_CREATE_STATEMENT);
                ejecutar(SchemaUtil.PELICULA_INSERT_STATEMENT);
                ejecutar(SchemaUtil.VIDEOJUEGOS_INSERT_STATEMENT);
                ejecutar(SchemaUtil.CLIENTE_INSERT_STATEMENT);
                ejecutar(SchemaUtil.ALQUILER_INSERT_STATEMENT);

                for(int i = 0; i < 59; i++)
                {
                    ejecutar(SchemaUtil.VIP_60_STATEMENT);
                }

                for (int i = 0; i < 10; i++)
                {
                    ejecutar(SchemaUtil.VIP_10_STATEMENT);
                }
            }
        }
    }
}

