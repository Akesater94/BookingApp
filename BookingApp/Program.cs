using System.Globalization;

namespace BookingApp;

internal class Program
{
    public async static Task Main(string[] args)
    {
        var cultureInfo = new CultureInfo("sv-SE");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        App app = new App();
        await app.Run();
    }
}
