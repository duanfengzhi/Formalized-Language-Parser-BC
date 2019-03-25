using System;

public class Trim : Function
{
    public virtual string Name
    {
        get
        {
            return "trim";
        }
    }

    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        string result = null;
        string exceptionMessage = "One string argument is required.";
        try
        {
            string stringValue = arguments;
            string argumentOne = FunctionHelper.trimAndRemoveQuoteChars(stringValue, evaluator.QuoteCharacter);
            result = argumentOne.Trim();
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
