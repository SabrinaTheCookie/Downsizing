using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ExitButton : MonoBehaviour
{
    public InputActionAsset exitButton;
    public string ActionName = "ExitButton";
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InputAction inputAction = exitButton.FindAction(ActionName);
        if (inputAction!=null)
        {
            inputAction.performed += ExitButton_performed;
            inputAction.Enable();
        }
        
    }

    private void ExitButton_performed(InputAction.CallbackContext obj)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        
    }
}
