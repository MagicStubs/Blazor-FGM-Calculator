namespace FGM.DataLayer.Interfaces;

public interface IYongerExtrapolationCalculator
{
    decimal[] ExtrapolateYongerAgeGroups(decimal[] ageGroup);
}