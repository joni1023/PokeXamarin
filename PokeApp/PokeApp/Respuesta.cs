using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApp
{
    public class Respuesta
    {
        public int count { get; set; }
        public List<Pokemon> results { get; set; }

        public string next { get; set; }

    }
}
