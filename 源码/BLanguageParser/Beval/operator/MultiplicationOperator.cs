
public class MultiplicationOperator : AbstractOperator
{


    public MultiplicationOperator()
        : base("*", 6)
    {
    }

    public override double evaluate(double leftOperand, double rightOperand)
    {
        double? rtnValue = new double?(leftOperand * rightOperand);

        return rtnValue.Value;
    }
}
