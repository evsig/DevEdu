using System;
using System.Text;

namespace DevEdu.Models.InputModels
{
    public class NewsInputModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublicationDate { get; set; }
        public int AuthorId { get; set; }
        public int? RecipientID { get; set; }
        public int? GroupID { get; set; }
    }
}
