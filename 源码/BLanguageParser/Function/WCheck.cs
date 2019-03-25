using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// while语句执行条件判断函数
/// </summary>
public class WCheck
{
    public static bool W_Check(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref int start, ref int end, ref int end_num)
    {
        // 将初始下标指向传过来的while语句判断条件开始的地方
        tt_i = start;
        // 遍历 直到下标tt_i指向传过来的 while语句判断语句结尾标志 end
        for (; tt_i < end; tt_i++)
        {
            // 当读取到操作符时 跳转执行 operator函数
            if (tt[tt_i].Type.Equals("operator"))
            {
                // 定义一个变量来存储返回类型
                int temp = BOperator.Operator(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                // 如果变量temp是0 返回false 同时让下标tt_i指向while语句的结尾
                // 如果变量temp是非0 返回 true
                if (temp == 0)
                {
                    tt_i = end_num;
                    return false;
                }
                else
                {
                    return true;
                }
                // 当读取到类型是数字 并且下一位是右括号时
            }
            else if (tt[tt_i].Type.Equals("number") && tt[tt_i + 1].Code.Equals(")"))
            {
                // 将对应的Code转换为int型赋值给temp
                int temp = int.Parse(tt[tt_i].Code);
                // 如果变量temp是0 返回false 同时让下标tt_i指向while语句的结尾
                // 如果变量temp是非0 返回 true
                if (temp == 0)
                {
                    tt_i = end_num;
                    return false;
                }
                else
                {
                    return true;
                }
                // 当读取到类型是变量 并且下一位是右括号时
            }
            else if (tt[tt_i].Type.Equals("var") && tt[tt_i + 1].Code.Equals(")"))
            {
                // 如果变量map里存在该变量 进行下一步判断
                if (varmap.ContainsKey(tt[tt_i].Code))
                {
                    // 将map里变量对应的值赋值给临时变量temp
                    int temp;
                    varmap.TryGetValue(tt[tt_i].Code, out temp);
                    // 如果变量temp是0 返回false 同时让下标tt_i指向while语句的结尾
                    // 如果变量temp是非0 返回 true
                    if (temp == 0)
                    {
                        tt_i = end_num;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    // 如果变量map里不存在该变量 抛出异常
                }
                else
                {
                    parseresult += "变量" + tt[tt_i].Code + "未定义\r\n";
                    throw new NotDefineVarException("变量未定义");
                }
            }
        }
        return true;
    }
}
