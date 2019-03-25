using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// B语言while循环
/// 规定 while语句一定要具有左右花括号
/// </summary>
public class BWhile
{
    public static void B_While(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref Dictionary<string, int> markmap, ref bool while_or)
    {
        // while 判断条件开始的地方
        int start = 0;
        // while 判断条件结束的地方
        int end = 0;
        // while 循环体开始的地方
        int start_num = 0;
        // while 循环体结束的地方
        int end_num = 0;
        // 存储左花括号的个数
        int hasQH = 0;

        // 如果在while关键字后不是左括号，则报出语法错误
        if ((!tt[tt_i + 1].Code.Equals("(")))
        {
            parseresult += "语法错误 while语句左括号不存在\r\n";
            throw new WrongGrammarException("语法错误 while语句左括号不存在");
            // 否则将左括号下一位的下标值赋值给判断条件开始处start的值
        }
        else
        {
            start = tt_i + 1;
        }
        // 找到第一个左花括号
        for (; !(tt[tt_i].Code.Equals("{")); tt_i++)
        {
            if (tt[tt_i + 1].Code.Equals("{"))
            {
                // 读到左花括号，将hasQH值加1
                hasQH += 1;
                // 判断左花括号前是否为右括号
                // 如果是，则将右括号前一位的下标赋值给判断条件结束end的值
                // 并且将左花括号下一位的下标赋值给标志循环体开始的地方start_num的值
                // 如果不是，则报错并终止程序
                if ((tt[tt_i].Code.Equals(")")))
                {
                    start_num = tt_i + 2;
                    end = tt_i;
                }
                else
                {
                    parseresult += "语法错误 左右括号不匹配\r\n";
                    throw new WrongGrammarException("语法错误 左右括号不匹配");
                }
            }
        }

        tt_i = tt_i + 1;
        // 判断while语句是否正确
        for (; tt[tt_i + 1] != null; tt_i++)
        {
            // 当读到左花括号时 判断 前面一个是不是右括号
            // 如果不是就报错并终止程序
            if (tt[tt_i].Code.Equals("{"))
            {
                // 读到左花括号，将hasQH值加1
                hasQH += 1;
                // 判断左花括号前是否为右括号
                // 如果不是，则报错并终止程序
                if ((tt[tt_i - 1].Code.Equals(")")))
                {
                    continue;
                }
                else
                {
                    parseresult += "语法错误 左右括号不匹配\r\n";
                    throw new WrongGrammarException("语法错误 左右括号不匹配");
                }
                // 如果没有读取到左花括号而是读取到了右花括号 报错并终止程序
                // 并且如果右花括号前面不是分号 报错并终止程序
            }
            if (tt[tt_i + 1].Code.Equals("}"))
            {
                hasQH -= 1;
                Console.WriteLine(hasQH + " .............");
                if (hasQH == 0)
                {
                    end_num = tt_i;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        if (hasQH != 0)
        {
            parseresult += "语法错误\r\n";
            throw new WrongGrammarException("语法错误");
        }

        /*
         * 将判断条件之间的语句传入WCheck 并且传入while语句循环体的结尾标志end_num 首先检查经过处理之后的判断条件是否为真
         * 如果为真则进入循环体执行 --调用Parser函数 否则直接将全局下标 tt_i指向while循环体的末尾
         */
        // 进行while循环语句
        if (WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start, ref end, ref end_num) == true)
        {
            while (WCheck.W_Check(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref start, ref end, ref end_num) && while_or)
            {
                Parser_ReNamed.Parser_ReNamed1(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, start_num, end_num, ref while_or);
            }
            while_or = true;
        }
        else
        {
            tt_i = end_num;
            return;
        }
        tt_i = end_num + 1;
        return;
    }
}
