using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 定义变量
/// </summary>
public class Auto
{
    public static void M_Auto(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        // 如果是要定义数组，判断是否为变量[数值]结构
        if (tt[tt_i + 1].Type.Equals("var") && tt[tt_i + 2].Code.Equals("[") && tt[tt_i + 3].Type.Equals("number") && tt[tt_i + 4].Code.Equals("]"))
        {
            // 将B语言规定的数组长度保存为临时变量temp中
            int temp = int.Parse(tt[tt_i + 3].Code);

            // 创建一个MyArray类保存该数组
            a[a_i++] = new MyArray(tt[tt_i + 1].Code, temp);

            // 有以下标，至逗号或分号处
            tt_i += 4;

            // 如果下一位是分号则返回出去
            if (tt[tt_i + 1].Code.Equals(";"))
            {
                tt_i++;
                return;
                // 如果下一位是逗号，则递归
            }
            else if (tt[tt_i + 1].Code.Equals(","))
            {
                tt_i++;
                M_Auto(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                // 若在他们之后不是逗号和分号，则报出语法错误并终止程序
            }
            else
            {
                parseresult += "定义变量语句出错\r\n";
                throw new WrongGrammarException("定义变量语句出错");
            }
            // 如果下一位是变量，则放入变量栈中
        }
        else if (tt[tt_i + 1].Type.Equals("var"))
        {
            // 同时放入数值型和字符串型变量map中
            varmap[tt[tt_i + 1].Code] = 0;
            varmap2[tt[tt_i + 1].Code] = null;

            // 将下标右移至变量处
            tt_i++;

            // 如果下下一位是分号，则下标加一并返回去
            if ((tt[tt_i + 1].Code.Equals(";")))
            {
                tt_i++;
                return;
                // 如果下一位是等号，则进行赋值处理后返回主函数
            }
            else if ((tt[tt_i + 1].Code.Equals("=")))
            {
                tt_i++;
                try
                {
                    BOperator.Operator(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }
                catch (NotDefineVarException e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
                return;
                // 如果下一位是逗号，则递归
            }
            else if (tt[tt_i + 1].Code.Equals(","))
            {
                tt_i++;
                M_Auto(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                // 如果不是上述情况，则出现语法错误，报错并终止程序
            }
            else
            {
                parseresult += "定义变量语句出错\r\n";
                throw new WrongGrammarException("定义变量语句出错");
            }
            // 如果不是上述情况，则出现语法错误，报错并终止程序
        }
        else
        {
            parseresult += "定义变量语句出错\r\n";
            throw new WrongGrammarException("定义变量语句出错");
        }
    }
}

