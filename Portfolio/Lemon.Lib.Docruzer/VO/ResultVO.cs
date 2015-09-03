using System;
using System.Collections.Generic;
using System.Linq;

namespace Lemon.Lib.Docruzer.VO
{
    /// <summary>
    /// 검색 엔진의 검색 결과를 담을 모델입니다.
    /// </summary>
    public class ResultVO
    {
        public ResultVO()
        {
        }

        private int mReturnCode = 0;
        /// <summary>
        /// 검색엔진에 쿼리 전송 결과 코드.
        /// </summary>
        public int ReturnCode
        {
            get { return mReturnCode; }
            set { mReturnCode = value; }
        }
        /// <summary>
        /// 검색엔진에 쿼리 전송 결과 메시지.
        /// </summary>
        public string ReturnMessage { get; set; }

        public int Total { get; set; }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public object[] ColNames { get; set; }

        public object[] Scores { get; set; }

        public object[] RowIds { get; set; }

        public IEnumerable<Dictionary<string, object>> Data { get; set; }
    }

    /// <summary>
    /// 검색 엔진의 검색 결과를 담을 모델입니다.
    /// </summary>
    public class ResultVO<T>
    {
        public ResultVO()
        {
        }

        private int mReturnCode = 0;
        /// <summary>
        /// 검색엔진에 쿼리 전송 결과 코드.
        /// </summary>
        public int ReturnCode
        {
            get { return mReturnCode; }
            set { mReturnCode = value; }
        }
        /// <summary>
        /// 검색엔진에 쿼리 전송 결과 메시지.
        /// </summary>
        public string ReturnMessage { get; set; }

        public int Total { get; set; }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public object[] ColNames { get; set; }

        public object[] Scores { get; set; }

        public object[] RowIds { get; set; }

        public IEnumerable<T> mData = null;
        public IEnumerable<T> Data
        {
            get
            {
                if (mData == null)
                {
                    mData = Enumerable.Empty<T>();
                }
                return mData;
            }
            set { mData = value; }
        }
    }
}
