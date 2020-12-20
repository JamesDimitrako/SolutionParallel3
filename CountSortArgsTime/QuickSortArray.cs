using System;

namespace CountSortArgsTime
{

    public class QuickSortArray
    {
        static int Partition(int[] array, int low,
            int high)
        {
            //1. Select a pivot point.
            int pivot = array[high];

            int lowIndex = (low - 1);

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    lowIndex++;

                    int temp = array[lowIndex];
                    array[lowIndex] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[lowIndex + 1];
            array[lowIndex + 1] = array[high];
            array[high] = temp1;

            return lowIndex + 1;
        }

        public static void Sort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(array, low, high);

                //3. Recursively continue sorting the array
                Sort(array, low, partitionIndex - 1);
                Sort(array, partitionIndex + 1, high);
            }
        }

        public static void Program()
        {
            int[] array = new int[10];
            int length = array.Length;

            for (int i = 0; i < length-1; i++)
            {
                int procIndex = i; //closure capture 
                array[i] = procIndex;
            }

            Console.WriteLine("QuickSort");
            Sort(array, 0, length - 1);


            Console.ReadKey();
        }
    }}