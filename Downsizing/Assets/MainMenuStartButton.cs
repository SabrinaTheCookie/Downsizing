using UnityEngine;

public class MainMenuStartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.Instance.SetSelected(transform);
    }
}
