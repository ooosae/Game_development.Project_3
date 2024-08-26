using Ecs.Services;
using UnityEngine;

namespace Ecs
{
    public class DefeatSceneData : MonoBehaviour
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        public CoroutineRunner CoroutineRunner => _coroutineRunner;
    }
}