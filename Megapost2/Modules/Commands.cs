using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2 {
    class Commands {
        public string name { get; set; }
        public string URL { get; set; }

        public Commands(string name, string URL) {
            this.name = name;
            this.URL = URL;
        }
    }
}
