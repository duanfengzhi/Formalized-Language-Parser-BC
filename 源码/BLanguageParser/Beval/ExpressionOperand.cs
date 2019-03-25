
public class ExpressionOperand
{


    private string value = null;

    // The unary operator for the operand, if one exists.
    private Operator unaryOperator = null;


    public ExpressionOperand(string value, Operator unaryOperator)
    {
        this.value = value;
        this.unaryOperator = unaryOperator;
    }


    public virtual string Value
    {
        get
        {
            return value;
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
