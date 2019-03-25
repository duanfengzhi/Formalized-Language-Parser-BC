/// <summary>
/// A function that can be specified in an expression.
/// </summary>
public interface Function
{
    /// <summary>
    /// Returns the name of the function.
    /// </summary>
    /// <returns> The name of this function class. </returns>
    string Name { get; }

    /// <summary>
    /// Executes the function for the specified argument. This method is called
    /// internally by Evaluator.
    /// </summary>
    /// <param name="evaluator">
    ///            An instance of Evaluator. </param>
    /// <param name="arguments">
    ///            The arguments that will be evaluated by the function. It is up
    ///            to the function implementation to break the string into one or
    ///            more arguments.
    /// </param>
    /// <returns> The value of the evaluated argument and its type.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if the argument(s) are not valid for this function. </exception>
    //ORIGINAL LINE: public FunctionResult execute(net.sourceforge.jeval.Evaluator evaluator, String arguments) throws FunctionException;
    FunctionResult execute(Evaluator evaluator, string arguments);
}
