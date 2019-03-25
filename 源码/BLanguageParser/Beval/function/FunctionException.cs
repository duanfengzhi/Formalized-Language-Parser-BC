using System;

/// <summary>
/// This exception is thrown when an error occurs while processing a function.
/// </summary>
public class FunctionException : Exception
{
    private const long serialVersionUID = 4767250768467137620L;

    /// <summary>
    /// This constructor takes a custom message as input.
    /// </summary>
    /// <param name="message">
    ///            A custom message for the exception to display. </param>
    public FunctionException(string message) : base(message)
    {
    }

    /// <summary>
    /// This constructor takes an exception as input.
    /// </summary>
    /// <param name="message">
    ///            A custom message for the exception to display. </param>
    /// <param name="exception">
    ///            An exception. </param>
    public FunctionException(string message, Exception exception) : base(message, exception)
    {
    }
}
