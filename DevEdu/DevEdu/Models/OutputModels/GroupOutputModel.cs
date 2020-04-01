using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
	public class GroupOutputModel
	{
		public int GroupId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string TimeStartString { get; set; }

	}
}