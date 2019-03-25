using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 循环体执行函数 原理和主函数BParser相似，规定了开头和结束下标
public class Parser_ReNamed
{
    public static void Parser_ReNamed1(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref Dictionary<string, int> markmap, int start_num, int end_num, ref bool while_or)
    {
        try
        {
            for (tt_i = start_num; tt_i < end_num && tt[tt_i] != null; )
            {
                // 遇到break，跳出本次parser执行
                if (while_or == false)
                {
                    return;
                }
                // 读取到auto关键字,跳转到auto函数处理
                if (tt[tt_i].Type.Equals("auto"))
                {
                    Auto.M_Auto(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到putnumb关键字,跳转到putnumb函数,输出数值型变量值
                else if (tt[tt_i].Type.Equals("putnumb"))
                {
                    Putnumb.Put_numb(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到putchar关键字,跳转到putchar函数,输出字符型型变量值
                else if (tt[tt_i].Type.Equals("putchar"))
                {
                    Putchar.Put_char(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到类型为++ --,即自增自减，跳转到unaryOperation函数
                else if (tt[tt_i].Code.Equals("++") || tt[tt_i].Code.Equals("--"))
                {
                    UnaryOperation.Unary_Operation(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到类型为operator,即操作符类型，跳转到operator函数
                else if (tt[tt_i].Type.Equals("operator"))
                {
                    BOperator.Operator(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到关键字getchar,跳转到getchar函数进行获取字符串输入操作
                else if (tt[tt_i].Type.Equals("getchar"))
                {
                    Getchar.Get_char(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到关键字getnumb,跳转到getnumb函数进行获取数值输入操作
                else if (tt[tt_i].Type.Equals("getnumb"))
                {
                    Getnumb.Get_numb(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到if关键字,跳转到if函数,执行if语句
                else if (tt[tt_i].Type.Equals("if"))
                {
                    Bif.B_if(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, ref while_or);
                }
                // 读取到if关键字,跳转到if函数,执行if语句
                else if (tt[tt_i].Type.Equals("goto"))
                {
                    Bgoto.B_goto(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap);
                }
                // 读取到note关键字, 跳转到Note函数处理注释
                else if (tt[tt_i].Type.Equals("note"))
                {
                    Note.M_Note(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到putstr函数，输出数组
                else if (tt[tt_i].Type.Equals("putstr"))
                {
                    Putstr.Put_str(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                // 读取到break关键字
                else if (tt[tt_i].Type.Equals("break"))
                {
                    Console.WriteLine("break开始运行");
                    Break.MBreak(ref while_or, ref tt_i);
                }
                // 读取到continue
                else if (tt[tt_i].Type.Equals("continue"))
                {
                    return;
                }
                // 都不满足就将tt_i下标加1,不作处理
                else
                {
                    tt_i++;
                }
            }
        }
        catch (NotDefineVarException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
        catch (NotAVarException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
        catch (WrongGrammarException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
        catch (NotFoundMarkException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
        tt_i = end_num + 2;
        return;
    }
}

