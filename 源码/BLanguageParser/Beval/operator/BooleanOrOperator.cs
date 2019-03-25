

public class BooleanOrOperator : AbstractOperator
{

    public BooleanOrOperator()
        : base("||", 1)
    {
    }

    public override double evaluate(double leftOperand, double rightOperand)
    {
        if (leftOperand == 1 || rightOperand == 1)
        {
            return 1;
        }

        return 0;
    }
}
