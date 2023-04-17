namespace GPSLib
{
    public class CoordinateConversion
    {
        private static readonly double Multiplier = (float)(180 / Math.Pow(2, 31));

        /*
        * A semicircle is a unit of location-based measurement on an arc. 
        * An arc of 180 degrees is made up of many semicircle units; 2^31 semicircles to be exact. 
        * A semicircle can be represented by a 32-bit number. This number can hold more information and is more accurate for GPS systems than a simple longitude and latitude value.
        * Semicircles that correspond to North latitudes and East longitudes are indicated with positive values; 
        * semicircles that correspond to South latitudes and West longitudes are indicated with negative values.
        * The following formula shows how to convert between degrees and semicircles:
        *   degrees = semicircles * ( 180 / 2^31 )
        */
        public static float SemicircleToDegrees(int? semiCircle)
        {
            if (semiCircle.HasValue)
            {
                // Calculate in double (15 digit precision) and cast to float (7 digit precision) to avoid rounding losses
                return (float)(semiCircle.Value * Multiplier);
            }

            return -1;
        }
    }
}
