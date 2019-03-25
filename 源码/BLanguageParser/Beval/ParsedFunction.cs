
public class ParsedFunction
{


    private readonly Function function;

    // The arguments to the function.
    private readonly string arguments;

    // The unary operator for this object, if there is one.
    private readonly Operator unaryOperator;

    public ParsedFunction(Function function, string arguments, Operator unaryOperator)
    {

        this.function = function;
        this.arguments = arguments;
        this.unaryOperator = unaryOperator;
    }


    public virtual Function Function
    {
        get
        {
            return function;
        }
    }


    public virtual string Arguments
    {
        get
        {
            return arguments;
        }
    }

    public virtual Operator UnaryOperator
    {
        get
        {
            return unaryOperator;
        }
    }
}
