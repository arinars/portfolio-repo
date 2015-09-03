using System;

namespace Lemon.Lib.Docruzer.VO
{

    /// <summary>
    /// Summary description for ParameterVO
    /// </summary>
    public class ParameterVO : System.Web.UI.UserControl
    {
        public ParameterVO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /** 유저 idx */
        private String idx;

        /** 검색키워드 */
        private String kwd;

        /** 이전검색어 배열 */
        private String[] preKwds;

        /** 검색 카테고리(탭) */
        private String category;

        /** 검색 카테고리(서브) */
        private String subCategory;

        /** 검색대상 필드 */
        private String srchFd;

        /** 추천검색어 정보 */
        private String recKwd;

        /** 재검색 여부 (Boolean) */
        private Boolean reSrchFlag;

        /** 페이지사이즈 */
        private int pageSize;

        /** 검색결과페이지번호 */
        private int pageNum;

        /** 정렬 */
        private String sort;

        /** 상세검색 여부 플래그 */
        private Boolean detailSearch;

        /** 제외어 */
        private String xwd;

        /** 날짜선택사항 */
        private String date;

        /** 시작일 */
        private String startDate;

        /** 종료일 */
        private String endDate;

        /** 파일확장자 */
        private String fileExt;

        /** 연도 */
        private String year;


        public String getIdx()
        {
            return idx;
        }

        public void setIdx(String idx)
        {
            this.idx = idx;
        }

        public String getKwd()
        {
            return kwd;
        }

        public void setKwd(String kwd)
        {
            this.kwd = kwd;
        }

        public String[] getPreKwds()
        {
            return preKwds;
        }

        public void setPreKwds(String[] preKwds)
        {
            this.preKwds = preKwds;
        }

        public String getCategory()
        {
            return category;
        }

        public void setCategory(String category)
        {
            this.category = category;
        }

        public String getSubCategory()
        {
            return subCategory;
        }

        public void setSubCategory(String subCategory)
        {
            this.subCategory = subCategory;
        }

        public String getRecKwd()
        {
            return recKwd;
        }

        public void setRecKwd(String recKwd)
        {
            this.recKwd = recKwd;
        }

        public Boolean getReSrchFlag()
        {
            return reSrchFlag;
        }

        public void setReSrchFlag(Boolean reSrchFlag)
        {
            this.reSrchFlag = reSrchFlag;
        }

        public String getSrchFd()
        {
            return srchFd;
        }

        public void setSrchFd(String srchFd)
        {
            this.srchFd = srchFd;
        }

        public int getPageSize()
        {
            return pageSize;
        }

        public void setPageSize(int pageSize)
        {
            this.pageSize = pageSize;
        }

        public int getPageNum()
        {
            return pageNum;
        }

        public void setPageNum(int pageNum)
        {
            this.pageNum = pageNum;
        }

        public String getSort()
        {
            return sort;
        }

        public void setSort(String sort)
        {
            this.sort = sort;
        }

        public Boolean getDetailSearch()
        {
            return detailSearch;
        }

        public void setDetailSearch(Boolean detailSearch)
        {
            this.detailSearch = detailSearch;
        }

        public String getXwd()
        {
            return xwd;
        }

        public void setXwd(String xwd)
        {
            this.xwd = xwd;
        }

        public String getDate()
        {
            return date;
        }

        public void setDate(String date)
        {
            this.date = date;
        }

        public String getStartDate()
        {
            return startDate;
        }

        public void setStartDate(String startDate)
        {
            this.startDate = startDate;
        }

        public String getEndDate()
        {
            return endDate;
        }

        public void setEndDate(String endDate)
        {
            this.endDate = endDate;
        }

        public String getFileExt()
        {
            return fileExt;
        }

        public void setFileExt(String fileExt)
        {
            this.fileExt = fileExt;
        }

        public String getYear()
        {
            return year;
        }

        public void setYear(String year)
        {
            this.year = year;
        }
    }
}
