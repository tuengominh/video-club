using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoClub.Helper
{
    class ValidationUtil
    {
        public static string NullOrEmpty(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                throw new Exception("Los datos estan vacios or son nulos.");
            }
        }

        public static double NumeroDouble(string input)
        {
            if (Double.TryParse(input, out double n))
            {
                return n;
            }
            else
            {
                throw new Exception("No es numerico.");
            }
        }

        public static int NumeroInteger(string input)
        {
            if (Int32.TryParse(input, out int n))
            {
                return n;
            }
            else
            {
                throw new Exception("No es numerico.");
            }
        }

        public static int Ano(int ano)
        {
            if (ano > 1 && ano < 9999)
            {
                return ano;
            }
            else
            {
                throw new Exception("Ano debe ser entre 1 y 9999.");
            }
        }

        public static DateTime Fecha(string input)
        {
            if (DateTime.TryParse(input, out DateTime v))
            {
                return v;
            }
            else
            {
                throw new Exception("Fecha no es valido. Por favor escriba en este formato: yyyy-MM-dd.");
            }
        }

        public static string NIF(string input)
        {
            var nif = input.Trim().Replace(" ", "").Replace("-", "");
            bool isValid = Regex.Match(nif, @"([a-z]|[A-Z]|[0-9])[0-9]{7}([a-z]|[A-Z]|[0-9])").Success;
            if (isValid)
            {
                return nif;
            }
            else
            {
                throw new Exception("NIF no es valido.");
            }
        }

        public static string Telefono(string input)
        {
            var phoneNumber = input.Trim().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
            bool isValid = Regex.Match(phoneNumber, @"34(([6][0-9]{8}$)|([7][1-9]{1}[0-9]{7}$))").Success;
            if (isValid)
            {
                return phoneNumber;
            }
            else
            {
                throw new Exception("Telefono no es valido.");
            }
        }
  
        public static string Email(string input)
        {
            bool isValid = new EmailAddressAttribute().IsValid(input);
            if (isValid)
            {
                return input;
            }
            else
            {
                throw new Exception("Email no es valido.");
            }
        }
    }
}
