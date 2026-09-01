using UnityEngine;
using UnityEngine.InputSystem;

namespace Bow
{
    public class Bow : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private float cooldown;
        private Vector3 _prevMousePos = Vector3.zero;

        private float _nextShot = 0;
        

        private void Update()
        {
            if (Input.GetMouseButton(0)) HandleClicked();
            if (Input.mousePosition != _prevMousePos)
            {
                HandleAimChanged(Input.mousePosition);
            }
        }
        

        private void HandleClicked()
        {
            if (_nextShot <= Time.time)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(_prevMousePos.x, _prevMousePos.y, 0)));
                arrow.GetComponent<Arrow>().Dir = _prevMousePos;
                
                _nextShot = Time.time + cooldown; 
            }
            
        }

        private void HandleAimChanged(Vector2 dir)
        {
            _prevMousePos = new Vector3(dir.x, dir.y, 0);
            transform.Rotate(_prevMousePos - transform.position);
        }
    }
}