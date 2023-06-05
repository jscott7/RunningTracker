namespace GPSLib
{
    public class GpsException : Exception
    {
        public GpsException() { }

        public GpsException(string message)
        : base(message)
        {
        }

        public GpsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
