

public class NotEqualOperator : AbstractOperator
{


    public NotEqualOperator()
        : base("!=", 3)
    {
    }

    public override double evaluate(double leftOperand, double rightOperand)
    {
        if (leftOperand != rightOperand)
        {
            return 1;
        }

        return 0;
    }


    public override string evaluate(string leftOperand, string rightOperand)
    {
        if (leftOperand.CompareTo(rightOperand) != 0)
        {
            return EvaluationConstants.BOOLEAN_STRING_TRUE;
        }

        return EvaluationConstants.BOOLEAN_STRING_FALSE;
    }
}
