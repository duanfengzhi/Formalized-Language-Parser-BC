using System;
using System.Collections;

public class Substring : Function
{
    public virtual string Name
    {
        get
        {
            return "substring";
        }
    }

    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        string result = null;
        string exceptionMessage = "One string argument and two integer " + "arguments are required.";
        ArrayList values = FunctionHelper.getOneStringAndTwoIntegers(arguments, EvaluationConstants.FUNCTION_ARGUMENT_SEPARATOR);
        if (values.Count != 3)
        {
            throw new FunctionException(exceptionMessage);
        }
        try
        {
            string argumentOne = FunctionHelper.trimAndRemoveQuoteChars((string)values[0], evaluator.QuoteCharacter);
            int beginningIndex = ((int?)values[1]).Value;
            int endingIndex = ((int?)values[2]).Value;
            result = argumentOne.Substring(beginningIndex, endingIndex - beginningIndex);
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
