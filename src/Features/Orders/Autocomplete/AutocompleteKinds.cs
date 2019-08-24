using System.ComponentModel;

namespace Orders.Autocomplete
{
    public enum AutocompleteKinds:long
    {
        [Description("Бренд")]
        DeviceTrademark = 1,

        [Description("Комплектация")]
        DeviceOptions = 2,

        [Description("Внешний вид")]
        DeviceAppearance = 3,
    }
}
