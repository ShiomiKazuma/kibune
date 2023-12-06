using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace RSEngine
{
    public class InputWindow : MonoBehaviour
    {
        [SerializeField] InputActionAsset _inputAction;

        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this);
        }

        // �A�N�V���������w�肵�Ă���ɓo�^
        public void BindAction(string actionMapName, string actionName
            , Action<InputAction.CallbackContext> callbackAction, ActionInvokeFaze actionInvokingFaze)
        {
            var actionMap = _inputAction.FindActionMap(actionMapName);
            var action = actionMap.FindAction(actionName);
            switch (actionInvokingFaze)
            {
                case ActionInvokeFaze.Started:
                    action.started += callbackAction;
                    break;
                case ActionInvokeFaze.Performed:
                    action.performed += callbackAction;
                    break;
                case ActionInvokeFaze.Canceled:
                    action.canceled += callbackAction;
                    break;
            }
        }

        public T GetActionValueAs<T>(string actionMapName, string actionName)
        {
            var actionMap = _inputAction.FindActionMap(actionMapName);
            var action = actionMap.FindAction(actionName);
            T result = default;
            if (action != null)
            {
                var input = action.ReadValueAsObject();
                if (input != null)
                    result = (T)input;
            }
            return result;
        }

        public bool GetActionValueAsButton(string actionMapName, string actionName)
        {
            var actionMap = _inputAction.FindActionMap(actionMapName);
            var action = actionMap.FindAction(actionName);
            return action.IsPressed();
        }
    }

    public enum ActionInvokeFaze
    {
        Started,
        Performed,
        Canceled
    }
}