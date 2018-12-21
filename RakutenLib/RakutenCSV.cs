using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace RakutenLib
{
    static public class RakutenCSV
    {
        const string topText = "1,3\n";
        const int limitCode = 40;
        //{0}:count {1}:ループint {2}:１(全角数字)
        const string writeLineTitle = "{0},{1},1,登録銘柄{2}\n";
        //{0}:code {1}:会社名
        const string writeLineCodeText = "STK,{0},{1},1,,,,,0,1,0,0.0000,0,,,,,,\n";

        static public void OutPutRakutenCSV(ICollection<int> codeList, string filePath)
        {
            using (var sw = new StreamWriter(filePath, false, Encoding.GetEncoding("shift_jis")))
            {
                sw.Write(topText);
                var remainingCount = codeList.Count();
                int codeListIndex = 0;
                var codeListEntity = codeList.ToList();

                int count = 0;
                for (int i = 0; i < 10; i++)
                {
                    //銘柄登録数の計算
                    if(remainingCount > 0)
                    {
                        count = (limitCode <= remainingCount) ? limitCode : remainingCount;
                        remainingCount = remainingCount - limitCode;
                    }
                    else
                    {
                        count = 0;
                    }
                    sw.Write(writeLineTitle, count, i, Microsoft.VisualBasic.Strings.StrConv((i+1).ToString(), VbStrConv.Wide));
                    for (int loopCount = 0; loopCount < count; codeListIndex++,loopCount++ )
                    {
                        sw.Write(writeLineCodeText, codeListEntity[codeListIndex], "");
                    }
                }
            }                
        }
    }
}
