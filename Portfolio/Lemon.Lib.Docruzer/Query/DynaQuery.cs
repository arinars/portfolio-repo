using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using Lemon.Lib.Docruzer.Enum;

namespace Lemon.Lib.Docruzer.Query
{
    #region 주석
    /*
    /// <summary>
    /// 검색엔진 동적 쿼리 생성 API.
    /// <para>작성일 : 2014. 05. 15(수)</para>
    /// <para>작성자 : 김민섭</para>
    /// </summary>
    /// <remarks>
    /// 변경자/변경일 : OOO / yyyy.mm.dd
    /// 변경사유/내역 : 기능 추가로 변경함.
    /// </remarks>
    public class DynaQuery : IQuery, IOrderedQuery
    {
        private StringBuilder mSql = new StringBuilder(1024);
        private StringBuilder mOrderBy = new StringBuilder(1024);
        private List<string> mOrderByParams = new List<string>();
        private List<string> mCommands = new List<string>();

        /// <summary>
        /// 쿼리 인스턴스를 생성합니다.
        /// </summary>
        /// <returns></returns>
        public static IQuery Instance()
        {
            return new DynaQuery();
        }

        /// <summary>
        /// 하나의 명령을 생성하여 삽입합니다.
        /// </summary>
        /// <param name="aKey"></param>
        /// <param name="aOperator"></param>
        /// <param name="aValue"></param>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        public IQuery Command(string aKey, Operator aOperator, object aValue, SearchType aSearchType)
        {
            switch (aOperator)
            {
                case Operator.Equals:
                case Operator.LessThan:
                case Operator.LessThanEqualTo:
                case Operator.GreaterThan:
                case Operator.GreaterThanEqualTo:
                case Operator.NotEquals:
                case Operator.Like:
                case Operator.NotLike:
                    break;
                case Operator.In:

                    if (aValue != null && aValue is string[])
                    {
                        aValue = string.Join(",", aValue as string[]);
                    }

                    if (aValue != null && aValue is string)
                    {
                        string lValue = (string)aValue;
                        lValue = lValue.Trim();
                        if (!(lValue.StartsWith("{") || lValue.EndsWith("}")))
                        {
                            aValue = "{" + aValue + "}";
                        }
                    }
                    break;
                default:
                    break;
            }
            if (aSearchType == SearchType.None)
            {
                mCommands.Add(string.Format("{0} {1} {2}", aKey, getOperator(aOperator), aValue));
            }
            else
            {
                mCommands.Add(string.Format("{0} {1} {2} {3}", aKey, getOperator(aOperator), aValue, getSearchType(aSearchType)));
            }
            return this;
        }

        /// <summary>
        /// and 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        public IQuery And()
        {
            mCommands.Add("and");
            return this;
        }

        /// <summary>
        /// andnot 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        public IQuery AndNot()
        {
            mCommands.Add("andnot");
            return this;
        }

        /// <summary>
        /// or 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        public IQuery Or()
        {
            mCommands.Add("or");
            return this;
        }

        /// <summary>
        /// or 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        public IQuery Append(string aStr)
        {
            mCommands.Add(aStr);
            return this;
        }

        /// <summary>
        /// 현재까지 생성된 쿼리 전체를 그룹핑합니다. "( 쿼리 )"
        /// </summary>
        /// <returns></returns>
        public IQuery Grouping()
        {
            mCommands.Insert(0, "(");
            mCommands.Add(")");
            return this;
        }

        /// <summary>
        /// 그룹의 첫문자를 추가합니다. "("
        /// </summary>
        /// <returns></returns>
        public IQuery GroupStart()
        {
            mCommands.Add("(");
            return this;
        }

        /// <summary>
        /// 그룹의 끝문자를 추가합니다. ")"
        /// </summary>
        /// <returns></returns>
        public IQuery GroupEnd()
        {
            mCommands.Add(")");
            return this;
        }

        /// <summary>
        /// 결과 쿼리를 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            mSql.Clear();
            mSql.Append(string.Join(" ", mCommands));
            return mSql.ToString();
        }

        /// <summary>
        /// 결과 Order By 절을 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        public string GetOrderBy()
        {
            mOrderBy.Clear();
            if(mOrderByParams != null && mOrderByParams.Count() > 0) {
                mOrderBy.Append("order by ");
                mOrderBy.Append(string.Join(", ", mOrderByParams));
            }
            return mOrderBy.ToString();
        }

        /// <summary>
        /// 연산자명을 가져옵니다.
        /// </summary>
        /// <param name="aOperator"></param>
        /// <returns></returns>
        private string getOperator(Operator aOperator)
        {
            switch (aOperator)
            {
                case Operator.Equals: return "=";
                case Operator.GreaterThan: return ">";
                case Operator.GreaterThanEqualTo: return ">=";
                case Operator.LessThan: return "<";
                case Operator.LessThanEqualTo: return "<=";
                case Operator.NotEquals: return "!=";
                case Operator.Like: return "like";
                case Operator.NotLike: return "not like";
                case Operator.In: return "in";
                default: return "";
            }
        }
        /// <summary>
        /// 검색타입명을 가져옵니다.
        /// </summary>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        private string getSearchType(SearchType aSearchType)
        {
            switch (aSearchType)
            {
                case SearchType.None: return "";
                case SearchType.Natural: return "natural";
                case SearchType.Anyword: return "anyword";
                case SearchType.Allword: return "allword";
                case SearchType.Allin25: return "allin25";
                case SearchType.Alladjacent: return "alladjacent";
                case SearchType.Allorderadjacent: return "allorderadjacent";
                case SearchType.Allwordthruindex: return "allwordthruindex";
                case SearchType.Someword: return "someword";
                case SearchType.Somewordthruindex: return "somewordthruindex";
                case SearchType.Boolean: return "boolean";
                case SearchType.Similar: return "similar";
                case SearchType.Similar2: return "similar2";
                case SearchType.Replicate: return "replicate";
                case SearchType.Proxkeymatch: return "proxkeymatch";
                case SearchType.Synonym: return "synonym";
                default: return "";
            }
        }

        /// <summary>
        /// 컬럼명에 따라 오름차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery OrderBy(string aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", aColName, "asc"));
            return this;
        }

        /// <summary>
        /// 컬럼명에 따라 내림차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery OrderByDescending(string aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}",aColName,"desc"));
            return this;
        }

        /// <summary>
        /// 컬럼명에 따라 오름차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery ThenBy(string aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", aColName, "asc"));
            return this;
        }

        /// <summary>
        /// 컬럼명에 따라 내림차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery ThenByDescending(string aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", aColName, "desc"));
            return this;
        }
    }
    */
    #endregion
    /// <summary>
    /// 검색엔진 동적 쿼리 생성 API.
    /// <para>작성일 : 2014. 05. 15(수)</para>
    /// <para>작성자 : 김민섭</para>
    /// </summary>
    /// <remarks>
    /// 변경자/변경일 : OOO / yyyy.mm.dd
    /// 변경사유/내역 : 기능 추가로 변경함.
    /// </remarks>
    public class DynaQuery<T> : IQuery<T>, IOrderedQuery<T>
    {
        private StringBuilder mSql = new StringBuilder(1024);
        private StringBuilder mOrderBy = new StringBuilder(1024);
        private List<string> mOrderByParams = new List<string>();
        private List<Command> mCommands = new List<Command>();

