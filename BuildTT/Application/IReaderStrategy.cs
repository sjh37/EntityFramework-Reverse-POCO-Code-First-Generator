namespace BuildTT.Application
{
    public interface IReaderStrategy
    {
        (bool success, string[] data) ReadInput(string inputSource);
    }
}