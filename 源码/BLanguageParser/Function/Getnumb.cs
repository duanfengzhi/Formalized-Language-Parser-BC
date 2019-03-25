using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;

/// <summary>
/// 输入数值的函数
/// </summary>
public class Getnumb
{
    public static void Get_numb(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        // 判断下一位是否是左括号,下下一位是否是变量，下下下一位是否是右括号，下下下一位是否是分号
        if (tt[tt_i + 1].Code.Equals("(") && tt[tt_i + 2].Type.Equals("var") && tt[tt_i + 3].Code.Equals(")") && tt[tt_i + 4].Code.Equals(";"))
        {
            // 右移一位
            tt_i += 2;

            // 如果在数值型变量中存在，则可以进行输入赋值，赋值给对应字符串型变量
            if (varmap.ContainsKey(tt[tt_i].Code))
            {
                string stringInput = Interaction.InputBox("请输入变量 " + tt[tt_i].Code + " 的值：", "标题", "", -1, -1);
                if (string.ReferenceEquals(stringInput, null) || stringInput.Trim().Equals(""))
                {
                    parseresult += "不可输入空\r\n";
                    throw new NotDefineVarException("不可输入空");
                }
                parseresult += "输入完成：\r\n";
                int temp;
                if (Int32.TryParse(stringInput, out temp))
                    varmap[tt[tt_i].Code] = int.Parse(stringInput);
                else
                {
                    MessageBox.Show("输入数字型变量语句格式错误!", "错误", MessageBoxButtons.OKCancel);
                    parseresult += "语法错误 输入数字型变量语句格式错误\r\n";
                    throw new WrongGrammarException("语法错误 输入数字型变量语句格式错误");
                }
                // 将tt_i加2,下标位于分号，返回Parser函数
                tt_i += 2;
                return;
            }
            else
            {
                parseresult += "变量" + tt[tt_i].Code + "未定义\r\n";
                throw new NotDefineVarException("变量未定义");
            }
        }
        else
        {
            parseresult += "语法错误 输入数字型变量语句格式错误\r\n";
            throw new WrongGrammarException("语法错误 输入数字型变量语句格式错误");
        }
    }
}
