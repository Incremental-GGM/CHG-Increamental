using System;
using UnityEngine;

namespace Bow
{
    public class Arrow : MonoBehaviour
    {
        public Vector3 Dir;
        [SerializeField] private float _speed;
        private void Update()
        {
            transform.position += (Dir.normalized * Time.deltaTime);
        }
    }
}