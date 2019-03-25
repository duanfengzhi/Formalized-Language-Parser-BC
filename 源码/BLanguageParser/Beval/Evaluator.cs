using System;
using System.Collections;
using System.Text;


public class Evaluator
{


    private IList operators = new ArrayList();

    private IDictionary functions = new Hashtable();


    private IDictionary variables = new Hashtable();


    private char quoteCharacter = EvaluationConstants.SINGLE_QUOTE;

    private Operator openParenthesesOperator = new OpenParenthesesOperator();


    private Operator closedParenthesesOperator = new ClosedParenthesesOperator();


    private bool loadMathVariables;


    private bool loadMathFunctions;


    private bool loadStringFunctions;

    private bool processNestedFunctions_Renamed;


    private string previousExpression = null;


    private Stack previousOperatorStack = null;


    private Stack previousOperandStack = null;


    private Stack operatorStack = null;


    private Stack operandStack = null;


    private VariableResolver variableResolver = null;

    public Evaluator()
        : this(EvaluationConstants.SINGLE_QUOTE, true, true, true, true)
    {
    }

    public Evaluator(char quoteCharacter, bool loadMathVariables, bool loadMathFunctions, bool loadStringFunctions, bool processNestedFunctions)
    {


        installOperators();


        this.loadMathVariables = loadMathVariables;
        loadSystemVariables();

        this.loadMathFunctions = loadMathFunctions;
        this.loadStringFunctions = loadStringFunctions;
        loadSystemFunctions();


        QuoteCharacter = quoteCharacter;


        this.processNestedFunctions_Renamed = processNestedFunctions;
    }


    public virtual char QuoteCharacter
    {
        get
        {
            return quoteCharacter;
        }
        set
        {
            if (value == EvaluationConstants.SINGLE_QUOTE || value == EvaluationConstants.DOUBLE_QUOTE)
            {
                this.quoteCharacter = value;
            }
            else
            {
                throw new System.ArgumentException("Invalid quote character.");
            }
        }
    }


    public virtual void putFunction(Function function)
    {

        isValidName(function.Name);



        Function existingFunction = (Function)functions[function.Name];

        if (existingFunction == null)
        {
            functions[function.Name] = function;
        }
        else
        {
            throw new System.ArgumentException("A function with the same name " + "already exists.");
        }
    }


    public virtual Function getFunction(string functionName)
    {
        return (Function)functions[functionName];
    }


    public virtual void removeFunction(string functionName)
    {
        if (functions.Contains(functionName))
        {
            functions.Remove(functionName);
        }
        else
        {
            throw new System.ArgumentException("The function does not exist.");
        }
    }

    public virtual void clearFunctions()
    {
        // Remove all functions.
        functions.Clear();

        // Reload the system functions if necessary.
        loadSystemFunctions();
    }


    public virtual IDictionary Functions
    {
        get
        {
            return functions;
        }
        set
        {
            this.functions = value;
        }
    }



    public virtual void putVariable(string variableName, string variableValue)
    {
        // Make sure the variable name is valid.
        isValidName(variableName);

        variables[variableName] = variableValue;
    }


    public virtual string getVariableValue(string variableName)
    {

        string variableValue = null;


        if (variableResolver != null)
        {

            try
            {
                variableValue = variableResolver.resolveVariable(variableName);
            }
            catch (FunctionException fe)
            {
                throw new EvaluationException(fe.Message, fe);
            }
        }


        if (string.ReferenceEquals(variableValue, null))
        {

            variableValue = (string)variables[variableName];
        }

        if (string.ReferenceEquals(variableValue, null))
        {

            throw new EvaluationException("Can not resolve variable with name equal to \"" + variableName + "\".");
        }

        return variableValue;
    }


    public virtual void removeVaraible(string variableName)
    {
        if (variables.Contains(variableName))
        {
            variables.Remove(variableName);
        }
        else
        {
            throw new System.ArgumentException("The variable does not exist.");
        }
    }

