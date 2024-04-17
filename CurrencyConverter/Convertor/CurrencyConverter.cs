namespace CurrencyConverter.Convertor;

public class CurrencyConverter : ICurrencyConverter
{
    private readonly object _lock = new();

    private IEnumerable<Tuple<string, string, double>> _conversionRates = new List<Tuple<string, string, double>>
    {
        new("USD", "CAD", 1.34),
        new("CAD", "GBP", 0.58),
        new("USD", "EUR", 0.86),
        new("GBP", "XFR", 0.26)
    };

    public void ClearConfiguration()
    {
        lock (_lock)
        {
            _conversionRates = new List<Tuple<string, string, double>>();
        }
    }

    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
    {
        lock (_lock)
        {
            _conversionRates = conversionRates;
        }
    }

    public double Convert(string fromCurrency, string toCurrency, double amount)
    {
        while (true)
        {
            var directRate = FindConversionRate(fromCurrency, toCurrency);
            if (directRate.HasValue)
            {
                return amount * directRate.Value;
            }

            var reverseRate = FindConversionRate(toCurrency, fromCurrency);
            if (reverseRate.HasValue)
            {
                return amount / reverseRate.Value;
            }

            var intermediateCurrency = FindIntermediateCurrency(fromCurrency, toCurrency);
            var intermediateAmount = Convert(fromCurrency, intermediateCurrency, amount);
            fromCurrency = intermediateCurrency;
            amount = intermediateAmount;
        }
    }

    private double? FindConversionRate(string fromCurrency, string toCurrency)
    {
        lock (_lock)
        {
            var rateTuple = _conversionRates.FirstOrDefault(tuple => tuple.Item1 == fromCurrency && tuple.Item2 == toCurrency);
            return rateTuple?.Item3;
        }
    }

    private string FindIntermediateCurrency(string fromCurrency, string toCurrency)
    {
        lock (_lock)
        {
            var intermediateTuple = _conversionRates.FirstOrDefault(tuple => tuple.Item2 == toCurrency && tuple.Item1 != fromCurrency);
            return intermediateTuple?.Item1;
        }
    }
}