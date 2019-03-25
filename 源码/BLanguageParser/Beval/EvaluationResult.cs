using System;


public class EvaluationResult
{

    private string result;
    private char quoteCharacter;
    public EvaluationResult(string result, char quoteCharacter)
    {

        this.result = result;
        this.quoteCharacter = quoteCharacter;
    }


    public virtual char QuoteCharacter
    {
        get
        {
            return quoteCharacter;
        }
        set
        {
            this.quoteCharacter = value;
        }
    }


    public virtual string Result
    {
        get
        {
            return result;
        }
        set
        {
            this.result = value;
        }
    }



    public virtual bool BooleanTrue
    {
        get
        {

            if (!string.ReferenceEquals(result, null) && EvaluationConstants.BOOLEAN_STRING_TRUE.Equals(result))
            {

                return true;
            }

            return false;
        }
    }


    public virtual bool BooleanFalse
    {
        get
        {

            if (!string.ReferenceEquals(result, null) && EvaluationConstants.BOOLEAN_STRING_FALSE.Equals(result))
            {

                return true;
            }

            return false;
        }
    }


    public virtual bool String
    {
        get
        {

            if (!string.ReferenceEquals(result, null) && result.Length >= 2)
            {

                if (result[0] == quoteCharacter && result[result.Length - 1] == quoteCharacter)
                {

                    return true;
                }
            }

            return false;
        }
    }


    public virtual double? Double
    {
        get
        {

            return Convert.ToDouble(result);
        }
    }

    public virtual string UnwrappedString
    {
        get
        {

            if (!string.ReferenceEquals(result, null) && result.Length >= 2)
            {

                if (result[0] == quoteCharacter && result[result.Length - 1] == quoteCharacter)
                {

                    return result.Substring(1, (result.Length - 1) - 1);
                }
            }

            return null;
        }
    }
}

