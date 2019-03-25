
public class Eval : Function
{
    public virtual string Name
    {
        get
        {
            return "eval";
        }
    }

    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        string result = null;
        try
        {
            result = evaluator.evaluate(arguments, false, true);
        }
        catch (EvaluationException ee)
        {
            throw new FunctionException(ee.Message, ee);
        }
        int resultType = FunctionConstants.FUNCTION_RESULT_TYPE_NUMERIC;
        try
        {
            double.Parse(result);
        }
        catch (System.FormatException)
        {
            resultType = FunctionConstants.FUNCTION_RESULT_TYPE_STRING;
        }
        return new FunctionResult(result, resultType);
    }
}