        /// <summary>
        /// 쿼리 인스턴스를 생성합니다.
        /// </summary>
        /// <returns></returns>
        public static IQuery<T> Instance()
        {
            return new DynaQuery<T>();
        }

        /// <summary>
        /// 하나의 명령을 생성하여 삽입합니다.
        /// </summary>
        /// <param name="aKey"></param>
        /// <param name="aOperator"></param>
        /// <param name="aValue"></param>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        public IQuery<T> Command(Expression<Func<T, object>> aColName, Operator aOperator, object aValue, SearchType aSearchType)
        {
            //명령 생성시 Value 값이 없는 경우
            if (aValue == null)
            {
                //명령목록이 비어있지 않고, 마지막 명령이 논리연산자인 경우
                if (!IsEmptyQuery() && mCommands.Last().Type == CommandType.LogicalOperator)
                {
                    //마지막 논리 연산자 제거
                    RemoveLastCommand();
                }
                return this;
            }

            if (aValue is string[] && !(aValue as string[]).Any())
            {
                //명령목록이 비어있지 않고, 마지막 명령이 논리연산자인 경우
                if (!IsEmptyQuery() && mCommands.Last().Type == CommandType.LogicalOperator)
                {
                    //마지막 논리 연산자 제거
                    RemoveLastCommand();
                }
                return this;
            }

            if (aValue is string && string.IsNullOrEmpty(aValue as string))
            {
                //명령목록이 비어있지 않고, 마지막 명령이 논리연산자인 경우
                if (!IsEmptyQuery() && mCommands.Last().Type == CommandType.LogicalOperator)
                {
                    //마지막 논리 연산자 제거
                    RemoveLastCommand();
                }
                return this;
            }
            
            switch (aOperator)
            {
                case Operator.Equals:
                case Operator.LessThan:
                case Operator.LessThanEqualTo:
                case Operator.GreaterThan:
                case Operator.GreaterThanEqualTo:
                case Operator.NotEquals:
                case Operator.Like:
                case Operator.NotLike:
                    double lNumber;
                    if (!double.TryParse(Convert.ToString(aValue), out lNumber))
                    {
                        aValue = "'" + aValue + "'";
                    }
                    break;
                case Operator.In:

                    if (aValue is string[])
                    {
                        aValue = string.Join(",", aValue as string[]);
                    }

                    if (aValue is string)
                    {
                        string lValue = (string)aValue;
                        lValue = lValue.Trim();
                        if (!(lValue.StartsWith("{") || lValue.EndsWith("}")))
                        {
                            aValue = "{" + aValue + "}";
                        }
                    }
                    break;
                case Operator.NotIn:

                    if (aValue is string[])
                    {
                        aValue = string.Join(",", aValue as string[]);
                    }

                    if (aValue is string)
                    {
                        string lValue = (string)aValue;
                        lValue = lValue.Trim();
                        if (!(lValue.StartsWith("{") || lValue.EndsWith("}")))
                        {
                            aValue = "{" + aValue + "}";
                        }
                    }
                    break;
                default:
                    break;
            }
            if (aSearchType == SearchType.None)
            {
                mCommands.Add(new Command
                {
                    Type = CommandType.Command,
                    Value = string.Format("{0} {1} {2}", ReflectionUtil.GetMemberName(aColName), getOperator(aOperator), aValue)
                });
            }
            else
            {
                mCommands.Add(new Command
                {
                    Type = CommandType.Command,
                    Value = string.Format("{0} {1} {2} {3}", ReflectionUtil.GetMemberName(aColName), getOperator(aOperator), aValue, getSearchType(aSearchType))
                });
            }
            return this;
        }

