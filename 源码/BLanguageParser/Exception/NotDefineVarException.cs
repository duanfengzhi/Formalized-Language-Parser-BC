using System;

public class NotDefineVarException : ApplicationException
{
    /// <summary>
    /// 规定：变量没有定义为 1号异常（使用的变量未定义）
    /// </summary>
    private const long serialVersionUID = 1L;

    public NotDefineVarException(string message, Exception cause) : base(message, cause) {}

    public NotDefineVarException(string message) : base(message) {}
}