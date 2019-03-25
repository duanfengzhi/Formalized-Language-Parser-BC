using System;
using System.Collections;

public class CharAt : Function
{
    public virtual string Name
    {
        get
        {
            return "charAt";
        }
    }

    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        string result = null;
        string exceptionMessage = "One string and one integer argument " + "are required.";
        ArrayList values = FunctionHelper.getOneStringAndOneInteger(arguments, EvaluationConstants.FUNCTION_ARGUMENT_SEPARATOR);
        if (values.Count != 2)
        {
            throw new FunctionException(exceptionMessage);
        }
        try
        {
            string argumentOne = FunctionHelper.trimAndRemoveQuoteChars((string)values[0], evaluator.QuoteCharacter);
            int index = ((int?)values[1]).Value;
            char[] character = new char[1];
            character[0] = argumentOne[index];
            result = new string(character);
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
