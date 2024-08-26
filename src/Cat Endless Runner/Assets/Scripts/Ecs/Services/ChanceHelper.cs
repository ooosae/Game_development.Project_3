using System;

namespace Ecs.Services
{
    public class ChanceHelper
    {
        private readonly Random _random = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chance">0.0f - 100.0f</param>
        /// <returns></returns>
        public bool CalculateChance(float chance)
        {
            double randomValue = _random.NextDouble() * 100.0;
            return randomValue < chance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chance">0.0f - 100.0f</param>
        /// <param name="boosters">0.0f - 100.0f</param>
        /// <returns></returns>
        public bool CalculateChance(float chance, params float[] boosters)
        {
            double randomValue = _random.NextDouble() * 100.0;
            float boostedChance = chance;
            foreach (var booster in boosters)
            {
                boostedChance *= booster / 100.0f + 1.0f;
            }

            return randomValue < boostedChance;
        }
    }
}