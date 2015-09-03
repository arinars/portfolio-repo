using System;
using System.Linq;
using System.Collections;
using System.Configuration;
using ATLDOCRUZER_3_2Lib;
using System.Collections.Generic;
using Lemon.Lib.Docruzer.VO;
using System.Linq.Expressions;

namespace Lemon.Lib.Docruzer
{
    /// <summary>
    /// 검색엔진 API.
    /// <para>작성일 : 2014. 05. 14(수)</para>
    /// <para>작성자 : 김민섭</para>
    /// </summary>
    /// <remarks>
    /// 변경자/변경일 : OOO / yyyy.mm.dd
    /// 변경사유/내역 : 기능 추가로 변경함.
    /// </remarks>
    public class SearchManager
    {
        public string DAEMON_SERVICE_IP = ConfigurationManager.AppSettings["DAEMON_SERVICE_IP"];
        public int DAEMON_SERVICE_PORT = int.Parse(ConfigurationManager.AppSettings["DAEMON_SERVICE_PORT"]);
        public string DAEMON_SERVICE_ADDRESS = ConfigurationManager.AppSettings["DAEMON_SERVICE_ADDRESS"];
        //public string INDEX_CHARSET = "EUCKR";//ConfigurationManager.AppSettings["INDEX_CHARSET"];

        private ClientClass mClient = null;
        public ClientClass Client
        {
            get { return mClient; }
        }

        public SearchManager()
        {
            mClient = new ClientClass();
        }

        public int mTotal = 0;

