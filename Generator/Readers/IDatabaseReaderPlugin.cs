using System.Collections.Generic;
using Efrpg.LanguageMapping;

namespace Efrpg.Readers
{
    public interface IDatabaseReaderPlugin
    {
        List<RawTable>            ReadTables();
        List<RawForeignKey>       ReadForeignKeys();
        List<RawIndex>            ReadIndexes();
        List<RawExtendedProperty> ReadExtendedProperties();
        List<RawStoredProcedure>  ReadStoredProcs();
        List<RawSequence>         ReadSequences();

        IDatabaseToPropertyType GetDatabaseToPropertyTypeMapping();
    }
}