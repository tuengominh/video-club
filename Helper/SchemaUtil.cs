using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Helper
{
    class SchemaUtil
    {
        public static string DB_NAME = AppDomain.CurrentDomain.BaseDirectory + @"\VideoClubDataBase.db;version=3";

        //Tabla: Alquiler
        public static string ALQUILER_TABLE_NAME = "alquileres";
        public static string ALQUILER_KEY_ID = "alquiler_id";
        public static string ALQUILER_KEY_CLIENTE_ID = "cliente_id";
        public static string ALQUILER_KEY_PRODUCTO_ID = "producto_id";
        public static string ALQUILER_KEY_FECHA = "fecha_de_alquiler";
        public static string ALQUILER_KEY_TIEMPO = "tiempo_de_alquiler";
        public static string ALQUILER_KEY_DESCUENTO = "descuento";
        public static string ALQUILER_KEY_PAGO = "total_pago";
        public static string ALQUILER_KEY_ESTADO = "estado";

        //Tabla: Cliente
        public static string CLIENTE_TABLE_NAME = "clientes";
        public static string CLIENTE_KEY_ID = "cliente_id";
        public static string CLIENTE_KEY_NOMBRE = "nombre";
        public static string CLIENTE_KEY_APELLIDOS = "apellidos";
        public static string CLIENTE_KEY_DIRECCION = "direccion";
        public static string CLIENTE_KEY_NIF = "nif";
        public static string CLIENTE_KEY_TELEFONO = "telefono";
        public static string CLIENTE_KEY_EMAIL = "email";
        public static string CLIENTE_KEY_FECHA = "fecha_de_alta";
        public static string CLIENTE_KEY_ESTADO = "estado";
        public static string CLIENTE_KEY_VIP = "vip";

        //Tabla: Producto
        public static string PRODUCTO_TABLE_NAME = "productos";
        public static string PRODUCTO_KEY_ID = "producto_id";
        public static string PRODUCTO_KEY_TIPO = "tipo";
        public static string PRODUCTO_KEY_TITULO = "titulo";
        public static string PRODUCTO_KEY_NO_COPIA = "no_de_copias";
        public static string PRODUCTO_KEY_DISPONIBLE = "no_de_disponible";
        public static string PRODUCTO_KEY_ALQUILADO = "no_de_alquilado";
        public static string PRODUCTO_KEY_EXTRAVIDO = "no_de_extraviado";
        public static string PRODUCTO_KEY_PLATAFORMA = "plataforma";
        public static string PRODUCTO_KEY_DURACION = "duracion";
        public static string PRODUCTO_KEY_ANOPRODUCTION = "ano_production";
        public static string PRODUCTO_KEY_ANOCOMPRA = "ano_compra";

        //CREATE
        public static string PRODUCTO_CREATE_STATEMENT = "CREATE TABLE IF NOT EXISTS " + PRODUCTO_TABLE_NAME + "(" +
                PRODUCTO_KEY_ID + " INTEGER PRIMARY KEY," +
                PRODUCTO_KEY_TIPO + " TEXT NOT NULL," +
                PRODUCTO_KEY_TITULO + " TEXT NOT NULL," +
                PRODUCTO_KEY_NO_COPIA + " INTEGER NOT NULL," +
                PRODUCTO_KEY_DISPONIBLE + " INTEGER NOT NULL," +
                PRODUCTO_KEY_ALQUILADO + " INTEGER NOT NULL," +
                PRODUCTO_KEY_EXTRAVIDO + " INTEGER NOT NULL," +
                PRODUCTO_KEY_PLATAFORMA + " TEXT," +
                PRODUCTO_KEY_DURACION + " INTEGER," +
                PRODUCTO_KEY_ANOPRODUCTION + " INTEGER," +
                PRODUCTO_KEY_ANOCOMPRA + " INTEGER);";

        public static string CLIENTE_CREATE_STATEMENT = "CREATE TABLE IF NOT EXISTS " + CLIENTE_TABLE_NAME + "(" +
                CLIENTE_KEY_ID + " INTEGER PRIMARY KEY," +
                CLIENTE_KEY_NOMBRE + " TEXT NOT NULL," +
                CLIENTE_KEY_APELLIDOS + " TEXT NOT NULL," +
                CLIENTE_KEY_DIRECCION + " TEXT NOT NULL," +
                CLIENTE_KEY_NIF + " TEXT," +
                CLIENTE_KEY_TELEFONO + " TEXT," +
                CLIENTE_KEY_EMAIL + " TEXT NOT NULL," +
                CLIENTE_KEY_FECHA + " DATE NOT NULL," +
                CLIENTE_KEY_ESTADO + " TEXT NOT NULL," +
                CLIENTE_KEY_VIP + " INTEGER NOT NULL);";

        public static string ALQUILER_CREATE_STATEMENT = "CREATE TABLE IF NOT EXISTS " + ALQUILER_TABLE_NAME + "(" +
                ALQUILER_KEY_ID + " INTEGER PRIMARY KEY," +
                ALQUILER_KEY_CLIENTE_ID + " INTEGER NOT NULL," +
                ALQUILER_KEY_PRODUCTO_ID + " INTEGER NOT NULL," +
                ALQUILER_KEY_FECHA + " DATE NOT NULL," +
                ALQUILER_KEY_TIEMPO + " INTEGER NOT NULL," +
                ALQUILER_KEY_DESCUENTO + " REAL NOT NULL," +
                ALQUILER_KEY_PAGO + " REAL NOT NULL," +
                ALQUILER_KEY_ESTADO + " TEXT NOT NULL" +
                //"," +
                //"FOREIGN KEY(" + ALQUILER_KEY_CLIENTE_ID + ")" +
                //"REFERENCES " + CLIENTE_TABLE_NAME + "(" + CLIENTE_KEY_ID + ")," +
                //"FOREIGN KEY(" + ALQUILER_KEY_PRODUCTO_ID + ")" +
                //"REFERENCES " + PRODUCTO_TABLE_NAME + "(" + PRODUCTO_KEY_ID + ")
                ");";

        //INSERT
        public static string PELICULA_INSERT_STATEMENT = "INSERT INTO " + PRODUCTO_TABLE_NAME + "("
                + PRODUCTO_KEY_TIPO + ","
                + PRODUCTO_KEY_TITULO + ","
                + PRODUCTO_KEY_NO_COPIA + ","
                + PRODUCTO_KEY_DISPONIBLE + ","
                + PRODUCTO_KEY_ALQUILADO + ","
                + PRODUCTO_KEY_EXTRAVIDO + ","
                + PRODUCTO_KEY_DURACION + ","
                + PRODUCTO_KEY_ANOPRODUCTION + ","
                + PRODUCTO_KEY_ANOCOMPRA
                + ") VALUES" +
                "('Pelicula', 'Joker', 5, 5, 0, 0, 122, 2019, 2019)," +
                "('Pelicula', 'The Shape Of Water', 5, 3, 2, 0, 124, 2018, 2019)," +
                "('Pelicula', 'Parasite', 5, 4, 1, 0, 132, 2019, 2019);";

        public static string VIDEOJUEGOS_INSERT_STATEMENT = "INSERT INTO " + PRODUCTO_TABLE_NAME + "("
                + PRODUCTO_KEY_TIPO + ","
                + PRODUCTO_KEY_TITULO + ","
                + PRODUCTO_KEY_NO_COPIA + ","
                + PRODUCTO_KEY_DISPONIBLE + ","
                + PRODUCTO_KEY_ALQUILADO + ","
                + PRODUCTO_KEY_EXTRAVIDO + ","
                + PRODUCTO_KEY_PLATAFORMA
                + ") VALUES" +
                "('VideoJuegos', 'The Last Of Us', 5, 2, 2, 1,'PS3')," +
                "('VideoJuegos', 'Okami', 5, 5, 0, 0,'Wii')," +
                "('VideoJuegos', 'Test VIP 60', 65, 65, 0, 0,'Wii')," +
                "('VideoJuegos', 'Test VIP 5 2', 6, 6, 0, 0,'Wii');";

        public static string CLIENTE_INSERT_STATEMENT = "INSERT INTO " + CLIENTE_TABLE_NAME + "("
                + CLIENTE_KEY_NOMBRE + ","
                + CLIENTE_KEY_APELLIDOS + ","
                + CLIENTE_KEY_DIRECCION + ","
                + CLIENTE_KEY_NIF + ","
                + CLIENTE_KEY_TELEFONO + ","
                + CLIENTE_KEY_EMAIL + ","
                + CLIENTE_KEY_FECHA + ","
                + CLIENTE_KEY_ESTADO + ","
                + CLIENTE_KEY_VIP
                + ") VALUES" +
                "('Tue', 'Ngo', 'Sancho de Avila 22', '37888175E', '34658339065', 'tue.ngominh@gmail.com', '2019-09-11', 'Activo', 0)," +
                "('John', 'Smith', 'Binefar 7', '01974516N','34658562147', 'johns@gmail.com', '2018-02-11', 'Activo', 0)," +
                "('Liam', 'Lawrence', 'Almolgavers 113', '19348639G', '34692568475', 'liam2911@gmail.com', '2018-03-11', 'Bloqueado', 0)," +
                "('Fernando', 'Gonzales', 'Can N Oliva 45', '67345500M', '34677745158', 'fgonzales@gmail.com', '2017-09-12', 'Activo', 1), " +
                "('Xavi', 'Marti', 'Pujades 102', '67347800M', '34678845158', 'xavim@gmail.com', '2017-09-10', 'Activo', 0), " +
                "('Jennifer', 'Lopez', 'Aurora 22', '75028753Q', '34634339212', 'jenny78@gmail.com', '2018-08-11', 'Activo', 0);";

        public static string ALQUILER_INSERT_STATEMENT = "INSERT INTO " + ALQUILER_TABLE_NAME + "("
                + ALQUILER_KEY_CLIENTE_ID + ","
                + ALQUILER_KEY_PRODUCTO_ID + ","
                + ALQUILER_KEY_FECHA + ","
                + ALQUILER_KEY_TIEMPO + ","
                + ALQUILER_KEY_DESCUENTO + ","
                + ALQUILER_KEY_PAGO + ","
                + ALQUILER_KEY_ESTADO
                + ") VALUES" +
                "(4, 2, '2019-11-11', 5, 0.0 , 10.0, 'Regresado')," +
                "(1, 2, '2019-12-19', 8, 0.0, 16.0, 'Regresado')," +
                "(2, 3,'2020-01-03', 5, 0.0, 10.0, 'Regresado')," +
                "(3, 4, '2019-12-12', 10, 0.0, 40.0, 'Extraviado')," +
                "(6, 4, '2019-01-11', 10, 0.0, 40.0, 'Alquilado')," +
                "(2, 4,'2020-01-29', 20, 0.0, 80.0, 'Alquilado');";

        public static string VIP_60_STATEMENT = "INSERT INTO " + ALQUILER_TABLE_NAME + "("
                + ALQUILER_KEY_CLIENTE_ID + ","
                + ALQUILER_KEY_PRODUCTO_ID + ","
                + ALQUILER_KEY_FECHA + ","
                + ALQUILER_KEY_TIEMPO + ","
                + ALQUILER_KEY_DESCUENTO + ","
                + ALQUILER_KEY_PAGO + ","
                + ALQUILER_KEY_ESTADO
                + ") VALUES" +
                "(5, 6, '2017-01-11', 1, 0.0, 4.0, 'Regresado');";

        public static string VIP_10_STATEMENT = "INSERT INTO " + ALQUILER_TABLE_NAME + "("
                + ALQUILER_KEY_CLIENTE_ID + ","
                + ALQUILER_KEY_PRODUCTO_ID + ","
                + ALQUILER_KEY_FECHA + ","
                + ALQUILER_KEY_TIEMPO + ","
                + ALQUILER_KEY_DESCUENTO + ","
                + ALQUILER_KEY_PAGO + ","
                + ALQUILER_KEY_ESTADO
                + ") VALUES" +
                "(4, 7, '2019-12-30', 1, 0.0, 4.0, 'Regresado');";
    }
}

