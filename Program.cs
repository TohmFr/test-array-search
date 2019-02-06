using System;
using System.Collections.Generic;
using System.Linq;


namespace test_speed_array
{
    class Program
    {
        const int _arraySize = 10000000;


        static void Main(string[] args)
        {
            var rand = new Random();
            
            int[] array = new int[_arraySize];
            for(var i=0;i<_arraySize; i++){
                array[i] = (rand.Next());
            }

            Console.WriteLine($"Array size {_arraySize}");

            var numbersToFind = Enumerable.Range(1, 100).Select(x=>  array[rand.Next(_arraySize)]).ToList();
            
            DateTime start;
            int v = -1;
            int[] tValue;

            start= DateTime.Now;

            tValue = numbersToFind.Select(x => FindValue(array, x)).ToArray();  

            Console.WriteLine($"Method classique time={(DateTime.Now-start).TotalMilliseconds}");

            start= DateTime.Now;

             tValue = numbersToFind.Select(x => FindValue2(array, x)).ToArray();  

            Console.WriteLine($"Method Array.Find time={(DateTime.Now-start).TotalMilliseconds}");

            start= DateTime.Now;
            tValue = numbersToFind.Select(x => FindValue3(array, x)).ToArray();  

            Console.WriteLine($"Method Parallel time={(DateTime.Now-start).TotalMilliseconds}");

            Console.Read();
        }

        static int FindValue(int[] ints, int value){
            var result = -1;
            for(var i=ints.Length-1;i>=0 && result ==-1;i--){
                if(ints[i] == value){
                    result = ints[i];
                }
            }

            return result;
        }

        static int FindValue2(int[] ints, int value){          
            return  Array.Find(ints, x=> x == value);;
        }

        static int FindValue3(int[] ints, int value){
            var query = (from v in ints.AsParallel()
                        where v == value
                        select v).First();           
            return  query;
        }

    }
}
