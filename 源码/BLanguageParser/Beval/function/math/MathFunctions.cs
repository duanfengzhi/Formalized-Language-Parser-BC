using System;
using System.Collections;

/// <summary>
/// A groups of functions that can loaded at one time into an instance of
/// Evaluator. This group contains all of the functions located in the
/// net.sourceforge.jeval.function.math package.
/// </summary>
public class MathFunctions : FunctionGroup
{
    /// <summary>
    /// Used to store instances of all of the functions loaded by this class.
    /// </summary>
    private IList functions = new ArrayList();

    /// <summary>
    /// Default contructor for this class. The functions loaded by this class are
    /// instantiated in this constructor.
    /// </summary>
    public MathFunctions()
    {
        functions.Add(new Abs());
        functions.Add(new Acos());
        functions.Add(new Asin());
        functions.Add(new Atan());
        functions.Add(new Atan2());
        functions.Add(new Ceil());
        functions.Add(new Cos());
        functions.Add(new Exp());
        functions.Add(new Floor());
        functions.Add(new IEEEremainder());
        functions.Add(new Log());
        functions.Add(new Max());
        functions.Add(new Min());
        functions.Add(new Pow());
        functions.Add(new Random());
        functions.Add(new Sqrt());
    }

    /// <summary>
    /// Returns the name of the function group - "numberFunctions".
    /// </summary>
    /// <returns> The name of this function group class. </returns>
    public virtual string Name
    {
        get
        {
            return "numberFunctions";
        }
    }

    /// <summary>
    /// Returns a list of the functions that are loaded by this class.
    /// </summary>
    /// <returns> A list of the functions loaded by this class. </returns>
    public virtual IList Functions
    {
        get
        {
            return functions;
        }
    }

    /// <summary>
    /// Loads the functions in this function group into an instance of Evaluator.
    /// </summary>
    /// <param name="evaluator">
    ///            An instance of Evaluator to load the functions into. </param>
    //ORIGINAL LINE: public void load(final net.sourceforge.jeval.Evaluator evaluator)
    public virtual void load(Evaluator evaluator)
    {
        IEnumerator functionIterator = functions.GetEnumerator();

        while (functionIterator.MoveNext())
        {
            evaluator.putFunction((Function)functionIterator.Current);
        }
    }

    /// <summary>
    /// Unloads the functions in this function group from an instance of
    /// Evaluator.
    /// </summary>
    /// <param name="evaluator">
    ///            An instance of Evaluator to unload the functions from. </param>
    //ORIGINAL LINE: public void unload(final net.sourceforge.jeval.Evaluator evaluator)
    public virtual void unload(Evaluator evaluator)
    {
        IEnumerator functionIterator = functions.GetEnumerator();

        while (functionIterator.MoveNext())
        {
            evaluator.removeFunction(((Function)functionIterator.Current).Name);
        }
    }
}

