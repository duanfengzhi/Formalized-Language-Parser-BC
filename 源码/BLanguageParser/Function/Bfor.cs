using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// B语言for循环
/// 规定for语句一定要有左右花括号
/// </summary>
public class Bfor
{
    public static void B_for(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        // 圆括号中分号的数目
        int num = 0;
        // 第一部分开始与结束指针
        int exp1_start;
        int exp1_end;
        // 第二部分开始与结束指针
        int exp2_start;
        int exp2_end;
        // 第三部分开始与结束指针
        int exp3_start;
        int exp3_end = 0;
        // 循环部分开始与结束指针
        int start;
        int end = 0;
        // 判断for语句中分号的数目
        for (int temp = tt_i; !tt[temp].Code.Equals(")"); temp++)
        {
            if (tt[temp].Code.Equals(";"))
            {
                num++;
            }
        }
        // 对于for语句的检查
        if (num != 2)
        {
            Console.WriteLine("wrong!5 -- for语句不正确");
            Environment.Exit(0);
        }
        else
        {
            if (!tt[tt_i + 1].Code.Equals("("))
            {
                Console.WriteLine("wrong!4 -- 没有左括号");
                Environment.Exit(0);
            }
            else
            {
                // 第一部分
                exp1_start = tt_i + 2;
                for (; !tt[tt_i].Code.Equals(";"); tt_i++)
                {
                }
                exp1_end = tt_i;
                // 判断语句
                exp2_start = tt_i + 1;
                tt_i++;
                for (; !tt[tt_i].Code.Equals(";"); tt_i++)
                {
                }
                exp2_end = tt_i;
                // 第三部分
                exp3_start = tt_i + 1;
                for (; !tt[tt_i].Code.Equals(")"); tt_i++)
                {
                }
                exp3_end = tt_i;
                // 为了使语句完整，把最后一条语句后右括号变成分号
                tt[tt_i].Code = ";";
                tt[tt_i].Type = "quotation";
                // 对于for语句的检查
                if (!tt[tt_i + 1].Code.Equals("{"))
                {
                    Console.WriteLine("wrong!6 -- 没有左花括号");
                    Environment.Exit(0);
                }
                else
                {
                    // 循环部分
                    start = tt_i + 2;
                    for (; !tt[tt_i].Code.Equals("}"); tt_i++)
                    {
                        end = tt_i;
                    }
                    // System.out.println("suc");
                    // 先执行第一部分
                    FParser.F_Parser(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, exp1_start, exp1_end);
                    try
                    {
                        if (WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref exp2_start, ref exp2_end, ref end) == true)
                        {
                            // 进入循环语句
                            while (WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref exp2_start, ref exp2_end, ref end))
                            {
                                // 调用FParser，进行循环语句的执行
                                FParser.F_Parser(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, start, end);
                                FParser.F_Parser(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, exp3_start, exp3_end);
                            }
                        }
                        else
                        {
                            // 当判断语句为假时直接将全局指针指向do-while语句末尾
                            tt_i = end + 1;
                            return;
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
                    // 将全局指针指向do-while语句末尾
                    tt_i = end + 1;
                    return;
                }
            }
        }
    }

}

