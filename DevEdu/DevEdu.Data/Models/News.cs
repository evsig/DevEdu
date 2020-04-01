using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class News : ICloneable
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int AuthorId { get; set; }
        public int? RecipientID { get; set; }
        public int? GroupID { get; set; }
        public User User { get; set; }

        public object Clone()
        {
            return new News
            {
                AuthorId = AuthorId,
                Content = Content,
                GroupID = GroupID,
                PublicationDate = PublicationDate,
                RecipientID = RecipientID,
                Title = Title,
                User = User
            };
        }

        public override bool Equals(object obj)
        {
            return obj is News news &&
                   Id == news.Id &&
                   Title == news.Title &&
                   Content == news.Content &&
                   PublicationDate == news.PublicationDate &&
                   AuthorId == news.AuthorId &&
                   RecipientID == news.RecipientID &&
                   GroupID == news.GroupID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Content, PublicationDate, AuthorId, RecipientID, GroupID);
        }

        public override string ToString()
        {
            string news = "{";
            news = news + "\n\tId: " + Id;
            news = news + "\n\tTitle: " + Title;
            news = news + "\n\tContent: " + Content;
            news = news + "\n\tPublicationDate: " + PublicationDate;
            news = news + "\n\tAuthorId: " + AuthorId;
            news = news + "\n\tRecipientID: " + RecipientID;
            news = news + "\n\tGroupID: " + GroupID;
            news = news + "\n}";
            return news;
        }
    }
}
