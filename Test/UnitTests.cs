using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataBankOCR;

namespace Test
{
  [TestClass]
  public class UnitTests
  {
    const string TEST_PATH = @"TestFiles\";

    [TestMethod]
    public void UseCase1()
    {
      BankOCR.ReadFile(GetTestFileName("0"));
    }

    private string GetTestFileName(string prefix)
    {
      return string.Format("{0}fill_{1}.txt", TEST_PATH, prefix);
    }
  }
}
