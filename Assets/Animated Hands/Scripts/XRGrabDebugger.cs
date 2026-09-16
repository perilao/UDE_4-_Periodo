using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class XRGrabDebugger : MonoBehaviour
{
    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grab.hoverEntered.AddListener(OnHoverEntered);
        grab.hoverExited.AddListener(OnHoverExited);

        grab.selectEntered.AddListener(OnSelectEntered);
        grab.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        grab.hoverEntered.RemoveListener(OnHoverEntered);
        grab.hoverExited.RemoveListener(OnHoverExited);

        grab.selectEntered.RemoveListener(OnSelectEntered);
        grab.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"HOVER ENTER: {name}");
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"HOVER EXIT: {name}");
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log($"SELECT ENTER: {name}");
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log($"SELECT EXIT: {name}");
    }
}