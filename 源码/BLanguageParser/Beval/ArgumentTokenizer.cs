using System.Collections;


public class ArgumentTokenizer
{
    private bool InstanceFieldsInitialized = false;

    private void InitializeInstanceFields()
    {
        delimiter = defaultDelimiter;
    }




    public readonly char defaultDelimiter = EvaluationConstants.FUNCTION_ARGUMENT_SEPARATOR;

    private string arguments = null;


    private char delimiter;



    public ArgumentTokenizer(string arguments, char delimiter)
    {
        if (!InstanceFieldsInitialized)
        {
            InitializeInstanceFields();
            InstanceFieldsInitialized = true;
        }
        this.arguments = arguments;
        this.delimiter = delimiter;
    }


    public virtual bool hasMoreElements()
    {
        return hasMoreTokens();
    }


    public virtual bool hasMoreTokens()
    {

        if (arguments.Length > 0)
        {
            return true;
        }

        return false;
    }


    public virtual object nextElement()
    {
        return nextToken();
    }


    public virtual string nextToken()
    {
        int charCtr = 0;
        int size = arguments.Length;
        int parenthesesCtr = 0;
        string returnArgument = null;


        while (charCtr < size)
        {
            if (arguments[charCtr] == '(')
            {
                parenthesesCtr++;
            }
            else if (arguments[charCtr] == ')')
            {
                parenthesesCtr--;
            }
            else if (arguments[charCtr] == delimiter && parenthesesCtr == 0)
            {

                returnArgument = arguments.Substring(0, charCtr);
                arguments = arguments.Substring(charCtr + 1);
                break;
            }

            charCtr++;
        }

        if (string.ReferenceEquals(returnArgument, null))
        {
            returnArgument = arguments;
            arguments = "";
        }

        return returnArgument;
    }
}

