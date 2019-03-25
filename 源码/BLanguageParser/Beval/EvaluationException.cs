using System;

public class EvaluationException : Exception
{
    private const long serialVersionUID = -3010333364122748053L;
    public EvaluationException(string message)
        : base(message)
    {
    }

    public EvaluationException(string message, Exception exception)
        : base(message, exception)
    {
    }
}
