using System;
using System.Collections.Generic;

public class Parser
{
    // 变量的map，存储变量名和变量值，保存数值型变量
    Dictionary<string, int> varmap = new Dictionary<string, int>();
    // 保存字符串型变量的map
    Dictionary<string, string> varmap2 = new Dictionary<string, string>();
    // 保存数组型变量的map
    Dictionary<string, MyArray> varmap3 = new Dictionary<string, MyArray>();
    // goto标签的map，存储标签名，和序列下标
    Dictionary<string, int> markmap = Compile.markmap;
    WordType[] tt = Compile.tt;
    // 初始化一个MyArray类数组，用于保存数组类型变量
    MyArray[] a = new MyArray[10];
    // 作为MyArray类数组的下标
    int a_i = 0;
    // 设置是否break的标志
    bool while_or = true;
    // 作为遍历词法分析器生成的序列的下标
    int tt_i = 0;
    // 解释结果
    public string parseresult = "编译结果:\r\n";

    // 遍历词法分析器序列，对于不同类型的关键字，跳转到特殊函数执行
    public void BParser()
    {
        try
        {
            // 当词法分析器序列tt没有读尽，就继续处理
            while (tt[tt_i] != null)
            {
                // 读取到auto关键字,跳转到auto函数处理
                if (tt[tt_i].Type.Equals("auto"))
                {
                    Auto.M_Auto(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);

                }   // 读取到putnumb关键字,跳转到putnumb函数,输出数值型变量值
                else if (tt[tt_i].Type.Equals("putnumb"))
                {
                    Putnumb.Put_numb(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }   // 读取到putchar关键字,跳转到putchar函数,输出字符型型变量值
                else if (tt[tt_i].Type.Equals("putchar"))
                {
                    Putchar.Put_char(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);

                }   // 单目运算，自增自减
                else if (tt[tt_i].Code.Equals("++") || tt[tt_i].Code.Equals("--"))
                {
                    UnaryOperation.Unary_Operation(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);

                }   // 读取到类型为operator,即操作符类型，跳转到operator函数
                else if (tt[tt_i].Type.Equals("operator"))
                {
                    BOperator.Operator(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);

                }   // 读取到关键字getchar,跳转到getchar函数进行读入字符串操作
                else if (tt[tt_i].Type.Equals("getchar"))
                {
                    Getchar.Get_char(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }   // 读取到关键字getnumb,跳转到getnumb函数执行读入数值操作
                else if (tt[tt_i].Type.Equals("getnumb"))
                {
                    Getnumb.Get_numb(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }   // 读取到关键字goto,跳转到Bgoto函数处理goto语句
                else if (tt[tt_i].Type.Equals("goto"))
                {
                    Bgoto.B_goto(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap);
                }   // 读取到if关键字,跳转到Bif函数处理if语句
                else if (tt[tt_i].Type.Equals("if"))
                {
                    Bif.B_if(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, ref while_or);
                }   // 读取到while关键字, 跳转到BWhile函数处理while循环语句
                else if (tt[tt_i].Type.Equals("while"))
                {
                    BWhile.B_While(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, ref while_or);
                }   // 读取到do关键字,跳转到BDo_while函数执行do while函数执行
                else if (tt[tt_i].Type.Equals("do"))
                {
                    BDoWhile.B_Dowhile(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, ref while_or);
                }   // 读取到for关键字，执行for循环
                else if (tt[tt_i].Type.Equals("for"))
                {
                    Bfor.B_for(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }   // 读取到note关键字, 跳转到Note函数处理注释
                else if (tt[tt_i].Type.Equals("note"))
                {
                    Note.M_Note(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }   // 读取到putstr函数，输出数组
                else if (tt[tt_i].Type.Equals("putstr"))
                {
                    Putstr.Put_str(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3);
                }   // 读取到switch
                else if (tt[tt_i].Type.Equals("switch"))
                {
                    Bswitch.B_switch(ref tt, ref tt_i, ref a, ref a_i, ref parseresult, ref varmap, ref varmap2, ref varmap3, ref markmap, ref while_or);
                }   // 都不满足就将tt_i下标加1,不作处理
                else
                {
                    tt_i++;
                }
            }
        }
        catch (NotDefineVarException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
        catch (NotAVarException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
        catch (WrongGrammarException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
        catch (NotFoundMarkException e)
        {
            Console.WriteLine(e.ToString());
            Console.Write(e.StackTrace);
        }
    }
}
