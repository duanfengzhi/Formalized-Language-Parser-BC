/// <summary>
/// This is a wrapper for the result value returned from a function that not only
/// contains the result, but the type. All custom functions must return a
/// FunctionResult.
/// </summary>
public class FunctionResult
{
    // The value returned from a function call.
    private string result;

    // The type of the result. Can be a numberic or string. Boolean values come
    // back as numeric values.
    private int type;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="result">
    ///            The result value. </param>
    /// <param name="type">
    ///            The result type.
    /// </param>
    /// <exception cref="FunctionException">
    ///             Thrown if result type is invalid. </exception>
    //ORIGINAL LINE: public FunctionResult(String result, int type) throws FunctionException
    public FunctionResult(string result, int type)
    {
        if (type < FunctionConstants.FUNCTION_RESULT_TYPE_NUMERIC || type > FunctionConstants.FUNCTION_RESULT_TYPE_STRING)
        {
            throw new FunctionException("Invalid function result type.");
        }
        this.result = result;
        this.type = type;
    }

    /// <summary>
    /// Returns the result value.
    /// </summary>
    /// <returns> The result value. </returns>
    public virtual string Result
    {
        get
        {
            return result;
        }
    }

    /// <summary>
    /// Returns the result type.
    /// </summary>
    /// <returns> The result type. </returns>
    public virtual int Type
    {
        get
        {
            return type;
        }
    }
}

