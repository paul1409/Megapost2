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
