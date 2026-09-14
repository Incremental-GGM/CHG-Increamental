using System;
using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class HealthManager : MonoSingleton<HealthManager>
    {
        public UnityEvent OnDeath;
        public UnityEvent<int> OnHealthChange;

        [SerializeField] private int maxHealth = 3;
        private int _health;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;

                //게임오버 호출 예측이 어려워서 함수로 옮김
            }
        }

        protected override void Awake()
        {
            base.Awake();
			_health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
			_health -= damage;

			_health = Mathf.Clamp(_health, 0, maxHealth);
			OnHealthChange?.Invoke(_health);
			if (_health <= 0)
			{
				OnDeath?.Invoke();
				Debug.Log("게임 오버");
			}
		}
    }
}