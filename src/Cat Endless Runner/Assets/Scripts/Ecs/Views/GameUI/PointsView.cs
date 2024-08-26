using TMPro;
using UnityEngine;

namespace Ecs.Views.GameUI
{
    public class PointsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}