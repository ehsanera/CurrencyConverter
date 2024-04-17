using Asp.Versioning;
using CurrencyConverter.Convertor;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controller;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly ICurrencyConverter _currencyConverter;

    public ConfigController(ICurrencyConverter currencyConverter)
    {
        _currencyConverter = currencyConverter;
    }

    [HttpDelete]
    public void ClearConfiguration()
    {
        _currencyConverter.ClearConfiguration();
    }

    [HttpPost]
    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
    {
        _currencyConverter.UpdateConfiguration(conversionRates);
    }

    [HttpGet]
    public double Convert(string fromCurrency, string toCurrency, double amount)
    {
        return _currencyConverter.Convert(fromCurrency, toCurrency, amount);
    }
}