using FGM.Data.Enums;

namespace FGM.DataLayer.Interfaces;

public interface IOlderExtrapolationCalculatorFactorty
{
    IOlderExtrapolationCalculator GetOlderExtrapolationCalculator(OlderExtrapolationMethod method);
}