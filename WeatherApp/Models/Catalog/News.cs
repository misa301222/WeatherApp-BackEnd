using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class News
    {
        public News(int newsId, string header, string body, DateTime publishedDate, string imageURL, int notificationType, int reactionLike, int reactionWow, int reactionSad, int reactionAngry)
        {
            NewsId = newsId;
            Header = header;
            Body = body;
            PublishedDate = publishedDate;
            ImageURL = imageURL;
            NotificationType = notificationType;
            ReactionLike = reactionLike;
            ReactionWow = reactionWow;
            ReactionSad = reactionSad;
            ReactionAngry = reactionAngry;
        }
        [Key]
        public int NewsId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageURL { get; set; }
        public int NotificationType { get; set; }
        public int ReactionLike { get; set; }
        public int ReactionWow { get; set; }
        public int ReactionSad { get; set; }
        public int ReactionAngry { get; set; }
    }
}
