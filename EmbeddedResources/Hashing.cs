using System.Linq;
using System.Collections.Generic;


namespace EmbeddedResources
{
    internal static class Hashing
    {
        /// <summary>
        /// Calculate hashcode on multiple values.
        /// </summary>
        /// <param name="arg">Hashcode calculation participant.</param>
        /// <returns>Hash code.</returns>
        public static int HashMany(params object[] arg)
        {
            const int hashSeed = 17;
            return arg.Aggregate(hashSeed, CombineHash);
        }

        private static int CombineHash(int acc, object obj)
        {
            var hash = obj != null
                ? obj.GetHashCode()
                : 0;

            unchecked
            {
                const int hashMultiplier = 23;
                return acc * hashMultiplier + hash;
            }
        }
    }
}
