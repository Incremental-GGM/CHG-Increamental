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
            set
            {
                _damage = value;
                _damage = Mathf.Clamp(_damage, 0, int.MaxValue);
            }
        }
        
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private LayerMask whatIsEnemy;
        [SerializeField] private float cooldown;
        [SerializeField] private int _defaultDamage;
        private int _damage;
        private Vector2 _mousePos = Vector2.zero;
        private float angle;

        private float _nextShot = 0;

        private void Update()
        {
            if (RunManager.Instance.EndRunning) return;
            if (Input.GetMouseButton(0)) HandleClicked();
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePos = dir - (Vector2)transform.position;
    
            angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;
    
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        

        private void HandleClicked()
        {
            if (_nextShot <= Time.time)
            {
                Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0,0, angle)).GetComponent<Arrow>();
                arrow.Init(Damage);
                
                _nextShot = Time.time + cooldown; 
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
        
        
        
    }
}