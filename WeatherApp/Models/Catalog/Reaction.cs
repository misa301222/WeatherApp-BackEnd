using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class Reaction
    {
        public Reaction(string email, int newsId, int reactionLike, int reactionWow, int reactionSad, int reactionAngry)
        {
            Email = email;
            NewsId = newsId;
            ReactionLike = reactionLike;
            ReactionWow = reactionWow;
            ReactionSad = reactionSad;
            ReactionAngry = reactionAngry;
        }

        [Key]
        public string Email { get; set; }
        [Key]
        public int NewsId { get; set; }
        public int ReactionLike { get; set; }
        public int ReactionWow { get; set; }
        public int ReactionSad { get; set; }
        public int ReactionAngry { get; set; }
    }
}
