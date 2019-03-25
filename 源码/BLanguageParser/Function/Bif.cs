using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// B语言的if
/// 规定 if-else语句一定要具有左右花括号
/// </summary>
public class Bif
{
    public static void B_if(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref Dictionary<string, int> markmap, ref bool while_or)
    {
        // if 判断条件开始的地方
        int start = 0;
        // if 判断条件结束的地方
        int end = 0;
        // if 循环体开始的地方
        int start_num = 0;
        // if 循环体结束的地方
        int end_num = 0;
        // else 循环体开始的地方
        int estart_num = 0;
        // else 循环体结束的地方
        int eend_num = 0;
        // 是否有左花括号
        bool hasQH = false;
        // 是否有else语句
        bool hasElse = false;

        if ((!tt[tt_i + 1].Code.Equals("(")))
        {
            parseresult += "语法错误 if语句左括号不存在\r\n";
            throw new WrongGrammarException("语法错误 if语句左括号不存在");
        }
        else
        {
            start = tt_i + 1;
        }

        // 判断if语句是否正确
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
                    parseresult += "语法错误 if语句左右括号不匹配\r\n";
                    throw new WrongGrammarException("语法错误 if语句左右括号不匹配");
                }
                // 如果没有读取到左花括号而是读取到了右花括号 报错并终止程序
                // 并且如果右花括号前面不是分号 报错并终止程序
                // 在判断结束后，将左花括号是否存在的标志设为错误
                // 以便于进行else语句的判断
            }
            if (tt[tt_i + 1].Code.Equals("}"))
            {
                end_num = tt_i;
                if (hasQH == false || !(tt[tt_i].Code.Equals(";")))
                {
                    parseresult += "语法错误 if语句左右花括号不匹配\r\n";
                    throw new WrongGrammarException("语法错误 if语句左右花括号不匹配");
                }
                else
                {
                    hasQH = false;
                }
            }
        }

        // 判断是否存在else语句
        if (tt[tt_i + 1] != null)
        {
            tt_i += 1;
            if (tt[tt_i].Code.Equals("else"))
            {
                hasElse = true;
                // 判断else语句是否正确
                for (; !(tt[tt_i].Code.Equals("}")); tt_i++)
                {
                    if (tt[tt_i].Code.Equals("{"))
                    {
                        hasQH = true;
                        estart_num = tt_i + 1;
                    }
                    if (tt[tt_i + 1].Code.Equals("}"))
                    {
                        eend_num = tt_i;
                        if (hasQH == false || !(tt[tt_i].Code.Equals(";")))
                        {
                            parseresult += "语法错误 else语句异常\r\n";
                            throw new WrongGrammarException("语法错误 else语句异常");
                        }
                    }
                }
            }
            else
            {
                hasElse = false;
            }
        }

        // 执行if-else语句
        // 当else语句存在 并且if语句的判断条件错误
        if (hasElse == true && WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start, ref end, ref end_num) == false)
        {
            // 传入的结尾标志为 else语句的结尾
            Parser_ReNamed.Parser_ReNamed1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, estart_num, eend_num, ref while_or);
            return;
            // 当else语句不存在 并且if语句的判断条件为真
        }
        else if (hasElse == false && WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start, ref end, ref end_num) == true)
        {
            // 传入的结尾标志为 if语句的结尾
            Parser_ReNamed.Parser_ReNamed1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, start_num, end_num, ref while_or);
            return;
            // 当else语句不存在 并且if语句的判断条件为真
        }
        else if (hasElse == false && WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start, ref end, ref end_num) == false)
        {
            // 直接返回
            return;
            // 当else语句存在 并且if语句的判断条件为真
        }
        else if (hasElse == true && WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start, ref end, ref end_num) == true)
        {
            // 传入的结尾标志为 else语句的结尾
            Parser_ReNamed.Parser_ReNamed1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, start_num, end_num, ref while_or);
            return;
        }
    }
}

