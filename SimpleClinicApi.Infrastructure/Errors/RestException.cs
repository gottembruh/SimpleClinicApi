using System;
using System.Net;

namespace SimpleClinicApi.Infrastructure.Errors;

public class RestException(HttpStatusCode code, object? errors = null) : Exception
{
    public object? Errors { get; } = errors;

    public HttpStatusCode Code { get; } = code;

    public override string Message
    {
        get
        {
            return Errors switch
            {
                null => base.Message,
                string s => s,
                Exception ex => ex.Message,
                _ => Errors.ToString() ?? string.Empty,
            };
        }
    }
}
