using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TWCodeAnalysis
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Learning());

            var @class = new x();
            var @struct = new y();

            Test.IncNumber(@class);
            Test.IncNumber(@struct);

        }

        class x
        {
            public int i;
        }

        struct y
        {
            public int i;
        }

        static class Test
        {
            // pass class / refrence
            public static void IncNumber(x test)
            {
                test.i++;
            }
            // pass struct / value
            public static void IncNumber(y test)
            {
                test.i++;
            }
        }
    }


    /*
     * class tcSolution{
     * 
     *  input -> pateka do solucijata
     *  
     *  vector <tcProject>(List)
     *      
     * }
     * class tcProject{
     * 
     *  vector <tcPlcProjectFile>
     * }
     * class tcPlcProject{
     * 
     *  vector <tcFiles> gvlFiles
     *  vector <tcFiles> pouFiles
     *  
     * }
     * class tcFile{
     *  
     *  filepath string;
     *  vector <variable> variables
     *  
     *  getImplementation(){
     *      get how much is the variable used in that file
     *  }
     *  getDeclaration()
     *  {
     *      define variable   
     *  }
     * 
     * }
     * 
     * struct variable {
     *  string name;
     *  string type;
     *  string startingValue ( := ?? )
     * 
     * int usedCount;
     * 
     * }
     * 
     */
}
