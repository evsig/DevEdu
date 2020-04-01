using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class ThemeDetails
    {
        public ThemeDetails()
        {

        }

        public ThemeDetails(int programDetailId, string topic)
        {
            ProgramDetailId = programDetailId;
            Topic = topic;
        }
        public int? Id { get; set; }
        public int ProgramDetailId { get; set; }
        public string Topic { get; set; }


    }
}
