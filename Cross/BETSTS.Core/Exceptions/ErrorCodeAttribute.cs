using Microsoft.AspNetCore.Http;
using System;

namespace BETSTS.Core.Exceptions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ErrorCodeAttribute : Attribute
    {
        public int HttpResponseCode { get; set; } = StatusCodes.Status400BadRequest;

        public string Group { get; set; } = string.Empty;

        public string DefaultMessage { get; set; } = string.Empty;

        public ErrorCodeAttribute()
        {
        }

        public ErrorCodeAttribute(string group)
        {
            Group = group;
        }

        public ErrorCodeAttribute(string group, string defaultMessage) : this(group)
        {
            DefaultMessage = defaultMessage;
        }

        public ErrorCodeAttribute(string group, string defaultMessage, int httpResponseCode) : this(group, defaultMessage)
        {
            HttpResponseCode = httpResponseCode;
        }
    }
}