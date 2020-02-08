using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Helper
{
    class SQLBuilder
    {
        private string sql;
        private bool hayWhere;

        public SQLBuilder from(string tableName)
        {
            this.sql = "SELECT * FROM " + tableName;
            return this;
        }

        public SQLBuilder from(string tableName, string key)
        {
            this.sql = "SELECT " + key + " FROM " + tableName;
            return this;
        }

        public SQLBuilder where(string condition)
        {
            string clave;

            if (hayWhere)
            {
                clave = " AND ";
            }
            else
            {
                clave = " WHERE ";
            }

            this.sql += clave + condition;
            hayWhere = true;
            return this;
        }

        public SQLBuilder where(string key, string operador, string value)
        {
            string condition = key + " " + operador.ToUpper() + " ";
            if (!Int32.TryParse(value, out int n) || !Double.TryParse(value, out double d))
            {
                condition += "'" + value + "'";
            }
            else
            {
                condition += value;
            }
            return where(condition);
        }

        public SQLBuilder delete(string tableName)
        {
            this.sql = "DELETE FROM " + tableName;
            return this;
        }

        public string build()
        {
            return this.sql + ";";
        }
    }
}
