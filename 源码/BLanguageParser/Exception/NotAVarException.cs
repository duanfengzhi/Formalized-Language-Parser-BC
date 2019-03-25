using System;

public class NotAVarException : ApplicationException
{
    /// <summary>
    /// 规定：该内容不是一个变量为2号异常(在本该是变量的位置，存在了非法的字符)
    /// </summary>
    private const long serialVersionUID = 2L;

    public NotAVarException(string message, Exception cause) : base(message, cause) {}

    public NotAVarException(string message) : base(message) {}
}



