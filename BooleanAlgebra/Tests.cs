using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanAlgebra
{
    using System.Collections;
    class Tests
    {
        public static void bintest()
        {
            int[] imp1Const = { 1, 0, 1, 0 };
            BinaryArray bin1 = new BinaryArray(imp1Const);
            Console.WriteLine(bin1.ToString());

            int[] imp2Const = { 1, 0, 1, 1 };
            BinaryArray bin2 = new BinaryArray(imp2Const);
            Console.WriteLine(bin2.ToString());

            int[] imp3Const = { 1, 0, 1, 0 };
            BinaryArray bin3 = new BinaryArray(imp3Const);
            Console.WriteLine(bin3.ToString());

            int[] imp4Const = { 0, 0, 1, 1 };
            BinaryArray bin4 = new BinaryArray(imp4Const);
            Console.WriteLine(bin4.ToString());

            System.Console.WriteLine(bin1.Equals(bin2)); // False
            System.Console.WriteLine(bin1.Equals(bin3)); // True
            System.Console.WriteLine(bin1.Equals(1)); // False
            System.Console.WriteLine(bin1.bitValue[0] == true); // true
            System.Console.WriteLine(bin1.bitValue[1] == true); // false
            //System.Console.WriteLine(bin1.valueAt(5)); // should crash
        }
        public static void implicantTest()
        {
            Implicant imp1 = new Implicant(0, 5);
            Implicant imp2 = new Implicant(1, 5);
            Implicant imp3 = new Implicant(8, 5);
            Implicant imp4 = new Implicant(9, 5);
            System.Console.WriteLine($"{Implicant.CanCombine(imp1, imp2)}"); // true
            System.Console.WriteLine($"{Implicant.CanCombine(imp1, imp3)}"); // false
            Implicant imp1_2 = Implicant.Combine(imp1, imp2);
            Console.WriteLine($"{imp1_2.numOnes}");
            Implicant imp3_4 = Implicant.Combine(imp3, imp4);
            Console.WriteLine(imp1_2.ToString());
            Console.WriteLine(imp3_4.ToString());
            Console.WriteLine($"{Implicant.CanCombine(imp1_2, imp3_4)}");
            Implicant imp5 = Implicant.Combine(imp1_2, imp3_4);
            Console.WriteLine(imp5.ToString());
            
        }
        public static void quineTest()
        {
            int[] minterms1 = new int[10] {0,1,2,5,6,7,8,9,10,14};
            Quine quine1 = new Quine(minterms1, 4);
            System.Console.WriteLine(quine1.ToString());
            quine1.simplify();
        }
    }
}
