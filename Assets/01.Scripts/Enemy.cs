using System;
using Manager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _health = 5;
    [SerializeField] BigNumber _gold;
    
    private void Update()
    {
        transform.position += (Vector3.zero - transform.position).normalized * (_moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            GoldManager.Instance.AddGold(_gold);
            Destroy(gameObject); 
        }
    }
}