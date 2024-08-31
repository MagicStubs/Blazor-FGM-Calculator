using FGM.DataLayer.Interfaces;

namespace FGM.DataLayer.Services;

public class FiveYearExtrapolationCalculator(IExtrapolationService service) : IYongerExtrapolationCalculator
{
    public decimal[] ExtrapolateYongerAgeGroups(decimal[] ageGroup)
    {
        var startingIndex = service.FindFirstFilledIndex(ageGroup);
        for (int i = startingIndex; i > 0 && i < ageGroup.Length - 1; i--)
        {
            ageGroup[i - 1] = CalculateCellData(ageGroup[i], ageGroup[i + 1]);
        }
        
        return ageGroup;
    }

    private decimal CalculateCellData(decimal cellOne, decimal cellTwo)
    {
        return (cellOne / cellTwo) * cellOne;
    }
}