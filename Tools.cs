using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Gioco_generico
{
    public partial class Game1
    {
        public enum areas {WALKABLE, OBSTACLE, STREET}
        public struct layer
        {
            public int[,] tileMap;
            public int x_cols;
            public int y_rows;
            public int order;
            public int id;
            public string name;

            public layer(int[,] tiles, string name, int order, int x_cols, int y_rows, int id)
            {
                tileMap = tiles;
                this.name = name;
                this.order = order;
                this.x_cols = x_cols;
                this.y_rows = y_rows;
                this.id = id;
            }
        };
        public void loadTMX(string path, List<layer> layers)
        {
            var xml = XDocument.Load(path);

            int order = 0;

            foreach (XElement el in xml.Root.Descendants("layer"))
            {
                int id = Convert.ToInt32(el.Attribute("id").Value);
                string name = el.Attribute("name").Value;

                string[] lines = el.Element("data").Value.Split('\n');

                var x_cols = Convert.ToInt32(el.Attribute("width").Value);
                var y_rows = Convert.ToInt32(el.Attribute("height").Value);

                int[,] l = new int[y_rows, x_cols];

                for (int i = 1; i < y_rows + 1; i++)
                {
                    string[] line = lines[i].Split(',');
                    for (int j = 0; j < x_cols; j++)
                    {
                        l[i - 1, j] = Convert.ToInt32(line[j]);
                    }
                }

                /*for (int i = 1; i < y_rows + 1; i++)
                {
                    string s = "";
                    string[] line = lines[i].Split(',');
                    for (int j = 0; j < x_cols; j++)
                    {
                        s += l[i - 1, j];
                    }
                }*/
                layers.Add(new layer(l, name, order, x_cols, y_rows, id));
                order += 1;
            }
        }

        int counter = 0;
        string line;
        
        public void loadAdvices()
        {
            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"content/testi/advices2.txt");
            while ((line = file.ReadLine()) != null)
            {
                ConstVar.advices.Add(line);
                counter++;
            }
            file.Close();
        }

        public void loadNarratorText()
        {
            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"content/testi/narrator.txt");
            while ((line = file.ReadLine()) != null)
            {
                ConstVar.narratorScript.Add(line);
                counter++;
            }
            file.Close();
        }
    }
}
