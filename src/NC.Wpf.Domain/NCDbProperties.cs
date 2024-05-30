
namespace NC.Wpf.Domain
{
    public static class NCDbProperties
    {
        public static string DbTablePrefix { get; set; } = "NC";

        public static string? DbSchema { get; set; } = null;


        public const string ConnectionStringName = "Default"; // "NCService";
    }
}
