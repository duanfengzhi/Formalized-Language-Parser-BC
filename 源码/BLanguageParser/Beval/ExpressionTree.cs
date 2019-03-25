using System;

public class ExpressionTree
{


    private object leftOperand = null;


    private object rightOperand = null;

    private Operator @operator = null;


    private Operator unaryOperator = null;

    private Evaluator evaluator = null;


    public ExpressionTree(Evaluator evaluator, object leftOperand, object rightOperand, Operator @operator, Operator unaryOperator)
    {

        this.evaluator = evaluator;
        this.leftOperand = leftOperand;
        this.rightOperand = rightOperand;
        this.@operator = @operator;
        this.unaryOperator = unaryOperator;
    }


    public virtual object LeftOperand
    {
        get
        {
            return leftOperand;
        }
    }


    public virtual object RightOperand
    {
        get
        {
            return rightOperand;
        }
    }


    public virtual Operator Operator
    {
        get
        {
            return @operator;
        }
    }


    public virtual Operator UnaryOperator
    {
        get
        {
            return unaryOperator;
        }
    }


    public virtual string evaluate(bool wrapStringFunctionResults)
    {

        string rtnResult = null;

        // Get the left operand.
        string leftResultString = null;
        double? leftResultDouble = null;

        if (leftOperand is ExpressionTree)
        {
            leftResultString = ((ExpressionTree)leftOperand).evaluate(wrapStringFunctionResults);

            try
            {
                leftResultDouble = Convert.ToDouble(leftResultString);
                leftResultString = null;
            }
            catch (System.FormatException)
            {
                leftResultDouble = null;
            }
        }
        else if (leftOperand is ExpressionOperand)
        {

            ExpressionOperand leftExpressionOperand = (ExpressionOperand)leftOperand;

            leftResultString = leftExpressionOperand.Value;
            leftResultString = evaluator.replaceVariables(leftResultString);


            if (!evaluator.isExpressionString(leftResultString))
            {
                try
                {
                    leftResultDouble = Convert.ToDouble(leftResultString);
                    leftResultString = null;
                }
                catch (System.FormatException nfe)
                {
                    throw new EvaluationException("Expression is invalid.", nfe);
                }

                if (leftExpressionOperand.UnaryOperator != null)
                {
                    leftResultDouble = new double?(leftExpressionOperand.UnaryOperator.evaluate(leftResultDouble.Value));
                }
            }
            else
            {
                if (leftExpressionOperand.UnaryOperator != null)
                {
                    throw new EvaluationException("Invalid operand for " + "unary operator.");
                }
            }
        }
        else if (leftOperand is ParsedFunction)
        {

            ParsedFunction parsedFunction = (ParsedFunction)leftOperand;
            Function function = parsedFunction.Function;
            string arguments = parsedFunction.Arguments;
            arguments = evaluator.replaceVariables(arguments);

            if (evaluator.ProcessNestedFunctions)
            {
                arguments = evaluator.processNestedFunctions(arguments);
            }

            try
            {
                FunctionResult functionResult = function.execute(evaluator, arguments);
                leftResultString = functionResult.Result;

                if (functionResult.Type == FunctionConstants.FUNCTION_RESULT_TYPE_NUMERIC)
                {

                    double? resultDouble = Convert.ToDouble(leftResultString);


                    if (parsedFunction.UnaryOperator != null)
                    {
                        resultDouble = new double?(parsedFunction.UnaryOperator.evaluate(resultDouble.Value));
                    }

                    // Get the final result.
                    leftResultString = resultDouble.ToString();
                }
                else if (functionResult.Type == FunctionConstants.FUNCTION_RESULT_TYPE_STRING)
                {

                    // The result must be a string result.
                    if (wrapStringFunctionResults)
                    {
                        leftResultString = evaluator.QuoteCharacter + leftResultString + evaluator.QuoteCharacter;
                    }

                    if (parsedFunction.UnaryOperator != null)
                    {
                        throw new EvaluationException("Invalid operand for " + "unary operator.");
                    }
                }
            }
            catch (FunctionException fe)
            {
                throw new EvaluationException(fe.Message, fe);
            }

            if (!evaluator.isExpressionString(leftResultString))
            {
                try
                {
                    leftResultDouble = Convert.ToDouble(leftResultString);
                    leftResultString = null;
                }
                catch (System.FormatException nfe)
                {
                    throw new EvaluationException("Expression is invalid.", nfe);
                }
            }
        }
        else
        {
            if (leftOperand != null)
            {
                throw new EvaluationException("Expression is invalid.");
            }
        }

        // Get the right operand.
        string rightResultString = null;
        double? rightResultDouble = null;

        if (rightOperand is ExpressionTree)
        {
            rightResultString = ((ExpressionTree)rightOperand).evaluate(wrapStringFunctionResults);

            try
            {
                rightResultDouble = Convert.ToDouble(rightResultString);
                rightResultString = null;
            }
            catch (System.FormatException)
            {
                rightResultDouble = null;
            }

        }
        else if (rightOperand is ExpressionOperand)
        {

            //JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
            //ORIGINAL LINE: final ExpressionOperand rightExpressionOperand = (ExpressionOperand) rightOperand;
            ExpressionOperand rightExpressionOperand = (ExpressionOperand)rightOperand;
            rightResultString = ((ExpressionOperand)rightOperand).Value;
            rightResultString = evaluator.replaceVariables(rightResultString);

            // Check if the operand is a string or not. If it not a string,
            // then it must be a number.
            if (!evaluator.isExpressionString(rightResultString))
            {
                try
                {
                    rightResultDouble = Convert.ToDouble(rightResultString);
                    rightResultString = null;
                }
                catch (System.FormatException nfe)
                {
                    throw new EvaluationException("Expression is invalid.", nfe);
                }

                if (rightExpressionOperand.UnaryOperator != null)
                {
                    rightResultDouble = new double?(rightExpressionOperand.UnaryOperator.evaluate(rightResultDouble.Value));
                }
            }
            else
            {
                if (rightExpressionOperand.UnaryOperator != null)
                {
                    throw new EvaluationException("Invalid operand for " + "unary operator.");
                }
            }
        }
        else if (rightOperand is ParsedFunction)
        {

            ParsedFunction parsedFunction = (ParsedFunction)rightOperand;

            Function function = parsedFunction.Function;
            string arguments = parsedFunction.Arguments;
            arguments = evaluator.replaceVariables(arguments);

            if (evaluator.ProcessNestedFunctions)
            {
                arguments = evaluator.processNestedFunctions(arguments);
            }

            try
            {
                FunctionResult functionResult = function.execute(evaluator, arguments);
                rightResultString = functionResult.Result;

                if (functionResult.Type == FunctionConstants.FUNCTION_RESULT_TYPE_NUMERIC)
                {

                    double? resultDouble = Convert.ToDouble(rightResultString);

                    // Process a unary operator if one exists.
                    if (parsedFunction.UnaryOperator != null)
                    {
                        resultDouble = new double?(parsedFunction.UnaryOperator.evaluate(resultDouble.Value));
                    }

                    // Get the final result.
                    rightResultString = resultDouble.ToString();
                }
                else if (functionResult.Type == FunctionConstants.FUNCTION_RESULT_TYPE_STRING)
                {

                    // The result must be a string result.
                    if (wrapStringFunctionResults)
                    {
                        rightResultString = evaluator.QuoteCharacter + rightResultString + evaluator.QuoteCharacter;
                    }

                    if (parsedFunction.UnaryOperator != null)
                    {
                        throw new EvaluationException("Invalid operand for " + "unary operator.");
                    }
                }
            }
            catch (FunctionException fe)
            {
                throw new EvaluationException(fe.Message, fe);
            }

            if (!evaluator.isExpressionString(rightResultString))
            {
                try
                {
                    rightResultDouble = Convert.ToDouble(rightResultString);
                    rightResultString = null;
                }
                catch (System.FormatException nfe)
                {
                    throw new EvaluationException("Expression is invalid.", nfe);
                }
            }
        }
        else if (rightOperand == null)
        {
            // Do nothing.
        }
        else
        {
            throw new EvaluationException("Expression is invalid.");
        }

        // Evaluate the the expression.
        if (leftResultDouble != null && rightResultDouble != null)
        {
            double doubleResult = @operator.evaluate(leftResultDouble.Value, rightResultDouble.Value);

            if (UnaryOperator != null)
            {
                doubleResult = UnaryOperator.evaluate(doubleResult);
            }

            rtnResult = (new double?(doubleResult)).ToString();
        }
        else if (!string.ReferenceEquals(leftResultString, null) && !string.ReferenceEquals(rightResultString, null))
        {
            rtnResult = @operator.evaluate(leftResultString, rightResultString);
        }
        else if (leftResultDouble != null && rightResultDouble == null)
        {
            double doubleResult = -1;

            if (unaryOperator != null)
            {
                doubleResult = unaryOperator.evaluate(leftResultDouble.Value);
            }
            else
            {
                // Do not allow numeric (left) and
                // string (right) to be evaluated together.
                throw new EvaluationException("Expression is invalid.");
            }

            rtnResult = (new double?(doubleResult)).ToString();
        }
        else
        {
            throw new EvaluationException("Expression is invalid.");
        }

        return rtnResult;
    }
}
