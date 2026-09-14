using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "PlayerInput", order = 0)]
public class PlayerInputSO : ScriptableObject//, Controls.IPlayerActions
{
    /*public event Action<Vector2> OnAimChanged;
    public event Action OnClicked;
    
    private Controls _controls;
    
    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }*/

    /*public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAimChanged?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnClicked?.Invoke();
            Debug.Log("AA");
        }
    }*/
}