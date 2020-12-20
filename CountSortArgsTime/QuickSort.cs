using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountSortArgsTime
{
    /// <summary>
    /// Quick Sort implementation for Generic IList interface, this can be used for list and also for Arrays
    /// As array implements IList 
    /// </summary>
    public static class QuickSort
    {
        static int Partition<T>(IList<T> list, int low,
                                        int high) where T : IComparable<T>
        {
            //1. Select a pivot point.
            var pivot = list[high];

            int lowIndex = (low - 1);

            //2. Reorder the collection.
            for (int j = low; j < high; j++)
            {
                if (list[j].CompareTo(pivot) <= 0)
                {
                    lowIndex++;

                    var temp = list[lowIndex];
                    list[lowIndex] = list[j];
                    list[j] = temp;
                }
            }

            var temp1 = list[lowIndex + 1];
            list[lowIndex + 1] = list[high];
            list[high] = temp1;

            return lowIndex + 1;
        }

        /// <summary>
        /// 1. Select a pivot point (implementation below selects the last value).
        /// 2. Reorder the collection so that all values less than the pivot are before that pivot, and all values
        ///   greater than the pivot are after the pivot.
        ///   After this partitioning, the pivot element is in its final position.
        /// 3. Recursively do this partitioning on the "less than pivot" set and the "greater than pivot" set.
        ///   Continue recursively applying this algorithm until the set is sorted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private static IList<T> QuickySort<T>(IList<T> list, int low, int high)
            where T : IComparable<T>
        {
            if (low < high)
            {
                int partitionIndex = Partition(list, low, high);

                //3. Recursively continue sorting the List
                QuickySort(list, low, partitionIndex - 1);
                QuickySort(list, partitionIndex + 1, high);
            }

            return list;
        }
        
        private static IList<T> ParallelQuickySort<T>(IList<T> list, int low, int high, int depthRemaining = 0)
            where T : IComparable<T>
        {
            if (low < high)
            {
                int partitionIndex = Partition(list, low, high);

                //3. Recursively continue sorting the List
                Parallel.Invoke((() => ParallelQuickySort(list, low, partitionIndex - 1)));
                Parallel.Invoke(() => ParallelQuickySort(list, partitionIndex + 1, high));
            }

            return list;
        }

        /// <summary>
        /// Sort any type of List with QuickSort Algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IList<T> QuickySort<T>(IList<T> list) where T : IComparable<T>
        {
            return QuickySort(list, 0, list.Count-1);
        }
        
        public static IList<T> ParallelQuickySort<T>(IList<T> list) where T : IComparable<T>
        {
            return ParallelQuickySort(list, 0, list.Count-1);
        }
    }
}