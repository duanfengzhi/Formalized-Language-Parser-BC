
internal class NextOperator
{


    private Operator @operator = null;


    private int index = -1;


    public NextOperator(Operator @operator, int index)
    {
        this.@operator = @operator;
        this.index = index;
    }

    public virtual Operator Operator
    {
        get
        {
            return @operator;
        }
    }


    public virtual int Index
    {
        get
        {
            return index;
        }
    }
}
