#region

using System;
using UnityEngine;
using UnityEngine.InputSystem;

#endregion

namespace Services.Input
{
    [CreateAssetMenu(fileName = "InputActionsReader", menuName = "InputActionsReader")]
    public class InputActionsReader : ScriptableObject, InputActions.IPlayerActions
    {
        private InputActions _inputActions;

        public Action<float> OnMovementInput { get; set; }

        public void OnEnable()
        {
            if (_inputActions != null)
            {
                return;
            }

            _inputActions = new InputActions();
            _inputActions.Player.SetCallbacks(this);
            _inputActions.Player.Enable();
        }


        public void OnMovement(InputAction.CallbackContext context)
        {
            OnMovementInput?.Invoke(context.ReadValue<float>());
        }
    }
}