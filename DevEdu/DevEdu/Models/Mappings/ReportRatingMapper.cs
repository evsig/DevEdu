using DevEdu.Data.Models;
using DevEdu.Models.InputModel;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class ReportRatingMapper
    {
        public static List<ReportRatingOutputModel> ToOutputModels(List<ReportRating> raitings)
        {
            List<ReportRatingOutputModel> result = new List<ReportRatingOutputModel>();

            foreach (ReportRating raiting in raitings)
            {
                result.Add(ToOutputModel(raiting));
            }

            return result;
        }

        public static ReportRatingOutputModel ToOutputModel(ReportRating currentRaiting)
        {
            return new ReportRatingOutputModel
            {
                CourseName = currentRaiting.CourseName,
                RatingByCourse = currentRaiting.RatingByCourse,
                CountOfStudents = currentRaiting.CountOfStudents 
            };
        }

        public static List<ReportRating> FromInputModels(List<ReportRatingInputModel> raitings)
        {
            List<ReportRating> result = new List<ReportRating>();

            foreach (ReportRatingInputModel raiting in raitings)
            {
                result.Add(FromInputModel(raiting));
            }

            return result;
        }

        public static ReportRating FromInputModel(ReportRatingInputModel currentRaiting)
        {
            if (currentRaiting.EndDate!=null&& currentRaiting.StartDate != null)
            {
                return new ReportRating
                {
                    StartDate = DateTime.Parse(currentRaiting.StartDate),
                    EndDate = DateTime.Parse(currentRaiting.EndDate)
                };
            }
            return null;
        }
    }
}
