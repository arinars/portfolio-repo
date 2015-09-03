using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Lemon.Lib.Docruzer.Enum;

namespace Lemon.Lib.Docruzer.Query
{
    #region 주석
    /*
    /// <summary>
    /// 검색엔진 동적 쿼리 생성을 위한 인터페이스입니다.
    /// <para>작성일 : 2014. 05. 15(수)</para>
    /// <para>작성자 : 김민섭</para>
    /// </summary>
    /// <remarks>
    /// 변경자/변경일 : OOO / yyyy.mm.dd
    /// 변경사유/내역 : 기능 추가로 변경함.
    /// </remarks>
    public interface IQuery
    {
        /// <summary>
        /// 하나의 명령을 생성하여 삽입합니다.
        /// </summary>
        /// <param name="aKey"></param>
        /// <param name="aOperator"></param>
        /// <param name="aValue"></param>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        IQuery Command(string aKey, Operator aOperator, object aValue, SearchType aSearchType);
        /// <summary>
        /// and 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        IQuery And();
        /// <summary>
        /// andnot 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        IQuery AndNot();
        /// <summary>
        /// or 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        IQuery Or();
        /// <summary>
        /// 문자열을 삽입합니다.
        /// </summary>
        /// <returns></returns>
        IQuery Append(string aStr);
        /// <summary>
        /// 현재까지 생성된 쿼리 전체를 그룹핑합니다. "( 쿼리 )"
        /// </summary>
        /// <returns></returns>
        IQuery Grouping();
        /// <summary>
        /// 그룹의 첫문자를 추가합니다. "("
        /// </summary>
        /// <returns></returns>
        IQuery GroupStart();
        /// <summary>
        /// 그룹의 끝문자를 추가합니다. ")"
        /// </summary>
        /// <returns></returns>
        IQuery GroupEnd();
        /// <summary>
        /// 컬럼명에 따라 오름차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery OrderBy(string aColName);
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery OrderByDescending(string aColName);

        /// <summary>
        /// 결과 쿼리를 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        string GetQuery();
        /// <summary>
        /// 결과 Order By 절을 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        string GetOrderBy();
    }
    */
    #endregion
    /// <summary>
    /// 검색엔진 동적 쿼리 생성을 위한 인터페이스입니다.
    /// <para>작성일 : 2014. 05. 15(수)</para>
    /// <para>작성자 : 김민섭</para>
    /// </summary>
    /// <remarks>
    /// 변경자/변경일 : OOO / yyyy.mm.dd
    /// 변경사유/내역 : 기능 추가로 변경함.
    /// </remarks>
    public interface IQuery<T>
    {
        /// <summary>
        /// 하나의 명령을 생성하여 삽입합니다.
        /// </summary>
        /// <param name="aKey"></param>
        /// <param name="aOperator"></param>
        /// <param name="aValue"></param>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        IQuery<T> Command(Expression<Func<T, object>> aColName, Operator aOperator, object aValue, SearchType aSearchType);
        /// <summary>
        /// 하나의 명령을 생성하여 삽입합니다.
        /// </summary>
        /// <param name="aKey"></param>
        /// <param name="aOperator"></param>
        /// <param name="aValue"></param>
        /// <param name="aSearchType"></param>
        /// <returns></returns>
        IQuery<T> Command(string aKey, Operator aOperator, object aValue, SearchType aSearchType);
        /// <summary>
        /// and 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        IQuery<T> And();
        /// <summary>
        /// andnot 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        IQuery<T> AndNot();
        /// <summary>
        /// or 연산자를 삽입합니다.
        /// 두 명령 사이에 사용합니다.
        /// </summary>
        /// <returns></returns>
        IQuery<T> Or();
        /// <summary>
        /// 문자열을 삽입합니다.
        /// </summary>
        /// <returns></returns>
        IQuery<T> Append(string aStr);
        /// <summary>
        /// 현재까지 생성된 쿼리 전체를 그룹핑합니다. "( 쿼리 )"
        /// </summary>
        /// <returns></returns>
        IQuery<T> Grouping();
        /// <summary>
        /// 그룹의 첫문자를 추가합니다. "("
        /// </summary>
        /// <returns></returns>
        IQuery<T> GroupStart();
        /// <summary>
        /// 그룹의 끝문자를 추가합니다. ")"
        /// </summary>
        /// <returns></returns>
        IQuery<T> GroupEnd();
        /// <summary>
        /// 컬럼명에 따라 오름차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery<T> OrderBy(Expression<Func<T, object>> aColName);
        /// <summary>
        /// 컬럼명에 따라 오름차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery<T> OrderBy(string aColName);
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery<T> OrderByDescending(Expression<Func<T, object>> aColName);
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 정렬합니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery<T> OrderByDescending(string aColName);

        /// <summary>
        /// 결과 쿼리를 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        string GetQuery();
        /// <summary>
        /// 결과내 검색 쿼리를 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        string GetQuery(string aBeforeQuery);
        /// <summary>
        /// 결과 Order By 절을 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        string GetOrderBy();
        /// <summary>
        /// 모든 쿼리를 초기화 한다.
        /// </summary>
        void ClearQuery();
        void ClearOrderBy();

        bool IsEmptyQuery();
        bool IsEmptyOrderBy();
    }
}
