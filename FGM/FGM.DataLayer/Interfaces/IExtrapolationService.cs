namespace FGM.DataLayer.Interfaces;

public interface IExtrapolationService
{
    int FindFirstFilledIndex(decimal[] ageData);

    int FindLastFilledIndex(decimal[] ageData);
}