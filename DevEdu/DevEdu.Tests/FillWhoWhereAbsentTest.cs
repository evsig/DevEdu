using System;
using System.Collections.Generic;
using System.Text;
using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace DevEdu.Tests
{
    [TestFixture]
    public class FillWhoWhereAbsentTest
    {
  //      #region
  //      LessonStorage lessonStorage;
  //      UserStorage userStorage;
  //      GroupStorage groupStorage;
  //      #endregion

  //      [Test]
  //      public async void FillWhoWereAbsentTest()
  //      {
  //          using IDbConnection connection = new SqlConnection(DBConnection.connString);
  //          connection.Open();
  //          IDbTransaction transaction = connection.BeginTransaction();
  //          lessonStorage = new LessonStorage(connection, transaction);
  //          userStorage = new UserStorage(connection, transaction);
  //          groupStorage = new GroupStorage(connection, transaction);

  //          try
  //          {
  //              List<Journal> journals = await SetUp();
  //              lessonStorage.FillWhoWhereAbsent(journals);
  //              bool res = true;
  //              List<Journal> actual = new List<Journal>();
  //              for (int i = 0; i < 4; i++)
  //              {
  //                  Journal journal = lessonStorage.GetJournalById((int)journals[i].Id);
  //                  actual.Add(journal);
  //              }
  //              foreach (var item in actual)
  //              {
  //                  if (item.IsAbsent == false) { res = false; }
  //              }
  //              Assert.IsTrue(res);
  //              TearDown(journals);

  //              transaction.Commit();
  //          }
  //          catch
  //          {
  //              transaction.Rollback();
  //              throw new System.Exception();
  //          }

  //      }
  //      #region
  //      List<User_Role> UserRoles { get; set; }
  //      List<StudentGroup> StudentGroups { get; set; }
  //      TeacherGroup Teacher_Group { get; set; }
  //      List<User> Users { get; set; }
  //      int GroupId { get; set; }
  //      #endregion
  //      #region SetUp
  //      public async Task<List<Journal>> SetUp()
  //      {
  //          Users = new List<User>();
  //          for (int i = 0; i < 5; i++)
  //          {
  //              User user = new User
  //              {
  //                  FirstName = "Гэндальф",
  //                  LastName = "Серый",
  //                  Patronymic = "Дворфович",
  //                  BirthDate = new DateTime(1799, 10, 10),
  //                  Email = "Test0@mail.ru",
  //                  Phone = "111",
  //                  Password = "111",
  //                  Login = "Gend",
  //                  Bio = "Gray",
  //                  CityId = 976
  //              };
  //              user.Id = await userStorage.UserAddOrUpdate(user);
  //              Users.Add(user);
  //          }

  //          UserRoles = new List<User_Role>();
  //          for (int i = 0; i < 5; i++)
  //          {
  //              int id = 0;
  //              if (i == 4) { id = 251; }
  //              else { id = 250; }
  //              User_Role role = new User_Role
  //              {
  //                  Id = null,
  //                  UserId = (int)Users[i].Id,
  //                  RoleId = id
  //              };
  //              role.Id = await userStorage.User_RoleAdd(role);
  //              UserRoles.Add(role);
  //          }

  //          Group group = new Group
  //          {
  //              StartDate = new DateTime(2019, 12, 4),
  //              EndDate = new DateTime(2020, 4, 4),
  //              TimeStart = new TimeSpan(15, 30, 0),
  //              Duration = 4,
  //              CourseProgramId = 935
  //          };
  //          GroupId = groupStorage.GroupAddOrUpdate(group).Result;

  //          StudentGroups = new List<StudentGroup>();
  //          for (int i = 0; i < 4; i++)
  //          {
  //              StudentGroup studentGroup = new StudentGroup
  //              {
  //                  UserId = 0,
  //                  GroupId = 0,
  //                  Rating = 3
  //              };
  //              StudentGroups.Add(studentGroup);
  //              StudentGroups[i].UserId = (int)Users[i].Id;
  //              StudentGroups[i].GroupId = GroupId;
  //              StudentGroups[i].Id = groupStorage.StudentGroupInsert(StudentGroups[i]).Id;
  //          }

  //          Teacher_Group = new TeacherGroup();
  //          Teacher_Group.UserId = (int)Users[4].Id;
  //          Teacher_Group.GroupId = GroupId;
  //          groupStorage.TeacherGroupInsert(Teacher_Group);

  //          Lesson lesson = new Lesson
  //          {
  //              GroupId = GroupId,
  //              Date = new DateTime(2020, 03, 11),
  //              Hometask = "отправиться в крестовый поход",
  //              Videos = "посмотреть на Иерусалим",
  //              ToRead = "инструкция: как карать божьим мечом, почитать про Deus Vult"
  //          };
  //        //  int lessonId = lessonStorage.AddOrUpdateLesson(lesson);
  ////          lesson.Id = lessonId;

  //          List<Journal> journals = new List<Journal>();
  //          for (int i = 0; i < 4; i++)
  //          {
  //              Journal journal = new Journal
  //              {
  //                  UserId = 0,
  //                  IsAbsent = true,
  //                  Feadback = "something",
  //                  AbsentReason = "something else",
  //                  Lesson = new Lesson()
  //              };
  //              journals.Add(journal);
  //              journals[i].Lesson = lesson;
  //       //       journals[i].Lesson.Id = lessonId;
  //              journals[i].UserId = (int)Users[i].Id;
  //              journals[i].IsAbsent = false;
  //             // journals[i].Id =  lessonStorage.AddOrUpdateJournal(journals[i]);
  //          }
  //          return journals;
  //      }
  //      #endregion
  //      #region
  //      public void TearDown(List<Journal> journals)
  //      {
  //          List<Journal> tempJournals = new List<Journal>();
  //          for (int i = 0; i < journals.Count; i++)
  //          {
  //              Journal journal = new Journal();
  //              tempJournals.Add(journal);
  //              tempJournals[i].Lesson = new Lesson();
  //              tempJournals[i].Lesson = journals[i].Lesson;
  //              tempJournals[i].UserId = journals[i].UserId;
  //          }//создала временный лист журналов, чтобы не потерять инфу из исходного после его удаления
  //          for (int i = 0; i < journals.Count; i++)
  //          {
  //              lessonStorage.DeleteJournal((int)journals[i].Id);
  //          }//удалила журналы
  //          lessonStorage.DeleteLesson((int)tempJournals[0].Lesson.Id);//удалила урок
  //          for (int i = 0; i < 4; i++)
  //          {
  //              groupStorage.StudentGroupDeleteById((int)StudentGroups[i].Id);
  //          }//удалила привязку студента к группе
  //        //  groupStorage.TeacherGroupDelete(Teacher_Group.GroupId, Teacher_Group.UserId);//удалила привязку препода к группе
  //          for (int i = 0; i < 5; i++)
  //          {
  //              userStorage.User_RoleDelete(UserRoles[i]);
  //          }//удалила студентов из таблицы юзер роли
  //          userStorage.User_RoleDelete(UserRoles[4]);//удалила роль у препода
  //          groupStorage.GroupDelete(GroupId);//удалила группу 
  //          for (int i = 0; i < 5; i++)
  //          {
  //              userStorage.UserDeleteById((int)Users[i].Id);//удалила юзеров
  //          }
  //      }
  //      #endregion
  //      [Test]
  //      public async void FillWhoWereAbsentTest()
  //      {
  //          List<Journal> journals = await SetUp();
  //          lessonStorage.FillWhoWhereAbsent(journals);
  //          bool res = true;
  //          List<Journal> actual = new List<Journal>();
  //          for (int i = 0; i < 4; i++)
  //          {
  //           //   Journal journal = lessonStorage.GetJournalById((int)journals[i].Id);
  //            //  actual.Add(journal);
  //          }
  //          foreach (var item in actual)
  //          {
  //              if (item.IsAbsent == false) { res = false; }
  //          }
  //          Assert.IsTrue(res);
  //          TearDown(journals);
  //      }
    }
}
