using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTDealsScanerEngine
{
    public class Similarity
    {

        private int min(int one, int two, int three)
        {
            int min = one;
            if (two < min)
            {
                min = two;
            }
            if (three < min)
            {
                min = three;
            }
            return min;
        }

        public int LD(String str1, String str2)
        {
            int[,] d;     // 矩阵
            int n = str1.Length;
            int m = str2.Length;
            int i;     // 遍历str1的
            int j;     // 遍历str2的
            char ch1;     // str1的
            char ch2;     // str2的
            int temp;     // 记录相同字符,在某个矩阵位置值的增量,不是0就是1
            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {
                return n;
            }

            d = new int[n + 1, m + 1];

            for (i = 0; i <= n; i++)
            {     // 初始化第一列
                d[i, 0] = i;
            }
            for (j = 0; j <= m; j++)
            {     // 初始化第一行
                d[0, j] = j;
            }
            for (i = 1; i <= n; i++)
            {     // 遍历str1
                ch1 = str1[i - 1];
                // 去匹配str2
                for (j = 1; j <= m; j++)
                {
                    ch2 = str2[j - 1];
                    if (ch1 == ch2)
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = 1;
                    }
                    // 左边+1,上边+1, 左上角+temp取最小
                    d[i, j] = min(d[i - 1, j] + 1, d[i, j - 1] + 1, d[i - 1, j - 1] + temp);
                }
            }
            return d[n, m];
        }

        public double sim(String str1, String str2)
        {
            int ld = LD(str1, str2);
            return 1 - (double)ld / Math.Max(str1.Length, str2.Length);
        }
    }
}
