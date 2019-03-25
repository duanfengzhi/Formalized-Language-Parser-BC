using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// goto函数
/// </summary>
public class Bgoto
{
    public static void B_goto(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3, ref Dictionary<string, int> markmap)
    {
        // 判断goto后是否跟着标签变量和分号
        if (tt[tt_i + 1].Type.Equals("var") && tt[tt_i + 2].Code.Equals(";"))
        {
            // 将下标右移一位
            tt_i++;
            // 判断hash表中是否存有该标签
            if (markmap.ContainsKey(tt[tt_i].Code))
            {
                // 将标签对应的指向跳转的下标赋值给tt_i
                int temp;
                markmap.TryGetValue(tt[tt_i].Code, out temp);
                tt_i = temp;
                return;
                // 如果不存在该标签,则抛出异常
            }
            else
            {
                parseresult += "goto语句后的指定标签不存在\r\n";
                throw new NotFoundMarkException("goto语句后的指定标签不存在");
            }

            // 如果goto之后不是标签,则报错并终止程序
        }
        else
        {
            parseresult += "goto语句后的指定标签格式不正确\r\n";
            throw new NotFoundMarkException("goto语句后的指定标签格式不正确");
        }
    }
}
