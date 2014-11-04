using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataBankOCR
{
    public class BankOCR
    {
      const short ENTRY_LENGTH = 3;
      const short LINE_LENGTH = 27;
      const short ENTRY_LINES = 4;
      const short NO_ELEMENTS = 9;

      public static void ReadFile(string filename)
      {
        string[][] tmp = new string[NO_ELEMENTS][];
        string fileContent = File.ReadAllText(filename);

        var tmp2 = GetDigit(fileContent, 1);
        
        
      }

      private static string[][] GetDigit(string fileContent, short digitPos)
      {
        string[][] tmp = new string[ENTRY_LINES][];

        StringReader sr = new StringReader(fileContent);
        string tmpLine = string.Empty;
        short lineNo = 0;
        int startPos = digitPos * ENTRY_LENGTH;
        while (tmpLine != null)
        {
          tmpLine = sr.ReadLine();
          tmp[lineNo] = new string[ENTRY_LENGTH];

          for (int i = startPos; i < (startPos + ENTRY_LENGTH); i++)
          {
            tmp[lineNo][i] = tmpLine.Substring(i, 1);
          }
          lineNo++;
        }


        return tmp;
      }
    }
}