        /// <summary>
        /// 사용자의 질의어를 서버로 보낸다.
        /// </summary>
        /// <param name="aScenario">시나리오</param>
        /// <param name="aQuery">검색쿼리</param>
        /// <param name="aLogInfo">로그정보</param>
        /// <param name="aHilightText">하이라이트 키워드</param>
        /// <param name="aOrderBy">정렬절</param>
        /// <param name="aPageNum">페이지번호</param>
        /// <param name="aPageSize">페이지사이즈</param>
        /// <param name="aUseRowId">rowid 사용 유무 플래그</param>
        /// <param name="aLanguage">언어셋</param>
        /// <param name="aCharset">캐릭터셋</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="KonanException"></exception>
        /// <returns>결과 VO</returns>
        public ResultVO SubmitQuery(string aScenario, string aQuery, string aLogInfo, string aHilightText,
            string aOrderBy, int aPageNum, int aPageSize, bool aUseRowId, int aLanguage, int aCharset)
        {
            ResultVO lResult = new ResultVO();
            lResult.ReturnCode = 0;
            lResult.ReturnMessage = string.Empty;

            object lTotal = 0;
            object lRows = 0;
            object lCols = 0;
            object lColNameArrayObject = null;

            object lRowIds = null;
            object lScores = null;
            object lRowArrayObject = null; //한개의 로우 데이터 object[]
            object lfdataLength = null;
            
            
            int i, j;

            int lStartNum = (aPageNum - 1) * aPageSize;
            
            
            try
            {
                if (mClient.BeginSession() < 0)
                {
                    throw (new KonanException("BeginSession : " + mClient.GetErrorMessage()));
                }

                lResult.ReturnCode = mClient.SetOption(mClient.OPTION_SOCKET_TIMEOUT_REQUEST, 3 * 60);
                if (lResult.ReturnCode < 0)
                {
                    throw (new KonanException("SetOption : " + mClient.GetErrorMessage()));
                }
                //if ("utf8".Equals(INDEX_CHARSET, StringComparison.OrdinalIgnoreCase))
                //{
                //    //문자 세트가 utf 8 인 경우 설정.
                //    lResult.ReturnCode = mClient.SetOption(mClient.OPTION_REQUEST_CHARSET_UTF8, 1);

                //    if (lResult.ReturnCode < 0)
                //    {
                //        throw (new KonanException("SetOption : " + mClient.GetErrorMessage()));
                //    }
                //}

                lResult.ReturnCode = mClient.SubmitQuery(DAEMON_SERVICE_IP, DAEMON_SERVICE_PORT, "", aLogInfo, aScenario, aQuery,
                    aOrderBy, aHilightText, lStartNum, aPageSize, aLanguage, aCharset);

                if (lResult.ReturnCode != 0)
                {
                    throw (new KonanException("ReturnCode : " + lResult.ReturnCode + " Scenario : " + aScenario + "\n Query : " + aQuery + "\n OrderBy : " + aOrderBy));
                }

                if (mClient.GetResult_TotalCount(out lTotal) < 0)
                {
                    throw (new KonanException("GetResult_TotalCount:" + mClient.GetErrorMessage()));
                }

                if (mClient.GetResult_RowSize(out lRows) < 0)
                {
                    throw (new KonanException("GetResult_RowSize:" + mClient.GetErrorMessage()));
                }

                if (mClient.GetResult_ColumnSize(out lCols) < 0)
                {
                    throw (new KonanException("GetResult_ColumnSize:" + mClient.GetErrorMessage()));
                }
                if (mClient.GetResult_ColumnName(out lColNameArrayObject, (int)lCols) < 0)
                {
                    throw (new KonanException("GetResult_ColumnName:" + mClient.GetErrorMessage()));
                }

                if (aUseRowId)
                {

                    lRowIds = new object[(int)lRows];
                    lScores = new object[(int)lRows];

                    if (mClient.GetResult_ROWID(out lRowIds, out lScores) < 0)
                    {
                        throw (new KonanException("GetResult_ROWID:" + mClient.GetErrorMessage()));
                    }
                }

                object[] lColNameArray = (object[])lColNameArrayObject;

                List<Dictionary<string, object>> lData = new List<Dictionary<string, object>>(); //결과 데이터
                for (i = 0; i < (int)lRows; i++) 
                {
                    mClient.GetResult_Row(out lRowArrayObject, out lfdataLength, i);
                    object[] lRowArray = (object[])lRowArrayObject;

                    Dictionary<string, object> lRowDic = new Dictionary<string, object>();
                    for (j = 0; j < (int)lCols; j++)
                    {
                        lRowDic.Add((string)lColNameArray[j], lRowArray[j]);
                    }

                    lData.Add(lRowDic);
		        }

                lResult.Total = (int)lTotal;
                lResult.Rows = (int)lRows;
                lResult.Cols = (int)lCols;
                lResult.RowIds = (object[])lRowIds;
                lResult.Scores = (object[])lScores;
                lResult.ColNames = lColNameArray;
                lResult.Data = lData;
                mTotal = (int)lTotal;
            }
            catch (KonanException e)
            {
                throw (new KonanException("Unexpected Error : " + e.Message));
            }
            catch (Exception e)
            {
                throw (new Exception("Unexpected Error : " + e.Message));
            }
            finally
            {
                mClient.EndSession();
            }

            return lResult;
        }

        /// <summary>
        /// 검색엔진의 스칼라 값을 가져온다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aColName"></param>
        /// <param name="aScenario"></param>
        /// <param name="aQuery"></param>
        /// <param name="aGroupBy"></param>
        /// <returns></returns>
        public IDictionary<string, object> GetScalar<T>(Expression<Func<T, object>> aColName, string aScenario, string aQuery)
        {
            IDictionary<string, object> lResult = new Dictionary<string, object>();
            int lReturnCode = 0;
            string lReturnMessage = string.Empty;

            object lGroupCount;
            object lOutGroupKeyCount;
            object lGroupKeyVal;
            object lGroupSize;

            int lPageNumber = 1;
            int lPageSize = 1000;
            int lStartNum = (lPageNumber - 1) * lPageSize;

            string lGroupBy = "group by " + ReflectionUtil.GetMemberName(aColName);
            string lOrderBy = "order by " + ReflectionUtil.GetMemberName(aColName);
            string lLogInfo = "";

            try
            {
                if (mClient.BeginSession() < 0)
                {
                    throw (new KonanException("BeginSession : " + mClient.GetErrorMessage()));
                }

                lReturnCode = mClient.SetOption(mClient.OPTION_SOCKET_TIMEOUT_REQUEST, 3 * 60);
                if (lReturnCode < 0)
                {
                    throw (new KonanException("SetOption : " + mClient.GetErrorMessage()));
                }

                lReturnCode = mClient.Search(DAEMON_SERVICE_ADDRESS, aScenario, string.Format("{0} {1}", aQuery, lGroupBy), lOrderBy, "", lLogInfo, lStartNum, lPageSize, mClient.LC_KOREAN, mClient.CS_EUCKR);

                if (lReturnCode != 0)
                {
                    throw (new KonanException("ReturnCode : " + lReturnCode + " Scenario : " + aScenario + "\n Query : " + aQuery));
                }

                if (mClient.GetResult_GroupBy(out lGroupCount, out lOutGroupKeyCount, out lGroupKeyVal, out lGroupSize, 10) < 0)
                {
                    throw (new KonanException("GetResult_GroupBy:" + mClient.GetErrorMessage()));
                }

                for(int i = 0; i < (int)lGroupCount; i++) {
                    var key = (string)((object[,])lGroupKeyVal)[i, 0];
                    var val = ((object[])lGroupSize)[i];
                    lResult.Add(key.ToUpper(), val);
                }
                
                //mTotal = (int)lTotal;
            }
            catch (KonanException e)
            {
                throw (new KonanException("Unexpected Error : " + e.Message));
            }
            catch (Exception e)
            {
                throw (new Exception("Unexpected Error : " + e.Message));
            }
            finally
            {
                mClient.EndSession();
            }

            return lResult;
        }

