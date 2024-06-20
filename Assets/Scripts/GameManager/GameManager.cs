using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public AudioManager AudioManager { get; private set; }

        public PUNConnection PUNConnection { get; private set; }

        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            FindManagers();
        }

        private void FindManagers()
        {
            AudioManager = GetComponentInChildren<AudioManager>();
            AudioManager.Construct();

            PUNConnection = GetComponentInChildren<PUNConnection>();
        }
    }
}