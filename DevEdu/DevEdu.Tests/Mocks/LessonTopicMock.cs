using DevEdu.Data.Models;
using System;
using System.Collections.Generic;

namespace DevEdu.Tests.Mocks
{ 
 public class LessonTopicMock
{
    public static List<LessonTopic> lessonTopicMocks = new List<LessonTopic>()
        {
            new LessonTopic()
            {
                LessonId = 10,
                ThemeDetailsId = 20
            },
            new LessonTopic()
            {
                LessonId = 30,
                ThemeDetailsId = 40
            }
        };
 }
}


   

