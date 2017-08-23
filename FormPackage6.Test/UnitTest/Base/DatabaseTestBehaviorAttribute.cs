using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Tests.UnitTest.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseTestBehaviorAttribute : Attribute
    {
        public DatabaseBehavior Behavior { get; private set; }

        public DatabaseTestBehaviorAttribute(DatabaseBehavior behavior)
        {
            Behavior = behavior;
        }
    }
}
