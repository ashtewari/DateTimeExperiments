// <auto-generated>
// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
// </auto-generated>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace DateTimeExperiments
{
    public partial class UtilsTest
    {
[TestMethod]
[PexGeneratedBy(typeof(UtilsTest))]
public void IsDaylightSavingTimeUsingCustomTimeZone532()
{
    bool b;
    b = this.IsDaylightSavingTimeUsingCustomTimeZone(default(DateTime));
    Assert.AreEqual<bool>(false, b);
}
    }
}