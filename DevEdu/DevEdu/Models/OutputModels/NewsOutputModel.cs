using System;
using System.Text;

namespace DevEdu.Models.OutputModels
{
    public class NewsOutputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublicationDate { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