        /// <summary>
        /// 하나의 명령을 생성하여 삽입합니다.
        /// </summary>
        /// <param name="aKey"></param>
        /// <param name="aOperator"></param>
        /// <param name="aValue"></param>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        public IQuery<T> Command(string aKey, Operator aOperator, object aValue, SearchType aSearchType)
        {
            //명령 생성시 Value 값이 없는 경우
            if (aValue == null)
            {
                //명령목록이 비어있지 않고, 마지막 명령이 논리연산자인 경우
                if (!IsEmptyQuery() && mCommands.Last().Type == CommandType.LogicalOperator)
                {
                    //마지막 논리 연산자 제거
                    RemoveLastCommand();
                }
                return this;
            }
            switch (aOperator)
            {
                case Operator.Equals:
                case Operator.LessThan:
                case Operator.LessThanEqualTo:
                case Operator.GreaterThan:
                case Operator.GreaterThanEqualTo:
                case Operator.NotEquals:
                case Operator.Like:
                case Operator.NotLike:
                    double lNumber;
                    if (!double.TryParse(Convert.ToString(aValue), out lNumber))
                    {
                        aValue = "'" + aValue + "'";
                    }
                    break;
                case Operator.In:

                    if (aValue is string[])
                    {
                        aValue = string.Join(",", aValue as string[]);
                    }

                    if (aValue is string)
                    {
                        string lValue = (string)aValue;
                        lValue = lValue.Trim();
                        if (!(lValue.StartsWith("{") || lValue.EndsWith("}")))
                        {
                            aValue = "{" + aValue + "}";
                        }
                    }
                    break;
                default:
                    break;
            }
            if (aSearchType == SearchType.None)
            {
                mCommands.Add(new Command
                {
                    Type = CommandType.Command,
                    Value = string.Format("{0} {1} {2}", aKey, getOperator(aOperator), aValue)
                });
            }
            else
            {
                mCommands.Add(new Command
                {
                    Type = CommandType.Command,
                    Value = string.Format("{0} {1} {2} {3}", aKey, getOperator(aOperator), aValue, getSearchType(aSearchType))
                });
            }
            return this;
        }

