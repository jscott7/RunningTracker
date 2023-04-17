namespace GPSLib
{
    public class Utils
    {
        /// <summary>
        /// Get the minimum and maximum value from list of Latitude or Longitude data
        /// </summary>
        /// <param name="data">Latitude or Longitude</param>
        /// <returns>Minimum and Maximum value or null</returns>
        public static (float? Min, float? Max) GetMinMax(List<float> data)
        {
            float? min = null, max = null;

            foreach (var item in data)
            {
                if (!min.HasValue || item < min)
                {
                    min = item;
                }
                else if (!max.HasValue || item > max)
                {
                    max = item;
                }
            }

            return (min, max);
        }
    }
}
