using System;

namespace com.konantech.search.data.OpenApiResultVO
{
    /// <summary>
    /// Summary description for OpenApiResultVO
    /// </summary>
    public class OpenApiResultVO
    {
        public OpenApiResultVO()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int total;

        private int rows;

        private String[] title;

        private String[] content;

        private DateTime[] pubDate;

        private String[] linkUrl;

        private String[] writer;

        private String[] thumbNail;

        private String[] sizeHeight;

        private String[] sizeWidth;

        
        // getter & setter
        public int getRows()
        {
            return rows;
        }

        public void setRows(int rows)
        {
            this.rows = rows;
        }

        public int getTotal()
        {
            return total;
        }

        public void setTotal(int total)
        {
            this.total = total;
        }

        public String[] getTitle()
        {
            return title;
        }

        public void setTitle(String[] title)
        {
            this.title = title;
        }

        public String[] getContent()
        {
            return content;
        }

        public void setContent(String[] content)
        {
            this.content = content;
        }

        public DateTime[] getPubDate()
        {
            return pubDate;
        }

        public void setPubDate(DateTime[] pubDate)
        {
            this.pubDate = pubDate;
        }

        public String[] getLinkUrl()
        {
            return linkUrl;
        }

        public void setLinkUrl(String[] linkUrl)
        {
            this.linkUrl = linkUrl;
        }

        public String[] getWriter()
        {
            return writer;
        }

        public void setWriter(String[] writer)
        {
            this.writer = writer;
        }

        public String[] getThumbNail()
        {
            return thumbNail;
        }

        public void setThumbNail(String[] thumbNail)
        {
            this.thumbNail = thumbNail;
        }

        public String[] getSizeHeight()
        {
            return sizeHeight;
        }

        public void setSizeHeight(String[] sizeHeight)
        {
            this.sizeHeight = sizeHeight;
        }

        public String[] getSizeWidth()
        {
            return sizeWidth;
        }

        public void setSizeWidth(String[] sizeWidth)
        {
            this.sizeWidth = sizeWidth;
        }
    }
}
