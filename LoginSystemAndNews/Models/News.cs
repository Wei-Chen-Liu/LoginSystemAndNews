using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LoginSystemAndNews.Models.News
{
    public class News 
    {

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(4000)]
        [Description("新聞標題")]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(4000)]
        [Description("新聞類型")]
        public string Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(4000)]
        [Description("新聞內容")]
        public string Content { get; set; }

        public DateTime? Date { get; set; }

    }
}
