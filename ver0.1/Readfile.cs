using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Readfile
{
    class Program
    {
        static void Main(string[] args)
        {
            int i=1;
            string t;
            string[] str = new string[1000];
            string pat = @"</?TD>";
            string pat2 = "<TR ALIGN=\"CENTER\" HEIGHT=\"30\">";
            string pat3 = @"(^[\u4e00-\u9fa5])([\d,]+)";
            Regex r = new Regex(pat3);
            while ((t = Console.ReadLine()) != null)
            {
                if (Regex.IsMatch(t, "<TR ALIGN=\"CENTER\" HEIGHT=\"30\">",RegexOptions.IgnoreCase))
                {
                    String[] elements = Regex.Split(t, pat2,RegexOptions.IgnoreCase);
                    foreach(var element in elements)
                    {
                        String[] seconds = Regex.Split(element, pat,RegexOptions.IgnoreCase);
                        foreach(var second in seconds)
                        {
                            if(Regex.IsMatch(second, @"(^[\u4e00-\u9fa5])[\d]"))
                            {
                                String[] thirds = Regex.Split(second, @"</a>");
                                Console.WriteLine(i);
                                int count = 0;
                                Queue<Group> tt=new Queue<Group>();
                                foreach (var third in thirds)
                                {
                                    Match m = r.Match(third);
                                    while (m.Success)
                                    {
                                        for (int j = 1; j <= 2; j++)
                                        {
                                            Group g = m.Groups[j];
                                            tt.Enqueue(g);
                                        }
                                        m = m.NextMatch();
                                        count++;
                                    }
                                }
                                Console.WriteLine(count);
                                while (tt.Count != 0)
                                {
                                    Console.WriteLine(tt.Dequeue());
                                }
                                i++;
                            }
                        }
                    }
                }
                    
            }
        }
    }
}
