using System;
using Manager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int health = 5;
    
    private void Update()
    {
        transform.position += (Vector3.zero - transform.position).normalized * (moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GoldManager.Instance.GetGold(1);   
            Destroy(gameObject); 
        }
    }
}