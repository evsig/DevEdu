using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Common
{
    public static class ExistingRoles
    {
        public const string Teacher = "Преподаватель";
        public const string Student = "Студент";
        public const string Admin = "Админ";
        public const string HR = "HR";
        public const string HrOrTeacher = HR + "," + Teacher;
    }
}
