﻿/** cmdlist class **/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Megapost2 {
    class cmdlist {

        string dir = Path.Combine(Directory.GetCurrentDirectory(), "memes.txt");
        string multidir = Path.Combine(Directory.GetCurrentDirectory(), "randmemes.txt");
        StreamReader file;
        Random rand;
        public cmdlist() {
            rand = new Random();
        }

        //Reads through the commands
        public List<Commands> reader() {
            file = new StreamReader(dir);
            string line;
            List<Commands> cmds = new List<Commands>();
            while ((line = file.ReadLine()) != null) {
                string[] cmd = line.Split(null);
                cmds.Add(new Commands(cmd[0], cmd[1]));
            }
            file.Close();
            return cmds;
        }

        //Checks for command existence
        public bool exists(string name) {
            string[] read = File.ReadAllLines(dir);
            List<string> lines = read.OfType<string>().ToList();
            foreach (string s in lines) {
                string[] line = s.Split(null);
                if (line[0] == name) return true;
            }
            return false;
        }

        //Adds the command
        public void add(string name, string URL) {
            StreamWriter w = File.AppendText(dir);
            w.WriteLine(name + " " + URL);
            w.Close();
        }

        //Undos a specified command
        public void undo(string name) {
            var lines = File.ReadAllLines(dir);
            File.WriteAllLines(dir, lines.Take(lines.Length - 1).ToArray());
        }

        //Runs memes with multiple links
        public string contained(string s) {
            StreamReader file = new StreamReader(multidir);
            string line;
            string ret = "";
            while ((line = file.ReadLine()) != null) {
                string[] cmd = line.Split(null);
                if (cmd[0] == s) {
                    ret = cmd[rand.Next(1, cmd.Length)];
                    break;
                }
            }
            return ret;
        }

        //Checks if the command exists
        public bool multiExists(string name) {
            foreach (string s in multiNames()) {
                string[] line = s.Split(null);
                if (line[0] == name) return true;
            }
            return false;
        }

        //Check if link exists within the list
        public bool linkExists(string s, string l) {
            string[] read = File.ReadAllLines(multidir);
            List<string> lines = read.OfType<string>().ToList();
            foreach (string str in lines) {
                string[] line = s.Split(null);
                if (line[0] == s)
                    foreach (string links in line) if (links == l) return true;
            }
            return false;
        }

        //Creates a new command with multiple links
        public void createMulti(List<string> s) {
            StreamWriter w = File.AppendText(multidir);
            string toWrite = "";
            foreach (string str in s) toWrite += str + " ";
            w.WriteLine(toWrite);
            w.Close();
        }

        //Creates a new command with one link
        public void createMulti(string n, string l) {
            try { File.AppendAllText(multidir, n + " " + l); }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
        }

        //Adds a new link to a previously existing command
        public void addMulti(string n, string l) {
            try {
                string[] read = File.ReadAllLines(multidir);
                List<string> lines = read.OfType<string>().ToList();
                foreach (string s in multiNames()) {
                    string[] line = s.Split(null);
                    if (line[0] == n)
                        if (!line.Contains(l)) {
                            string str = s + " " + l;
                            lines.Remove(s);
                            lines.Add(str);
                            File.WriteAllLines(multidir, lines);
                        }
                }
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
        }

        //Gets the names of the commands
        public List<string> multiNames() {
            string[] read = File.ReadAllLines(multidir);
            List<string> lines = read.OfType<string>().ToList();
            return lines;
        }

        //gets the list of multi commands
        public string multiList(string s) {
            string ret = "";
            string[] read = File.ReadAllLines(multidir);
            List<string> lines = read.OfType<string>().ToList();
            foreach (string str in multiNames()) {
                string[] line = str.Split(null);
                if (line[0] == s) {
                    foreach (string l in line) ret += "<" + l + ">" + "\n";
                    break;
                }
            }
            return ret;
        }

        //Adds a new command
        public void add(Commands newcmd) {
            StreamWriter w = File.AppendText(dir);
            w.WriteLine(newcmd.name + " " + newcmd.URL);
            w.Close();
        }

        //Gets a list from the multidir
        public List<string> randlist() {
            List<string> list = new List<string>();
            StreamReader file = new StreamReader(multidir);
            string line;
            while ((line = file.ReadLine()) != null) list.Add(line.Split(null)[0]);
            return list;
        }
    }
}