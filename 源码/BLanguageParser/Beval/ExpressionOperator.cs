

public class ExpressionOperator
{

    // The operator that this object represents.
    private Operator @operator = null;

    // The unary operator for this object, if there is one.
    private Operator unaryOperator = null;


    public ExpressionOperator(Operator @operator, Operator unaryOperator)
    {
        this.@operator = @operator;
        this.unaryOperator = unaryOperator;
    }


    public virtual Operator Operator
    {
        get
        {
            return @operator;
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
