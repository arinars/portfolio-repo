using System;

/// <summary>
/// Summary description for KonanException
/// </summary>

public class KonanException : System.Web.HttpException
{
    public KonanException(string message)
        :
        base(message) // 메시지를 기본 클래스에 전달한다.
    {

    }
}

