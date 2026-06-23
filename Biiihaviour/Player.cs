using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [Inject] SpeechBoxManager _speechBoxManager;
        [Inject] CharacterManager _characterManager;
        [Inject] GameInputManager _gameInputManager;    

        public float avatarScale = 1f;
        [Header("REFERENCES")]
        public PlayerControl playerControl;
        public PlayerUI playerUI;
        public GameObject lamp;


        [HideInInspector] public UnityAction nextAction;
        private bool _pendingInvokeNextAction = false;

        private void Start()
        {
            _speechBoxManager.ShowSpeech(false);
            _characterManager.RegisterCharacter(new CharacterData { id = name, name = name, bindInstance = gameObject });
            _gameInputManager.OnWorldPositionClicked.AddListener();
        }
        public void ShowLamp(bool show)
        {
            lamp.SetActive(show);   
        }
        public void ShowSpeech(bool show, string text = null) => _speechBoxManager.ShowSpeech(show, transform, text);
        public void AssignNextAction(UnityAction unityAction)
        {
            nextAction = unityAction;
            if (_pendingInvokeNextAction && nextAction != null)
            {
                nextAction.Invoke();
                nextAction = null;
                _pendingInvokeNextAction = false;
            }
        }
        public bool InvokePendingAction()
        {
            if (nextAction != null)
            {
                nextAction.Invoke();
                nextAction = null;
                return true;
            }
            return false;
        }
        public void InvokeNextAction()
        {
            if (nextAction != null)
            {
                nextAction.Invoke();
                nextAction = null;
            }
            else
            {
                _pendingInvokeNextAction = true;
            }
        }
        public void CancleNextAction()
        {
            nextAction = null;
            _pendingInvokeNextAction = false;
        }
    }
}