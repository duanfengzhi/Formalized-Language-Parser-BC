using System;

public class ToLowerCase : Function
{
    public virtual string Name
    {
        get
        {
            return "toLowerCase";
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
            result = argumentOne.ToLower();
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
