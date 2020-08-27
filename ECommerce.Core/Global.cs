using System.Windows;

namespace ECommerce.Core
{
    public static class Global
    {
        private static bool IsPropertyExists(string key)
        {
            return Application.Current.Properties.Contains(key);
        }

        private static object GetProperty(string key)
        {
            if (IsPropertyExists(key))
            {
                return Application.Current.Properties[key];
            }

            return null;
        }

        private static void SetProperty(string key, object value)
        {
            Application.Current.Properties[key] = value;
        }

        public static object UserName
        {
            get
            {
                return GetProperty("Username");

            }
            set
            {
                SetProperty("Username", value);
            }
        }

        public static object UserId
        {
            get
            {
                return GetProperty("UserId");

            }
            set
            {
                SetProperty("UserId", value);
            }
        }

        public static object FilterMinPrice
        {
            get
            {
                return GetProperty("FilterMinPrice");

            }
            set
            {
                SetProperty("FilterMinPrice", value);
            }
        }

        public static object FilterMaxPrice
        {
            get
            {
                return GetProperty("FilterMaxPrice");

            }
            set
            {
                SetProperty("FilterMaxPrice", value);
            }
        }
    }
}
