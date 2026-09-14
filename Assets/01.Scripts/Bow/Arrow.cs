using System;
using UnityEngine;

namespace Bow
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _whatIsEnemy;

        private int _damage = 1;

        public void Init(int damage)
        {
            _damage = damage;
        }
        
        private void Update()
        {
            transform.position += transform.right * (Time.deltaTime * _speed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}