    public virtual void clearVariables()
    {

        variables.Clear();

        loadSystemVariables();
    }


    public virtual IDictionary Variables
    {
        get
        {
            return variables;
        }
        set
        {
            this.variables = value;
        }
    }



    public virtual VariableResolver VariableResolver
    {
        get
        {
            return variableResolver;
        }
        set
        {
            this.variableResolver = value;
        }
    }



    public virtual string evaluate(string expression)
    {
        return evaluate(expression, true, true);
    }


    public virtual string evaluate()
    {

        string expression = previousExpression;

        if (string.ReferenceEquals(expression, null) || expression.Length == 0)
        {
            throw new EvaluationException("No expression has been specified.");
        }

        return evaluate(expression, true, true);
    }


    public virtual string evaluate(string expression, bool keepQuotes, bool wrapStringFunctionResults)
    {


        parse(expression);

        string result = getResult(operatorStack, operandStack, wrapStringFunctionResults);

        if (isExpressionString(result) && !keepQuotes)
        {
            result = result.Substring(1, (result.Length - 1) - 1);
        }

        return result;
    }



    public virtual string evaluate(bool keepQuotes, bool wrapStringFunctionResults)
    {


        string expression = previousExpression;

        if (string.ReferenceEquals(expression, null) || expression.Length == 0)
        {
            throw new EvaluationException("No expression has been specified.");
        }

        return evaluate(expression, keepQuotes, wrapStringFunctionResults);
    }


    public virtual bool getBooleanResult(string expression)
    {


        string result = evaluate(expression);

        try
        {
            double? doubleResult = Convert.ToDouble(result);

            if (doubleResult.Value == 1.0)
            {
                return true;
            }
        }
        catch (System.FormatException)
        {
            return false;
        }

        return false;
    }


    public virtual double getNumberResult(string expression)
    {

        string result = evaluate(expression);
        double? doubleResult = null;

        try
        {
            doubleResult = Convert.ToDouble(result);
        }
        catch (System.FormatException nfe)
        {
            throw new EvaluationException("Expression does not produce a number.", nfe);
        }

        return doubleResult.Value;
    }


    public virtual void parse(string expression)
    {

        // Save the expression.
        bool parse = true;
        if (!expression.Equals(previousExpression))
        {
            previousExpression = expression;
        }
        else
        {
            parse = false;
            operatorStack = (Stack)previousOperatorStack.Clone();
            operandStack = (Stack)previousOperandStack.Clone();
        }

        try
        {
            if (parse)
            {

                operandStack = new Stack();
                operatorStack = new Stack();


                bool haveOperand = false;
                bool haveOperator = false;
                Operator unaryOperator = null;


                int numChars = expression.Length;
                int charCtr = 0;

                while (charCtr < numChars)
                {
                    Operator @operator = null;
                    int operatorIndex = -1;


                    if (EvaluationHelper.isSpace(expression[charCtr]))
                    {
                        charCtr++;
                        continue;
                    }


                    NextOperator nextOperator = getNextOperator(expression, charCtr, null);

                    if (nextOperator != null)
                    {
                        @operator = nextOperator.Operator;
                        operatorIndex = nextOperator.Index;
                    }

                    if (operatorIndex > charCtr || operatorIndex == -1)
                    {
                        charCtr = processOperand(expression, charCtr, operatorIndex, operandStack, unaryOperator);

                        haveOperand = true;
                        haveOperator = false;
                        unaryOperator = null;
                    }

                    // Check if it is time to process an operator.
                    if (operatorIndex == charCtr)
                    {
                        if (nextOperator.Operator.Unary && (haveOperator || charCtr == 0))
                        {
                            charCtr = processUnaryOperator(operatorIndex, nextOperator.Operator);

                            if (unaryOperator == null)
                            {
                                // We have an unary operator.
                                unaryOperator = nextOperator.Operator;
                            }
                            else
                            {
                                throw new EvaluationException("Consecutive unary " + "operators are not allowed (index=" + charCtr + ").");
                            }
                        }
                        else
                        {
                            charCtr = processOperator(expression, operatorIndex, @operator, operatorStack, operandStack, haveOperand, unaryOperator);

                            unaryOperator = null;
                        }

                        if (!(nextOperator.Operator is ClosedParenthesesOperator))
                        {
                            haveOperand = false;
                            haveOperator = true;
                        }
                    }
                }

                // Save the parsed operators and operands.
                previousOperatorStack = (Stack)operatorStack.Clone();
                previousOperandStack = (Stack)operandStack.Clone();
            }
        }
        catch (Exception e)
        {
            // Clear the previous expression, because it is invalid.
            previousExpression = "";

            throw new EvaluationException(e.Message, e);
        }
    }


