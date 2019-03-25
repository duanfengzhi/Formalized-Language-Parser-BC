using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 与Parser不同之处在于循环结束后全局指针不会后移
public class FParser
{
    public static void F_Parser(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, int start_num, int end_num)
    {
        for (tt_i = start_num; tt_i < end_num && tt[tt_i] != null; )
        {
            if (tt[tt_i].Type.Equals("auto"))
            {
                try
                {
                    Auto.M_Auto(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                catch (WrongGrammarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                catch (NotAVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            else if (tt[tt_i].Type.Equals("putnumb"))
            {
                try
                {
                    Putnumb.Put_numb(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                catch (NotDefineVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                catch (WrongGrammarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                catch (NotAVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            else if (tt[tt_i].Type.Equals("putchar"))
            {
                try
                {
                    Putchar.Put_char(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                catch (NotDefineVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                catch (WrongGrammarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            else if (tt[tt_i].Code.Equals("++") || tt[tt_i].Code.Equals("--"))
            {
                try
                {
                    UnaryOperation.Unary_Operation(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                catch (NotDefineVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                catch (WrongGrammarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            else if (tt[tt_i].Type.Equals("operator"))
            {
                try
                {
                    BOperator.Operator(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                catch (NotAVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                catch (NotDefineVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            else if (tt[tt_i].Type.Equals("getchar"))
            {
                try
                {
                    Getchar.Get_char(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                catch (NotDefineVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                catch (WrongGrammarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            else
            {
                tt_i++;
            }
        }
        return;
    }
}

