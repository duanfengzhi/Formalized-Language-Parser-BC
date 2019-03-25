public class WordType
{
    private string type;
    private string code;

    public WordType(string type, string code)
    {
        this.type = type;
        this.code = code;
    }

    public override string ToString()
    {
        return "WordType [Type=" + type + ", Code=" + code + "]";
    }

    public string Type
    {
        get { return type; }
        set { this.type = value; }
    }

    public string Code
    {
        get { return code; }
        set { this.code = value; }
    }
}
