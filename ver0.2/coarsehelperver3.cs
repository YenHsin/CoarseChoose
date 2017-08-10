using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coarsehelper
{
    class Program
    {
        static void Main(string[] args)
        {
            string t;
            schedules = new List<ulong>();
            all = new List<ulong>();
            List<ulong> schedulessecond = new List<ulong>();
            while ((t = Console.ReadLine()) != null)
            {
                int biorsuan = int.Parse(t);
                t = Console.ReadLine();
                int number = int.Parse(t);
                ulong schedule = 0;
                int n = int.Parse(Console.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    int sevenday = 0;
                    switch (t = Console.ReadLine())
                    {
                        case "一":
                            sevenday = 1;
                            break;
                        case "二":
                            sevenday = 2;
                            break;
                        case "三":
                            sevenday = 3;
                            break;
                        case "四":
                            sevenday = 4;
                            break;
                        case "五":
                            sevenday = 5;
                            break;
                        case "六":
                            sevenday = 6;
                            break;
                        case "七":
                            sevenday = 0;
                            break;
                    }
                    int[] days = Console.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
                    foreach (int dd in days)
                    {
                        schedule += ((ulong)1 << (sevenday * 9 + dd - 1));
                    }
                }
                if (biorsuan == 1)
                    schedules.Add(schedule);
                else
                    schedulessecond.Add(schedule);
                all.Add(schedule);
            }
            Console.WriteLine("必修/必帶+同時段課程");
            Console.WriteLine(findmax(0));
            ulong next=0;
            myNode temp;
            Stack<myNode> myStack = new Stack<myNode>();
            string str;
            myStack.Push(new myNode(0, ""));
            while (myStack.Count != 0)
            {
                temp = myStack.Pop();
                str = temp.mystring;
                if (results[temp.good].choose == null)
                {
                    ulong[] final = str.ToString().Split(' ').Where(x => x != "").Select(x => ulong.Parse(x)).ToArray();
                    foreach (var ii in final)
                    {
                        for (int i = 0; i < all.Count; i++)
                        {
                            if (ii == all[i])
                            {
                                Console.Write((i + 1) + " ");
                            }
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine();
                    next = temp.good;
                    break;
                }
                else
                {
                    foreach (var tt in results[temp.good].choose)
                    {
                        myStack.Push(new myNode(tt + temp.good, str + " " + tt));
                        //Console.WriteLine(Convert.ToString((long)tt,2));
                        //Console.WriteLine(temp.good);
                    }
                }
            }
            schedules = schedulessecond;
            Console.WriteLine("選修");
            Console.WriteLine(findmax(next));
            myStack = new Stack<myNode>();
            myStack.Push(new myNode(next, ""));
            while (myStack.Count != 0)
            {
                temp = myStack.Pop();
                str = temp.mystring;
                if (results[temp.good].choose == null)
                {
                    ulong[] final = str.ToString().Split(' ').Where(x => x != "").Select(x => ulong.Parse(x)).ToArray();
                    foreach (var ii in final)
                    {
                        for (int i = 0; i < all.Count; i++)
                        {
                            if (ii == all[i])
                            {
                                Console.Write((i + 1) + " ");
                            }
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine();
                    next = temp.good;
                    break;
                }
                else
                {
                    foreach (var tt in results[temp.good].choose)
                    {
                        myStack.Push(new myNode(tt + temp.good, str + " " + tt));
                        //Console.WriteLine(Convert.ToString((long)tt,2));
                        //Console.WriteLine(temp.good);
                    }
                }
            }
        }
        static Dictionary<ulong, myvalue> results = new Dictionary<ulong, myvalue>();
        static List<ulong> schedules;
        static List<ulong> all;
        static int findmax(ulong input)
        {
            bool end = true;
            int max = 0;
            List<ulong> themax = new List<ulong>();
            foreach (ulong can in schedules)
            {
                if ((can & input) != 0) continue;
                else end = false;
                int ttt = evaluate(can);
                ulong tinput = input + can;
                if (!results.ContainsKey(tinput))
                {
                    findmax(tinput);
                }
                if (ttt + results[tinput].numbers > max)
                {
                    max = ttt + results[tinput].numbers;
                    themax = new List<ulong>();
                    themax.Add(can);
                }
                else if (ttt + results[tinput].numbers == max)
                {
                    themax.Add(can);
                }
            }
            if (end)
            {
                results[input] = new myvalue(0, null);
                return 0;
            }
            results[input] = new myvalue(max, themax);
            //Console.WriteLine(Convert.ToString((long)themax, 2));
            return max;
        }
        static int evaluate(ulong input)
        {
            int ans = 0;
            while (input != 0)
            {
                ans += (int)(input % 2);
                input >>= 1;
            }
            return ans;
        }
        class myvalue
        {
            public int numbers;
            public List<ulong> choose;
            public myvalue(int numbers, List<ulong> choose)
            {
                this.numbers = numbers;
                this.choose = choose;
            }
        }
        class myNode
        {
            public ulong good;
            public string mystring;
            public myNode(ulong good, string mystring)
            {
                this.good = good;
                this.mystring = mystring;
            }

        }
    }
}
