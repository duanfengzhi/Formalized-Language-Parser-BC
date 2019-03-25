using System;
using System.Collections;

/// <summary>
/// This class contains many methods that are helpful when writing functions.
/// Some of these methods were created to help with the creation of the math and
/// string functions packaged with Evaluator.
/// </summary>
public class FunctionHelper
{
    /// <summary>
    /// This method first removes any white space at the beginning and end of the
    /// input string. It then removes the specified quote character from the the
    /// first and last characters of the string if a quote character exists in
    /// those positions. If quote characters are not in the first and last
    /// positions after the white space is trimmed, then a FunctionException will
    /// be thrown.
    /// </summary>
    /// <param name="arguments">
    ///            The arguments to trim and revove quote characters from. </param>
    /// <param name="quoteCharacter">
    ///            The character to remove from the first and last position of
    ///            the trimmed string.
    /// </param>
    /// <returns> The arguments with white space and quote characters removed.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if quote characters do not exist in the first and
    ///                last positions after the white space is trimmed. </exception>
    //ORIGINAL LINE: public static String trimAndRemoveQuoteChars(final String arguments, final char quoteCharacter) throws FunctionException
    public static string trimAndRemoveQuoteChars(string arguments, char quoteCharacter)
    {
        string trimmedArgument = arguments;
        trimmedArgument = trimmedArgument.Trim();
        if (trimmedArgument[0] == quoteCharacter)
        {
            trimmedArgument = trimmedArgument.Substring(1, trimmedArgument.Length - 1);
        }
        else
        {
            throw new FunctionException("Value does not start with a quote.");
        }
        if (trimmedArgument[trimmedArgument.Length - 1] == quoteCharacter)
        {
            trimmedArgument = trimmedArgument.Substring(0, trimmedArgument.Length - 1);
        }
        else
        {
            throw new FunctionException("Value does not end with a quote.");
        }
        return trimmedArgument;
    }

    /// <summary>
    /// This methods takes a string of input function arguments, evaluates each
    /// argument and creates a Double value for each argument from the result of
    /// the evaluations.
    /// </summary>
    /// <param name="arguments">
    ///            The arguments to parse. </param>
    /// <param name="delimiter">
    ///            The delimiter to use while parsing.
    /// </param>
    /// <returns> An array list of Double values found in the input string.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if the string does not properly parse into Double
    ///                values. </exception>
    //ORIGINAL LINE: public static java.util.ArrayList getDoubles(final String arguments, final char delimiter) throws FunctionException
    public static ArrayList getDoubles(string arguments, char delimiter)
    {
        ArrayList returnValues = new ArrayList();
        try
        {
            //ORIGINAL LINE: final net.sourceforge.jeval.ArgumentTokenizer tokenizer = new net.sourceforge.jeval.ArgumentTokenizer(arguments, delimiter);
            ArgumentTokenizer tokenizer = new ArgumentTokenizer(arguments, delimiter);
            while (tokenizer.hasMoreTokens())
            {
                //ORIGINAL LINE: final String token = tokenizer.nextToken().trim();
                string token = tokenizer.nextToken().Trim();
                returnValues.Add(Convert.ToDouble(token));
            }
        }
        catch (Exception e)
        {
            throw new FunctionException("Invalid values in string.", e);
        }
        return returnValues;
    }

    /// <summary>
    /// This methods takes a string of input function arguments, evaluates each
    /// argument and creates a String value for each argument from the result of
    /// the evaluations.
    /// </summary>
    /// <param name="arguments">
    ///            The arguments of values to parse. </param>
    /// <param name="delimiter">
    ///            The delimiter to use while parsing.
    /// </param>
    /// <returns> An array list of String values found in the input string.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if the stirng does not properly parse into String
    ///                values. </exception>
    //ORIGINAL LINE: public static java.util.ArrayList getStrings(final String arguments, final char delimiter) throws FunctionException
    public static ArrayList getStrings(string arguments, char delimiter)
    {
        //ORIGINAL LINE: final java.util.ArrayList returnValues = new java.util.ArrayList();
        ArrayList returnValues = new ArrayList();
        try
        {
            ArgumentTokenizer tokenizer = new ArgumentTokenizer(arguments, delimiter);
            while (tokenizer.hasMoreTokens())
            {
                //ORIGINAL LINE: final String token = tokenizer.nextToken();
                string token = tokenizer.nextToken();
                returnValues.Add(token);
            }
        }
        catch (Exception e)
        {
            throw new FunctionException("Invalid values in string.", e);
        }
        return returnValues;
    }

