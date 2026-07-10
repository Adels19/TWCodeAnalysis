using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlPractice2
{
     class Functions
    {

        List<string> titles = new List<string>();


        public List<string> GetTitles(string filename)
        {

            XDocument doc = XDocument.Load(filename);


            var root = doc.Root;

            foreach ( var element in root.Elements())
            {
                XElement first = element.Elements().FirstOrDefault();
                if (first != null)
                {
                    titles.Add(first.Value);
                }
                //titles.Add(element.Elements().FirstOrDefault().Value);
                
            }

            return titles;
        }

        public string GetDetails(string filename, int index)
        {
            XDocument doc = XDocument.Load(filename);
            

            var item = doc.Root.Elements().ElementAt(index);

            string result = "";

            foreach ( var element in item.Elements())
            {
                result += element.Name + " - " + element.Value + Environment.NewLine; ;
            }

            return result;

        }

        public class FunctionBlock
        {
            public string Name { get; set; }
            public string Declaration { get; set; }
            public string Implementation { get; set; }
        }

       

    }
}
