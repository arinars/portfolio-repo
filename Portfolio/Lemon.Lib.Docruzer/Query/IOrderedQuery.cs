using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Lemon.Lib.Docruzer.Query
{
    #region 주석
    /*
    /// <summary>
    /// OrderBy절 생성시, 이 인터페이스를 사용합니다.
    /// 검색엔진 동적 쿼리 생성을 위한 인터페이스입니다.
    /// <para>작성일 : 2014. 05. 15(수)</para>
    /// <para>작성자 : 김민섭</para>
    /// </summary>
    /// <remarks>
    /// 변경자/변경일 : OOO / yyyy.mm.dd
    /// 변경사유/내역 : 기능 추가로 변경함.
    /// </remarks>
    public interface IOrderedQuery : IQuery
    {
        /// <summary>
        /// 컬럼명에 따라 오름차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery ThenBy(string aColName);
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery ThenByDescending(string aColName);
    }
    */
    #endregion
    /// <summary>
    /// OrderBy절 생성시, 이 인터페이스를 사용합니다.
    /// 검색엔진 동적 쿼리 생성을 위한 인터페이스입니다.
    /// <para>작성일 : 2014. 05. 15(수)</para>
    /// <para>작성자 : 김민섭</para>
    /// </summary>
    /// <remarks>
    /// 변경자/변경일 : OOO / yyyy.mm.dd
    /// 변경사유/내역 : 기능 추가로 변경함.
    /// </remarks>
    public interface IOrderedQuery<T> : IQuery<T>
    {
        /// <summary>
        /// 컬럼명에 따라 오름차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery<T> ThenBy(Expression<Func<T, object>> aColName);
        /// <summary>
        /// 컬럼명에 따라 오름차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery<T> ThenBy(string aColName);
        
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery<T> ThenByDescending(Expression<Func<T, object>> aColName);
        /// <summary>
        /// 컬럼명에 따라 내림차순으로 다시 정렬합니다.
        /// OrderBy, OrderByDescending을 사용한 이후에 사용할 수 있습니다.
        /// </summary>
        /// <param name="aColName"></param>
        /// <returns></returns>
        IOrderedQuery<T> ThenByDescending(string aColName);
    }
}
