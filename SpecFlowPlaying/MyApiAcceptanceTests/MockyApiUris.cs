using System;

namespace MyApiAcceptanceTests
{
    public static class MockyApiUris
    {
        public static readonly Uri RequiredEmailAddressNotGiven = new Uri("http://www.mocky.io/v2/56718781400000f725d62b3f");
        public static readonly Uri RequiredClientIdNotGiven = new Uri("http://www.mocky.io/v2/566ee5aa100000d629718e30");
        public static readonly Uri RequiredProductIdNotGiven = new Uri("http://www.mocky.io/v2/566ee2551000002729718e2d");
        public static readonly Uri RequiredFormIdNotGiven = new Uri("http://www.mocky.io/v2/566ee2a71000002729718e2e");
        public static readonly Uri InvalidClientIdPassed = new Uri("http://www.mocky.io/v2/566ed77e100000b526718e1f");
        public static readonly Uri InvalidProductIdPassed = new Uri("http://www.mocky.io/v2/566ee5da100000d629718e32");
        public static readonly Uri InvalidFormIdPassed = new Uri("http://www.mocky.io/v2/566ed898100000f526718e21");
        public static readonly Uri InvalidEmailAddressPassed = new Uri("http://www.mocky.io/v2/566eeb41100000132a718e3a");
        public static readonly Uri InvalidTitlePassed = new Uri("http://www.mocky.io/v2/566ef4031000009b2c718e3f");
    }
}