        /// <summary>
        /// 사용자의 질의어를 서버로 보낸다.
        /// </summary>
        /// <param name="aScenario">시나리오</param>
        /// <param name="aQuery">검색쿼리</param>
        /// <param name="aLogInfo">로그정보</param>
        /// <param name="aHilightText">하이라이트 키워드</param>
        /// <param name="aOrderBy">정렬절</param>
        /// <param name="aPageNum">페이지번호</param>
        /// <param name="aPageSize">페이지사이즈</param>
        /// <param name="aUseRowId">rowid 사용 유무 플래그</param>
        /// <param name="aLanguage">언어셋</param>
        /// <param name="aCharset">캐릭터셋</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="KonanException"></exception>
        /// <returns>결과 VO</returns>
        public ResultVO<T> SubmitQuery<T>(string aScenario, string aQuery, string aLogInfo, string aHilightText,
            string aOrderBy, int aPageNum, int aPageSize, bool aUseRowId, int aLanguage, int aCharset)
        {
            ResultVO<T> lResult = new ResultVO<T>();
            lResult.ReturnCode = 0;
            lResult.ReturnMessage = string.Empty;

            object lTotal = 0;
            object lRows = 0;
            object lCols = 0;
            object lColNameArrayObject = null;

            object lRowIds = null;
            object lScores = null;
            object lRowArrayObject = null; //한개의 로우 데이터 object[]
            object lfdataLength = null;


            int i, j;

            int lStartNum = (aPageNum - 1) * aPageSize;


            try
            {
                if (mClient.BeginSession() < 0)
                {
                    throw (new KonanException("BeginSession : " + mClient.GetErrorMessage()));
                }

                lResult.ReturnCode = mClient.SetOption(mClient.OPTION_SOCKET_TIMEOUT_REQUEST, 3 * 60);
                if (lResult.ReturnCode < 0)
                {
                    throw (new KonanException("SetOption : " + mClient.GetErrorMessage()));
                }
                //if ("utf8".Equals(INDEX_CHARSET, StringComparison.OrdinalIgnoreCase))
                //{
                //    //문자 세트가 utf 8 인 경우 설정.
                //    lResult.ReturnCode = mClient.SetOption(mClient.OPTION_REQUEST_CHARSET_UTF8, 1);

                //    if (lResult.ReturnCode < 0)
                //    {
                //        throw (new KonanException("SetOption : " + mClient.GetErrorMessage()));
                //    }
                //}

                lResult.ReturnCode = mClient.SubmitQuery(DAEMON_SERVICE_IP, DAEMON_SERVICE_PORT, "", aLogInfo, aScenario, aQuery,
                    aOrderBy, aHilightText, lStartNum, aPageSize, aLanguage, aCharset);
                
                if (lResult.ReturnCode != 0)
                {
                    throw (new KonanException("ReturnCode : " + lResult.ReturnCode + " Scenario : " + aScenario + "\n ErrorMessage : " + mClient.GetErrorMessage() + "\n Query : " + aQuery + "\n OrderBy : " + aOrderBy));
                }

                if (mClient.GetResult_TotalCount(out lTotal) < 0)
                {
                    throw (new KonanException("GetResult_TotalCount:" + mClient.GetErrorMessage()));
                }

                if (mClient.GetResult_RowSize(out lRows) < 0)
                {
                    throw (new KonanException("GetResult_RowSize:" + mClient.GetErrorMessage()));
                }

                if (mClient.GetResult_ColumnSize(out lCols) < 0)
                {
                    throw (new KonanException("GetResult_ColumnSize:" + mClient.GetErrorMessage()));
                }
                if (mClient.GetResult_ColumnName(out lColNameArrayObject, (int)lCols) < 0)
                {
                    throw (new KonanException("GetResult_ColumnName:" + mClient.GetErrorMessage()));
                }

                if (aUseRowId)
                {

                    lRowIds = new object[(int)lRows];
                    lScores = new object[(int)lRows];

                    if (mClient.GetResult_ROWID(out lRowIds, out lScores) < 0)
                    {
                        throw (new KonanException("GetResult_ROWID:" + mClient.GetErrorMessage()));
                    }
                }

                object[] lColNameArray = (object[])lColNameArrayObject;

                List<T> lData = new List<T>(); //결과 데이터
                for (i = 0; i < (int)lRows; i++)
                {
                    mClient.GetResult_Row(out lRowArrayObject, out lfdataLength, i);
                    object[] lRowArray = (object[])lRowArrayObject;
                    
                    T lRow = Activator.CreateInstance<T>();
                    for (j = 0; j < (int)lCols; j++)
                    {
                        System.Reflection.PropertyInfo lProperty = lRow.GetType().GetProperty((string)lColNameArray[j]);
                        if (lProperty != null)
                        {
                            Type t = Nullable.GetUnderlyingType(lProperty.PropertyType) ?? lProperty.PropertyType;
                            object lSafeValue = (lRowArray[j] == null || (string)lRowArray[j] == string.Empty) ? null : Convert.ChangeType(lRowArray[j], t);
                            lProperty.SetValue(lRow, lSafeValue, null);
                        }
                    }

                    lData.Add(lRow);
                }

                lResult.Total = (int)lTotal;
                lResult.Rows = (int)lRows;
                lResult.Cols = (int)lCols;
                lResult.RowIds = (object[])lRowIds;
                lResult.Scores = (object[])lScores;
                lResult.ColNames = lColNameArray;
                lResult.Data = lData;
                mTotal = (int)lTotal;
            }
            catch (KonanException e)
            {
                throw (new KonanException("Unexpected Error : " + e.Message));
            }
            catch (Exception e)
            {
                throw (new Exception("Unexpected Error : " + e.Message));
            }
            finally
            {
                mClient.EndSession();
            }

            return lResult;
        }

