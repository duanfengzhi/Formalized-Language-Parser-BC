using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// switch语句的使用
/// </summary>
public class Bswitch
{
    /// <summary>
    /// 判断switch语句的条件
    /// </summary>
    public static int Scheck1(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref int start)
    {
        //数组下标指向判定条件开始的地方
        tt_i = start;
        //用于存储条件的值
        int temp;
        //返回条件变量的值
        varmap.TryGetValue(tt[tt_i].Code, out temp);
        return temp;

    }

    public static string Scheck2(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref int start)
    {
        //数组下标指向判定条件开始的地方
        tt_i = start;
        //用于存储条件的值
        string temp;
        //返回条件变量的值
        temp = varmap2[tt[tt_i].Code];
        return temp;
    }

    /// <summary>
    /// switch语句的使用
    /// </summary>
    public static void B_switch(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref Dictionary<string, int> markmap, ref bool while_or)
    {
        // switch判断条件开始的地方
        int start = 0;
        // switch判断条件结束的地方
        int end = 0;
        // switch 循环体开始的地方
        int start_num = 0;
        // switch 循环体结束的地方
        int end_num = 0;
        // case语句开始的地方
        int cstart_num = 0;
        // case语句结束的地方
        int cend_num = 0;
        //用来存储tt_i的中间取值，回到switch开始的地方
        int Stemp = 0;
        bool hasQH = false;
        // 是否有左花括号
        bool hasCase = false;
        // 是否有case语句
        bool hasMH = false;
        //是否有冒号

        if ((!tt[tt_i + 1].Code.Equals("(")))
        {
            Console.WriteLine("wrong!173 -- 找不到左括号");
            Environment.Exit(0);
        }
        else
        {
            start = tt_i + 1;
        }

        //判断switch语句的条件是否存在
        //保留当前tt_i的取值
        Stemp = tt_i;
        for (; !(tt[tt_i].Code.Equals("}")); tt_i++)
        {
            // 当读到左花括号时 判断 前面一个是不是右括号
            // 如果不是就报错并终止程序
            if (tt[tt_i].Code.Equals("{"))
            {
                hasQH = true;
                if ((tt[tt_i - 1].Code.Equals(")")))
                {
                    start_num = tt_i + 1;
                    end = tt_i - 1;
                }
                else
                {
                    Console.WriteLine("wrong!201 -- 请检查switch语句是否正确");
                    Environment.Exit(0);
                }

            }
            // 如果没有读取到左花括号而是读取到了右花括号 报错并终止程序
            // 并且如果右花括号前面不是分号 报错并终止程序
            // 在判断结束后，将左花括号是否存在的标志设为错误
            if (tt[tt_i + 1].Code.Equals("}"))
            {
                end_num = tt_i;
                if (hasQH == false || !(tt[tt_i].Code.Equals(";")))
                {
                    Console.WriteLine("wrong!201 -- 请检查switch语句是否正确");
                    Environment.Exit(0);
                }
                else
                {
                    hasQH = false;
                }
            }
        }

        //回到当初的tt_i取值
        tt_i = Stemp;
        if (tt[tt_i + 1] != null)
        {
            tt_i += 1;
            // 判断是否存在case语句
            if (tt[tt_i].Code.Equals("case"))
            {
                hasCase = true;
                // 判断case语句是否正确
                for (; !(tt[tt_i].Code.Equals("break")); tt_i++)
                {
                    if (tt[tt_i].Code.Equals(":"))
                    {
                        hasMH = true;
                        cstart_num = tt_i + 1;
                    }
                    //判断单个case语句何时结束
                    if (tt[tt_i + 1].Code.Equals("break"))
                    {
                        cend_num = tt_i;
                        if (!(tt[tt_i].Code.Equals(";")))
                        {
                            Console.WriteLine("wrong!233 -- 请检查case语句是否正确");
                            Environment.Exit(0);
                        }
                    }
                }
            }
            else
            {
                hasCase = false;
            }
        }

        //执行switch语句
        tt_i = Stemp;
        //创建一个整数型数组，用于存储每次出现case时的位置
        int[] case_position = new int[100];
        //case_position数组的下标
        int cposition = 0;
        //循环记录每次出现case的地方
        for (; !tt[tt_i].Code.Equals("}"); tt_i++)
        {
            if (tt[tt_i].Code.Equals("case"))
            {
                case_position[cposition] = tt_i;
                cposition++;
            }
        }
        //调取条件变量的取值
        //如果条件变量是数字型
        if (tt[start].Type.Equals("number"))
        {
            for (int i = 0; i <= cposition; cposition++)
            {
                if (Scheck1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start) == tt[case_position[i] + 1].GetHashCode())
                {
                    Parser_ReNamed.Parser_ReNamed1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, case_position[i] + 3, case_position[i + 1] - 1, ref while_or);
                }
            }
        }
        else
        {
            //如果条件变量是字符型
            for (int i = 0; i <= cposition; cposition++)
            {
                if (string.ReferenceEquals(Scheck2(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start), tt[case_position[i] + 2].Code))
                {
                    Parser_ReNamed.Parser_ReNamed1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, case_position[i] + 4, case_position[i + 1] - 1, ref while_or);
                }
            }
        }
    }
}
