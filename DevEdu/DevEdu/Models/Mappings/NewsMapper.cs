using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class NewsMapper
    {
        public static List<NewsOutputModel> ToOutputModels( List<News> newsList)
        {
            List<NewsOutputModel> result = new List<NewsOutputModel>();
            foreach (News news in newsList)
            {
                result.Add(ToOutputModel(news));
            }

            return result;
        }

        public static NewsOutputModel ToOutputModel(News news)
        {
            var result = new NewsOutputModel
            {
                Id = (int)news.Id,
                Title = news.Title,
                Content = news.Content,
                PublicationDate = news.PublicationDate.ToString(),
                AuthorName = news.User.FirstName,
                AuthorLastName = news.User.LastName
            };
            return result;
        }

        public static News ToDataModel(NewsInputModel news)
        {
            var result =  new News
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                PublicationDate = Convert.ToDateTime(news.PublicationDate),
                AuthorId = news.AuthorId,
                RecipientID = news.RecipientID,
                GroupID = news.GroupID
            };

            return result;
        }
    }
}
