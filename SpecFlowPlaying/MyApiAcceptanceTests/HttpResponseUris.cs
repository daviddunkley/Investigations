using System;

namespace MyApiAcceptanceTests
{
    public static class HttpResponseUris
    {
        public static readonly Uri Http200Ok = new Uri("http://httpstat.us/200");
        public static readonly Uri Http201Created = new Uri("http://httpstat.us/201");
        public static readonly Uri Http202Accepted = new Uri("http://httpstat.us/202");
        public static readonly Uri Http204NoContent = new Uri("http://httpstat.us/204");
        public static readonly Uri Http400BadRequest = new Uri("http://httpstat.us/400");
        public static readonly Uri Http409Conflict = new Uri("http://httpstat.us/409");
        public static readonly Uri Http500SystemError = new Uri("http://httpstat.us/500");
    }
}
