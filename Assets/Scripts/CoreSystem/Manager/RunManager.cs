using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class RunManager : MonoSingleton<RunManager>
    {
        [SerializeField] private GameObject reStartBtn;
        private bool _endRunning = false;

        public bool EndRunning => _endRunning;

        protected override void Awake()
        {
            base.Awake();
            reStartBtn.SetActive(false);
        }
        
        public void HandleRestart()
        {
            reStartBtn.SetActive(true);
            _endRunning = true;
        }

        public void Restart()
        {
            _endRunning = false;
            SceneManager.LoadScene(0);
        }
    }
}