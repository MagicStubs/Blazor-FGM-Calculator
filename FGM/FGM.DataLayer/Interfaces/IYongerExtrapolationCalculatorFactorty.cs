using FGM.Data.Enums;

namespace FGM.DataLayer.Interfaces;

public interface IYongerExtrapolationCalculatorFactorty
{
    IYongerExtrapolationCalculator GetExtrapolationCalculator(YongerExtrapolationMethod method);
}