using System;
using System.Collections;

public class CompareToIgnoreCase : Function
{
    public virtual string Name
    {
        get
        {
            return "compareToIgnoreCase";
        }
    }

    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        int? result = null;
        string exceptionMessage = "Two string arguments are required.";
        ArrayList strings = FunctionHelper.getStrings(arguments, EvaluationConstants.FUNCTION_ARGUMENT_SEPARATOR);
        if (strings.Count != 2)
        {
            throw new FunctionException(exceptionMessage);
        }
        try
        {
            string argumentOne = FunctionHelper.trimAndRemoveQuoteChars((string)strings[0], evaluator.QuoteCharacter);
            string argumentTwo = FunctionHelper.trimAndRemoveQuoteChars((string)strings[1], evaluator.QuoteCharacter);
            //result = new int?(argumentOne.compareToIgnoreCase(argumentTwo));
        }
        catch (FunctionException fe)
        {
            throw new FunctionException(fe.Message, fe);
        }
        catch (Exception e)
        {
            throw new FunctionException(exceptionMessage, e);
        }
        return new FunctionResult(result.ToString(), FunctionConstants.FUNCTION_RESULT_TYPE_NUMERIC);
    }
}
