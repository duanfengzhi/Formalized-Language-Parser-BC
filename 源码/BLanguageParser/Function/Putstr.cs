using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 输出数组
/// </summary>
public class Putstr
{
    public static void Put_str(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        // 判断下一位是否是左括号
        if (tt[tt_i + 1].Code.Equals("("))
        {
            // 右移至左括号
            tt_i++;
            if (tt[tt_i + 1].Type.Equals("var") && tt[tt_i + 2].Code.Equals(")") && tt[tt_i + 3].Code.Equals(";"))
            {
                for (int i = 0; i < a_i; i++)
                {
                    if (a[i].Name.Equals(tt[tt_i + 1].Code))
                    {
                        parseresult += a[i].putBMyArray() + "\r\n";
                        tt_i += 3;
                        return;
                    }
                    parseresult += "数组" + tt[tt_i + 1].Code + "未定义\r\n";
                    throw new WrongGrammarException("语法错误 输出数字语句格式错误");
                }
            }
            else
            {
                parseresult += "语法错误 输出数字语句格式错误\r\n";
                throw new WrongGrammarException("语法错误 输出数字语句格式错误");
            }
        }
        else
        {
            parseresult += "语法错误 输出数字语句格式错误\r\n";
            throw new WrongGrammarException("语法错误 输出数字语句格式错误");
        }
    }
}