    private void installOperators()
    {

        operators.Add(openParenthesesOperator);
        operators.Add(closedParenthesesOperator);
        operators.Add(new AdditionOperator());
        operators.Add(new SubtractionOperator());
        operators.Add(new MultiplicationOperator());
        operators.Add(new DivisionOperator());
        operators.Add(new EqualOperator());
        operators.Add(new NotEqualOperator());

        operators.Add(new LessThanOrEqualOperator()); // Length of 2.
        operators.Add(new LessThanOperator()); // Length of 1.
        operators.Add(new GreaterThanOrEqualOperator()); // Length of 2.
        operators.Add(new GreaterThanOperator()); // Length of 1.


        operators.Add(new BooleanAndOperator());
        operators.Add(new BooleanOrOperator());
        operators.Add(new BooleanNotOperator());
        operators.Add(new ModulusOperator());
    }


    private int processOperand(string expression, int charCtr, int operatorIndex, Stack operandStack, Operator unaryOperator)
    {

        string operandString = null;
        int rtnCtr = -1;

        // Get the operand to process.
        if (operatorIndex == -1)
        {
            operandString = expression.Substring(charCtr).Trim();
            rtnCtr = expression.Length;
        }
        else
        {
            operandString = expression.Substring(charCtr, operatorIndex - charCtr).Trim();
            rtnCtr = operatorIndex;
        }

        if (operandString.Length == 0)
        {
            throw new EvaluationException("Expression is invalid.");
        }


        ExpressionOperand operand = new ExpressionOperand(operandString, unaryOperator);
        operandStack.Push(operand);

        return rtnCtr;
    }


    private int processOperator(string expression, int originalOperatorIndex, Operator originalOperator, Stack operatorStack, Stack operandStack, bool haveOperand, Operator unaryOperator)
    {

        int operatorIndex = originalOperatorIndex;
        Operator @operator = originalOperator;


        if (haveOperand && @operator is OpenParenthesesOperator)
        {
            NextOperator nextOperator = processFunction(expression, operatorIndex, operandStack);

            @operator = nextOperator.Operator;
            operatorIndex = nextOperator.Index + @operator.Length;

            nextOperator = getNextOperator(expression, operatorIndex, null);

            if (nextOperator != null)
            {
                @operator = nextOperator.Operator;
                operatorIndex = nextOperator.Index;
            }
            else
            {
                return operatorIndex;
            }
        }


        if (@operator is OpenParenthesesOperator)
        {

            ExpressionOperator expressionOperator = new ExpressionOperator(@operator, unaryOperator);
            operatorStack.Push(expressionOperator);
        }
        else if (@operator is ClosedParenthesesOperator)
        {
            ExpressionOperator stackOperator = null;

            if (operatorStack.Count > 0)
            {
                stackOperator = (ExpressionOperator)operatorStack.Peek();
            }

            // Process until we reach an open parentheses.
            while (stackOperator != null && !(stackOperator.Operator is OpenParenthesesOperator))
            {
                processTree(operandStack, operatorStack);

                if (operatorStack.Count > 0)
                {
                    stackOperator = (ExpressionOperator)operatorStack.Peek();
                }
                else
                {
                    stackOperator = null;
                }
            }

            if (operatorStack.Count == 0)
            {
                throw new EvaluationException("Expression is invalid.");
            }

            ExpressionOperator expressionOperator = (ExpressionOperator)operatorStack.Pop();

            if (!(expressionOperator.Operator is OpenParenthesesOperator))
            {
                throw new EvaluationException("Expression is invalid.");
            }

            // Process the unary operator if we have one.
            if (expressionOperator.UnaryOperator != null)
            {
                object operand = operandStack.Pop();

                ExpressionTree tree = new ExpressionTree(this, operand, null, null, expressionOperator.UnaryOperator);

                operandStack.Push(tree);
            }
        }
        else
        {
            // Process non-param operator.
            if (operatorStack.Count > 0)
            {
                ExpressionOperator stackOperator = (ExpressionOperator)operatorStack.Peek();

                while (stackOperator != null && stackOperator.Operator.Precedence >= @operator.Precedence)
                {
                    processTree(operandStack, operatorStack);

                    if (operatorStack.Count > 0)
                    {
                        stackOperator = (ExpressionOperator)operatorStack.Peek();
                    }
                    else
                    {
                        stackOperator = null;
                    }
                }
            }

            ExpressionOperator expressionOperator = new ExpressionOperator(@operator, unaryOperator);

            operatorStack.Push(expressionOperator);
        }


        int rtnCtr = operatorIndex + @operator.Length;

        return rtnCtr;
    }


