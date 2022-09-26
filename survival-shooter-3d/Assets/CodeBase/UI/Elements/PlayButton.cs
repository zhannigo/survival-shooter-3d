using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{ 
    public class PlayButton : MonoBehaviour
    {
        public Button _button;
        private string _sceneName;
        private GameStateMachine _stateMachine;
        
        public void Construct(string sceneName, GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _sceneName = sceneName;
        }
        private void Start() => 
            _button.onClick.AddListener(LoadGame);
        private void LoadGame() => 
            _stateMachine.Enter<LevelLoadState, string>(_sceneName);
        
    }
}

