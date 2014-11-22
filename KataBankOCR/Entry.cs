using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataBankOCR
{
  public class Entry
  {
    public const short LINE_LENGTH = Entry.DIGIT_MAX * Entry.DIGIT_LENGTH;
    public const short ENTRY_LINES = 4; //each entry is n lines
    public const short DIGIT_LENGTH = 3;
    public const short DIGIT_MIN = 1;
    public const short DIGIT_MAX = 9;

    private short[] _digits = new short[DIGIT_MAX];

    /// <summary>
    /// 9 digits
    /// Pos 0 - 8
    /// </summary>
    public short[] Digits
    {
      get { return _digits; }
    }

    #region Utils

    public static bool TryConvertBitValue(short bitValue, out short number)
    {
      number = 0;

      switch (bitValue)
      {
        case 6:
          number = 0;
          break;

        default:
          return false;
      }

      return true;
    }

    

    #endregion


  }
}
