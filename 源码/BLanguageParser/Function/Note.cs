using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 处理B语言注释体的函数
public class Note
{
    public static void M_Note(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        // 是否有左注释的标志
        bool hasLeft = false;

        for (; tt[tt_i + 1] != null; tt_i++)
        {
            if (tt[tt_i].Code.Equals("/*"))
            {
                hasLeft = true;
                continue;
            }
            else if (tt[tt_i].Code.Equals("*/"))
            {
                if (hasLeft == true)
                {
                    hasLeft = false;
                }
                else
                {
                    parseresult += "请检查注释语句\r\n";
                    throw new WrongGrammarException("请检查注释语句");
                }
                tt_i += 1;
                return;
            }
            else
            {
                continue;
            }
        }
        if (tt[tt_i + 1] == null || hasLeft == true)
        {
            parseresult += "请检查注释语句\r\n";
            throw new WrongGrammarException("请检查注释语句");
        }
    }
}

