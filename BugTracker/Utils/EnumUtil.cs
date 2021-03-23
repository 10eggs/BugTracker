using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Utils
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return (T [])Enum.GetValues(typeof(T));
        }
    }
}
