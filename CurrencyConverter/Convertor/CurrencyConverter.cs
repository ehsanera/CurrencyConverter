namespace CurrencyConverter.Convertor;

public class CurrencyConverter : ICurrencyConverter
{
    private IEnumerable<Tuple<string, string, double>> _conversionRates = new List<Tuple<string, string, double>>
    {
        new("USD", "CAD", 1.34),
        new("CAD", "GBP", 0.58),
        new("USD", "EUR", 0.86)
    };

    public void ClearConfiguration()
    {
        _conversionRates = new List<Tuple<string, string, double>>();
    }

    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
    {
        _conversionRates = conversionRates;
    }

    public double Convert(string fromCurrency, string toCurrency, double amount)
    {
        var straight = _conversionRates.FirstOrDefault(tuple => tuple.Item1 == fromCurrency && tuple.Item2 == toCurrency);
        var reverseStraight = _conversionRates.FirstOrDefault(tuple => tuple.Item1 == toCurrency && tuple.Item2 == fromCurrency);

        if (straight != null)
        {
            return amount * straight.Item3;
        }

        if (reverseStraight != null)
        {
            return amount / reverseStraight.Item3;
        }

        var first = Convert(fromCurrency, "USD", amount);
        return Convert("USD", toCurrency, first);
    }
}