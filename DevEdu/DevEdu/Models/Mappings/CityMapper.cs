using DevEdu.Data;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class CityMapper
    {
        public static List<CityOutputModel> ToOutputModels(List<City> cities)
        {
            List<CityOutputModel> result = new List<CityOutputModel>();

            foreach (City city in cities)
            {
                result.Add(ToOutputModel(city));
            }

            return result;
        }

        public static CityOutputModel ToOutputModel(City city)
        {
            return new CityOutputModel
            {
                Id = (int)city.Id,
                Name = city.Name
            };
        }

        public static City ToDataModel(CityInputModel model)
        {
            return new City
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
