using System;

public class Length : Function
{
    public virtual string Name
    {
        get
        {
            return "length";
        }
    }

    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        int? result = null;
        string exceptionMessage = "One string argument is required.";
        try
        {
            string stringValue = arguments;
            string argumentOne = FunctionHelper.trimAndRemoveQuoteChars(stringValue, evaluator.QuoteCharacter);
            result = new int?(argumentOne.Length);
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
