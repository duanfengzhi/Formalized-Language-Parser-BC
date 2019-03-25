

public class SubtractionOperator : AbstractOperator
{

    public SubtractionOperator()
        : base("-", 5, true)
    {
    }


    public override double evaluate(double leftOperand, double rightOperand)
    {
        double? rtnValue = new double?(leftOperand - rightOperand);

        return rtnValue.Value;
    }


    public override double evaluate(double operand)
    {
        return -operand;
    }
}