    private int processUnaryOperator(int operatorIndex, Operator @operator)
    {

        int rtnCtr = operatorIndex + @operator.Symbol.Length;

        return rtnCtr;
    }


    private NextOperator processFunction(string expression, int operatorIndex, Stack operandStack)
    {

        int parenthesisCount = 1;
        NextOperator nextOperator = null;
        int nextOperatorIndex = operatorIndex;

        // Loop until we find the function's closing parentheses.
        while (parenthesisCount > 0)
        {
            nextOperator = getNextOperator(expression, nextOperatorIndex + 1, null);

            if (nextOperator == null)
            {
                throw new EvaluationException("Function is not closed.");
            }
            else if (nextOperator.Operator is OpenParenthesesOperator)
            {
                parenthesisCount++;
            }
            else if (nextOperator.Operator is ClosedParenthesesOperator)
            {
                parenthesisCount--;
            }

            // Get the next operator index.
            nextOperatorIndex = nextOperator.Index;
        }


        string arguments = expression.Substring(operatorIndex + 1, nextOperatorIndex - (operatorIndex + 1));

        // Pop the function name from the stack.
        ExpressionOperand operand = (ExpressionOperand)operandStack.Pop();
        Operator unaryOperator = operand.UnaryOperator;
        string functionName = operand.Value;

        // Validate that the function name is valid.
        try
        {
            isValidName(functionName);
        }
        catch (System.ArgumentException iae)
        {
            throw new EvaluationException("Invalid function name of \"" + functionName + "\".", iae);
        }

        // Get the function object.
        //JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
        //ORIGINAL LINE: final net.sourceforge.jeval.function.Function function = (net.sourceforge.jeval.function.Function) functions.get(functionName);
        Function function = (Function)functions[functionName];

        if (function == null)
        {
            throw new EvaluationException("A function is not defined (index=" + operatorIndex + ").");
        }


        ParsedFunction parsedFunction = new ParsedFunction(function, arguments, unaryOperator);
        operandStack.Push(parsedFunction);

        return nextOperator;
    }


    private void processTree(Stack operandStack, Stack operatorStack)
    {

        object rightOperand = null;
        object leftOperand = null;
        Operator @operator = null;

        // Get the right operand node from the tree.
        if (operandStack.Count > 0)
        {
            rightOperand = operandStack.Pop();
        }

        // Get the left operand node from the tree.
        if (operandStack.Count > 0)
        {
            leftOperand = operandStack.Pop();
        }

        // Get the operator node from the tree.
        @operator = ((ExpressionOperator)operatorStack.Pop()).Operator;


        ExpressionTree tree = new ExpressionTree(this, leftOperand, rightOperand, @operator, null);

        // Push the tree onto the stack.
        operandStack.Push(tree);
    }


