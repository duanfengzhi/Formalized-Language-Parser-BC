using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 处理自增自减符号
public class UnaryOperation
{
    public static int Unary_Operation(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        // ++情况，分为在变量之前和在变量之后两种情况
        if (tt[tt_i].Code.Equals("++"))
        {
            // 判断是不是变量++; 或者变量++)的情况
            if (tt[tt_i - 1].Type.Equals("var") && (tt[tt_i + 1].Code.Equals(";") || tt[tt_i + 1].Code.Equals(")")))
            {
                // 判断变量是否被声明
                if (varmap.ContainsKey(tt[tt_i - 1].Code))
                {
                    int temp;
                    varmap.TryGetValue(tt[tt_i - 1].Code, out temp);
                    varmap[tt[tt_i - 1].Code] = temp + 1;
                    tt_i++;
                    return temp;
                }
                else
                {
                    parseresult += "变量" + tt[tt_i - 1].Code + "未定义\r\n";
                    throw new NotDefineVarException("变量未定义");
                }
                // 判断是不是++变量; 或者++变量)的情况
            }
            else if (tt[tt_i + 1].Type.Equals("var") && (tt[tt_i + 2].Code.Equals(";") || tt[tt_i + 2].Code.Equals(")")))
            {
                // 判断变量是否被声明
                if (varmap.ContainsKey(tt[tt_i + 1].Code))
                {
                    int temp;
                    varmap.TryGetValue(tt[tt_i + 1].Code, out temp);
                    varmap[tt[tt_i + 1].Code] = temp + 1;
                    tt_i += 2;
                    return temp + 1;
                }
                else
                {
                    parseresult += "变量" + tt[tt_i + 1].Code + "未定义\r\n";
                    throw new NotDefineVarException("变量未定义");
                }
            }
            else
            {
                // 抛出语句错误
                parseresult += "语法错误 \r\n";
                throw new WrongGrammarException("语法错误\r\n");
            }
            // --情况，分为在变量之前和在变量之后两种情况
        }
        else
        {
            // 判断是不是变量--; 或者变量--)的情况
            if (tt[tt_i - 1].Type.Equals("var") && (tt[tt_i + 1].Code.Equals(";") || tt[tt_i + 1].Code.Equals(")")))
            {
                // 判断变量是否被声明
                if (varmap.ContainsKey(tt[tt_i - 1].Code))
                {
                    int temp;
                    varmap.TryGetValue(tt[tt_i - 1].Code, out temp);
                    varmap[tt[tt_i - 1].Code] = temp - 1;
                    tt_i++;
                    return temp;
                }
                else
                {
                    parseresult += "变量" + tt[tt_i - 1].Code + "未定义\r\n";
                    throw new NotDefineVarException("变量未定义");
                }
                // 判断是不是++变量; 或者++变量)的情况
            }
            else if (tt[tt_i + 1].Type.Equals("var") && (tt[tt_i + 2].Code.Equals(";") || tt[tt_i + 2].Code.Equals(")")))
            {
                // 判断变量是否被声明
                if (varmap.ContainsKey(tt[tt_i + 1].Code))
                {
                    int temp;
                    varmap.TryGetValue(tt[tt_i + 1].Code, out temp);
                    varmap[tt[tt_i + 1].Code] = temp - 1;
                    tt_i += 2;
                    return temp - 1;
                }
                else
                {
                    parseresult += "变量" + tt[tt_i + 1].Code + "未定义\r\n";
                    throw new NotDefineVarException("变量未定义");
                }
            }
            else
            {
                // 抛出语句错误
                parseresult += "语法错误 \r\n";
                throw new WrongGrammarException("语法错误\r\n");
            }

        }
    }
}

