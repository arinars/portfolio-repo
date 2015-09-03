using System;
using System.Text;

/// <summary>
/// @author Lee JunHyuk (KONAN Technology)
/// @editor Jang Jinhoo (KONAN Technology)
/// </summary>
public class DCUtil
{
	public DCUtil()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    /**검색어에 대한 escape 처리.
	* @param kwd 검색어 
	* @return escape된 검색어
    * @author Lee JunHyuk
	**/

    public static string escapeQuery(string kwd)
    {
        string str = "";

        char[] arr = kwd.ToCharArray();
        
        foreach (char c in arr)
        {
            switch (c)
            {
                case '\"': str += "\\\""; break;
                case '\'': str += "\\'"; break;
                case '\\': str += "\\\\"; break;
                case '?': str += "\\?"; break;
                case '*': str += "\\*"; break;
                default: str += c; break;
            }
        }
        return str;
    }

    /** 검색엔진 로그정보 로그포맷. 
	* <br>[사이트명@카테고리명+사용자ID$|첫검색|페이지번호|정렬방법^키워드]##이전검색어|현재검색어] 
	* @param site 사이트명
	* @param nmSchCat 카테고리명
	* @param userId 사용자ID
	* @param kwd 키워드
	* @param pageNum 페이지번호
	* @param reSchFlag 재검색여부(true/false)
	* @param orderNm 정렬방법
	* @param recKwd 추천검색어('이전검색어|현재검색어')
	*
	* @return 검색 로그 string
    * @author Lee JunHyuk
	*/
    public static string getLogInfo(string site, string nmSchCat, string userId, string kwd,
                    int pageNum, Boolean reSchFlag, string orderNm, string recKwd)
    {
        StringBuilder logInfo = new StringBuilder("");

        logInfo.Append(site);
        logInfo.Append("@");

        logInfo.Append(nmSchCat);
        logInfo.Append("+");

        // 페이지 이동은 검색으로 간주하지 않음
        if (pageNum > 1)
        {
            logInfo.Append("$||");
            logInfo.Append(pageNum);
            logInfo.Append("|");

        }
        else
        {
            logInfo.Append(userId);
            logInfo.Append("$|");

            if (reSchFlag)
            {
                logInfo.Append("재검색|");
            }
            else
            {
                logInfo.Append("첫검색|");
            }
            logInfo.Append(pageNum);
            logInfo.Append("|");
        }

        logInfo.Append(orderNm);
        logInfo.Append("^");
        logInfo.Append(kwd);

        // 추천어로그
        if (recKwd != null && recKwd.Length > 0)
        {
            logInfo.Append("]##").Append(recKwd);
        }
        return logInfo.ToString();
    }

    /** 키워드/코드형식쿼리 생성. 
     *
     * @param logicOp 연결 논리연산자
     * @param nmFd 검색대상 필드명 또는 인덱스명
     * @param kwd 검색어
     * @param schMethod 검색메소드
     * @param query 쿼리 String 
     *
     * @return 쿼리 StringBuffer
     * @author Lee JunHyuk
     */
    public static StringBuilder makeQuery(String srchFd, String kwd, String method,
                                         StringBuilder query, String logicOp)
    {
        StringBuilder tempQuery = new StringBuilder();

        if (query != null && query.Length > 0)
        {
            tempQuery.Append(query);
        }

        if (kwd != null && kwd.Length > 0)
        {
            if (tempQuery.Length > 0)
            {
                if ("".Equals(logicOp, StringComparison.OrdinalIgnoreCase))
                {
                    tempQuery.Append(" AND ");
                }
                else
                {
                    tempQuery.Append(" " + logicOp + " ");
                }
            }
            tempQuery.Append(srchFd);
            tempQuery.Append("='");
            tempQuery.Append(escapeQuery(kwd));
            tempQuery.Append("' ");
            tempQuery.Append(method);
        }
        return tempQuery;
    }

