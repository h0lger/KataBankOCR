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

        var tmp2 = GetDigit(fileContent);
        
        
      }

      private static IDictionary<short, string> GetDigit(string fileContent)
      {
        IDictionary<short, string> tmp =
          new Dictionary<short, string>();


        StringReader sr = new StringReader(fileContent);
        string tmpLine = string.Empty;
        
        
        while (tmpLine != null)
        {
          short digitIndex = 0;
          tmpLine = sr.ReadLine();
          if (tmpLine == null)
            break;
          if(!tmp.ContainsKey(digitIndex))
            tmp.Add(new KeyValuePair<short,string>(digitIndex, string.Empty));
          
          for (int i = 0; i < LINE_LENGTH; i++)
          {
            if (i > 0 && i % ENTRY_LINES == 0)
              continue;

            string tmpS = tmpLine.Substring(i, 1);
            if (string.IsNullOrEmpty(tmpS) || string.IsNullOrWhiteSpace(tmpS))
              tmp[digitIndex] += "0";
            else
              tmp[digitIndex] += "1";

            if (i > 0 && i % ENTRY_LENGTH - 1 == 0)
            {
              digitIndex++;
              if (!tmp.ContainsKey(digitIndex))
                tmp.Add(new KeyValuePair<short, string>(digitIndex, string.Empty));
            }
            
              
            
          }          
        }


        return tmp;
      }
    }
}
