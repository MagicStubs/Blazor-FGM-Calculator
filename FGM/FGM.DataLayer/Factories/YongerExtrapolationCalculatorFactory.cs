using FGM.Data.Enums;
using FGM.DataLayer.Interfaces;

namespace FGM.Data.Factory;

public class YongerExtrapolationCalculatorFactory(IDictionary<YongerExtrapolationMethod, Func<IYongerExtrapolationCalculator>> calculators) : IYongerExtrapolationCalculatorFactorty
{
    public IYongerExtrapolationCalculator GetExtrapolationCalculator(YongerExtrapolationMethod method)
    {
        if (!calculators.TryGetValue(method, out var calculator) || calculator is null)
            throw new NotSupportedException($"No extrapolation calculator found for type {method}");

        return calculator();
    }
}