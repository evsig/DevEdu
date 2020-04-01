using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    public static class StudentGroupMock
    {
        public static List<StudentGroup> listStudentGroup = new List<StudentGroup>
        {
           new StudentGroup()
           {
                UserId=0,
                GroupId=0,
                Rating = 3
           },
            new StudentGroup()
           {
                UserId=0,
                GroupId=0,
                Rating=3
           }

        };
        
    }
}