        /// <summary>
        /// 이 함수는 SubmitQuery  함수와 동일하게 사용자의 질의어를 서버로 보낸다.
        /// 분산 검색이나 GetResult_GroupBy 함수를 사용하기 위해서는 반드시 이 API를 사용해야 한다.
        /// </summary>
        /// <param name="aScenario">시나리오</param>
        /// <param name="aQuery">검색쿼리</param>
        /// <param name="aLogInfo">로그정보</param>
        /// <param name="aHilightText">하이라이트 키워드</param>
        /// <param name="aOrderBy">정렬절</param>
        /// <param name="aPageNum">페이지번호</param>
        /// <param name="aPageSize">페이지사이즈</param>
        /// <param name="aUseRowId">rowid 사용 유무 플래그</param>
        /// <param name="aLanguage">언어셋</param>
        /// <param name="aCharset">캐릭터셋</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="KonanException"></exception>
        /// <returns>결과 VO</returns>
        public ResultVO Search(string aScenario, string aQuery, string aLogInfo, string aHilightText,
            string aOrderBy, int aPageNum, int aPageSize, bool aUseRowId, int aLanguage, int aCharset)
        {
            ResultVO lResult = new ResultVO();
            lResult.ReturnCode = 0;
            lResult.ReturnMessage = string.Empty;

            object lTotal = 0;
            object lRows = 0;
            object lCols = 0;
            object lColNameArrayObject = null;

            object lRowIds = null;
            object lScores = null;
            object lRowArrayObject = null; //한개의 로우 데이터 object[]
            object lfdataLength = null;

            int i, j;

            int lStartNum = (aPageNum - 1) * aPageSize;

            try
            {
                if (mClient.BeginSession() < 0)
                {
                    throw (new KonanException("BeginSession : " + mClient.GetErrorMessage()));
                }

                //if ("utf8".Equals(INDEX_CHARSET, StringComparison.OrdinalIgnoreCase))
                //{
                //    //문자 세트가 utf 8 인 경우 설정.
                //    lResult.ReturnCode = mClient.SetOption(mClient.OPTION_REQUEST_CHARSET_UTF8, 1);

                //    if (lResult.ReturnCode < 0)
                //    {
                //        throw (new KonanException("SetOption : " + mClient.GetErrorMessage()));
                //    }
                //}

                lResult.ReturnCode = mClient.Search(DAEMON_SERVICE_ADDRESS, aScenario, aQuery, aOrderBy, aHilightText,
                    aLogInfo, lStartNum, aPageSize, aLanguage, aCharset);

                if (lResult.ReturnCode != 0)
                {
                    throw (new KonanException("ReturnCode : " + lResult.ReturnCode + " Scenario : " + aScenario + "\n Query : " + aQuery + "\n OrderBy : " + aOrderBy));
                }

                if (mClient.GetResult_TotalCount(out lTotal) < 0)
                {
                    throw (new KonanException("GetResult_TotalCount:" + mClient.GetErrorMessage()));
                }

                if (mClient.GetResult_RowSize(out lRows) < 0)
                {
                    throw (new KonanException("GetResult_RowSize:" + mClient.GetErrorMessage()));
                }

                if (mClient.GetResult_ColumnSize(out lCols) < 0)
                {
                    throw (new KonanException("GetResult_ColumnSize:" + mClient.GetErrorMessage()));
                }
                if (mClient.GetResult_ColumnName(out lColNameArrayObject, (int)lCols) < 0)
                {
                    throw (new KonanException("GetResult_ColumnName:" + mClient.GetErrorMessage()));
                }

                if (aUseRowId)
                {

                    lRowIds = new object[(int)lRows];
                    lScores = new object[(int)lRows];

                    if (mClient.GetResult_ROWID(out lRowIds, out lScores) < 0)
                    {
                        throw (new KonanException("GetResult_ROWID:" + mClient.GetErrorMessage()));
                    }
                }

                object[] lColNameArray = (object[])lColNameArrayObject;

                List<Dictionary<string, object>> lData = new List<Dictionary<string, object>>(); //결과 데이터
                for (i = 0; i < (int)lRows; i++)
                {
                    mClient.GetResult_Row(out lRowArrayObject, out lfdataLength, i);
                    object[] lRowArray = (object[])lRowArrayObject;

                    Dictionary<string, object> lRowDic = new Dictionary<string, object>();
                    for (j = 0; j < (int)lCols; j++)
                    {
                        lRowDic.Add((string)lColNameArray[j], lRowArray[j]);
                    }

                    lData.Add(lRowDic);
                }

                lResult.Total = (int)lTotal;
                lResult.Rows = (int)lRows;
                lResult.Cols = (int)lCols;
                lResult.RowIds = (object[])lRowIds;
                lResult.Scores = (object[])lScores;
                lResult.Data = lData;
                mTotal = (int)lTotal;
            }
            catch (KonanException e)
            {
                throw (new KonanException("Unexpected Error : " + e.Message));
            }
            catch (Exception e)
            {
                throw (new Exception("Unexpected Error : " + e.Message));
            }
            finally
            {
                mClient.EndSession();
            }

            return lResult;
        }

