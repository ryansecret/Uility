using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStucture
{
    public  class Sort
    {
        public List<int> ShellSort(List<int> list,int seed)
        {
            int increment = list.Count;
            do
            {
                increment = increment/seed + 1;
                for (int i = increment+1; i < list.Count; i++)
                {
                    int tem = list[i];
                    int j;
                    for ( j = i-increment; j > 0&&list[j]>tem; j=j-increment)
                    {
                        list[j + increment] = list[j];

                    }
                    list[j + increment] = tem;
                }

            }
            while (increment>1);
            return list;
        }

        public List<int> InsertSort(List<int> list )
        {
            int count = list.Count;
            for (int i = 1; i < count; i++)
            {
                int tem = list[i];
                int innerIndex = i;
                while (innerIndex>0&&list[innerIndex-1]>tem)
                {
                    list[innerIndex] = list[innerIndex-1];
                    innerIndex--;
                }
                list[innerIndex] = tem;
            }
            return list;
        }

        public  static void MergeSortFunction(List<int> array, int first, int last)
        {
            try
            {
                if (first < last)   //子表的长度大于1，则进入下面的递归处理
                {
                    int mid = (first + last) / 2;   //子表划分的位置
                    MergeSortFunction(array, first, mid);   //对划分出来的左侧子表进行递归划分
                    MergeSortFunction(array, mid + 1, last);    //对划分出来的右侧子表进行递归划分
                    MergeSortCore(array, first, mid, last); //对左右子表进行有序的整合（归并排序的核心部分）
                }
            }
            catch (Exception ex)
            { }
        }

        //归并排序的核心部分：将两个有序的左右子表（以mid区分），合并成一个有序的表
        private static void MergeSortCore(List<int> array, int first, int mid, int last)
        {
            try
            {
                int indexA = first; //左侧子表的起始位置
                int indexB = mid + 1;   //右侧子表的起始位置
                int[] temp = new int[last + 1]; //声明数组（暂存左右子表的所有有序数列）：长度等于左右子表的长度之和。
                int tempIndex = 0;
                while (indexA <= mid && indexB <= last) //进行左右子表的遍历，如果其中有一个子表遍历完，则跳出循环
                {
                    if (array[indexA] <= array[indexB]) //此时左子表的数 <= 右子表的数
                    {
                        temp[tempIndex++] = array[indexA++];    //将左子表的数放入暂存数组中，遍历左子表下标++
                    }
                    else//此时左子表的数 > 右子表的数
                    {
                        temp[tempIndex++] = array[indexB++];    //将右子表的数放入暂存数组中，遍历右子表下标++
                    }
                }
                //有一侧子表遍历完后，跳出循环，将另外一侧子表剩下的数一次放入暂存数组中（有序）
                while (indexA <= mid)
                {
                    temp[tempIndex++] = array[indexA++];
                }
                while (indexB <= last)
                {
                    temp[tempIndex++] = array[indexB++];
                }

                //将暂存数组中有序的数列写入目标数组的制定位置，使进行归并的数组段有序
                tempIndex = 0;
                for (int i = first; i <= last; i++)
                {
                    array[i] = temp[tempIndex++];
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public  static void QuickSort(List<int> a, int start, int end)
        {
            int i = start, j = end;
            int pivot = a[i];
            while (i < j)
            {
                while (i < j && pivot <= a[j]) j--;
                a[i] = a[j];
                while (i < j && a[i] <= pivot) i++;
                a[j] = a[i];
            }
            a[i] = pivot;
            if (i > start) QuickSort(a, start, i);
            if (i < end) QuickSort(a, i + 1, end);
        }

        static void SelectionSort(IList<int> list)
        {
            int upperBound = list.Count;
            for (int outer = 0; outer < upperBound; outer++)
            {
                int minIndex = outer;
                for (int inner = outer + 1; inner < upperBound; inner++)
                {
                    if (list[inner] < list[minIndex])
                    {
                        minIndex = inner;
                    }
                }
                int temp = list[minIndex];
                list[minIndex] = list[outer];
                list[outer] = temp;
            }
        }
    }
}
