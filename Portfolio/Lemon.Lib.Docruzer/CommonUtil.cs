using System;
using System.Text;
using System.Text.RegularExpressions;
using Lemon.Lib.Docruzer.VO;

namespace com.konantech.search.util
{

    /// <summary>
    /// Summary description for CommonUtil
    /// </summary>
    public class CommonUtil
    {
        public CommonUtil()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /** 입력받은 문자열특수문자를 html format으로 변환.
        *	@param str
        *	@return 변환된 문자열
        */
        public static string formatHtml(string str)
        {
            if (str.Length == 0) return "&nbsp;";

            string t = "";

            char[] arr = str.ToCharArray();
            foreach (char c in arr)
            {
                switch (c)
                {
                    case '<': t += "&lt;"; break;
                    case '>': t += "&gt;"; break;
                    case '&': t += "&amp;"; break;
                    case '\"': t += "&quot;"; break;
                    case '\'': t += "\\\'"; break;
                    case '\r': t += "<br>\n"; break;
                    case '\n': t += "<br>\n"; break;
                    default: t += c; break;
                }
            }
            return t;
        }

        /** YYYYMMDD 포멧의 문자열을 입력받아 정의한 구분자를 사용하여 YYYY.MM.DD 포멧으로 변환.
        *	@param s
        *	@param deli	
        *
        *	@return 변환된 문자열
        */

        public static string formatDateStr(string str, string deli)
        {
            string t = "";
            str = str.Trim();

            if (str != null || str.Length > 0 || !"".Equals(str))
            {

                if (str.Length >= 8)
                {
                    t = str.Substring(0, 4) + deli + str.Substring(4, 2) + deli + str.Substring(6, 2);
                }
                else
                {
                    t = str;
                }
            }

            return t;
        }

        /**
        * 문자열이 긴 경우에 입력받은 문자길이로 자른다.
        *	@param str 
        *	@param cutLen 
        *	@param tail
        *
        *	@return String
        */

        public static string getCutString(string str, int cutlen, string tail)
        {
            string returnstr = "";
            int charcode = 0, charlen = 0;
            foreach (char c in str)
            {
                charcode = (int)c;
                if (charcode > 128)
                {
                    charlen = charlen + 2;
                }
                else
                {
                    charlen++;
                }
                if (charlen > cutlen)
                {
                    break;
                }
                else
                {
                    returnstr = returnstr + c.ToString();
                }
            }
            return returnstr + tail;
        }

        /** 널이거나 빈 문자열을 원하는 스트링으로 변환한다<br>
        * 단, 좌우 공백이 있는 문자열은 trim 한다 <br>.
        *
        * @param org 입력문자열
        * @param converted 변환문자열
        *
        * @return 치환된 문자열
        */
        public static string null2Str(string org, string converted)
        {
            if (org == null || org.Trim().Length == 0)
            {
                return converted;
            }
            else
            {
                return org.Trim();
            }
        }


        /** 널이거나 빈 문자열(숫자형)을 integer로 변환한다.
        *
        * @param org 입력문자열
        * @param converted 변환숫자
        *
        * @return 치환된 Interger
        */
        public static int null2Int(string org, int converted)
        {
            int i = 0;


            if (org == null || org.Trim().Length == 0)
            {
                return converted;
            }
            else
            {
                try
                {
                    i = Int32.Parse(org);
                }
                catch (Exception e)
                {
                    throw (new Exception("null2Int Error : " + e.Message));
                }

                return i;
            }
        }

        /** Int형 숫자의 포맷을  ###,### 으로 변환하여 리턴함.
        * @param num 정수값
        *
        * @return 변환된 문자열
        */
        public static string formatMoney(int num)
        {
            string str = "";

            str = num.ToString("#,#;0;0");

            return str;

        }

        /** string형 숫자의 포맷을  ###,### 으로 변환하여 리턴함.
        * @param num 숫자형 문자
        *
        * @return 변환된 문자열
        */
        public static string formatMoney(string num)
        {
            string str = "";

            //str = num.ToString("#,#;0;0");

            str = int.Parse(num).ToString("#,#;0;0");

            return str;

        }

        public static string makeReturnValue(string target, string str, string returnVal)
        {
            if (target.Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return returnVal;
            }
            else
            {
                return "";
            }
        }

