using System;

namespace MyApiAcceptanceTests
{
    public static class HttpStatApiUris
    {
        public static readonly Uri Ok = new Uri("http://httpstat.us/200");
        public static readonly Uri Created = new Uri("http://httpstat.us/201");
        public static readonly Uri Accepted = new Uri("http://httpstat.us/202");
        public static readonly Uri NoContent = new Uri("http://httpstat.us/204");
        public static readonly Uri BadRequest = new Uri("http://httpstat.us/400");
        public static readonly Uri Conflict = new Uri("http://httpstat.us/409");
        public static readonly Uri SystemError = new Uri("http://httpstat.us/500");
    }
}
