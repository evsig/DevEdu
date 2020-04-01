using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class GroupWithTopicAndQuantity
    {
		public int GroupId { get; set; }
		public string Topic { get; set; }
		public int Quantity { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
