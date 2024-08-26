using System;
using Ecs.LeaderBord.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Ecs.LeaderBord
{
    public class LeaderBordController : IEcsInitSystem
    {
        private EcsCustomInject<LeaderBordDataController> _leaderBordDataController;
        private EcsCustomInject<StartSceneData> _startSceneData;
        
        private LeaderBordView _leaderBordView;

        public void Init(IEcsSystems systems)
        {
            _leaderBordView = _startSceneData.Value.LeaderBordView;
            
            UpdateLeaderBord();
        }

        private void UpdateLeaderBord()
        {
            var leaderBordData = _leaderBordDataController.Value.GetLeaderBordData();
            _leaderBordView.DeactivateAll();
            if (leaderBordData.LeaderBordResults.Count > 3)
            {
                Debug.LogError("Leader board should be size 3 or less.");
            }
            
            for (int i = 0; i < leaderBordData.LeaderBordResults.Count; i++)
            {
                var date = new DateTime(leaderBordData.LeaderBordResults[i].Date);
                var value = leaderBordData.LeaderBordResults[i].Points.ToString();
                _leaderBordView.SetResultActive(i, true);
                _leaderBordView.SetResultValues(i, date.ToString(), value);
            }
        }
    }
}