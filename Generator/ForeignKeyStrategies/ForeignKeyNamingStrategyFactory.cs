using Efrpg.Filtering;

namespace Efrpg.ForeignKeyStrategies
{
    public static class ForeignKeyNamingStrategyFactory
    {
        public static IForeignKeyNamingStrategy Create(IDbContextFilter filter, Table table)
        {
            switch (Settings.ForeignKeyNamingStrategy)
            {
                case ForeignKeyNamingStrategy.Current:
                    return new LegacyForeignKeyNamingStrategy(filter, table);

                default:
                    return new LatestForeignKeyNamingStrategy(filter, table);
            }
        }
    }
}