using System.Collections;

/// <summary>
/// A groups of functions that can loaded at one time into an instance of
/// Evaluator.
/// </summary>
public interface FunctionGroup
{
    /// <summary>
    /// Returns the name of the function group.
    /// </summary>
    /// <returns> The name of this function group class. </returns>
    string Name { get; }

    /// <summary>
    /// Returns a list of the functions that are loaded by this class.
    /// </summary>
    /// <returns> A list of the functions loaded by this class. </returns>
    IList Functions { get; }

    /// <summary>
    /// Loads the functions in this function group into an instance of Evaluator.
    /// </summary>
    /// <param name="evaluator">
    ///            An instance of Evaluator to load the functions into. </param>
    void load(Evaluator evaluator);

    /// <summary>
    /// Unloads the functions in this function group from an instance of
    /// Evaluator.
    /// </summary>
    /// <param name="evaluator">
    ///            An instance of Evaluator to unload the functions from. </param>
    void unload(Evaluator evaluator);
}
