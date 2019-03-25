
public class EvaluationConstants
{


    public const char SINGLE_QUOTE = '\'';
    public const char DOUBLE_QUOTE = '"';
    public const char OPEN_BRACE = '{';
    public const char CLOSED_BRACE = '}';
    public const char POUND_SIGN = '#';

    public static readonly string OPEN_VARIABLE = POUND_SIGN.ToString() + OPEN_BRACE.ToString();

    public static readonly string CLOSED_VARIABLE = CLOSED_BRACE.ToString();

    public const string BOOLEAN_STRING_TRUE = "1.0";

    public const string BOOLEAN_STRING_FALSE = "0.0";

    public const char COMMA = ',';
    public const char FUNCTION_ARGUMENT_SEPARATOR = COMMA;
}

