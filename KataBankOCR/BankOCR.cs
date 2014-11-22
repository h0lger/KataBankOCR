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

      /// <summary>
      /// Get entries from file
      /// </summary>
      /// <param name="filename">filename incl path</param>
      /// <returns></returns>
      public static IList<Entry> GetEntriesFromFile(string filename)
      {
        string fileContent = File.ReadAllText(filename);
        IList<Entry> ent = GetDigits(fileContent);

        return ent;
      }

      /// <summary>
      /// Get digits from file content
      /// </summary>
      /// <param name="fileContent"></param>
      /// <returns></returns>
      private static IList<Entry> GetDigits(string fileContent)
      {
        IList<Entry> entries = new List<Entry>();        
        string[][] currD = null;
        short currELine = 0;
        int line = 0;

        using (StringReader sr = new StringReader(fileContent))
        {
          string tmpLine = sr.ReadLine();

          while (tmpLine != null)
          {
            //Check if it's a new entry
            if (line % Entry.ENTRY_LINES == 0)
            {
              currD = new string[Entry.DIGIT_MAX][]; //0-8
              currELine = 0;
            }

            //iterate all digits
            for (short i = 0; i < Entry.DIGIT_MAX; i++)
            {
              if (currD[i] == null)
                currD[i] = new string[Entry.ENTRY_LINES];
              currD[i][currELine] = GetDigitFromLine(tmpLine, (short)(i + 1));
            }

            line++;
            currELine++;

            //If end of entry, add it
            if (line > 0 && line % Entry.ENTRY_LINES == 0)
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
              entries.Add(ent);
            }
            tmpLine = sr.ReadLine();
          }//while
        } //using

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
