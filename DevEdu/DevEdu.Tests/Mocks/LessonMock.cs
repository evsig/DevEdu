using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    public class LessonMock
    {
        public static List<Lesson> lessonMocks = new List<Lesson>()
    {
        new Lesson()
        {

            GroupId = 1,
            Date = new DateTime(2020, 03, 11),
            Hometask = "отправиться в крестовый поход",
            Videos = "посмотреть на Иерусалим",
            ToRead = "инструкция: как карать божьим мечом, почитать про Deus Vult"
        },

        new Lesson()
        {

            GroupId = 2,
            Date = new DateTime(2020, 03, 24),
            Hometask = "украсть кольцо у Голлума",
            Videos = "заглянуть в око Саурона",
            ToRead = "прочесть карту Средиземья"
        },
    };

    }
}
