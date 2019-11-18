namespace Efrpg.Generators
{
    public class ResultSetResultReaderCommand
    {
        public int Index { get; }
        public string ReaderCommand { get; }
        public bool NotLastRecord { get; }
        public string WriteStoredProcReturnModelName { get; }

        public ResultSetResultReaderCommand(int index, string readerCommand, bool notLastRecord, string writeStoredProcReturnModelName)
        {
            Index                          = index;
            ReaderCommand                  = readerCommand;
            NotLastRecord                  = notLastRecord;
            WriteStoredProcReturnModelName = writeStoredProcReturnModelName;
        }
    }
}