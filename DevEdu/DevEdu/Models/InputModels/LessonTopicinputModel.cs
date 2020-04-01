using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class LessonTopicInputModel
    {
        public int? Id { get; set; }
        public int LessonId { get; set; }
        public int ThemeDetailsId { get; set; }
    }
}