    /** 
     * 기본쿼리 (검색어 + 제외어) 생성
     * @param logicOp 연결 논리연산자
     * @param nmFd 검색대상 필드명 또는 인덱스명
     * @param kwd 검색어
     * @param xwd 제외어
     * @param schMethod 검색메소드
     * @param query 쿼리 String 
     *
     * @return 쿼리 StringBuffer
     * @author Lee JunHyuk
     */
    public static StringBuilder makeQuery(String srchFd, String kwd, String xwd, String method,
                                         StringBuilder query, String logicOp)
    {
        StringBuilder tempQuery = new StringBuilder();

        if (query != null && query.Length > 0)
        {
            tempQuery.Append(query);
        }

        if (kwd != null && kwd.Length > 0)
        {
            if (tempQuery.Length > 0)
            {
                if ("".Equals(logicOp, StringComparison.OrdinalIgnoreCase))
                {
                    tempQuery.Append(" AND ");
                }
                else
                {
                    tempQuery.Append(" " + logicOp.ToUpper() + " ");
                }
            }
            tempQuery.Append(srchFd);
            tempQuery.Append("='");
            tempQuery.Append(escapeQuery(kwd));
            tempQuery.Append("' ");
            tempQuery.Append(method);

            if (xwd.Length > 0)
            {
                tempQuery.Append(" AND NOT ");
                tempQuery.Append(srchFd);
                tempQuery.Append("='");
                tempQuery.Append(escapeQuery(xwd));
                tempQuery.Append("' ");
                tempQuery.Append(method);
            }            
        }
        return tempQuery;
    }

    /** 
	* 
	* @param nmFd 		- 
	* @param startVal	- 
	* @param endVal 	- 
	* @param query 		- 
	* @return String
    * @author Lee JunHyuk
	*/
    public static StringBuilder makeRangeQuery(String nmFd, String startVal, String endVal,
                                                StringBuilder query)
    {

        StringBuilder tempQuery = new StringBuilder("");

        if ("".Equals(startVal) && "".Equals(endVal))
        {
            return query;
        }

        if (query != null && query.Length > 0)
        {
            tempQuery.Append("(" + query + ")");
            tempQuery.Append(" AND ");
        }

        tempQuery.Append("(");

        if (!startVal.Equals(""))
        {
            tempQuery.Append(nmFd);
            tempQuery.Append(" >= '");
            tempQuery.Append(startVal);
            tempQuery.Append("'");
        }

        if (!endVal.Equals(""))
        {
            if (!startVal.Equals(""))
            {
                tempQuery.Append(" AND ");
            }
            tempQuery.Append(nmFd);
            tempQuery.Append(" <= '");
            tempQuery.Append(endVal);
            tempQuery.Append("'");
        }

        tempQuery.Append(")");

        return tempQuery;
    }

    /** 
    * 
    * @param nmFd 
    * @param kwd 
    * @param prevKwd 
    * @param prevKwdLength 
    * @param schMethod 
    * 
    * @return query 
    * @author Lee JunHyuk
    */

    public static StringBuilder makePreQuery(string nmFd, string kwd, string[] prevKwd,
                                            int prevKwdLength, string schMethod)
    {
        StringBuilder query = new StringBuilder("");

        if (prevKwd != null && prevKwdLength > 0)
        {
            for (int i = 0; i < prevKwdLength; i++)
            {
                if (!escapeQuery(prevKwd[i]).Equals(kwd, StringComparison.OrdinalIgnoreCase))
                {
                    if (query.Length > 0)
                    {
                        query.Append(" AND ");
                    }
                    query.Append(nmFd);
                    query.Append("='");
                    query.Append(escapeQuery(prevKwd[i]));
                    query.Append("' ");
                    query.Append(schMethod);
                }
            }
            if (query.Length > 0)
            {
                query = new StringBuilder("(").Append(query).Append(")");
            }
        }

        return query;
    }

    /** 
    * IN QUERY 생성 메소드(IN QUERY 지원 독크루져일때만 사용)
    * @param srchFd(검색필드)
    * @param kwd(키워드)
    * @param method(검색메소드)
    * @param query(기존쿼리)
    * @param logicOp(and, or)
    * 
    * @return rtnQuery 
    * @author Jang Jinhoo
    */

    public static StringBuilder makeInQuery(String srchFd, String[] kwd, StringBuilder query, String logicOp)    
    {
        StringBuilder rtnQuery = new StringBuilder();
                
        if (query.ToString().Length > 0)
        {
            rtnQuery.Append(query);

            if (!"".Equals(logicOp) && "".Equals("and", StringComparison.OrdinalIgnoreCase))
            {
                rtnQuery.Append(" AND ");
            }
            else
            {
                rtnQuery.Append(" " + logicOp.ToUpper() + " ");
            }
        }

        rtnQuery.Append(" ( ");
        rtnQuery.Append(srchFd);
        rtnQuery.Append(" in {");

        for (int i = 0; i < kwd.Length; i++)
        {
            if (i > 0)
            {
                rtnQuery.Append(",");
            }
            rtnQuery.Append("'" + kwd[i] + "'");
        }

        rtnQuery.Append("} )");

        return rtnQuery;
    }   
}
