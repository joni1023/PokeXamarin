using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApp
{
    public class Pokemon
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int order { get; set; }

        public Sprites sprites { get; set; }
        public string image { get; set; }
    }
    public class Sprites
    {
        public string back_default { get; set; }
        public object back_female { get; set; }
        public string back_shiny { get; set; }
        public object back_shiny_female { get; set; }
        public string front_default { get; set; }
        public object front_female { get; set; }
        public string front_shiny { get; set; }
        public object front_shiny_female { get; set; }
    }
}
