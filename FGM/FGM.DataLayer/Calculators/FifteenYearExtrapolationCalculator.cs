using FGM.DataLayer.Interfaces;

namespace FGM.DataLayer.Services;

public class FifteenYearExtrapolationCalculator(IExtrapolationService service) : IYongerExtrapolationCalculator
{
    public decimal[] ExtrapolateYongerAgeGroups(decimal[] ageGroup)
    {
        var startingIndex = service.FindFirstFilledIndex(ageGroup);

        for (int i = startingIndex; i > 0; i--)
        {
            ageGroup[i - 1] = CalculateCellData(ageGroup[i], ageGroup[i + 1], ageGroup[i + 2], ageGroup[i + 3]);
        }
        return ageGroup;
    }

    private decimal CalculateCellData(decimal cellOne, decimal cellTwo, decimal cellThree, decimal cellFour)
    {
        return ((cellOne / cellTwo + cellTwo/ cellThree + cellThree / cellFour) / 3 ) * cellOne;
    }
}