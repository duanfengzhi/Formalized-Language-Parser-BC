using System;

public class WrongGrammarException : ApplicationException
{
    /// <summary>
    /// 规定：语法错误为3号异常(出现了不符合规定的语法)
    /// </summary>
    private const long serialVersionUID = 3L;

    public WrongGrammarException(string message, Exception cause) : base(message, cause) {}

    public WrongGrammarException(string message) : base(message) {}
}