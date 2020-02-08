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
    public class ProductoRepo
    {
        private DBHelper dBHelper;

        public ProductoRepo()
        {
            this.dBHelper = new DBHelper(SchemaUtil.DB_NAME);
        }

        public List<Producto> getTodos()
        {
            List<Producto> list = getProductosConQuery(new SQLBuilder().from(SchemaUtil.PRODUCTO_TABLE_NAME).build());
            if (list?.Any() != true)
            {
                throw new Exception("No hay productos en la base de datos.");
            }
            return list;
        }

        public List<Producto> getPeliculas()
        {
            List<Producto> list = getProductosConQuery(new SQLBuilder().from(SchemaUtil.PRODUCTO_TABLE_NAME).where(SchemaUtil.PRODUCTO_KEY_TIPO, "=", "Pelicula").build());
            if (list?.Any() != true)
            {
                throw new Exception("No hay peliculas en la base de datos.");
            }
            return list;
        }

        public List<Producto> getVideoJuegos()
        {
            List<Producto> list = getProductosConQuery(new SQLBuilder().from(SchemaUtil.PRODUCTO_TABLE_NAME).where(SchemaUtil.PRODUCTO_KEY_TIPO, "=", "VideoJuegos").build());
            if (list?.Any() != true)
            {
                throw new Exception("No hay video juegos en la base de datos.");
            }
            return list;
        }

        public Producto getProductoConID(int id)
        {
            List<Producto> list = getProductosConQuery(new SQLBuilder().from(SchemaUtil.PRODUCTO_TABLE_NAME).where(SchemaUtil.PRODUCTO_KEY_ID + "=" + id).build());
            if (list?.Any() != true)
            {
                throw new Exception("No existe esta id.");
            }
            return list[0];
        }

        public Producto getProductoConTitulo(string titulo)
        {
            List<Producto> list = getProductosConQuery(new SQLBuilder().from(SchemaUtil.PRODUCTO_TABLE_NAME).where(SchemaUtil.PRODUCTO_KEY_TITULO, "=", titulo).build());
            if (list?.Any() != true)
            {
                throw new Exception("No existe este titulo.");
            }
            return list[0];
        }

        public int anadirPelicula(Pelicula p)
        {
            if (p == null)
            {
                throw new Exception("El producto es nulo.");
            }

            string sql = "INSERT INTO " + SchemaUtil.PRODUCTO_TABLE_NAME + "("
                                + SchemaUtil.PRODUCTO_KEY_TIPO + ","
                                + SchemaUtil.PRODUCTO_KEY_TITULO + ","
                                + SchemaUtil.PRODUCTO_KEY_NO_COPIA + ","
                                + SchemaUtil.PRODUCTO_KEY_DISPONIBLE + ","
                                + SchemaUtil.PRODUCTO_KEY_ALQUILADO + ","
                                + SchemaUtil.PRODUCTO_KEY_EXTRAVIDO + ","
                                + SchemaUtil.PRODUCTO_KEY_DURACION + ","
                                + SchemaUtil.PRODUCTO_KEY_ANOPRODUCTION + ","
                                + SchemaUtil.PRODUCTO_KEY_ANOCOMPRA
                                + " )" + "VALUES" + " ("
                                + "'" + p.tipo.ToString() + "',"
                                + "'" + p.titulo + "',"
                                + p._noDeCopias + ","
                                + p._disponible + ","
                                + p._alquilado + ","
                                + p._extraviado + ","
                                + p.duracion + ","
                                + p._anoProduction + ","
                                + p._anoCompra + ");";

            Console.WriteLine(sql);

            this.dBHelper.ejecutar(sql);

            return getTodos().Last().productCodigo;
        }

        public void actualizarPelicula(Pelicula p, int id)
        {
            if (p == null)
            {
                throw new Exception("El producto es nulo.");
            }

            string sql = "INSERT OR REPLACE INTO " + SchemaUtil.PRODUCTO_TABLE_NAME + "("
                                + SchemaUtil.PRODUCTO_KEY_ID + ","
                                + SchemaUtil.PRODUCTO_KEY_TIPO + ","
                                + SchemaUtil.PRODUCTO_KEY_TITULO + ","
                                + SchemaUtil.PRODUCTO_KEY_NO_COPIA + ","
                                + SchemaUtil.PRODUCTO_KEY_DISPONIBLE + ","
                                + SchemaUtil.PRODUCTO_KEY_ALQUILADO + ","
                                + SchemaUtil.PRODUCTO_KEY_EXTRAVIDO + ","
                                + SchemaUtil.PRODUCTO_KEY_DURACION + ","
                                + SchemaUtil.PRODUCTO_KEY_ANOPRODUCTION + ","
                                + SchemaUtil.PRODUCTO_KEY_ANOCOMPRA
                                + " )" + "VALUES" + " (" + id + ","
                                + "'" + p.tipo.ToString() + "',"
                                + "'" + p.titulo + "',"
                                + p._noDeCopias + ","
                                + p._disponible + ","
                                + p._alquilado + ","
                                + p._extraviado + ","
                                + p.duracion + ","
                                + p._anoProduction + ","
                                + p._anoCompra + ");";

            Console.WriteLine(sql);

            this.dBHelper.ejecutar(sql);
        }

        public int anadirVideoJuegos(VideoJuegos p)
        {
            if (p == null)
            {
                throw new Exception("El producto es nulo.");
            }

            string sql = "INSERT INTO " + SchemaUtil.PRODUCTO_TABLE_NAME + "("
                                + SchemaUtil.PRODUCTO_KEY_TIPO + ","
                                + SchemaUtil.PRODUCTO_KEY_TITULO + ","
                                + SchemaUtil.PRODUCTO_KEY_NO_COPIA + ","
                                + SchemaUtil.PRODUCTO_KEY_DISPONIBLE + ","
                                + SchemaUtil.PRODUCTO_KEY_ALQUILADO + ","
                                + SchemaUtil.PRODUCTO_KEY_EXTRAVIDO + ","
                                + SchemaUtil.PRODUCTO_KEY_PLATAFORMA 
                                + " )" + "VALUES" + " ("+ 
                                "'" + p.tipo.ToString() + "',"
                                + "'" + p.titulo + "',"
                                + p._noDeCopias + ","
                                + p._disponible + ","
                                + p._alquilado + ","
                                + p._extraviado + ","
                                + "'" + p.plataforma.ToString() + "');";

            Console.WriteLine(sql);

            this.dBHelper.ejecutar(sql);

            return getTodos().Last().productCodigo;
        }

        public void actualizarVideoJuegos(VideoJuegos p, int id)
        {
            if (p == null)
            {
                throw new Exception("El producto es nulo.");
            }

            string sql = "INSERT OR REPLACE INTO " + SchemaUtil.PRODUCTO_TABLE_NAME + "("
                                + SchemaUtil.PRODUCTO_KEY_ID + ","
                                + SchemaUtil.PRODUCTO_KEY_TIPO + ","
                                + SchemaUtil.PRODUCTO_KEY_TITULO + ","
                                + SchemaUtil.PRODUCTO_KEY_NO_COPIA + ","
                                + SchemaUtil.PRODUCTO_KEY_DISPONIBLE + ","
                                + SchemaUtil.PRODUCTO_KEY_ALQUILADO + ","
                                + SchemaUtil.PRODUCTO_KEY_EXTRAVIDO + ","
                                + SchemaUtil.PRODUCTO_KEY_PLATAFORMA
                                + " )" + "VALUES" + " (" 
                                + id + ","
                                + "'" + p.tipo.ToString() + "',"
                                + "'" + p.titulo + "',"
                                + p._noDeCopias + ","
                                + p._disponible + ","
                                + p._alquilado + ","
                                + p._extraviado + ","
                                + "'" + p.plataforma.ToString() + "');";

            Console.WriteLine(sql);

            this.dBHelper.ejecutar(sql);
        }

        private List<Producto> getProductosConQuery(string query)
        {
            List<Producto> list = new List<Producto>();

            DataTable dataTable = this.dBHelper.leer(query);
            foreach (DataRow row in dataTable.Rows)
            {
                if (row[SchemaUtil.PRODUCTO_KEY_TIPO].ToString() == "Pelicula")
                {
                    list.Add(getPeliculaDeRow(row));
                }
                else if (row[SchemaUtil.PRODUCTO_KEY_TIPO].ToString() == "VideoJuegos")
                {
                    list.Add(getVideoJuegosDeRow(row));
                }
            }

            return list;
        }
        private Pelicula getPeliculaDeRow(DataRow row)
        {
            Pelicula p = new Pelicula();

            p.productCodigo = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_ID].ToString());
            p.tipo = Tipo.Pelicula;
            p.titulo = row[SchemaUtil.PRODUCTO_KEY_TITULO].ToString();
            p.noDeCopias = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_NO_COPIA].ToString());
            p.disponible = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_DISPONIBLE].ToString());
            p.alquilado = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_ALQUILADO].ToString());
            p.extraviado = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_EXTRAVIDO].ToString());
            p.duracion = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_DURACION].ToString());
            p.anoProduccion = ValidationUtil.Ano(ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_ANOPRODUCTION].ToString()));
            p.anoCompra = ValidationUtil.Ano(ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_ANOCOMPRA].ToString()));

            return p;
        }

        private VideoJuegos getVideoJuegosDeRow(DataRow row)
        {
            VideoJuegos p = new VideoJuegos();

            p.productCodigo = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_ID].ToString());
            p.tipo = Tipo.VideoJuegos;
            p.titulo = row[SchemaUtil.PRODUCTO_KEY_TITULO].ToString();
            p.noDeCopias = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_NO_COPIA].ToString());
            p.disponible = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_DISPONIBLE].ToString());
            p.alquilado = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_ALQUILADO].ToString());
            p.extraviado = ValidationUtil.NumeroInteger(row[SchemaUtil.PRODUCTO_KEY_EXTRAVIDO].ToString());

            string tempPlataforma = row[SchemaUtil.PRODUCTO_KEY_PLATAFORMA].ToString();
            if (tempPlataforma == "PC")
            {
                p.plataforma = Plataforma.PC;
            } 
            else if (tempPlataforma == "PS2")
            {
                p.plataforma = Plataforma.PS2;
            }
            else if (tempPlataforma == "PS3")
            {
                p.plataforma = Plataforma.PS3;
            }
            else if (tempPlataforma == "Xbox")
            {
                p.plataforma = Plataforma.Xbox;
            }
            else if (tempPlataforma == "Wii")
            {
                p.plataforma = Plataforma.Wii;
            }

            return p;
        }
    }
}
