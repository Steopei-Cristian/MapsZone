using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat1.Model
{
    internal class KMP
    {
        private string haystack;
        private string needle;

        private int[] lps;

        public string HAYSTACK { get => this.haystack!; }
        public string NEEDLE { get => this.needle!; }

        public KMP(string a, string b)
        {
            this.haystack = a;
            this.needle = b;

            lps = new int[needle.Length];
        }

        private void buildLPS()
        {
            int len = 0, i = 1, m = needle.Length;
            lps[0] = 0;
            while(i < m)
            {
                if (needle[len] == needle[i])
                {
                    lps[i] = len + 1;
                    len++;
                    i++;
                }
                else
                {
                    if (len == 0)
                    {
                        lps[i] = 0;
                        i++;
                    }
                    else
                        len = lps[len - 1];
                }
            }
        }

        public int search()
        {
            int i = 0, j = 0;
            int n = haystack.Length, m = needle.Length;
            buildLPS();
            while(i < n)
            {
                if (haystack[i] == needle[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    if (j == 0)
                        i++;
                    else
                        j = lps[j - 1];
                }

                if (j == m)
                    return i - j;
            }
            return -1;
        }

        public int getCount()
        {
            int i = 0, j = 0;
            buildLPS();
            int n = haystack.Length, m = needle.Length;

            int res = 0;

            while(i < n)
            {
                if (haystack[i] == needle[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    if (j == 0)
                        i++;
                    else
                        j = lps[j - 1];
                }
                if(j == m)
                {
                    res++;
                    j = 0;
                }
            }
            return res;
        }
    }
}