        /// <summary>
        /// and 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        public IQuery<T> And()
        {
            if (this.IsEmptyQuery())
            {
                return this;
            }
            if (IsValidQuery())
            {
                mCommands.Add(new Command
                {
                    Type = CommandType.LogicalOperator,
                    Value = "and"
                });
            }
            return this;
        }

        /// <summary>
        /// andnot 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        public IQuery<T> AndNot()
        {
            if (this.IsEmptyQuery())
            {
                return this;
            }
            if (IsValidQuery())
            {
                mCommands.Add(new Command
                {
                    Type = CommandType.LogicalOperator,
                    Value = "andnot"
                });
            }
            return this;
        }

        /// <summary>
        /// or 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        public IQuery<T> Or()
        {
            if (this.IsEmptyQuery())
            {
                return this;
            }

            if (IsValidQuery())
            {
                mCommands.Add(new Command
                {
                    Type = CommandType.LogicalOperator,
                    Value = "or"
                });
            }
            return this;
        }

        /// <summary>
        /// or 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        public IQuery<T> Append(string aLogicalOperator)
        {
            if (this.IsEmptyQuery())
            {
                return this;
            }
            if (!(mCommands.Last().Type == CommandType.Command || mCommands.Last().Type == CommandType.GroupEnd))
            {
                return this;
            }
            mCommands.Add(new Command
            {
                Type = CommandType.LogicalOperator,
                Value = aLogicalOperator
            });
            return this;
        }

        /// <summary>
        /// 현재까지 생성된 쿼리 전체를 그룹핑합니다. "( 쿼리 )"
        /// </summary>
        /// <returns></returns>
        public IQuery<T> Grouping()
        {
            if (!this.IsEmptyQuery())
            {
                mCommands.Insert(0, new Command
                {
                    Type = CommandType.GroupStart,
                    Value = "("
                });
                mCommands.Add(new Command
                {
                    Type = CommandType.GroupEnd,
                    Value = ")"
                });
            }
            return this;
        }

        /// <summary>
        /// 그룹의 첫문자를 추가합니다. "("
        /// </summary>
        /// <returns></returns>
        public IQuery<T> GroupStart()
        {
            mCommands.Add(new Command
            {
                Type = CommandType.GroupStart,
                Value = "("
            });
            return this;
        }

        /// <summary>
        /// 그룹의 끝문자를 추가합니다. ")"
        /// </summary>
        /// <returns></returns>
        public IQuery<T> GroupEnd()
        {
            if (IsEmptyQuery())
            {
                return this;
            }

            CommandType lLastType = mCommands.Last().Type;
            if (!IsValidQuery())
            {
                RemoveLastCommand();
            }

            if (IsEmptyQuery())
            {
                return this;
            }
            
            if (IsValidQuery())
            {
                mCommands.Add(new Command
                {
                    Type = CommandType.GroupEnd,
                    Value = ")"
                });
            }
            return this;
        }

        /// <summary>
        /// 결과 쿼리를 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            if (IsEmptyQuery())
            {
                return string.Empty;
            }

            while (!IsValidQuery())
            {
                RemoveLastCommand();
            }

            while (!IsValidQuery())
            {
                RemoveLastCommand();
            }
            

            mSql.Clear();
            mSql.Append(string.Join(" ", mCommands.Select(x => x.Value)));
            return mSql.ToString();
        }

        /// <summary>
        /// 결과내 검색 쿼리를 생성합니다.
        /// </summary>
        /// <param name="aBeforeQuery">이전 쿼리</param>
        /// <returns></returns>
        public string GetQuery(string aBeforeQuery)
        {
            if (IsEmptyQuery())
            {
                return string.Empty;
            }

            while (!IsValidQuery())
            {
                RemoveLastCommand();
            }

            while (!IsValidQuery())
            {
                RemoveLastCommand();
            }


            mSql.Clear();
            mSql.Append(string.Join(" ", mCommands.Select(x => x.Value)));

            string lQuery = string.Format("({0}) and ({1})", aBeforeQuery, mSql.ToString());
            return lQuery;
        }

