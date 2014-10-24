using System.Collections.Generic;

namespace DataStucture
{
    public class Search
    {
        public int RecurBinarySecarch(List<int> list, int lower, int upper, int value)
        {
            if (lower > upper)
            {
                return -1;
            }
            else
            {
                int mid = (lower + upper)/2;
                if (list[mid] == value)
                {
                    return mid;
                }
                else
                {
                    if (list[mid] > value)
                    {
                        return RecurBinarySecarch(list, lower, mid - 1, value);
                    }
                    else
                    {
                        return RecurBinarySecarch(list, mid + 1, upper, value);
                    }
                }
            }
        }
    }
}