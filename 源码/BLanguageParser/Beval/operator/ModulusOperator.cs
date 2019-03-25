
public class ModulusOperator : AbstractOperator
{

    public ModulusOperator()
        : base("%", 6)
    {
    }

    public override double evaluate(double leftOperand, double rightOperand)
    {
        double? rtnValue = new double?(leftOperand % rightOperand);

        return rtnValue.Value;
    }
}
