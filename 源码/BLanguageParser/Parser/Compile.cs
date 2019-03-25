using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
public class Compile
{
    // 代码所属类别的下标
    int ch;
    // 解析后的码
    int code;
    // 作为存入WordType对象 tt的下标
    int tt_i = 0;
    // 标签的Dictionary，存储标签名，和序列下标
    public static Dictionary<string, int> markmap = new Dictionary<string, int>();
    // 声明一个WordType类数组，用来保存解析出的<类型，码字>序列
    public static WordType[] tt = new WordType[300];
    // 存放构成单词符号的字符串
    public StringBuilder strToken = new StringBuilder();
    // 处理关键字,operator表示运算符 quotation表示单引号，分号 brackets表示括号等
    public string[] retainWord = new string[] { "auto", "putnumb", "putchar", "operator", "quotation", "extern", "getchar", "mark", "goto", "brackets", "if", "else", "while", "var", "number", "getnumb", "for", "do", "note", "putstr", "switch", "break", "continue" };

    // 判断是否是字母
    public bool IsLetter()
    {
        if ((ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 122))
        {
            return true;
        }
        return false;
    }

    // 判断是否是数字
    public bool IsDigit()
    {
        if (ch >= 48 && ch <= 57)
        {
            return true;
        }
        return false;
    }

    // 判断是否是空格
    public bool IsBC(int ch)
    {
        if (ch == 32)
        {
            return true;
        }
        return false;
    }

    // 连接字符
    public void Contact(char ch)
    {
        strToken.Append(ch);
    }

    // 判断是否是保留字
    public int Reserve()
    {
        for (int i = 0; i < retainWord.Length; i++)
        {
            if (strToken.ToString().Equals(retainWord[i]))
            {
                return i;
            }
        }
        if (strToken.Length != 0)
        {
            if (strToken[0] >= '0' && strToken[0] <= '9')
            {
                return 14;
            }
        }
        if (strToken.Length != 0)
        {
            if (strToken[0] == '<' || strToken[0] == '>' || strToken[0] == '=' || strToken[0] == '+' || strToken[0] == '-' || strToken[0] == '*' || strToken[0] == '/')
            {
                return 3;
            }
        }
        return 13;
    }

    public void Retract()
    {
        code = Reserve();
        if (strToken.Length != 0)
        {
            Console.WriteLine("('" + code + "','" + strToken + "')");
            tt[tt_i++] = new WordType(retainWord[code], strToken.ToString());
            strToken.Remove(0, strToken.Length);
        }
    }

