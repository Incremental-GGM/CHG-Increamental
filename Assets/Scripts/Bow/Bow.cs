using System;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bow
{
    public class Bow : MonoBehaviour
    {
        public int Damage
        {
            get => _damage;
            private set
            {
                _damage = value;
                _damage = Mathf.Clamp(_damage, 0, int.MaxValue);
            }
        }
        
        //Arrow
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private float _cooldown = 0.3f;
        [SerializeField] private int _defaultDamage = 3;

        //Animation
        [SerializeField] private Animator animator;

        //Collision
        [SerializeField] private LayerMask _whatIsEnemy;

		private int _damage;
        private Vector2 _mousePos = Vector2.zero;
        private float _angle;
        private float _nextShot = 0;

        private void Awake()
        {
            UpgradeManager.Instance.OnStatChanged += HandleUpgrade;
        }

        private void Update()
        {
            if (RunManager.Instance.EndRunning) return;
            if (Input.GetMouseButton(0)) HandleClicked();
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePos = dir - (Vector2)transform.position;
    
            _angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;
    
            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
        

        private void HandleClicked()
        {
            if (_nextShot <= Time.time)
            {
                Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0,0, _angle)).GetComponent<Arrow>();
                arrow.Init(Damage);
                
                _nextShot = Time.time + _cooldown;

                animator.Play("Anim_Bow_Attack");
			}
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                HealthManager.Instance.TakeDamage(1);
                Destroy(collision.gameObject);
            }
        }

        public void DamageUp(int damage) => Damage += damage;
        private void HandleUpgrade(string key, int arg2)
        {
            
        }
    }
}