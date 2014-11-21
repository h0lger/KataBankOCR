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
      
      const short LINE_LENGTH = Entry.DIGIT_MAX * Entry.DIGIT_LENGTH;
      const short ENTRY_LINES = 4; //each entry is n lines
      

      public static void ReadFile(string filename)
      {        
        string fileContent = File.ReadAllText(filename);

        IList<Entry> tmp2 = GetDigit(fileContent);
        
        
      }

      private static IList<Entry> GetDigit(string fileContent)
      {
        IList<Entry> entries = new List<Entry>();
        Entry currE = null; //current entry
        string[][] currD = null;
        short currELine = 0;
        int line = 0;

        StringReader sr = new StringReader(fileContent);
        string tmpLine = sr.ReadLine();        
        
        while (tmpLine != null)
        {          
          //Check if it's a new entry
          if (line % ENTRY_LINES == 0)
          {            
            currD = new string[Entry.DIGIT_MAX][]; //0-8
            currELine = 0;
          }


          //iterate all digits
          for (short i = 0; i < Entry.DIGIT_MAX; i++)
          {
            if (currD[i] == null)
              currD[i] = new string[ENTRY_LINES];
            currD[i][currELine] = GetDigitFromLine(tmpLine, (short)(i + 1));
          }

          line++;
          currELine++;

          //If end of entry, add it
          if (line > 0 && line % ENTRY_LINES == 0)
          {            
            Entry ent = new Entry();
            short i = 0;
            short number = 0;
            foreach (string[] digit in currD)
            {
              if (Entry.TryConvertBitValue(GetDigitBitValue(digit), out number))
                ent.Digits[i] = number;
              i++;
            }            
          }
            

          tmpLine = sr.ReadLine();          
        }

        return entries;
      }

      /// <summary>
      /// Returns digit from one line
      /// </summary>
      /// <param name="line"></param>
      /// <param name="digit">1-9</param>
      /// <returns></returns>
      private static string GetDigitFromLine(string line, short digit)
      {
        if (digit < Entry.DIGIT_MIN || digit > Entry.DIGIT_MAX)
          throw new InvalidOperationException();

        return line.Substring((Entry.DIGIT_LENGTH * (digit - 1)), Entry.DIGIT_LENGTH);
      }

      /// <summary>
      /// Returns digit value from one line
      /// </summary>
      /// <param name="digit"></param>
      /// <returns></returns>
      private static short GetDigitBitValue(string digit)
      {
        short retValue = 0;
        foreach (char c in digit.ToCharArray())
        {
          if (!char.IsWhiteSpace(c))
            retValue++;
        }

        return retValue;
      }

      private static short GetDigitBitValue(string[] digit)
      {
        short retValue = 0;

        //Iterate all lines for the digit
        foreach (string sLine in digit)
        {
          retValue += GetDigitBitValue(sLine);
        }

        return retValue;
      }
    }
}
