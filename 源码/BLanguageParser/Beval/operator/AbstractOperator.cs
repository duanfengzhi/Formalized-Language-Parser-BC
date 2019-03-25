
public abstract class AbstractOperator : Operator
{

    private string symbol = null;

    private int precedence = 0;

    private bool unary = false;

    public AbstractOperator(string symbol, int precedence)
    {

        this.symbol = symbol;
        this.precedence = precedence;
    }

    public AbstractOperator(string symbol, int precedence, bool unary)
    {

        this.symbol = symbol;
        this.precedence = precedence;
        this.unary = unary;
    }

    public virtual double evaluate(double leftOperand, double rightOperand)
    {
        return 0;
    }

    public virtual string evaluate(string leftOperand, string rightOperand)
    {
        throw new EvaluationException("Invalid operation for a string.");
    }

    public virtual double evaluate(double operand)
    {
        return 0;
    }


    public virtual string Symbol
    {
        get
        {
            return symbol;
        }
    }

    public virtual int Precedence
    {
        get
        {
            return precedence;
        }
    }

    public virtual int Length
    {
        get
        {
            return symbol.Length;
        }
    }


    public virtual bool Unary
    {
        get
        {
            return unary;
        }
    }

    public override bool Equals(object @object)
    {
        if (@object == null)
        {
            return false;
        }

        if (!(@object is AbstractOperator))
        {
            throw new System.InvalidOperationException("Invalid operator object.");
        }

        AbstractOperator @operator = (AbstractOperator)@object;

        if (symbol.Equals(@operator.Symbol))
        {
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return Symbol;
    }
}
