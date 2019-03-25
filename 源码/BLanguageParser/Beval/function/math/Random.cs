using System;

/// <summary>
/// This class is a function that executes within Evaluator. The function returns
/// a random double value greater than or equal to 0.0 and less than 1.0. See the
/// Math.random() method in the JDK for a complete description of how this
/// function works.
/// </summary>
public class Random : Function
{
    /// <summary>
    /// Returns the name of the function - "random".
    /// </summary>
    /// <returns> The name of this function class. </returns>
    public virtual string Name
    {
        get
        {
            return "random";
        }
    }

    /// <summary>
    /// Executes the function for the specified argument. This method is called
    /// internally by Evaluator.
    /// </summary>
    /// <param name="evaluator">
    ///            An instance of Evaluator. </param>
    /// <param name="arguments">
    ///            Not used.
    /// </param>
    /// <returns> A random double value greater than or equal to 0.0 and less than
    ///         1.0.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if the argument(s) are not valid for this function. </exception>
    //ORIGINAL LINE: public net.sourceforge.jeval.function.FunctionResult execute(final net.sourceforge.jeval.Evaluator evaluator, final String arguments) throws net.sourceforge.jeval.function.FunctionException
    public virtual FunctionResult execute(Evaluator evaluator, string arguments)
    {
        double? result = new double?(GlobalRandom.NextDouble);
        return new FunctionResult(result.ToString(), FunctionConstants.FUNCTION_RESULT_TYPE_NUMERIC);
    }
}
