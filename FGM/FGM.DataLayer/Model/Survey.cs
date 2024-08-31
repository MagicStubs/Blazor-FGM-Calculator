using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FGM.DataLayer;

public class Survey
{
    public int Id { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
    public string Name { get; set; }
    public int YearOfSurvey { get; set; }
    public decimal FifteenToNineteen { get; set; }
    public decimal TwentyToTwentyfour { get; set; }
    public decimal TwentyfiveToTwentynine { get; set; }
    public decimal ThirtyToThirtyfour { get; set; }
    public decimal ThirtyfiveToThitrynine { get; set; }
    public decimal FourtyToFourtyfour { get; set; }
    public decimal FourtyfiveToFourtynine { get; set; }
}