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
            while ((t = Console.ReadLine()) != null)
            {
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
                schedules.Add(schedule);
            }
            Console.WriteLine(findmax(0));
            /*foreach(var i in findans(0))
            {
                Console.WriteLine(i);
            }*/
            List<ulong> nowlist = new List<ulong>(schedules);
            myNode temp;
            Stack<myNode> myStack = new Stack<myNode>();
            string str ;
            myStack.Push(new myNode(0,""));
            while (myStack.Count != 0)
            {
                temp = myStack.Pop();
                str = temp.mystring;
                if (results[temp.good].choose == null)
                {
                    ulong[] final = str.ToString().Split(' ').Where(x=>x!="").Select(x => ulong.Parse(x)).ToArray();
                    foreach(var ii in final)
                    {
                        for(int i = 0; i < schedules.Count; i++)
                        {
                            if (ii == schedules[i])
                            {
                                Console.Write((i+1) + " ");
                            }
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine();
                    break;
                }
                else
                {
                    foreach (var tt in results[temp.good].choose)
                    {
                        myStack.Push(new myNode(tt+temp.good,str+" "+tt));
                        //Console.WriteLine(Convert.ToString((long)tt,2));
                        //Console.WriteLine(temp.good);
                    }
                }
            }
            /*List<ulong> good;ulong current=0 ;
            while ((good = results[current].choose) != null)
            {
                Console.WriteLine(schedules.IndexOf(good[0]) + 1);
                current += good[0];
            }*/
            /*foreach(var pair in results)
            {
                if(pair.Value.choose!=null)
                Console.WriteLine(String.Join(" ", pair.Value.choose));
            }*/
        }
        static Dictionary<ulong, myvalue> results = new Dictionary<ulong, myvalue>();
        static List<ulong> schedules;
        static int findmax(ulong input)
        {
            bool end = true;
            int max = 0;
            List<ulong> themax=new List<ulong>();
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
                }else if(ttt + results[tinput].numbers == max)
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
            public myNode(ulong good,string mystring)
            {
                this.good = good;
                this.mystring = mystring;
            }
            
        }
        /*static List<string> findans(ulong table)
        {
            if (results[table].choose == null)
            {
                return new List<string>() { "" };
            }
            List<string> temp = new List<string>();
            foreach(var i in results[table].choose)
            {
                foreach(var j in findans(table + i))
                {
                    temp.Add(i + j);
                }
            }
            return temp;
        }*/
    }
}
