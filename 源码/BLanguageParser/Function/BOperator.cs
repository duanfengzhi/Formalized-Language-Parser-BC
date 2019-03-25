using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 处理操作符的函数
/// </summary>
public class BOperator
{
    public static int Operator(ref WordType[] tt, ref int tt_i, ref MyArray[] a, ref int a_i, ref string parseresult, ref Dictionary<string, int> varmap, ref Dictionary<string, string> varmap2, ref Dictionary<string, MyArray> varmap3)
    {
        Eval eval = new Eval();
        string myString = "";
        if (tt[tt_i].Code.Equals("="))
        {
            if (tt[tt_i - 1].Type.Equals("var") && tt[tt_i + 1].Code.Equals("'") && tt[tt_i + 2].Type.Equals("var") && tt[tt_i + 3].Code.Equals("'") && tt[tt_i + 4].Code.Equals(";"))
            {
                if (varmap.ContainsKey(tt[tt_i - 1].Code))
                {
                    varmap2[tt[tt_i - 1].Code] = tt[tt_i + 2].Code;
                    tt_i += 4;
                    return 1;
                }
            }

            if (tt[tt_i - 1].Code.Equals("]") && tt[tt_i - 3].Code.Equals("[") && tt[tt_i - 4].Type.Equals("var"))
            {
                // 读取数组变量位置
                string arrName = tt[tt_i - 4].Code;

                int placeNumber = 0;
                if (tt[tt_i - 2].Type.Equals("var"))
                {
                    // 变量
                    varmap.TryGetValue(tt[tt_i - 2].Code, out placeNumber);
                }
                else
                {
                    placeNumber = int.Parse(tt[tt_i - 2].Code);
                }
                // 等号右边运算式获取
                tt_i++;
                while (!tt[tt_i].Code.Equals(";"))
                {
                    if (tt[tt_i].Type.Equals("var"))
                    {
                        if (tt[tt_i + 1].Code.Equals("[") && tt[tt_i + 3].Code.Equals("]"))
                        {
                            bool varExistFlag = false;
                            for (int i = 0; i < a_i; i++)
                            {
                                if (a[i].Name.Equals(tt[tt_i].Code))
                                {
                                    varExistFlag = true;
                                    break;
                                }
                            }
                            if (!varExistFlag)
                            {
                                parseresult += "变量" + tt[tt_i].Code + "未定义\r\n";
                                throw new NotDefineVarException("变量未定义");
                            }
                            int placeNumnbertemp = 0;
                            if (tt[tt_i + 2].Type.Equals("var"))
                            {
                                // 变量
                                varmap.TryGetValue(tt[tt_i + 2].Code, out placeNumber);
                            }
                            else
                            {
                                placeNumnbertemp = int.Parse(tt[tt_i + 2].Code);
                            }
                            for (int i = 0; i < a_i; i++)
                            {
                                if (a[i].Name.Equals(tt[tt_i].Code))
                                {
                                    int vartemp = a[i].M_Array[placeNumnbertemp];
                                    myString += vartemp;
                                    break;
                                }
                            }
                            tt_i += 3;
                        }
                        else
                        {
                            myString += varmap[tt[tt_i].Code];
                        }
                    }
                    else
                    {
                        myString += tt[tt_i].Code;
                    }
                    tt_i++;
                }
                try
                {
                    string res = eval.execute(new Evaluator(), myString).Result;
                    // varmap.put(temp, (int) Double.parseDouble(res));
                    // 遍历MyArray数组，查找数组变量名是否被定义
                    for (int i = 0; i < a_i; i++)
                    {
                        if (a[i].Name.Equals(arrName))
                        {
                            a[i].setBMyArray(placeNumber, (int)double.Parse(res));
                            return 1;
                        }
                    }
                    parseresult += "变量未定义\r\n";
                    throw new NotDefineVarException("变量未定义");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            if (tt[tt_i - 1].Type.Equals("var"))
            {
                string temp = tt[tt_i - 1].Code;
                if (!varmap.ContainsKey(temp))
                {
                    Console.WriteLine("wrong!");
                    Environment.Exit(0);
                }
                tt_i++;
                while (!tt[tt_i].Code.Equals(";"))
                {
                    if (tt[tt_i].Type.Equals("var"))
                    {
                        if (tt[tt_i + 1].Code.Equals("[") && tt[tt_i + 3].Code.Equals("]"))
                        {
                            bool varExistFlag = false;
                            for (int i = 0; i < a_i; i++)
                            {
                                if (a[i].Name.Equals(tt[tt_i].Code))
                                {
                                    varExistFlag = true;
                                    break;
                                }
                            }
                            if (!varExistFlag)
                            {
                                parseresult += "变量未定义\r\n";
                                throw new NotDefineVarException("变量未定义");
                            }
                            int placeNumnbertemp = 0;
                            if (tt[tt_i + 2].Type.Equals("var"))
                            {
                                // 变量
                                varmap.TryGetValue(tt[tt_i + 2].Code, out placeNumnbertemp);
                            }
                            else
                            {
                                placeNumnbertemp = int.Parse(tt[tt_i + 2].Code);
                            }
                            for (int i = 0; i < a_i; i++)
                            {
                                if (a[i].Name.Equals(tt[tt_i].Code))
                                {
                                    int vartemp = a[i].M_Array[placeNumnbertemp];
                                    myString += vartemp;
                                    break;
                                }
                            }
                            tt_i += 3;
                        }
                        else
                        {
                            myString += varmap[tt[tt_i].Code];
                        }

                    }
                    else
                    {
                        myString += tt[tt_i].Code;
                    }
                    tt_i++;
                }
                try
                {
                    Console.WriteLine(myString);
                    string res = eval.execute(new Evaluator(), myString).Result;
                    varmap[temp] = (int)double.Parse(res);
                    return 1;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }
            }
            else
            {
                parseresult += "语句错误 执行的参数不是变量\r\n";
                throw new NotAVarException("语句错误 执行的参数不是变量");
            }
        }
        else
        {
            if (tt[tt_i - 1].Type.Equals("var"))
            {
                myString += varmap[tt[tt_i - 1].Code];
            }
            else
            {
                myString += tt[tt_i - 1].Code;
            }
            while (!(tt[tt_i].Code.Equals(";")) && (!(tt[tt_i].Code.Equals(")") && (tt[tt_i + 1].Code.Equals("{") || tt[tt_i + 1].Code.Equals(";")))))
            {

                if (tt[tt_i].Type.Equals("var"))
                {
                    if (varmap.ContainsKey(tt[tt_i].Code))
                    {
                        myString += varmap[tt[tt_i].Code];
                    }
                    else
                    {
                        Console.WriteLine("wrong!");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    myString += tt[tt_i].Code;
                }
                tt_i++;
            }
            try
            {
                Console.WriteLine(myString);
                string res = eval.execute(new Evaluator(), myString).Result;
                return (int)double.Parse(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }
        return 0;
    }
}

