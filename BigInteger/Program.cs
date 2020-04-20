using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigInteger
{
    class Program
    {
        static void Main(string[] args)
        {
            NumarMare a = new NumarMare("1");
            NumarMare b = new NumarMare("1");
            NumarMare fact = new NumarMare("1");
            NumarMare c = a + b;

            for (int i = 3; i <= 100; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }

            for (int i = 2; i <= 1000; i++)
                fact = fact * new NumarMare(Convert.ToString(i));

            Console.WriteLine("Al 100-lea termen fibonacci este {0}", c);
            Console.WriteLine("1000! = {0}", fact);
            Console.ReadKey();
        }
    }

    internal class NumarMare
    {
        private string data;

        public NumarMare() : this("0")
        {
        }

        public NumarMare(string s)
        {
            data = s;
        }

        public static NumarMare operator +(NumarMare a, NumarMare b)
        {
            StringBuilder c = new StringBuilder();
            Stack<string> s = new Stack<string>();

            int i = a.data.Length - 1, j = b.data.Length - 1;
            int carry = 0, sum;

            while(i >= 0 && j >= 0) {
                sum = (a.data[i] - '0' + b.data[j] - '0' + carry) % 10;
                carry = (a.data[i] - '0' + b.data[j] - '0' + carry) / 10;

                s.Push(Convert.ToString(sum));

                i--;
                j--;
            }

            while (i >= 0)
            {
                sum = a.data[i] - '0' + carry;
                carry = sum / 10;
                sum = sum % 10;

                s.Push(Convert.ToString(sum));

                i--;
            }

            while (j >= 0)
            {
                sum = b.data[j] - '0' + carry;
                carry = sum / 10;
                sum = sum % 10;

                s.Push(Convert.ToString(sum));

                j--;
            }

            if (carry != 0)
                s.Push(Convert.ToString(carry));

            while (s.Count != 0)
                c.Append(s.Pop());

            return new NumarMare(c.ToString());
        }

        public static NumarMare operator *(NumarMare a, NumarMare b)
        {
            Stack<string> s = new Stack<string>();
            StringBuilder c = new StringBuilder();
            NumarMare result = new NumarMare();

            int i = a.data.Length - 1, j = b.data.Length - 1;
            int carry = 0, prod, k;

            while (j >= 0)
            {
                k = i;
                carry = 0;

                while (k >= 0)
                {
                    prod = ((a.data[k] - '0') * (b.data[j] - '0') + carry) % 10;
                    carry = ((a.data[k] - '0') * (b.data[j] - '0') + carry) / 10;

                    s.Push(Convert.ToString(prod));

                    k--;
                }

                if (carry != 0)
                    s.Push(Convert.ToString(carry));

                while (s.Count != 0)
                    c.Append(s.Pop());

                for (k = 0; k < b.data.Length - 1 - j; k++)
                    c.Append("0");

                result = result + new NumarMare(c.ToString());
                c.Clear();

                j--;
            }

            return result;
        }

        public override string ToString() => $"{data}";
    }
}
