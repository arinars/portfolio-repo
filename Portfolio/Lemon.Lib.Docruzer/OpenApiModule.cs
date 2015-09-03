using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Configuration;
using com.konantech.search.data.OpenApiResultVO;

namespace com.konantech.search.module.OpenApiModule
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenApiModule
    {
        XmlDocument doc = new XmlDocument();
        
        public String naverApiKey = "a1d35eefe09bc946cfdc92887900724b";//ConfigurationManager.AppSettings["NAVER_API_KEY"];

        public OpenApiModule()
        {
            //
            // TODO: 여기에 생성자 논리를 추가합니다.
            //
        }

        /**
         * 네이버 뉴스 검색 메소드
         * 
         * @param kwd(키워드)
         * @param pageNum(시작 페이지)
         * @param pageSize(화면에 보여질 레코드 개수)
         * @param rsb(데이터를 담을 Value Object)
         * 
         * @return status(상태값 양수면 이상 없음)
         */
        public int SearchNaverNews(String kwd, int pageNum, int pageSize, OpenApiResultVO rsb)
        {
            int status = 0;
            int start = (pageNum * pageSize) - (pageSize-1);

            String url = "http://openapi.naver.com/search?key=" + naverApiKey + "&query=" + kwd +
            "&target=news&start=" + start + "&display=" + pageSize;

            // 네이버 결과값
            int total = 0;     // 문서 총 개수
            int row = 0;        // 현재 보여주는 문서 개수            
            String[] title;     // 제목
            String[] content;   // 내용
            DateTime[] pubDate; // 작성일
            String[] linkUrl;   // 링크

            int i = 0;      // 배열 처리를 위한 변수
            
            doc.Load(url);

            XmlNode node = doc.SelectSingleNode("rss");
            XmlNode n = node.SelectSingleNode("channel");

            total = Convert.ToInt32(n.SelectSingleNode("total").InnerText);
            row = Convert.ToInt16(n.SelectSingleNode("display").InnerText);

            rsb.setRows(row);

            if (total > 500)
                total = 500;

            rsb.setTotal(total);

            if (row > 0)
            {
                title = new String[row];
                content = new String[row];
                pubDate = new DateTime[row];
                linkUrl = new String[row];

                foreach (XmlNode el in n.SelectNodes("item"))
                {
                    title[i] = el.SelectSingleNode("title").InnerText;
                    content[i] = el.SelectSingleNode("description").InnerText;
                    pubDate[i] = Convert.ToDateTime(el.SelectSingleNode("pubDate").InnerText);                    
                    linkUrl[i] = el.SelectSingleNode("link").InnerText;
                    i++;
                }

                rsb.setTitle(title);
                rsb.setContent(content);
                rsb.setPubDate(pubDate);
                rsb.setLinkUrl(linkUrl);
            }           

            return status;
        }

        /**
         * 네이버 전문자료 검색 메소드
         * 
         * @param kwd(키워드)
         * @param pageNum(시작 페이지)
         * @param pageSize(화면에 보여질 레코드 개수)
         * @param rsb(데이터를 담을 Value Object)
         * 
         * @return status(상태값 양수면 이상 없음)
         */
        public int SearchNaverDoc(String kwd, int pageNum, int pageSize, OpenApiResultVO rsb)
        {
            int status = 0;
            int start = (pageNum * pageSize) - (pageSize - 1);

            String url = "http://openapi.naver.com/search?key=" + naverApiKey + "&query=" + kwd +
            "&target=doc&start=" + start + "&display=" + pageSize;

            // 네이버 결과값
            int total = 0;     // 문서 총 개수
            int row = 0;        // 현재 보여주는 문서 개수            
            String[] title;     // 제목
            String[] content;   // 내용            
            String[] linkUrl;   // 링크

            int i = 0;      // 배열 처리를 위한 변수

            doc.Load(url);

            XmlNode node = doc.SelectSingleNode("rss");
            XmlNode n = node.SelectSingleNode("channel");

            total = Convert.ToInt32(n.SelectSingleNode("total").InnerText);
            row = Convert.ToInt16(n.SelectSingleNode("display").InnerText);

            rsb.setRows(row);

            if (total > 500)
                total = 500;

            rsb.setTotal(total);

            if (row > 0)
            {
                title = new String[row];
                content = new String[row];
                linkUrl = new String[row];

                foreach (XmlNode el in n.SelectNodes("item"))
                {
                    title[i] = el.SelectSingleNode("title").InnerText;
                    content[i] = el.SelectSingleNode("description").InnerText;                    
                    linkUrl[i] = el.SelectSingleNode("link").InnerText;
                    i++;
                }

                rsb.setTitle(title);
                rsb.setContent(content);                
                rsb.setLinkUrl(linkUrl);
            }

            return status;
        }

        /**
         * 네이버 블로그 검색 메소드
         * 
         * @param kwd(키워드)
         * @param pageNum(시작 페이지)
         * @param pageSize(화면에 보여질 레코드 개수)
         * @param rsb(데이터를 담을 Value Object)
         * 
         * @return status(상태값 양수면 이상 없음)
         */
        public int SearchNaverBlog(String kwd, int pageNum, int pageSize, String sortStr, OpenApiResultVO rsb)
        {
            int status = 0;
            int start = (pageNum * pageSize) - (pageSize - 1);
            String sort = "sim";

            if (sortStr.Equals("d"))
            {
                sort = "date";
            }

            String url = "http://openapi.naver.com/search?key=" + naverApiKey + "&query=" + kwd +
            "&target=blog&start=" + start + "&display=" + pageSize + "&sort=" + sort;

            // 네이버 결과값
            int total = 0;      // 문서 총 개수
            int row = 0;        // 현재 보여주는 문서 개수            
            String[] title;     // 제목
            String[] content;   // 내용            
            String[] writer;    // 작성자
            String[] linkUrl;   // 링크

            int i = 0;      // 배열 처리를 위한 변수

            doc.Load(url);

            XmlNode node = doc.SelectSingleNode("rss");
            XmlNode n = node.SelectSingleNode("channel");

            total = Convert.ToInt32(n.SelectSingleNode("total").InnerText);
            row = Convert.ToInt16(n.SelectSingleNode("display").InnerText);

            rsb.setRows(row);

            if (total > 500)
                total = 500;

            rsb.setTotal(total);

            if (row > 0)
            {
                title = new String[row];
                content = new String[row];
                linkUrl = new String[row];
                writer = new String[row];

                foreach (XmlNode el in n.SelectNodes("item"))
                {
                    title[i] = el.SelectSingleNode("title").InnerText;
                    content[i] = el.SelectSingleNode("description").InnerText;
                    linkUrl[i] = el.SelectSingleNode("link").InnerText;
                    writer[i] = el.SelectSingleNode("bloggername").InnerText;
                    i++;
                }

                rsb.setTitle(title);
                rsb.setContent(content);
                rsb.setLinkUrl(linkUrl);
                rsb.setWriter(writer);
            }

            return status;
        }

        /**
         * 네이버 이미지 검색 메소드
         * 
         * @param kwd(키워드)
         * @param pageNum(시작 페이지)
         * @param pageSize(화면에 보여질 레코드 개수)
         * @param rsb(데이터를 담을 Value Object)
         * 
         * @return status(상태값 양수면 이상 없음)
         */
        public int SearchNaverImage(String kwd, int pageNum, int pageSize, OpenApiResultVO rsb)
        {
            int status = 0;
            int start = (pageNum * pageSize) - (pageSize - 1);
            
            String url = "http://openapi.naver.com/search?key=" + naverApiKey + "&query=" + kwd +
            "&target=image&start=" + start + "&display=" + pageSize;

            // 네이버 결과값
            int total = 0;       // 문서 총 개수
            int row = 0;         // 현재 보여주는 문서 개수            
            String[] title;      // 제목            
            String[] linkUrl;    // 링크
            String[] thumbNail;  // 썸네일
            String[] sizeHeight; // 높이
            String[] sizeWidth;  // 넓이

            int i = 0;      // 배열 처리를 위한 변수

            doc.Load(url);

            XmlNode node = doc.SelectSingleNode("rss");
            XmlNode n = node.SelectSingleNode("channel");

            total = Convert.ToInt32(n.SelectSingleNode("total").InnerText);
            row = Convert.ToInt16(n.SelectSingleNode("display").InnerText);

            rsb.setRows(row);

            if (total > 500)
                total = 500;

            rsb.setTotal(total);

            if (row > 0)
            {
                title = new String[row];
                linkUrl = new String[row];
                thumbNail = new String[row];
                sizeHeight = new String[row];
                sizeWidth = new String[row];

                foreach (XmlNode el in n.SelectNodes("item"))
                {
                    title[i] = el.SelectSingleNode("title").InnerText;
                    linkUrl[i] = el.SelectSingleNode("link").InnerText;
                    thumbNail[i] = el.SelectSingleNode("thumbnail").InnerText;
                    sizeHeight[i] = el.SelectSingleNode("sizeheight").InnerText;
                    sizeWidth[i] = el.SelectSingleNode("sizewidth").InnerText;
                    i++;
                }

                rsb.setTitle(title);
                rsb.setLinkUrl(linkUrl);
                rsb.setThumbNail(thumbNail);
                rsb.setSizeHeight(sizeHeight);
                rsb.setSizeWidth(sizeWidth);
            }

            return status;
        }
    }
}