using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class RunManager : MonoSingleton<RunManager>
    {
        public bool EndRunning => _endRunning;

        [SerializeField] GameObject upgradePanel;
        [SerializeField] private GameObject reStartBtn;
		[SerializeField] EnemySpawner spawner;

		private bool _endRunning = false;



        protected override void Awake()
        {
            base.Awake();
            reStartBtn.SetActive(false);
        }
        
        public void HandleRestart()
		{
			upgradePanel.gameObject.SetActive(true);
			reStartBtn.SetActive(true);

			spawner.StopWave();
            _endRunning = true;
        }

        public void Restart()
		{
			upgradePanel.gameObject.SetActive(false);
			spawner.ResetWave();
			_endRunning = false;
        }
    }
}