    /// <summary>
    /// This methods takes a string of input function arguments, evaluates each
    /// argument and creates a one Integer and one String value for each argument
    /// from the result of the evaluations.
    /// </summary>
    /// <param name="arguments">
    ///            The arguments of values to parse. </param>
    /// <param name="delimiter">
    ///            The delimiter to use while parsing.
    /// </param>
    /// <returns> An array list of object values found in the input string.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if the stirng does not properly parse into the
    ///                proper objects. </exception>
    //ORIGINAL LINE: public static java.util.ArrayList getOneStringAndOneInteger(final String arguments, final char delimiter) throws FunctionException
    public static ArrayList getOneStringAndOneInteger(string arguments, char delimiter)
    {
        ArrayList returnValues = new ArrayList();
        try
        {
            //ORIGINAL LINE: final net.sourceforge.jeval.ArgumentTokenizer tokenizer = new net.sourceforge.jeval.ArgumentTokenizer(arguments, delimiter);
            ArgumentTokenizer tokenizer = new ArgumentTokenizer(arguments, delimiter);
            int tokenCtr = 0;
            while (tokenizer.hasMoreTokens())
            {
                if (tokenCtr == 0)
                {
                    //ORIGINAL LINE: final String token = tokenizer.nextToken();
                    string token = tokenizer.nextToken();
                    returnValues.Add(token);
                }
                else if (tokenCtr == 1)
                {
                    //ORIGINAL LINE: final String token = tokenizer.nextToken().trim();
                    string token = tokenizer.nextToken().Trim();
                    //returnValues.Add(new int?((Convert.ToDouble(token)).intValue()));
                }
                else
                {
                    throw new FunctionException("Invalid values in string.");
                }

                tokenCtr++;
            }
        }
        catch (Exception e)
        {
            throw new FunctionException("Invalid values in string.", e);
        }

        return returnValues;
    }

    /// <summary>
    /// This methods takes a string of input function arguments, evaluates each
    /// argument and creates a two Strings and one Integer value for each
    /// argument from the result of the evaluations.
    /// </summary>
    /// <param name="arguments">
    ///            The arguments of values to parse. </param>
    /// <param name="delimiter">
    ///            The delimiter to use while parsing.
    /// </param>
    /// <returns> An array list of object values found in the input string.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if the stirng does not properly parse into the
    ///                proper objects. </exception>
    //ORIGINAL LINE: public static java.util.ArrayList getTwoStringsAndOneInteger(final String arguments, final char delimiter) throws FunctionException
    public static ArrayList getTwoStringsAndOneInteger(string arguments, char delimiter)
    {
        //ORIGINAL LINE: final java.util.ArrayList returnValues = new java.util.ArrayList();
        ArrayList returnValues = new ArrayList();
        try
        {
            //ORIGINAL LINE: final net.sourceforge.jeval.ArgumentTokenizer tokenizer = new net.sourceforge.jeval.ArgumentTokenizer(arguments, delimiter);
            ArgumentTokenizer tokenizer = new ArgumentTokenizer(arguments, delimiter);
            int tokenCtr = 0;
            while (tokenizer.hasMoreTokens())
            {
                if (tokenCtr == 0 || tokenCtr == 1)
                {
                    //ORIGINAL LINE: final String token = tokenizer.nextToken();
                    string token = tokenizer.nextToken();
                    returnValues.Add(token);
                }
                else if (tokenCtr == 2)
                {
                    //ORIGINAL LINE: final String token = tokenizer.nextToken().trim();
                    string token = tokenizer.nextToken().Trim();
                    //returnValues.Add(new int?((Convert.ToDouble(token)).intValue()));
                }
                else
                {
                    throw new FunctionException("Invalid values in string.");
                }
                tokenCtr++;
            }
        }
        catch (Exception e)
        {
            throw new FunctionException("Invalid values in string.", e);
        }
        return returnValues;
    }

    /// <summary>
    /// This methods takes a string of input function arguments, evaluates each
    /// argument and creates a one String and two Integers value for each
    /// argument from the result of the evaluations.
    /// </summary>
    /// <param name="arguments">
    ///            The arguments of values to parse. </param>
    /// <param name="delimiter">
    ///            The delimiter to use while parsing.
    /// </param>
    /// <returns> An array list of object values found in the input string.
    /// </returns>
    /// <exception cref="FunctionException">
    ///                Thrown if the stirng does not properly parse into the
    ///                proper objects. </exception>
    //ORIGINAL LINE: public static java.util.ArrayList getOneStringAndTwoIntegers(final String arguments, final char delimiter) throws FunctionException
    public static ArrayList getOneStringAndTwoIntegers(string arguments, char delimiter)
    {
        //ORIGINAL LINE: final java.util.ArrayList returnValues = new java.util.ArrayList();
        ArrayList returnValues = new ArrayList();
        try
        {
            //ORIGINAL LINE: final net.sourceforge.jeval.ArgumentTokenizer tokenizer = new net.sourceforge.jeval.ArgumentTokenizer(arguments, delimiter);
            ArgumentTokenizer tokenizer = new ArgumentTokenizer(arguments, delimiter);
            int tokenCtr = 0;
            while (tokenizer.hasMoreTokens())
            {
                if (tokenCtr == 0)
                {
                    //ORIGINAL LINE: final String token = tokenizer.nextToken().trim();
                    string token = tokenizer.nextToken().Trim();
                    returnValues.Add(token);
                }
                else if (tokenCtr == 1 || tokenCtr == 2)
                {
                    //ORIGINAL LINE: final String token = tokenizer.nextToken().trim();
                    string token = tokenizer.nextToken().Trim();
                    //returnValues.Add(new int((Convert.ToDouble(token)).intValue()));
                }
                else
                {
                    throw new FunctionException("Invalid values in string.");
                }

                tokenCtr++;
            }
        }
        catch (Exception e)
        {
            throw new FunctionException("Invalid values in string.", e);
        }

        return returnValues;
    }
}

