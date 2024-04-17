namespace TestCurrencyConverter;

public class TestCurrencyConverter
{
    [TestFixture]
    public class CurrencyConverterTests
    {
        private CurrencyConverter.Convertor.CurrencyConverter _converter;

        [SetUp]
        public void Setup()
        {
            _converter = new CurrencyConverter.Convertor.CurrencyConverter();
        }

        [Test]
        public void Convert_USD_to_CAD()
        {
            double amount = 100;
            double expected = amount * 1.34;

            double result = _converter.Convert("USD", "CAD", amount);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Convert_CAD_to_GBP()
        {
            double amount = 100;
            double expected = amount * 0.58;

            double result = _converter.Convert("CAD", "GBP", amount);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Convert_USD_to_EUR()
        {
            double amount = 100;
            double expected = amount * 0.86;

            double result = _converter.Convert("USD", "EUR", amount);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Convert_EUR_to_USD()
        {
            double amount = 100;
            double expected = amount / 0.86;

            double result = _converter.Convert("EUR", "USD", amount);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Convert_CAD_to_EUR()
        {
            double amount = 100;
            double expected = amount / 1.34 * 0.86;

            double result = _converter.Convert("CAD", "EUR", amount);

            Assert.That(result, Is.EqualTo(expected));
        }
        
        [Test]
        public void Convert_USD_to_GBP()
        {
            double amount = 100;
            double expected = amount * 1.34 * 0.58;

            double result = _converter.Convert("USD", "GBP", amount);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}