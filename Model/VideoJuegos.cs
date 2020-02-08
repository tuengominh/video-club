using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Model
{
    public enum Plataforma { PC, Xbox, PS2, PS3, Wii }

    public class VideoJuegos : Producto
    {
        public Tipo tipo { get; set; } = Tipo.VideoJuegos;

        public Plataforma plataforma { get; set; }
    }
}