        /// <summary>
        /// 결과 Order By 절을 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        public string GetOrderBy()
        {
            mOrderBy.Clear();
            if (mOrderByParams != null && mOrderByParams.Count() > 0)
            {
                mOrderBy.Append("order by ");
                mOrderBy.Append(string.Join(", ", mOrderByParams));
            }
            return mOrderBy.ToString();
        }

        /// <summary>
        /// 연산자명을 가져옵니다.
        /// </summary>
        /// <param name="aOperator"></param>
        /// <returns></returns>
        private string getOperator(Operator aOperator)
        {
            switch (aOperator)
            {
                case Operator.Equals: return "=";
                case Operator.GreaterThan: return ">";
                case Operator.GreaterThanEqualTo: return ">=";
                case Operator.LessThan: return "<";
                case Operator.LessThanEqualTo: return "<=";
                case Operator.NotEquals: return "!=";
                case Operator.Like: return "like";
                case Operator.NotLike: return "not like";
                case Operator.In: return "in";
                default: return "";
            }
        }
        /// <summary>
        /// 검색타입명을 가져옵니다.
        /// </summary>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        private string getSearchType(SearchType aSearchType)
        {
            switch (aSearchType)
            {
                case SearchType.None: return "";
                case SearchType.Natural: return "natural";
                case SearchType.Anyword: return "anyword";
                case SearchType.Allword: return "allword";
                case SearchType.Allin25: return "allin25";
                case SearchType.Alladjacent: return "alladjacent";
                case SearchType.Allorderadjacent: return "allorderadjacent";
                case SearchType.Allwordthruindex: return "allwordthruindex";
                case SearchType.Someword: return "someword";
                case SearchType.Somewordthruindex: return "somewordthruindex";
                case SearchType.Boolean: return "boolean";
                case SearchType.Similar: return "similar";
                case SearchType.Similar2: return "similar2";
                case SearchType.Replicate: return "replicate";
                case SearchType.Proxkeymatch: return "proxkeymatch";
                case SearchType.Synonym: return "synonym";
                default: return "";
            }
        }

        /// <summary>
        /// 컬럼명에 따라 오름차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery<T> OrderBy(Expression<Func<T, object>> aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", ReflectionUtil.GetMemberName(aColName), "asc"));
            return this;
        }
        /// <summary>
        /// 컬럼명에 따라 오름차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery<T> OrderBy(string aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", aColName, "asc"));
            return this;
        }
        
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery<T> OrderByDescending(Expression<Func<T, object>> aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", ReflectionUtil.GetMemberName(aColName), "desc"));
            return this;
        }
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery<T> OrderByDescending(string aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", aColName, "desc"));
            return this;
        }

        /// <summary>
        /// 컬럼명에 따라 오름차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery<T> ThenBy(Expression<Func<T, object>> aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", ReflectionUtil.GetMemberName(aColName), "asc"));
            return this;
        }
        /// <summary>
        /// 컬럼명에 따라 오름차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery<T> ThenBy(string aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", aColName, "asc"));
            return this;
        }

        /// <summary>
        /// 컬럼명에 따라 내림차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery<T> ThenByDescending(Expression<Func<T, object>> aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", ReflectionUtil.GetMemberName(aColName), "desc"));
            return this;
        }
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        public IOrderedQuery<T> ThenByDescending(string aColName)
        {
            mOrderByParams.Add(string.Format("{0} {1}", aColName, "desc"));
            return this;
        }

        public void ClearQuery()
        {
            mCommands.Clear();
        }

        public void ClearOrderBy()
        {
            mOrderByParams.Clear();
        }

        public bool IsEmptyQuery()
        {
            return mCommands.Count() == 0;
        }

        public bool IsEmptyOrderBy()
        {
            return mOrderByParams.Count() == 0;
        }

        /// <summary>
        /// 쿼리가 올바른지 검사한다. 쿼리가 비어 있을시에도 true 리턴
        /// </summary>
        /// <returns></returns>
        private bool IsValidQuery()
        {
            if (IsEmptyQuery()) {
                return true;
            }
            CommandType lLastType = mCommands.Last().Type;
            if (lLastType == CommandType.Command || lLastType == CommandType.GroupEnd)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 마지막 명령을 지운다.
        /// </summary>
        private void RemoveLastCommand()
        {
            mCommands.RemoveAt(mCommands.Count - 1);
        }
    }
}
