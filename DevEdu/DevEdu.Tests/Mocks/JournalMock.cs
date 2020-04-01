using DevEdu.Data.Models;
using System;
using System.Collections.Generic;

namespace DevEdu.Tests.Mocks
{
    public class JournalMock
    {
 
        public static List<Journal> journalMocks = new List<Journal>()
        {
            new Journal()
            {

                UserId = 1219,
                Lesson = LessonMock.lessonMocks[0],
                IsAbsent = true,
                Feadback = "something",
                AbsentReason = "something else",
            },
            new Journal()
            {
                UserId = 1219,
            Lesson = LessonMock.lessonMocks[1],
                IsAbsent = false,
                Feadback = "some",
                AbsentReason = "thing"
            }
        };
    }
}