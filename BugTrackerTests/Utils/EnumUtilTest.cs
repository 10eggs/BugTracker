using BugTracker.Models.TicketProperties;
using BugTracker.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace BugTrackerTests.Utils
{
    public class EnumUtilTest
    {
        [Fact]
        public void CheckMethodReturnsAllValuesFromEnumType()
        {
            var enums = EnumUtil.GetValues<TicketCategory>();
            foreach (var e in enums)
            {
                Debug.WriteLine($"Value of enum is: {e}");
                Debug.WriteLine($"Type is : {e.GetType()}");
            }
        }
    }
}
