using System;
using System.Collections;

/// <summary>
/// This class is a function that executes within Evaluator. The function returns
/// the remainder operation on two arguments as prescribed by the IEEE 754
/// complete description of how this function works.
/// </summary>
public class IEEEremainder : Function
{
    /// <summary>
    /// Returns the name of the function - "IEEEremainder".
    /// </summary>
    /// <returns> The name of this function class. </returns>
    public virtual string Name
    {
        get
        {
            return "IEEEremainder";
        }
    }

    /// <summary>
    /// Executes the function for the specified argument. This method is called
    /// internally by Evaluator.
    /// </summary>
    /// <param name="evaluator">
    ///            An instance of Evaluator. </param>
    /// <param name="arguments">
    ///            A string argument that will be converted into two double
    ///            values and evaluated.
    /// </param>
    /// <returns> The the remainder operation on two arguments as prescribed by the
    ///         IEEE 754 standard.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if the argument(s) are not valid for this function. </exception>
    //ORIGINAL LINE: public net.sourceforge.jeval.function.FunctionResult execute(final net.sourceforge.jeval.Evaluator evaluator, final String arguments) throws net.sourceforge.jeval.function.FunctionException
    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        double? result = null;
        ArrayList numbers = FunctionHelper.getDoubles(arguments, EvaluationConstants.FUNCTION_ARGUMENT_SEPARATOR);

        if (numbers.Count != 2)
        {
            throw new FunctionException("Two numeric arguments are required.");
        }

        try
        {
            double argumentOne = ((double?)numbers[0]).Value;
            double argumentTwo = ((double?)numbers[1]).Value;
            result = new double?(Math.IEEERemainder(argumentOne, argumentTwo));
        }
        catch (Exception e)
        {
            throw new FunctionException("Two numeric arguments are required.", e);
        }
        return new FunctionResult(result.ToString(), FunctionConstants.FUNCTION_RESULT_TYPE_NUMERIC);
    }
}
