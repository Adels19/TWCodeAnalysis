using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


namespace TWCodeAnalysis
{
    class functions
    {


        // receive a filename 
        public List<string> ReadXml(string fileName)
        {
                    

            XDocument doc = XDocument.Load(fileName);       // "Take the XML file from the disk and put it into memory."
            XElement root = doc.Root;  // gives me the root // <library>

                List<string> items = new List<string>();    // just for storing informations we want

                foreach (XElement element in root.Elements()) // <book> // root.Elements() -> all child elements
            {

                    XElement first = element.Elements().FirstOrDefault();   // "Give me the first child, if it exists."

                    if (first != null)
                    {
                        items.Add(first.Value);     // added to the list the value of the element
                    }

                }

                //IEnumerable<XElement> node = doc.Root.Elements();   the real books, child of root

                return items;   // return a list of strings

        }

       


        // pretty similar to the ReadXml, first it reads the file, and the index of the selected item, then 
        public string GetDetails(string fileName, int index)
        {

            XDocument doc = XDocument.Load(fileName);
            var item = doc.Root.Elements().ElementAt(index);   // by index because of ListBox

            string result = ""; 

            foreach(var child in item.Elements())
            {
                result += child.Name + " : " + child.Value + Environment.NewLine;
            }

            return result;


           
        }

    }
}
