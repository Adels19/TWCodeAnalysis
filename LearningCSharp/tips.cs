//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LearningCSharp
//{
//    internal class tips
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello World!");


//            // VARIABLES

//            string name = "Adela";
//            Console.WriteLine(name);

//            int number = 22;
//            Console.WriteLine(number);

//            int number2;
//            number2 = 23;

//            Console.WriteLine(number2);

//            double doubleNumber = 5.99D;
//            Console.WriteLine(doubleNumber);

//            //D-> double
//            //F-> float
//            //M-> decimal
//            //L-> long
//            //UL->unsigned long
//            //UI->unsigned int

//            string text = "Hello";
//            char letter = 'H';

//            Console.WriteLine(text + " " + letter);



//            //string firstName = "Adela";
//            //string lastName = "Elenin";
//            //Console.WriteLine(firstName + " " + lastName);


//            bool isCSharpFun = true;
//            Console.WriteLine(isCSharpFun);



//            // USER INPUT

//            Console.WriteLine("enter username:");
//            //string userName = Console.ReadLine();

//            //Console.WriteLine("username:" + userName);

//            // must convert for int
//            Console.WriteLine("enter age:");
//            //int age = Convert.ToInt32(Console.ReadLine());
//            //Console.WriteLine(age);



//            // MATH

//            int x = 10;
//            int y = 15;
//            Math.Min(x, y);
//            int max = Math.Max(x, y);
//            Console.WriteLine(max);
//            double round = Math.Round(9.99);
//            Console.WriteLine(round);


//            // STRING
//            string txt = "hello world!";
//            Console.WriteLine(txt.ToUpper());


//            string firstName = "John ";
//            string lastName = "Doe";
//            string name2 = string.Concat(firstName, lastName);
//            Console.WriteLine(name2);
//            Console.WriteLine(firstName + lastName);


//            // Interpolation

//            string name3 = $"{firstName}{lastName}";
//            Console.WriteLine(name3);

//            // access strings
//            Console.WriteLine(firstName[0]);

//            Console.WriteLine(firstName.IndexOf("J"));


//            string str = "Microsam";
//            int charPos = str.IndexOf("s");
//            string str2 = str.Substring(charPos);
//            Console.WriteLine(str2);

//            Console.WriteLine(str.Substring(0, charPos));


//            string txt2 = "It's \\ \"alright\".";
//            Console.WriteLine(txt2);



//            // foreach

//            string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };
//            int n = 5;

//            //foreach(string car in cars)
//            //{
//            //    Console.Write(car + " ");
//            //}
//            //Console.WriteLine();


//            //string[] cars2 = new string[2];
//            //for(int i=0; i < cars2.Length; i++)
//            //{
//            //    cars2[i] = Console.ReadLine();
//            //}


//            // array
//            int[] unordered = { 1, 3, 2, 6, 5, 4 };



//            // CONDITIONS

//            Array.Sort(unordered);

//            foreach (int a in unordered)
//            {
//                Console.Write(a + " ");
//            }
//            Console.WriteLine();

//            Array.Sort(cars);
//            foreach (string c in cars)
//            {
//                Console.Write(c + " ");
//            }

//            // array.sort works for both



//            // MULTIDIMENSIONAL 
//            int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };

//            foreach (int i in matrix)
//            {
//                Console.WriteLine(i);
//            }

//            for (int i = 0; i < matrix.GetLength(0); i++)
//            {
//                for (int j = 0; j < matrix.GetLength(1); j++)
//                {
//                    Console.Write(matrix[i, j] + "");
//                }
//                {

//                }
//            }

//            Console.WriteLine();

//            // methods

//            void MyMethod()
//            {
//                Console.WriteLine("I just got executed!");
//            }

//            MyMethod();


//            Level level = Level.Medium;
//            Console.WriteLine(level);



//            // TRY CATCH

//            try
//            {

//                int[] array = { 1, 2, 3 };

//                if (array.Length < 10)
//                {
//                    throw new Exception("Array is too small for index 10");
//                }

//                Console.WriteLine(array[11]);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);    // or ex.Message implemented 
//            }
//            finally
//            {
//                Console.WriteLine("This is printed no matter had exception or no");
//            }



//        }

//        enum Level
//        {
//            Low, Medium, High
//        }





//    }
//}

