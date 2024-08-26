using System;
using UnityEngine;

namespace Ecs.LeaderBord.Data
{
    [Serializable]
    public class LeaderBordResult
    {
        [SerializeField] public long Date;
        [SerializeField] public int Points;
    }
}
