using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionReference gripReference;
    [SerializeField] private InputActionReference triggerReference;
    private Animator handAnimator;

    void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (handAnimator == null) 
            return;

        float gripValue = gripReference.action.ReadValue<float>();
        handAnimator.SetFloat($"Grip", gripValue);

        float triggerValue = triggerReference.action.ReadValue<float>();
        handAnimator.SetFloat($"Trigger", triggerValue);
    }
}