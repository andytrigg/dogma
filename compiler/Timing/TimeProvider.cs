using System;

namespace dogma.Timing
{
    public interface ITimeProvider
    {
        long NowAsUnixTimeMilliseconds();
        float ElapsedTimeSinceInSeconds(long startTimeMilliseconds);
    }

    public class TimeProvider : ITimeProvider
    {
        public long NowAsUnixTimeMilliseconds()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public float ElapsedTimeSinceInSeconds(long startTimeMilliseconds)
        {
            return NowAsUnixTimeMilliseconds() - startTimeMilliseconds / 1000f;
        }
    }
}