    public void scanner(string txt)
    {
        using (StreamReader br=new StreamReader(txt)) {

            /*bool isEmpty = false;
            if (br.ReadLine()==null)
               isEmpty = true; //判断打开的.txt文件是否为空
              Trace.Assert(!isEmpty);  //如果打开文件为空，则触发断言.*/

            while ((ch = br.Read()) != -1)
            {
                if (IsBC(ch) == false)
                {
                    if (IsLetter())
                    {
                        if ((strToken.Length != 0) && ((strToken[0] == '+') || (strToken[0] == '-') || (strToken[0] == '=') || (strToken[0] == '<') || (strToken[0] == '>') || (strToken[0] == '*') || (strToken[0] == '/')))
                        {
                            Retract();
                            Contact((char)ch);
                        }
                        else
                        {
                            Contact((char)ch);
                        }
                    }
                    else if (IsDigit())
                    {
                        if ((strToken.Length != 0) && ((strToken[0] == '+') || (strToken[0] == '-') || (strToken[0] == '=') || (strToken[0] == '<') || (strToken[0] == '>') || (strToken[0] == '*') || (strToken[0] == '/')))
                        {
                            Retract();
                            Contact((char)ch);
                        }
                        else
                        {
                            Contact((char)ch);
                        }
                    }
                    else if (ch == 61)
                    {
                        if ((strToken.Length != 0) && ((strToken[0] == '=') || (strToken[0] == '>') || (strToken[0] == '<') || (strToken[0] == '!')))
                        {
                            strToken.Append((char)ch);
                            Console.WriteLine("('" + 4 + "','" + strToken + "')");
                            tt[tt_i++] = new WordType("operator", strToken.ToString());
                            strToken.Remove(0, strToken.Length);
                        }
                        else if (strToken.Length != 0)
                        {
                            Retract();
                            strToken.Append((char)ch);
                        }
                        else
                        {
                            strToken.Append((char)ch);
                        }
                    }
                    else if (ch == 60 || ch == 62 || ch == 33)
                    {
                        strToken.Append((char)ch);
                    }
                    else if (ch == 42 || ch == 47)
                    {
                        if (ch == 47 && (strToken.Length != 0) && (strToken[0] == '*'))
                        {
                            Contact((char)ch);
                            Console.WriteLine("('" + 18 + "','" + strToken + "')");
                            tt[tt_i++] = new WordType("note", strToken.ToString());
                            strToken.Remove(0, strToken.Length);
                        }
                        else if (ch == 42 && (strToken.Length != 0) && (strToken[0] == '/'))
                        {
                            Contact((char)ch);
                            Console.WriteLine("('" + 18 + "','" + strToken + "')");
                            tt[tt_i++] = new WordType("note", strToken.ToString());
                            strToken.Remove(0, strToken.Length);
                        }
                        else if (strToken.Length != 0)
                        {
                            Retract();
                            strToken.Append((char)ch);
                        }
                        else
                        {
                            strToken.Append((char)ch);
                        }
                    }
                    else if (ch == 43)
                    {
                        if ((strToken.Length != 0) && (strToken[0] == '+'))
                        {
                            strToken.Append((char)ch);
                            Console.WriteLine("('" + 4 + "','" + strToken + "')");
                            tt[tt_i++] = new WordType("operator", strToken.ToString());
                            strToken.Remove(0, strToken.Length);
                        }
                        else if (strToken.Length != 0)
                        {
                            Retract();
                            strToken.Append((char)ch);
                        }
                        else
                        {
                            strToken.Append((char)ch);
                        }

                    }
                    else if (ch == '&')
                    {
                        if ((strToken.Length != 0) && (strToken[0] == '&'))
                        {
                            strToken.Append((char)ch);
                            Console.WriteLine("('" + 4 + "','" + strToken + "')");
                            tt[tt_i++] = new WordType("operator", strToken.ToString());
                            strToken.Remove(0, strToken.Length);
                        }
                        else
                        {
                            strToken.Append((char)ch);
                        }

                    }
                    else if (ch == '|')
                    {
                        if ((strToken.Length != 0) && (strToken[0] == '|'))
                        {
                            strToken.Append((char)ch);
                            Console.WriteLine("('" + 4 + "','" + strToken + "')");
                            tt[tt_i++] = new WordType("operator", strToken.ToString());
                            strToken.Remove(0, strToken.Length);
                        }
                        else
                        {
                            strToken.Append((char)ch);
                        }

                    }
                    else if (ch == 45)
                    {
                        if ((strToken.Length != 0) && (strToken[0] == '-'))
                        {
                            strToken.Append((char)ch);
                            Console.WriteLine("('" + 4 + "','" + strToken + "')");
                            tt[tt_i++] = new WordType("operator", strToken.ToString());
                            strToken.Remove(0, strToken.Length);
                        }
                        else if (strToken.Length != 0)
                        {
                            Retract();
                            strToken.Append((char)ch);
                        }
                        else
                        {
                            strToken.Append((char)ch);
                        }
                    }
                    else if (ch == ',')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("quotation", ",");
                    }
                    else if (ch == ';')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("quotation", ";");
                    }
                    else if (ch == ')')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("brackets", ")");
                    }
                    else if (ch == '(')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("brackets", "(");
                    }
                    else if (ch == '{')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("brackets", "{");
                    }
                    else if (ch == '[')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("brackets", "[");
                    }
                    else if (ch == ']')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("brackets", "]");
                    }
                    else if (ch == '}')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("brackets", "}");
                    }
                    else if (ch == ':')
                    {
                        if ((strToken.Length != 0))
                        {
                            char temp = strToken[0];
                            if ((temp >= 65 && temp <= 90) || (temp >= 97 && temp <= 122))
                            {
                                Console.WriteLine("('" + 7 + "','" + strToken + "')");
                                markmap[strToken.ToString()] = tt_i;
                                tt[tt_i++] = new WordType("mark", strToken.ToString());
                                strToken.Remove(0, strToken.Length);
                            }
                            else
                            {
                                Console.WriteLine("wrong!");
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("wrong!");
                            Environment.Exit(0);
                        }
                    }
                    else if (ch == '\'')
                    {
                        Retract();
                        Console.WriteLine("('" + 5 + "','" + (char)ch + "')");
                        tt[tt_i++] = new WordType("quotation", "'");
                    }

                }
                else
                {
                    Retract();
                }
            }

        }
    }
    
}


