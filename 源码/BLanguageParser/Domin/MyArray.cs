public class MyArray
{
    private string name;
    private int[] m_Array;

    public MyArray(string name, int num)
    {
        this.name = name;
        this.m_Array = new int[num];
    }

    public string Name
    {
        get { return name; }
        set { this.name = value; }
    }

    public int[] M_Array
    {
        get { return m_Array; }
        set { this.m_Array = value; }
    }

    public void setBMyArray(int num1, int num2)
    {
        m_Array[num1] = num2;
    }

    public string putBMyArray()
    {
        string Bparseresult = "";
        for (int i = 0; i < m_Array.Length; i++)
        {
            Bparseresult += (m_Array[i] + " ");
        }
        return Bparseresult;
    }
}
