using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants
{
    public class Constant
    {
        public class AppSettings
        {
            public const string DEFAULT_CONTROLLER_ROUTE = "api/[controller]";
            public const string DEFAULT_CONNECTION = "DefaultConnection";
            public const string AUTH_SECRET = "JWT:SecretKey";
        }
        public class DefaultValue
        {
            public const int DEFAULT_PAGE_SIZE = 10;
            public const int DEFAULT_PAGE_NO = 1;
            public const string DEFAULT_CONTROLLER_ROUTE = "api/[controller]/[action]";
            public const string DEFAULT_CONTROLLER_ROUTE_WITHOUT_ACTION = "api/[controller]";
            public const string DEFAULT_ACTION_ROUTE = "[action]";
        }
    }
}
