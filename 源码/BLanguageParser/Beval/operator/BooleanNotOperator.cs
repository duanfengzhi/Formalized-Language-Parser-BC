
public class BooleanNotOperator : AbstractOperator
{


    public BooleanNotOperator()
        : base("!", 0, true)
    {
    }


    public override double evaluate(double operand)
    {
        if (operand == 1)
        {
            return 0;
        }

        return 1;
    }
}
