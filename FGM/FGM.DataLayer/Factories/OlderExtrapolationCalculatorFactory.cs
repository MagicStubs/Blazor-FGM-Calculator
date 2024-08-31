using FGM.Data.Enums;
using FGM.DataLayer.Interfaces;

namespace FGM.Data.Factory;

public class OlderExtrapolationCalculatorFactory(IDictionary<OlderExtrapolationMethod, Func<IOlderExtrapolationCalculator>> calculators) : IOlderExtrapolationCalculatorFactorty
{
    public IOlderExtrapolationCalculator GetOlderExtrapolationCalculator(OlderExtrapolationMethod method)
    {
        if (!calculators.TryGetValue(method, out var calculator) || calculator is null)
            throw new NotSupportedException($"No extrapolation calculator found for type {method}");

        return calculator();
    }
}