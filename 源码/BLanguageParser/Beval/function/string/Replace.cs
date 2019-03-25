using System;
using System.Collections;

public class Replace : Function
{
    public virtual string Name
    {
        get
        {
            return "replace";
        }
    }

    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        string result = null;
        string exceptionMessage = "One string argument and two character " + "arguments are required.";
        ArrayList values = FunctionHelper.getStrings(arguments, EvaluationConstants.FUNCTION_ARGUMENT_SEPARATOR);
        if (values.Count != 3)
        {
            throw new FunctionException(exceptionMessage);
        }
        try
        {
            string argumentOne = FunctionHelper.trimAndRemoveQuoteChars((string)values[0], evaluator.QuoteCharacter);
            string argumentTwo = FunctionHelper.trimAndRemoveQuoteChars((string)values[1], evaluator.QuoteCharacter);
            string argumentThree = FunctionHelper.trimAndRemoveQuoteChars((string)values[2], evaluator.QuoteCharacter);
            char oldCharacter = ' ';
            if (argumentTwo.Length == 1)
            {
                oldCharacter = argumentTwo[0];
            }
            else
            {
                throw new FunctionException(exceptionMessage);
            }
            char newCharacter = ' ';
            if (argumentThree.Length == 1)
            {
                newCharacter = argumentThree[0];
            }
            else
            {
                throw new FunctionException(exceptionMessage);
            }
            result = argumentOne.Replace(oldCharacter, newCharacter);
        }
        catch (FunctionException fe)
        {
            throw new FunctionException(fe.Message, fe);
        }
        catch (Exception e)
        {
            throw new FunctionException(exceptionMessage, e);
        }
        return new FunctionResult(result, FunctionConstants.FUNCTION_RESULT_TYPE_STRING);
    }
}
