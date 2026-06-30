using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityEvent onAnimationEnd;
    
    void OnAnimationEnd()
    {
        onAnimationEnd?.Invoke();
    }

    public UnityEvent onAnimationEventA;

    void OnAnimationEventA()
    {
        onAnimationEventA?.Invoke();
    }


}