        public static string makeReturnValue(string target, string str, string trueVal, string falseVal)
        {
            if (target.Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return trueVal;
            }
            else
            {
                return falseVal;
            }
        }
        /*
        public static string UTF8_to_EUCKR(string strUTF8)
        {
            Encoding encUTF8;
            Encoding encEUCKR;

            byte[] byteUTF8;
            byte[] byteEUCKR;

            // get Encoding class
            encUTF8 = Encoding.UTF8;
            encEUCKR = Encoding.GetEncoding("euc-kr");

            // convert encoding from "UTF8" to "euc-kr"        
            byteUTF8 = encUTF8.GetBytes(strUTF8);
            byteEUCKR = Encoding.Convert(encUTF8, encEUCKR, byteUTF8);

            return encEUCKR.GetString(byteEUCKR);


        }

        public static string EUCKR_to_UTF8(string strEUCKR)
        {
            Encoding encUTF8;
            Encoding encEUCKR;

            byte[] byteUTF8;
            byte[] byteEUCKR;

            // get Encoding class
            encUTF8 = Encoding.UTF8;
            encEUCKR = Encoding.GetEncoding("euc-kr");

            // convert encoding from "euc-kr" to "utf8"
            byteEUCKR = encEUCKR.GetBytes(strEUCKR);
            byteUTF8 = Encoding.Convert(encEUCKR, encUTF8, byteEUCKR);

            return encUTF8.GetString(byteUTF8);

        }
        */

        /*
        public static string getTargetDate(int iDay)
        {

            DateTime temp = new DateTime();
            //Calend0ar temp = Calendar.getInstance();
            StringBuilder sbDate = new StringBuilder();

            temp.Add(iDay);
          
            temp.add(Calendar.DAY_OF_MONTH, iDay);

            int nYear = temp.get(Calendar.YEAR);
            int nMonth = temp.get(Calendar.MONTH) + 1;
            int nDay = temp.get(Calendar.DAY_OF_MONTH);

            sbDate.Append(nYear);
            if (nMonth < 10)
            {
                sbDate.Append("0");
                sbDate.Append(nMonth);
            }
            if (nDay < 10)
            {
                sbDate.Append("0");
                sbDate.Append(nDay);
            }
          
            return sbDate.ToString();
        }
        */

        /** 이전검색어 히든 태그 생성 후 반환.
        * 
        * @param srchParam ParameterVO 오브젝트
        * @return 이전 검색어 태그 문자열
        */

        public static string makeHtmlForPreKwd(ParameterVO srchParam)
        {
            StringBuilder preKwdStr = new StringBuilder("");
            int tmpCnt = 0;

            preKwdStr.Append("<input type='hidden' name=\"preKwd\" value=\"" + srchParam.getKwd() + "\">\n");

            if (srchParam.getReSrchFlag())
            {

                System.Diagnostics.Debug.WriteLine("srchParam.getPreKwds()-->" + srchParam.getPreKwds());
                if (srchParam.getPreKwds() != null)
                {
                    int preKwdCnt = srchParam.getPreKwds().Length;

                    tmpCnt = 0;
                    if (srchParam.getPreKwds()[0].Equals(srchParam.getKwd()) && preKwdCnt > 1) tmpCnt = 1;	// 
                    for (; tmpCnt < preKwdCnt; tmpCnt++)
                    {
                        preKwdStr.Append("<input type=\"hidden\" name=\"preKwd\" value=\"").Append(srchParam.getPreKwds()[tmpCnt]).Append("\">\n");
                    }

                    /* 이전검색어 & 키워드가 존재하는 경우 / 첫페이지내 검색시만 생성 / 2개 키워드가 같지 않은경우*/
                    if (srchParam.getKwd().Length > 0 && srchParam.getPageNum() == 1
                        && !srchParam.getKwd().Equals(srchParam.getPreKwds()[0]))
                    {
                        srchParam.setRecKwd(srchParam.getPreKwds()[0] + "||" + srchParam.getKwd());    // 추천검색어 구성용 단어 생성
                    }
                } // end if
            } // end if  

            return preKwdStr.ToString();
        }

