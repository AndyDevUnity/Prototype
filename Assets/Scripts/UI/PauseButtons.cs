using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseButtons : MonoBehaviour
    {
        [SerializeField]
        private Button resumeGameButton;

        [SerializeField]
        private Button offSoundButton;

        [SerializeField]
        private Button onSoundButton;

        [SerializeField]
        private Button helpButton;

        [SerializeField]
        private Button closeHelpCanvas;

        [SerializeField]
        private Button exitGameButton;

        [SerializeField]
        private Canvas gamePauseCanvas;

        [SerializeField]
        private Canvas helpCanvas;

        private AudioManager audioManager;

        private float minVolumeValue = 0f;
        private float defaultVolumeValue = 0.15f;

        private void OnEnable()
        {
            audioManager = GameManager.Instance.AudioManager;

            resumeGameButton.onClick.AddListener(OnResumeButton);
            offSoundButton.onClick.AddListener(() => audioManager.SetVolumeValue(minVolumeValue));
            onSoundButton.onClick.AddListener(() => audioManager.SetVolumeValue(defaultVolumeValue));
            helpButton.onClick.AddListener(() => helpCanvas.gameObject.SetActive(true));
            closeHelpCanvas.onClick.AddListener(() => helpCanvas.gameObject.SetActive(false));
            exitGameButton.onClick.AddListener(() => GameManager.Instance.PUNConnection.LeftRoom());
        }

        private void OnDisable()
        {
            resumeGameButton.onClick.RemoveAllListeners();
            offSoundButton.onClick.RemoveAllListeners();
            helpButton.onClick.RemoveAllListeners();
            exitGameButton.onClick.RemoveAllListeners();
        }

        private void OnResumeButton()
        {
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
}
