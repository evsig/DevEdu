using System;
using System.Collections.Generic;
using DevEdu.Data.Models;

namespace DevEdu.Tests.Mocks
{
    class GroupMock
    {
        public static List<Group> groupMocks = new List<Group>()
        {
            new Group() {
                StartDate = new DateTime(2019, 12, 4),
                EndDate = new DateTime(2020, 4, 4),
                TimeStart = new TimeSpan(15, 30, 0),
                Duration = 4,
                CourseProgramId = 2
            },
            new Group() {
                StartDate = new DateTime(2020, 1, 5),
                EndDate = new DateTime(2020, 5, 5),
                TimeStart = new TimeSpan(15, 30, 0),
                Duration = 5,
                CourseProgramId = 3
            }
        };       
    }
}