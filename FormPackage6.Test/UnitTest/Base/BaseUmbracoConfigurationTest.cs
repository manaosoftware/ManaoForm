using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Tests.UnitTest.Base
{
    [TestFixture]
    public abstract class BaseUmbracoConfigurationTest
    {
        [SetUp]
        public virtual void Initialize()
        {
            SettingsForTests.Reset();

        }

        [TearDown]
        public virtual void TearDown()
        {
            //reset settings
            SettingsForTests.Reset();

        }
    }
}