        public string[,] getPopularKwdAndTag(int doaminNo, int count)
	    {
            
            
            int i = 0;
            int rc = 0;
            int MaxOutCount = 10;       // 인기검색어 리턴 개수(max)

            if ( count > 0 ) {
			    MaxOutCount = count;
		    }

            object out_str = null;      // 인기검색어 배열
            object out_tag = null;      // 인기검색어의 태그값 배열
            object out_count = null;    // 리턴 인기검색어 수
            string[,] arr_ppk = null;   // 리턴 이차원배열(인기검색어, 태그)

            // DOCRUZER 객체 생성
            ClientClass crz = new ClientClass();

            try
            {
                // 핸들 생성
                if (crz.BeginSession() < 0)
                {
                    throw (new KonanException("BeginSession : " + crz.GetErrorMessage()));
                }

                //if ("utf8".Equals(INDEX_CHARSET, StringComparison.OrdinalIgnoreCase))
                //{

                //    rc = crz.SetOption(crz.OPTION_REQUEST_CHARSET_UTF8, 1);

                //    if (rc < 0)
                //    {
                //        throw (new KonanException("SetOption : " + crz.GetErrorMessage()));
                //    }
                //}
                
                //인기 검색어를 추출함. (태그 정보 포함)
                rc = crz.GetPopularKeyword2(DAEMON_SERVICE_ADDRESS, out out_count, out out_str, out out_tag, MaxOutCount, doaminNo);
                
                if (rc != 0)
                {
                    throw (new KonanException("rc : " + rc + " doaminNo : " + doaminNo + "\n count : " + count));
                }

                if ((int)out_count > 0)
                {
                    arr_ppk = new string[(int)out_count,2];
                    
                    for ( i = 0; i < (int)out_count; i++ )
                    {
                        arr_ppk[i,0] = ((object[])out_str)[i].ToString();
                        if (((object[])out_tag)[i].ToString().Length == 0)
                            arr_ppk[i, 1] = "&nbsp;";
                        else
                            arr_ppk[i,1] = ((object[])out_tag)[i].ToString();
                    }
                }
            }
            catch (KonanException e)
            {
                throw (new KonanException("Unexpected Error : " + e.Message));
            }
            catch (Exception e)
            {
                throw (new Exception("Unexpected Error : " + e.Message));
            }
            finally
            {
                // 핸들 삭제
                crz.EndSession();
            }
            

		    return arr_ppk;
	    }

