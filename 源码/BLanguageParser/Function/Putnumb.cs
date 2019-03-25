using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 输出数字函数
/// </summary>
public class Putnumb
{
    public static void Put_numb(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        // 判断下一位是否是左括号
        if (tt[tt_i + 1].Code.Equals("("))
        {
            // 右移一位至左括号处
            tt_i++;
            // 判断括号后是否是变量
            if (tt[tt_i + 1].Type.Equals("var"))
            {
                // 右移一位至变量处
                tt_i++;
                // 判断变量后是否跟着右括号和分号
                if (tt[tt_i + 1].Code.Equals(")") && tt[tt_i + 2].Code.Equals(";"))
                {
                    // 判断在数值型变量map中是否存在该变量，存在就输出
                    if (varmap.ContainsKey(tt[tt_i].Code))
                    {
                        Console.WriteLine(varmap[tt[tt_i].Code]);
                        parseresult += varmap[tt[tt_i].Code] + "\r\n";
                        // 下标移至分号处，返回主函数
                        tt_i += 2;
                        return;
                        // 不存在报出变量未定义，并终止程序
                    }
                    else
                    {
                        throw new NotDefineVarException("变量未定义");
                    }
                    // 判断变量后是否跟着[number]);
                }
                else if (tt[tt_i + 1].Code.Equals("[") && tt[tt_i + 3].Code.Equals("]") && tt[tt_i + 4].Code.Equals(")") && tt[tt_i + 5].Code.Equals(";"))
                {
                    // 如果中间一位是变量
                    if (tt[tt_i + 2].Type.Equals("var"))
                    {
                        // 判断下标变量是否存在
                        if (varmap.ContainsKey(tt[tt_i + 2].Code))
                        {
                            // 将变量值暂时保存，稍后作为数组下标
                            int numbtemp;
                            varmap.TryGetValue(tt[tt_i + 2].Code, out numbtemp);
                            // 判断数组变量是否存在
                            for (int i = 0; i < a_i; i++)
                            {
                                if (a[i].Name.Equals(tt[tt_i].Code))
                                {
                                    Console.WriteLine(a[i].M_Array[numbtemp]);
                                    parseresult += a[i].M_Array[numbtemp] + "\r\n";
                                    return;
                                }
                            }
                            // 抛出异常
                            parseresult += "找不到要输出的内容\r\n";
                            throw new NotAVarException("找不到要输出的内容");
                        }
                        else
                        {
                            // 变量未定义
                            parseresult += "变量未定义\r\n";
                            throw new NotDefineVarException("变量未定义");
                        }
                        // 如果中间一位是纯数字
                    }
                    else if (tt[tt_i + 2].Type.Equals("number"))
                    {
                        // 判断
                        int numbtemp = int.Parse(tt[tt_i + 2].Code);
                        // 判断数组变量是否存在
                        for (int i = 0; i < a_i; i++)
                        {
                            if (a[i].Name.Equals(tt[tt_i].Code))
                            {
                                Console.WriteLine(a[i].M_Array[numbtemp]);
                                parseresult += a[i].M_Array[numbtemp] + "\r\n";
                                return;
                            }
                        }
                        // 抛出异常
                        parseresult += "找不到要输出的内容\r\n";
                        throw new NotAVarException("找不到要输出的内容");
                    }
                    else
                    {

                    }
                    // 若不满足变量后是否跟着右括号和分号，则报出语法错误，并终止程序
                }
                else
                {
                    parseresult += "语法错误 输出数字语句格式错误\r\n";
                    throw new WrongGrammarException("语法错误 输出数字语句格式错误");
                }
                // 判断括号后是否是数字常量
            }
            else if (tt[tt_i + 1].Type.Equals("number"))
            {
                tt_i++;
                if (tt[tt_i + 1].Code.Equals(")") && tt[tt_i + 2].Code.Equals(";"))
                {
                    Console.WriteLine(tt[tt_i].Code);
                    parseresult += tt[tt_i].Code + "\r\n";
                    tt_i += 2;
                    return;
                }
                // 若括号后既不是变量也不是数字常量，则报出语法错误并终止程序
            }
            else
            {
                parseresult += "找不到要输出的内容\r\n";
                throw new NotAVarException("找不到要输出的内容");
            }
            // 如果关键字putnumb后没有紧跟着左括号，则报出语法错误并终止程序
        }
        else
        {
            parseresult += "语法错误 输出数字语句格式错误\r\n";
            throw new WrongGrammarException("语法错误 输出数字语句格式错误");
        }
    }
}
