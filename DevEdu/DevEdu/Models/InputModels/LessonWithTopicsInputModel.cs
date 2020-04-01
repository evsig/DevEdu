using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
	public class LessonWithTopicsInputModel
	{
		public int LessonId { get; set; }
		public int GroupId { get; set; }
		public int TeacherId { get; set; }
		public List<int> ThemeDetailsIds { get; set; }
	}
}
