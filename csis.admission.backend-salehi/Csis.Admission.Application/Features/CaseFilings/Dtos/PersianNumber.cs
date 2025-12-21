namespace Csis.Admission.Application.Features.CaseFilings.Commands;

internal static class PersianNumber
{

    public static string GET_Number_To_PersianString(string TXT) {
        string RET = " ", STRVA = " ";
        var MainStr = STR_To_Int(TXT);
        var Q = 0;
        for ( var i = MainStr.Length - 1; i >= 0; i-- ) {
            STRVA = " ";
            if ( RET is not " " and not null )
                STRVA = " و ";
            RET = Convert_STR(GETCountStr(MainStr[i]), Q) + STRVA + RET;
            Q++;
        }
        if ( RET is " " or null or "  " )
            RET = "صفر";
        return RET;
    }

    private static string[] STR_To_Int(string STR) {
        STR = GETCountStr(STR);
        var RET = new string[STR.Length / 3];
        var Q = 0;
        for ( var I = 0; I < STR.Length; I += 3 ) {
            RET[Q] = STR.Substring(I, 3);
            Q++;
        }
        return RET;
    }

    private static string GETCountStr(string STR) {
        var RET = STR;
        var LEN = (STR.Length / 3 + 1) * 3 - STR.Length;
        if ( LEN < 3 ) {
            for ( var i = 0; i < LEN; i++ ) {
                RET = "0" + RET;
            }
        }
        if ( RET == "" )
            return "000";
        return RET;
    }

    private static string Convert_STR(string INT, int Count) {
        var RET = "";
        //یک صد
        if ( Count == 0 ) {
            if ( INT.Substring(1, 1) == "1" && INT.Substring(2, 1) != "0" ) {
                RET = GET_Number(3, Convert.ToInt32(INT.Substring(0, 1)), " ") + GET_Number(1, Convert.ToInt32(INT.Substring(2, 1)), "");
            } else {
                var STR = GET_Number(0, Convert.ToInt32(INT.Substring(2, 1)), "");
                RET = GET_Number(3, Convert.ToInt32(INT.Substring(0, 1)), GET_Number(2, Convert.ToInt32(INT.Substring(1, 1)), "") + STR) + GET_Number(2, Convert.ToInt32(INT.Substring(1, 1)), STR) + GET_Number(0, Convert.ToInt32(INT.Substring(2, 1)), "");
            }
        }
        //هزار
        else if ( Count == 1 ) {
            RET = Convert_STR(INT, 0);
            RET += " هزار";
        }
        //میلیون
        else if ( Count == 2 ) {
            RET = Convert_STR(INT, 0);
            RET += " میلیون";
        }
        //میلیارد
        else if ( Count == 3 ) {
            RET = Convert_STR(INT, 0);
            RET += " میلیارد";
        }
        //میلیارد
        else if ( Count == 4 ) {
            RET = Convert_STR(INT, 0);
            RET += " تیلیارد";
        }
        //میلیارد
        else if ( Count == 5 ) {
            RET = Convert_STR(INT, 0);
            RET += " بیلیارد";
        } else {
            RET = Convert_STR(INT, 0);
            RET += Count.ToString();
        }
        return RET;
    }

    private static string GET_Number(int Count, int Number, string VA) {
        var RET = "";

        if ( VA is not "" and not null ) {
            VA = " و ";
        }
        if ( Count is 0 or 1 ) {
            var IsDah = Convert.ToBoolean(Count);
            var MySTR = new string[10];
            MySTR[1] = IsDah ? "یازده" : "یک" + VA;
            MySTR[2] = IsDah ? "دوازده" : "دو" + VA;
            MySTR[3] = IsDah ? "سیزده" : "سه" + VA;
            MySTR[4] = IsDah ? "چهارده" : "چهار" + VA;
            MySTR[5] = IsDah ? "پانزده" : "پنج" + VA;
            MySTR[6] = IsDah ? "شانزده" : "شش" + VA;
            MySTR[7] = IsDah ? "هفده" : "هفت" + VA;
            MySTR[8] = IsDah ? "هجده" : "هشت" + VA;
            MySTR[9] = IsDah ? "نوزده" : "نه" + VA;
            return MySTR[Number];
        } else if ( Count == 2 ) {
            var MySTR = new string[10];
            MySTR[1] = "ده";
            MySTR[2] = "بیست" + VA;
            MySTR[3] = "سی" + VA;
            MySTR[4] = "چهل" + VA;
            MySTR[5] = "پنجاه" + VA;
            MySTR[6] = "شصت" + VA;
            MySTR[7] = "هفتاد" + VA;
            MySTR[8] = "هشتاد" + VA;
            MySTR[9] = "نود" + VA;
            return MySTR[Number];
        } else if ( Count == 3 ) {
            var MySTR = new string[10];
            MySTR[1] = "یکصد" + VA;
            MySTR[2] = "دویست" + VA;
            MySTR[3] = "سیصد" + VA;
            MySTR[4] = "چهارصد" + VA;
            MySTR[5] = "پانصد" + VA;
            MySTR[6] = "ششصد" + VA;
            MySTR[7] = "هفتصد" + VA;
            MySTR[8] = "هشتصد" + VA;
            MySTR[9] = "نهصد" + VA;
            return MySTR[Number];
        }
        return RET;
    }
}
