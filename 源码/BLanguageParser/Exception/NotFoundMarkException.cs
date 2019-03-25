using System;

public class NotFoundMarkException : ApplicationException
{
    /// <summary>
    /// 规定：找不到goto语句的标签为4号异常(goto语句后的标签不合法)
    /// </summary>
    private const long serialVersionUID = 4L;

    public NotFoundMarkException(string message, Exception cause) : base(message, cause) {}

    public NotFoundMarkException(string message) : base(message) {}
}

