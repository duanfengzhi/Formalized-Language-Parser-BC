

/// The addition operator.

public class AdditionOperator : AbstractOperator
{


    public AdditionOperator()
        : base("+", 5, true)
    {
    }


    public override double evaluate(double leftOperand, double rightOperand)
    {
        double? rtnValue = new double?(leftOperand + rightOperand);

        return rtnValue.Value;
    }


    public override string evaluate(string leftOperand, string rightOperand)
    {
        string rtnValue = leftOperand.Substring(0, leftOperand.Length - 1) + rightOperand.Substring(1, rightOperand.Length - 1);

        return rtnValue;
    }


    public override double evaluate(double operand)
    {
        return operand;
    }
}
