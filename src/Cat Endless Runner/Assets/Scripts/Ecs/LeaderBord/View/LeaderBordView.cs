using System.Collections.Generic;
using UnityEngine;

namespace Ecs.LeaderBord.View
{
    public class LeaderBordView : MonoBehaviour
    {
        [SerializeField] private List<LeaderBordResult> _leaderBordRowViews;

        private void Awake()
        {
            if (_leaderBordRowViews.Count != 3)
            {
                Debug.LogError("Leader board rows count should be 3.");
            }
        }

        public void DeactivateAll()
        {
            _leaderBordRowViews.ForEach(x => x.gameObject.SetActive(false));
        }

        public void SetResultActive(int index, bool status)
        {
            if (!IsIndexValid(index))
            {
                return;
            }
            
            _leaderBordRowViews[index].gameObject.SetActive(status);
        }
        
        public void SetResultValues(int index, string date, string value)
        {
            if (!IsIndexValid(index))
            {
                return;
            }
            
            _leaderBordRowViews[index].SetDateText(date);
            _leaderBordRowViews[index].SetPointsText(value);
        }

        private bool IsIndexValid(int index)
        {
            return index >= 0 && index < _leaderBordRowViews.Count;
        }
    }
}