using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// do_while循环
/// 规定do后面语句用花括号括起来
/// </summary>
public class BDoWhile
{
    public static void B_Dowhile(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref Dictionary<string, int> markmap, ref bool while_or)
    {
        // if 判断条件开始的地方
        int start = 0;
        // if 判断条件结束的地方
        int end = 0;
        // if 循环体开始的地方
        int start_num = 0;
        // if 循环体结束的地方
        int end_num = 0;
        // 判断是否有while语句
        int has_while = tt_i;
        // 判断do后面是否是左花括号
        if (!tt[tt_i + 1].Code.Equals("{"))
        {
            // 如果不是报出异常并停止程序
            parseresult += "语法错误 do_while语句左花括号不存在\r\n";
            throw new WrongGrammarException("语法错误 do_while语句左花括号不存在");
        }
        else
        {
            // 当花括号输入正常时
            // 全局指针后移至do需要执行的语句
            tt_i += 2;
            // 将全局指针赋值给循环开始标志
            start_num = tt_i;
            // 指针后移，直到寻找到对应右花括号
            for (; !tt[tt_i].Code.Equals("}"); tt_i++)
            {
                // 将全局指针赋值给循环结束变量
                // 将判断语句传入Parser时包括判断语句前后的圆括号
                end_num = tt_i;
            }
            // 判断右花括号后面是否存在while语句
            // 当存在while语句时
            if (tt[tt_i + 1].Code.Equals("while"))
            {
                // 当while关键字后没有左括号时
                if (!tt[tt_i + 2].Code.Equals("("))
                {
                    // 报出异常并停止程序
                    parseresult += "语法错误 do_while语句左括号不存在\r\n";
                    throw new WrongGrammarException("语法错误 do_while语句左括号不存在");
                }
                else
                {
                    // while后括号正常
                    // 全局指针移至判断语句
                    tt_i += 3;
                    // 将左括号下标赋给判断开始语句
                    start = tt_i - 1;
                    // 指针后移，直到寻找到对应右括号
                    for (; !tt[tt_i].Code.Equals(")"); tt_i++)
                    {
                        // 将全局指针赋值给判断结束变量
                        end = tt_i + 1;
                    }
                    // 判断结束变量的值最终为右括号
                }
            }
            else
            {
                // 当循环语句结束后没有找到while关键字时
                // 报出异常并停止程序
                parseresult += "语法错误 do_while语句while关键字不存在\r\n";
                throw new WrongGrammarException("语法错误 do_while语句while关键字不存在");
            }
        }
        // 先将循环语句执行一次
        Parser_ReNamed.Parser_ReNamed1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, start_num, end_num, ref while_or);
        // 调用WCheck方法，进行判断语句的判断
        // 判断语句为真时
        if (WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start, ref end, ref end_num) == true)
        {
            // 进入循环语句
            while (WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start, ref end, ref end_num))
            {
                // 调用Parser，进行循环语句的执行
                Parser_ReNamed.Parser_ReNamed1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, start_num, end_num, ref while_or);
            }
        }
        else
        {
            // 当判断语句为假时直接将全局指针指向do-while语句末尾
            tt_i = end + 1;
            return;
        }
        // 将全局指针指向do-while语句末尾
        tt_i = end + 1;
        return;
    }
}

