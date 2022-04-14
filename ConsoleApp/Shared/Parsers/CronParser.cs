namespace Shared.Parsers
{
    public static class CronParser
    {
        public static int ParseCronToHours(string currentCron)
        {
            switch (currentCron)
            {
                case "0 */1 * * *": return 1;
                case "0 */3 * * *": return 3;
                case "0 */12 * * *": return 12;
                case "0 */24 * * *": return 24;
                default: return 1;
            }
        }

        public static string ParseHoursToCron(int hours)
        {
            switch (hours)
            {
                case 1: return "0 */1 * * *";
                case 3: return "0 */3 * * *";
                case 12: return "0 */12 * * *";
                case 24: return "0 */24 * * *";
                default: return "0 */1 * * *";
            }
        }
    }
}