    private string getResult(Stack operatorStack, Stack operandStack, bool wrapStringFunctionResults)
    {

        // The result to return.
        string resultString = null;

        // Process the rest of the operators left on the stack.
        while (operatorStack.Count > 0)
        {
            processTree(operandStack, operatorStack);
        }


        if (operandStack.Count != 1)
        {
            throw new EvaluationException("Expression is invalid.");
        }

        object finalOperand = operandStack.Pop();

        // Check if the final operand is a tree.
        if (finalOperand is ExpressionTree)
        {
            // Get the final result.
            resultString = ((ExpressionTree)finalOperand).evaluate(wrapStringFunctionResults);
        }
        // Check if the final operand is an operand.
        else if (finalOperand is ExpressionOperand)
        {
            ExpressionOperand resultExpressionOperand = (ExpressionOperand)finalOperand;

            resultString = ((ExpressionOperand)finalOperand).Value;
            resultString = replaceVariables(resultString);

            // Check if the operand is a string or not. If it not a string,
            // then it must be a number.
            if (!isExpressionString(resultString))
            {
                double? resultDouble = null;
                try
                {
                    resultDouble = Convert.ToDouble(resultString);
                }
                catch (Exception e)
                {
                    throw new EvaluationException("Expression is invalid.", e);
                }

                // Process a unary operator if one exists.
                if (resultExpressionOperand.UnaryOperator != null)
                {
                    resultDouble = new double?(resultExpressionOperand.UnaryOperator.evaluate(resultDouble.Value));
                }

                // Get the final result.
                resultString = resultDouble.ToString();
            }
            else
            {
                if (resultExpressionOperand.UnaryOperator != null)
                {
                    throw new EvaluationException("Invalid operand for " + "unary operator.");
                }
            }
        }
        else if (finalOperand is ParsedFunction)
        {

            ParsedFunction parsedFunction = (ParsedFunction)finalOperand;

            Function function = parsedFunction.Function;
            string arguments = parsedFunction.Arguments;

            if (processNestedFunctions_Renamed)
            {
                arguments = processNestedFunctions(arguments);
            }

            arguments = replaceVariables(arguments);

            // Get the final result.
            try
            {
                FunctionResult functionResult = function.execute(this, arguments);
                resultString = functionResult.Result;

                if (functionResult.Type == FunctionConstants.FUNCTION_RESULT_TYPE_NUMERIC)
                {

                    double? resultDouble = Convert.ToDouble(resultString);

                    // Process a unary operator if one exists.
                    if (parsedFunction.UnaryOperator != null)
                    {
                        resultDouble = new double?(parsedFunction.UnaryOperator.evaluate(resultDouble.Value));
                    }

                    // Get the final result.
                    resultString = resultDouble.ToString();
                }
                else if (functionResult.Type == FunctionConstants.FUNCTION_RESULT_TYPE_STRING)
                {

                    // The result must be a string result.
                    if (wrapStringFunctionResults)
                    {
                        resultString = quoteCharacter + resultString + quoteCharacter;
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
        }
        else
        {
            throw new EvaluationException("Expression is invalid.");
        }

        return resultString;
    }


    private NextOperator getNextOperator(string expression, int start, Operator match)
    {


        int numChars = expression.Length;
        int numQuoteCharacters = 0;

        for (int charCtr = start; charCtr < numChars; charCtr++)
        {
            // Keep track of open strings.
            if (expression[charCtr] == quoteCharacter)
            {
                numQuoteCharacters++;
            }

            // Do not look into open strings.
            if ((numQuoteCharacters % 2) == 1)
            {
                continue;
            }

            int numOperators = operators.Count;
            for (int operatorCtr = 0; operatorCtr < numOperators; operatorCtr++)
            {
                Operator @operator = (Operator)operators[operatorCtr];

                if (match != null)
                {
                    // Look through the operators until we find the
                    // one we are searching for.
                    if (!match.Equals(@operator))
                    {
                        continue;
                    }
                }

                // The operator can 1 or 2 characters in length.
                if (@operator.Length == 2)
                {
                    int endCtr = -1;
                    if (charCtr + 2 <= expression.Length)
                    {
                        endCtr = charCtr + 2;
                    }
                    else
                    {
                        endCtr = expression.Length;
                    }

                    // Look for a match.
                    if (expression.Substring(charCtr, endCtr - charCtr).Equals(@operator.Symbol))
                    {
                        NextOperator nextOperator = new NextOperator(@operator, charCtr);

                        return nextOperator;
                    }
                }
                else
                {
                    // Look for a match.
                    if (expression[charCtr] == @operator.Symbol[0])
                    {
                        NextOperator nextOperator = new NextOperator(@operator, charCtr);

                        return nextOperator;
                    }
                }
            }
        }

        return null;
    }


    protected internal virtual bool isExpressionString(string expressionString)
    {

        if (expressionString.Length > 1 && expressionString[0] == quoteCharacter && expressionString[expressionString.Length - 1] == quoteCharacter)
        {
            return true;
        }

        if (expressionString.IndexOf(quoteCharacter) >= 0)
        {
            throw new EvaluationException("Invalid use of quotes.");
        }

        return false;
    }


    public virtual void isValidName(string name)
    {

        if (name.Length == 0)
        {
            throw new System.ArgumentException("Variable is empty.");
        }

        // Check if name starts with a number.

        char firstChar = name[0];
        if (firstChar >= '0' && firstChar <= '9')
        {
            throw new System.ArgumentException("A variable or function name " + "can not start with a number.");
        }

        // Check if name contains with a quote character.
        if (name.IndexOf(EvaluationConstants.SINGLE_QUOTE) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain a quote character.");
        }
        else if (name.IndexOf(EvaluationConstants.DOUBLE_QUOTE) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain a quote character.");
        }

        // Check if name contains with a brace character.
        if (name.IndexOf(EvaluationConstants.OPEN_BRACE) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain an open brace character.");
        }
        else if (name.IndexOf(EvaluationConstants.CLOSED_BRACE) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain a closed brace character.");
        }
        else if (name.IndexOf(EvaluationConstants.POUND_SIGN) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain a pound sign character.");
        }


        IEnumerator operatorIterator = operators.GetEnumerator();

        while (operatorIterator.MoveNext())
        {

            Operator @operator = (Operator)operatorIterator.Current;

            if (name.IndexOf(@operator.Symbol, StringComparison.Ordinal) > -1)
            {
                throw new System.ArgumentException("A variable or function name " + "can not contain an operator symbol.");
            }
        }

        // Check if name contains other special characters.
        if (name.IndexOf("!", StringComparison.Ordinal) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain a special character.");
        }
        else if (name.IndexOf("~", StringComparison.Ordinal) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain a special character.");
        }
        else if (name.IndexOf("^", StringComparison.Ordinal) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain a special character.");
        }
        else if (name.IndexOf(",", StringComparison.Ordinal) > -1)
        {
            throw new System.ArgumentException("A variable or function name " + "can not contain a special character.");
        }
    }


    private void loadSystemFunctions()
    {
        // Install the math functions.
        if (loadMathFunctions)
        {

            FunctionGroup mathFunctions = new MathFunctions();

            mathFunctions.load(this);
        }

        // Install the string functions.
        if (loadStringFunctions)
        {

            FunctionGroup stringFunctions = new StringFunctions();

            stringFunctions.load(this);
        }
    }


    private void loadSystemVariables()
    {
        // Install the math variables.
        if (loadMathVariables)
        {
            // Add the two math variables.
            putVariable("E", (new double?(Math.E)).ToString());
            putVariable("PI", (new double?(Math.PI)).ToString());
        }
    }


    public virtual string replaceVariables(string expression)
    {

        int openIndex = expression.IndexOf(EvaluationConstants.OPEN_VARIABLE, StringComparison.Ordinal);

        if (openIndex < 0)
        {
            return expression;
        }

        string replacedExpression = expression;

        while (openIndex >= 0)
        {

            int closedIndex = -1;
            if (openIndex >= 0)
            {

                closedIndex = replacedExpression.IndexOf(EvaluationConstants.CLOSED_VARIABLE, openIndex + 1, StringComparison.Ordinal);
                if (closedIndex > openIndex)
                {

                    string variableName = StringHelperClass.SubstringSpecial(replacedExpression, openIndex + EvaluationConstants.OPEN_VARIABLE.Length, closedIndex);

                    // Validate that the variable name is valid.
                    try
                    {
                        isValidName(variableName);
                    }
                    catch (System.ArgumentException iae)
                    {
                        throw new EvaluationException("Invalid variable name of \"" + variableName + "\".", iae);
                    }

                    string variableValue = getVariableValue(variableName);

                    string variableString = EvaluationConstants.OPEN_VARIABLE + variableName + EvaluationConstants.CLOSED_VARIABLE;

                    replacedExpression = EvaluationHelper.replaceAll(replacedExpression, variableString, variableValue);
                }
                else
                {

                    break;
                }
            }


            openIndex = replacedExpression.IndexOf(EvaluationConstants.OPEN_VARIABLE, StringComparison.Ordinal);
        }

        // If an open brace is left over, then a variable could not be replaced.
        int openBraceIndex = replacedExpression.IndexOf(EvaluationConstants.OPEN_VARIABLE, StringComparison.Ordinal);
        if (openBraceIndex > -1)
        {
            throw new EvaluationException("A variable has not been closed (index=" + openBraceIndex + ").");
        }

        return replacedExpression;
    }


    protected internal virtual string processNestedFunctions(string arguments)
    {

        StringBuilder evaluatedArguments = new StringBuilder();

        // Process nested function calls.
        if (arguments.Length > 0)
        {

            Evaluator argumentsEvaluator = new Evaluator(quoteCharacter, loadMathVariables, loadMathFunctions, loadStringFunctions, processNestedFunctions_Renamed);
            argumentsEvaluator.Functions = Functions;
            argumentsEvaluator.Variables = Variables;
            argumentsEvaluator.VariableResolver = VariableResolver;


            ArgumentTokenizer tokenizer = new ArgumentTokenizer(arguments, EvaluationConstants.FUNCTION_ARGUMENT_SEPARATOR);

            IList evalautedArgumentList = new ArrayList();
            while (tokenizer.hasMoreTokens())
            {

                string argument = tokenizer.nextToken().Trim();

                try
                {
                    argument = argumentsEvaluator.evaluate(argument);
                }
                catch (Exception e)
                {
                    throw new EvaluationException(e.Message, e);
                }

                evalautedArgumentList.Add(argument);
            }

            IEnumerator evaluatedArgumentIterator = evalautedArgumentList.GetEnumerator();

            while (evaluatedArgumentIterator.MoveNext())
            {

                if (evaluatedArguments.Length > 0)
                {

                    evaluatedArguments.Append(EvaluationConstants.FUNCTION_ARGUMENT_SEPARATOR);
                }

                string evaluatedArgument = (string)evaluatedArgumentIterator.Current;
                evaluatedArguments.Append(evaluatedArgument);
            }
        }

        return evaluatedArguments.ToString();
    }


    public virtual bool LoadMathVariables
    {
        get
        {
            return loadMathVariables;
        }
    }


    public virtual bool LoadMathFunctions
    {
        get
        {
            return loadMathFunctions;
        }
    }


    public virtual bool LoadStringFunctions
    {
        get
        {
            return loadStringFunctions;
        }
    }


    public virtual bool ProcessNestedFunctions
    {
        get
        {
            return processNestedFunctions_Renamed;
        }
    }
}