        public string[] getRecommandKwd(string kwd, int doaminNo, int count)
        {


            int i = 0;
            int rc = 0;
            int MaxOutCount = 10;

            if (count > 0)
            {
                MaxOutCount = count;
            }

            object out_str = null;
            object out_count = null;
            string[] arr_reckwd = null;

            ClientClass crz = new ClientClass();

            try
            {
                if (crz.BeginSession() < 0)
                {
                    throw (new KonanException("BeginSession : " + crz.GetErrorMessage()));
                }

                //if ("utf8".Equals(INDEX_CHARSET, StringComparison.OrdinalIgnoreCase))
                //{

                //    rc = crz.SetOption(crz.OPTION_REQUEST_CHARSET_UTF8, 1);

                //    if (rc < 0)
                //    {
                //        throw (new KonanException("SetOption : " + crz.GetErrorMessage()));
                //    }
                //}

                rc = crz.RecommendKeyword(DAEMON_SERVICE_ADDRESS, out out_count, out out_str, MaxOutCount, kwd, doaminNo);

                if (rc != 0)
                {
                    throw (new KonanException("rc : " + rc + " doaminNo : " + doaminNo + "\n count : " + count));
                }

                if ((int)out_count > 0)
                {
                    arr_reckwd = new string[(int)out_count];

                    for (i = 0; i < (int)out_count; i++)
                    {
                        arr_reckwd[i] = ((object[])out_str)[i].ToString();
                    }
                }
            }
            catch (KonanException e)
            {
                throw (new KonanException("Unexpected Error : " + e.Message));
            }
            catch (Exception e)
            {
                throw (new Exception("Unexpected Error : " + e.Message));
            }
            finally
            {
                crz.EndSession();
            }

            return arr_reckwd;
        }
        
    }
}
