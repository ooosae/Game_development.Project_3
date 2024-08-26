using System.Collections.Generic;
using UnityEngine;

namespace Ecs.Extensions
{
    public static class ListExtension
    {
        public static T GetRandomValue<T>(this List<T> list)
        {
            var randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}