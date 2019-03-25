using System;
using System.Text;

public class EvaluationHelper
{

    public static string replaceAll(string expression, string oldString, string newString)
    {

        string replacedExpression = expression;

        if (!string.ReferenceEquals(replacedExpression, null))
        {
            int charCtr = 0;
            int oldStringIndex = replacedExpression.IndexOf(oldString, charCtr, StringComparison.Ordinal);

            while (oldStringIndex > -1)
            {

                StringBuilder buffer = new StringBuilder(replacedExpression.Substring(0, oldStringIndex) + replacedExpression.Substring(oldStringIndex + oldString.Length));


                buffer.Insert(oldStringIndex, newString);

                replacedExpression = buffer.ToString();

                charCtr = oldStringIndex + newString.Length;


                if (charCtr < replacedExpression.Length)
                {
                    oldStringIndex = replacedExpression.IndexOf(oldString, charCtr, StringComparison.Ordinal);
                }
                else
                {
                    oldStringIndex = -1;
                }
            }
        }

        return replacedExpression;
    }


    public static bool isSpace(char character)
    {

        if (character == ' ' || character == '\t' || character == '\n' || character == '\r' || character == '\f')
        {
            return true;
        }

        return false;
    }
}

