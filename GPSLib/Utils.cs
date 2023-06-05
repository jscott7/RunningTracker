namespace GPSLib
{
    public class Utils
    {
        /// <summary>
        /// Get the minimum and maximum value from list of Latitude or Longitude data
        /// </summary>
        /// <param name="data">Latitude or Longitude</param>
        /// <returns>Minimum and Maximum value</returns>
        public static (float Min, float Max) GetMinMax(List<float> data)
        {
            float? min = null, max = null;

            foreach (var item in data)
            {
                if (!min.HasValue || item < min)
                {
                    min = item;
                }
                
                if (!max.HasValue || item > max)
                {
                    max = item;
                }
            }

            if (!min.HasValue || !max.HasValue)
            {
                throw new GpsException("Unable to determine max or minimum values from input data list");
            }

            return (min.Value, max.Value);
        }

        public static float GetMid(List<float> data)
        {
            var minMax = Utils.GetMinMax(data);
            return (minMax.Min + minMax.Max) / 2;
        }
    }
}