        /** 첨부파일명에 따른  이미지 파일명을 리턴함. 
        * @param fileName 파일명 
        * @return 이미지 파일명
        */
        public static string getAttachFileImage(string fileName)
        {

            string fileExt = "";
            string imgFile = "";

            //파일 확장자명 추출
            fileExt = fileName.Substring(fileName.LastIndexOf(".") + 1);

            if ("doc".Equals(fileExt, StringComparison.OrdinalIgnoreCase) || "docx".Equals(fileExt, StringComparison.OrdinalIgnoreCase))
            {
                imgFile = "ico_doc.gif";
            }
            else if ("ppt".Equals(fileExt, StringComparison.OrdinalIgnoreCase) || "pptx".Equals(fileExt, StringComparison.OrdinalIgnoreCase))
            {
                imgFile = "ico_ppt.gif";
            }
            else if ("xls".Equals(fileExt, StringComparison.OrdinalIgnoreCase) || "xlsx".Equals(fileExt, StringComparison.OrdinalIgnoreCase))
            {
                imgFile = "ico_xls.gif";
            }
            else if ("hwp".Equals(fileExt, StringComparison.OrdinalIgnoreCase))
            {
                imgFile = "ico_hwp.gif";
            }
            else if ("zip".Equals(fileExt, StringComparison.OrdinalIgnoreCase) || "gzip".Equals(fileExt, StringComparison.OrdinalIgnoreCase)
                  || "tar".Equals(fileExt, StringComparison.OrdinalIgnoreCase) || "azip".Equals(fileExt, StringComparison.OrdinalIgnoreCase)
                  || "bzip".Equals(fileExt, StringComparison.OrdinalIgnoreCase))
            {
                imgFile = "ico_zip.gif";
            }
            else if ("pdf".Equals(fileExt, StringComparison.OrdinalIgnoreCase))
            {
                imgFile = "ico_pdf.gif";
            }
            else
            {
                imgFile = "ico_etc.gif";
            }

            return imgFile;
        }

        public static string[] SplitByString(string inStr, string delimeter)
        {
            int offset = 0;
            int index = 0;
            int[] offsets = new int[inStr.Length + 1];
            while (index < inStr.Length)
            {
                int indexOf = inStr.IndexOf(delimeter, index);
                if (indexOf != -1)
                {
                    offsets[offset++] = indexOf;
                    index = (indexOf + delimeter.Length);
                }
                else
                {
                    index = inStr.Length;
                }
            }
            string[] final = new string[offset + 1];
            if (offset == 0)
            {
                final[0] = inStr;
            }
            else
            {
                offset--;
                final[0] = inStr.Substring(0, offsets[0]);
                for (int i = 0; i < offset; i++)
                {
                    final[i + 1] = inStr.Substring(offsets[i] + delimeter.Length, offsets[i + 1] - offsets[i] - delimeter.Length);
                }
                final[offset + 1] = inStr.Substring(offsets[offset] + delimeter.Length);
            }
            return final;
        }

        /** 입력받은 문자열 XSS차단.
        *	@param str
        *	@return 변환된 문자열
        */
        public static string xssIntercept(string str)
        {
            if (str.Length == 0) return "";

            string description = "";

            description = Regex.Replace(str, "<", "&lt;");
            description = Regex.Replace(description, ">", "&gt;");
            description = Regex.Replace(description, "eval\\((.*)\\)", "");
            description = Regex.Replace(description, "[\\\"\\\'][\\s]*javascript:(.*)[\\\"\\\']", "\"\"");
            description = Regex.Replace(description, "script", "");

            return description;
        }

        /**
         * byte단위의 파일 사이즈를 보내면, 원하는 단위 형태로 크기를 리턴.
         * 
         * @param fileSize(int)
         * @param unit(str)
         *        
         * @return 변환된 파일사이즈
         */
        public static string getFileSize(long fileSize, string unit)
        {
            int modSize = 1024; // default KB
            int tempSize = 0;

            if ("mb".Equals(unit, StringComparison.OrdinalIgnoreCase))
            {
                modSize = modSize * 1024;
            }
            else if ("gb".Equals(unit, StringComparison.OrdinalIgnoreCase))
            {
                modSize = modSize * 1024 * 1024;
            }

            tempSize = (int)(fileSize / modSize);

            return CommonUtil.formatMoney(tempSize) + unit.ToUpper();
        }

        /**
         * 상단 검색 상태를 구하는 메소드
         * 
         * @param pageNum(int) - 페이지 넘버
         * @param pageSize(int) - 페이지 사이즈
         * @param total(int) - 검색된문서 총합
         *        
         * @return 변환된 상단 레이어
         */
        public static string getTopLayer(int pageNum, int pageSize, int total)
        {
            StringBuilder tempStr = new StringBuilder();
            int firstPage = (pageNum * pageSize) - (pageSize - 1);
            int lastPage = pageNum * pageSize;

            tempStr.Append(firstPage);
            tempStr.Append("-");

            if (lastPage > total)
                lastPage = total;

            tempStr.Append(lastPage);
            tempStr.Append(" / ");
            tempStr.Append(formatMoney(total));

            return tempStr.ToString();
        }
    }

}