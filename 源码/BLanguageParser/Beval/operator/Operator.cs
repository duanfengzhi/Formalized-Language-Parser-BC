
public interface Operator
{

    double evaluate(double leftOperand, double rightOperand);

    string evaluate(string leftOperand, string rightOperand);

    double evaluate(double operand);


    string Symbol { get; }


    int Precedence { get; }


    int Length { get; }


    bool Unary { get; }
}
