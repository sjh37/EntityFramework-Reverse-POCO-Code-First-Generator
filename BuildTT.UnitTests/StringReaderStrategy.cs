using BuildTT.Application;

namespace BuildTT.UnitTests
{
    public class StringReaderStrategy : IReaderStrategy
    {
        private readonly string[] _data;

        public StringReaderStrategy(string[] data)
        {
            _data = data;
        }

        public (bool success, string[] data) ReadInput(string inputSource)
        {
            return (true, _data);
        }
    }
}