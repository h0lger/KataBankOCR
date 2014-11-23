using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataBankOCR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
  [TestClass]
  public class UnitTests
  {
    const string TEST_PATH = @"TestFiles\";

    [TestMethod]
    public void UseCase1()
    {
      IList<Entry> ents = null;

      //Get file with only 0
       ents = BankOCR.GetEntriesFromFile(GetTestFileName("0"));
       Assert.AreEqual(1, ents.Count);
       ValidateEntries(ents[0], 9, 0);

       //Get file with only 1
       ents = BankOCR.GetEntriesFromFile(GetTestFileName("1"));
       Assert.AreEqual(1, ents.Count);
       ValidateEntries(ents[0], 9, 1);
    }

    #region Help methods

    private string GetTestFileName(string prefix)
    {
      return string.Format("{0}fill_{1}.txt", TEST_PATH, prefix);
    }

    /// <summary>
    /// Validate entries
    /// </summary>
    /// <param name="ents">Content</param>
    /// <param name="total">Total number of elements</param>
    /// <param name="value">What value all elements should be</param>
    private static void ValidateEntries(Entry ent, int total, short value)
    {
      Assert.AreEqual(total, ent.Digits.Count(), "Number of elements differ");      
      Assert.IsTrue(ent.Digits.All(n => n == value), "Not all elements have the same value");
    }
    #endregion
  }
}
