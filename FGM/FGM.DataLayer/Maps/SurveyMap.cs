using System.Globalization;
using FGM.DataLayer;

namespace FGM.Data.Maps;

using CsvHelper.Configuration;

public class SurveyMap : ClassMap<Survey>
{
    public SurveyMap()
    {
        // AutoMap will map all properties except those that are explicitly ignored
        Map(m => m.CountryName).Name("Country Name");
        Map(m => m.CountryCode).Name("Country Code");
        Map(m => m.YearOfSurvey).Name("Year Of Survey");
        Map(m => m.Name).Name("Survey Name");
        Map(m => m.FifteenToNineteen).Name("15-19");
        Map(m => m.TwentyToTwentyfour).Name("20-24");
        Map(m => m.TwentyfiveToTwentynine).Name("25-29");
        Map(m => m.ThirtyToThirtyfour).Name("30-34");
        Map(m => m.ThirtyfiveToThitrynine).Name("35-39");
        Map(m => m.FourtyToFourtyfour).Name("40-44");
        Map(m => m.FourtyfiveToFourtynine).Name("45-49");

        // Ignore the Id property since it's not present in the CSV
        Map(m => m.Id).Ignore();
    }
}
