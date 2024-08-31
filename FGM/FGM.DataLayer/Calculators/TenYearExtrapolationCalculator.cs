using FGM.DataLayer.Interfaces;

namespace FGM.DataLayer.Services;

public class TenYearExtrapolationCalculator(IExtrapolationService service) : IYongerExtrapolationCalculator
{
    public decimal[] ExtrapolateYongerAgeGroups(decimal[] ageGroup)
    {
        var startingIndex = service.FindFirstFilledIndex(ageGroup);

        for (int i = startingIndex; i > 0; i--)
        {
            ageGroup[i - 1] = CalculateCellData(ageGroup[i], ageGroup[i + 1], ageGroup[i + 2]);
        }
        return ageGroup;
    }

    private decimal CalculateCellData(decimal cellOne, decimal cellTwo, decimal cellThree)
    {
        return (cellOne / cellTwo + cellTwo / cellThree) / 2 * cellOne;
    }
}