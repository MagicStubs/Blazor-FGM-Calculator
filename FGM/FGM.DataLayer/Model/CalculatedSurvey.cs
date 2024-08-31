using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;

namespace FGM.DataLayer;

public class CalculatedSurvey
{
    [Ignore]
    public int Id { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
    public string Name { get; set; }
    public int YearOfSurvey { get; set; }
    public decimal ZeroToFour { get; set; }
    public decimal FiveToNine { get; set; }
    public decimal TenToForteen { get; set; }
    public decimal FifteenToNineteen { get; set; }
    public decimal TwentyToTwentyfour { get; set; }
    public decimal TwentyfiveToTwentynine { get; set; }
    public decimal ThirtyToThirtyfour { get; set; }
    public decimal ThirtyfiveToThitrynine { get; set; }
    public decimal FourtyToFourtyfour { get; set; }
    public decimal FourtyfiveToFourtynine { get; set; }
    public decimal FifteyToFifteyfour { get; set; }
    public decimal FifityfiveToFiftynine { get; set; }
    public decimal SixtyToSixtyfour { get; set; }
    public decimal SixtyfiveToSixtynine { get; set; }
    public decimal SeventyToSeventyfour { get; set; }
    public decimal SeventyfiveToSeventynine { get; set; }

    [NotMapped]
    public double[] GraphData
    {
        get
        {
            PopulateTempData();
            return TempAgeData.Reverse().Select(Convert.ToDouble).ToArray();
        }
    }
    public decimal EightyPlus { get; set; }
    public decimal[] TempAgeData = new decimal[17];
    public Survey? OrigonalSurvey;

    public void SaveTempAgeData()
    {
        this.ZeroToFour = TempAgeData[0];
        this.FiveToNine = TempAgeData[1];
        this.TenToForteen = TempAgeData[2];
        this.FifteenToNineteen = TempAgeData[3];
        this.TwentyToTwentyfour = TempAgeData[4];
        this.TwentyfiveToTwentynine = TempAgeData[5];
        this.ThirtyToThirtyfour = TempAgeData[6];
        this.ThirtyfiveToThitrynine = TempAgeData[7];
        this.FourtyToFourtyfour = TempAgeData[8];
        this.FourtyfiveToFourtynine = TempAgeData[9];
        this.FifteyToFifteyfour = TempAgeData[10];
        this.FifityfiveToFiftynine = TempAgeData[11];
        this.SixtyToSixtyfour = TempAgeData[12];
        this.SixtyfiveToSixtynine = TempAgeData[13];
        this.SeventyToSeventyfour = TempAgeData[14];
        this.SeventyfiveToSeventynine = TempAgeData[15];
        this.EightyPlus = TempAgeData[16];
    }

    public void PopulateTempData()
    {
        TempAgeData[0] = this.ZeroToFour;
        TempAgeData[1] = this.FiveToNine  ;
        TempAgeData[2] = this.TenToForteen;
        TempAgeData[3] = this.FifteenToNineteen;
        TempAgeData[4] = this.TwentyToTwentyfour;
        TempAgeData[5] = this.TwentyfiveToTwentynine ;
        TempAgeData[6] = this.ThirtyToThirtyfour;
        TempAgeData[7] = this.ThirtyfiveToThitrynine;
        TempAgeData[8] = this.FourtyToFourtyfour;
        TempAgeData[9] = this.FourtyfiveToFourtynine;
        TempAgeData[10] = this.FifteyToFifteyfour;
        TempAgeData[11] = this.FifityfiveToFiftynine;
        TempAgeData[12] = this.SixtyToSixtyfour;
        TempAgeData[13] = this.SixtyfiveToSixtynine;
        TempAgeData[14] = this.SeventyToSeventyfour;
        TempAgeData[15] = this.SeventyfiveToSeventynine;
        TempAgeData[16] = this.EightyPlus;
    }
}