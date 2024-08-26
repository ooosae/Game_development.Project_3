using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [Space] 
        [Header("Player")] 
        [SerializeField] private float _playerSpeedIncreasePerPoint = 0.1f;
        [SerializeField] private float _playerMaxSpeed = 20.0f;
        [SerializeField] private float _playerMovementSpeed = 9;
        [SerializeField] private float _playerJumpForce = 300;
        [SerializeField] private float _playerTimeBeforeNextJump = 1.2f;
        [SerializeField] private float _playerTimeBeforeNextSlide = 1.2f;
        [SerializeField] private float _playerHorizontalSpeed = 1.5f;
        
        [Header("Defeat")] 
        [SerializeField] private float _defeatPanelDuration;

        // Player
        public float PlayerSpeedIncreasePerPoint => _playerSpeedIncreasePerPoint;
        public float PlayerMaxSpeed => _playerMaxSpeed;
        public float PlayerMovementSpeed => _playerMovementSpeed;
        public float PlayerJumpForce => _playerJumpForce;
        public float PlayerTimeBeforeNextJump => _playerTimeBeforeNextJump;
        public float PlayerTimeBeforeNextSlide => _playerTimeBeforeNextSlide;
        public float PlayerHorizontalSpeed => _playerHorizontalSpeed;
        
        // Defeat
        public float DefeatPanelDuration => _defeatPanelDuration;
    }
}
