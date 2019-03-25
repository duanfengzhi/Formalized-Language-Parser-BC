using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 输出字符串
/// </summary>
public class Putchar
{
    public static void Put_char(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        // 计算单引号的个数
        int count = 0;

        // 判断下一位是不是左括号
        // 如果不是左括号 报错并终止程序
        if ((!tt[tt_i + 1].Code.Equals("(")))
        {
            parseresult += "语法错误 输出字符串语句左括号不存在\r\n";
            throw new WrongGrammarException("语法错误 输出字符串语句左括号不存在");
            // 如果是左括号，将当前全局下标tt_i加1
        }
        else
        {
            tt_i += 2;
        }
        // 判断当前位置是不是单引号 如过是单引号
        // 就将count赋值为1 并且下标加一、进入循环 输出两个单引号里的内容
        if (tt[tt_i].Code.Equals("'"))
        {
            tt_i = tt_i + 1;
            count = 1;
            for (; count < 2; tt_i++)
            {
                if (tt[tt_i + 1].Code.Equals("'"))
                {
                    count += 1;
                }
                // 判断是否读取到了换行标志 *n
                // 如果没有读到 将当前tt_i指向的Code打印输出
                if (!tt[tt_i].Code.Equals("*n"))
                {
                    parseresult += tt[tt_i].Code + " ";
                    // 如果读到了 就打印输出换行，
                    // 并且将下标tt_i加1
                }
                else
                {
                    Console.WriteLine();
                    parseresult += "\r\n";
                    continue;
                }
            }
            parseresult += "\r\n";
            return;
            // 判断下一位是否是变量
        }
        else if (tt[tt_i].Type.Equals("var"))
        {
            // 判断变量之后是否跟着右括号和分号
            if (tt[tt_i + 1].Code.Equals(")") && tt[tt_i + 2].Code.Equals(";"))
            {
                // 如果对应的数值型变量不为空，且对应的字符型变量也不为空，则输出字符型变量值
                if (varmap.ContainsKey(tt[tt_i].Code) && !string.ReferenceEquals(varmap2[tt[tt_i].Code], null))
                {
                    Console.WriteLine(varmap2[tt[tt_i].Code]);
                    parseresult += varmap2[tt[tt_i].Code] + "\r\n";
                    tt_i += 2;
                    return;
                    // 如果对应的数值型变量不为空，而对应的字符型变量为空，则输出数字型变量值
                }
                else if (varmap.ContainsKey("[tt[tt_i].Code]") && string.ReferenceEquals(varmap2[tt[tt_i].Code], null))
                {
                    Console.WriteLine(varmap[tt[tt_i].Code]);
                    parseresult += varmap[tt[tt_i].Code] + "\r\n";
                    tt_i += 2;
                    return;
                    // 如果找不到对应的变量值 则报错
                }
                else
                {
                    parseresult += "变量未定义\r\n";
                    throw new NotDefineVarException("变量未定义");
                }
            }
            // 如果读到的不是单引号 也不是变量名 则报错
        }
        else
        {
            parseresult += "语法错误 输出数字语句格式错误\r\n";
            throw new WrongGrammarException("语法错误 输出字符串语句格式错误");
        }
    }
}

