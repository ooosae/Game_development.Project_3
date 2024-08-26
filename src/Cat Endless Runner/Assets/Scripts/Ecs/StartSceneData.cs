using Ecs.LeaderBord.View;
using Ecs.Services;
using UnityEngine;

namespace Ecs
{
    public class StartSceneData : MonoBehaviour
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private LeaderBordView _leaderBordView;

        public LeaderBordView LeaderBordView => _leaderBordView;
        public CoroutineRunner CoroutineRunner => _coroutineRunner;
    }
}