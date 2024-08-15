using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Util
{
    public static class DataUtil
    {
        public static Nullable<T> DbValueToNullable<T>(object dbValue) where T : struct
        {
            Nullable<T> returnValue = null;

#pragma warning disable CS0252 // Possible unintended reference comparison; to get a value comparison, cast the left hand side to type 'string'
            if ((dbValue != null) && (dbValue != DBNull.Value) && (dbValue != string.Empty))
#pragma warning restore CS0252 // Possible unintended reference comparison; to get a value comparison, cast the left hand side to type 'string'
            {
                returnValue = (T)Convert.ChangeType(dbValue, typeof(T));
            }

            return returnValue;
        }

        public static T DbValueToDefault<T>(object obj)
        {
#pragma warning disable CS0252 // Possible unintended reference comparison; to get a value comparison, cast the left hand side to type 'string'
            if (obj == null || obj == DBNull.Value || obj == string.Empty) return default(T);
#pragma warning restore CS0252 // Possible unintended reference comparison; to get a value comparison, cast the left hand side to type 'string'
            else { return (T)Convert.ChangeType(obj, typeof(T)); }
        }

        public static List<string> SplitData(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new List<string>();
            }
            return text.Split('\\').ToList();
        }
    }
}
