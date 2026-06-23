using TMPro;
using UnityEngine;
namespace Game
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] SpeechBox _speechBox;
        public void ShowSpeech(bool show, string text = null) => _speechBox.Show(show, text);   
    }
}
