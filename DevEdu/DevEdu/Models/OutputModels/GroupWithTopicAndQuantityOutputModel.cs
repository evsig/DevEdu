using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
	public class GroupWithTopicAndQuantityOutputModel
	{	public int GroupId { get; set; }
		public string Topic { get; set; }
		public int Quantity { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
	}
}
