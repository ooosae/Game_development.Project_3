using Ecs.Services;
using UnityEngine;

namespace Ecs.Views
{
    public abstract class TileItemView : MonoBehaviour
    {
        public Pool<Transform> Pool { get; set; }
    }
}