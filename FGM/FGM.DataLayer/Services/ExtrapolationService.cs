using FGM.DataLayer.Interfaces;

namespace FGM.DataLayer.Services;

public class ExtrapolationService :IExtrapolationService
{
    public int FindFirstFilledIndex(decimal[] ageData)
    {
        int index;
        for (index = 0; index < ageData.Length; index++)
        {
            if (ageData[index] > 0)
                break;
        }

        return index;
    }

    public int FindLastFilledIndex(decimal[] ageData)
    {
        int index;
        for (index = ageData.Length - 1; index >= 0; index--)
        {
            if (ageData[index] > 0)
                break;
        }

        return index;
    }
}