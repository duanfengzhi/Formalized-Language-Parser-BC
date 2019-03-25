
internal static class GlobalRandom
{
    private static System.Random randomInstance = null;

    internal static double NextDouble
    {
        get
        {
            if (randomInstance == null)
                randomInstance = new System.Random();

            return randomInstance.NextDouble();
        }
    }
}