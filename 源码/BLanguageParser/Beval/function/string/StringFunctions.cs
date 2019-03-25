using System.Collections;

public class StringFunctions : FunctionGroup
{
    private IList functions = new ArrayList();
    public StringFunctions()
    {
        functions.Add(new CharAt());
        functions.Add(new CompareTo());
        functions.Add(new CompareToIgnoreCase());
        functions.Add(new Concat());
        functions.Add(new EndsWith());
        functions.Add(new Equals());
        functions.Add(new EqualsIgnoreCase());
        functions.Add(new Eval());
        functions.Add(new IndexOf());
        functions.Add(new LastIndexOf());
        functions.Add(new Length());
        functions.Add(new Replace());
        functions.Add(new StartsWith());
        functions.Add(new Substring());
        functions.Add(new ToLowerCase());
        functions.Add(new ToUpperCase());
        functions.Add(new Trim());
    }

    public virtual string Name
    {
        get
        {
            return "stringFunctions";
        }
    }

    public virtual IList Functions
    {
        get
        {
            return functions;
        }
    }

    public virtual void load(Evaluator evaluator)
    {
        IEnumerator functionIterator = functions.GetEnumerator();

        while (functionIterator.MoveNext())
        {
            evaluator.putFunction((Function)functionIterator.Current);
        }
    }

    public virtual void unload(Evaluator evaluator)
    {
        IEnumerator functionIterator = functions.GetEnumerator();
        while (functionIterator.MoveNext())
        {
            evaluator.removeFunction(((Function)functionIterator.Current).Name);
        }
    }
}

