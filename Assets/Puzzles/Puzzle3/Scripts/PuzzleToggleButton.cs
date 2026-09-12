using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class PuzzleToggleButton : PuzzleObject
{
    public event Action<PuzzleToggleButton> StateChanged;

    public enum ButtonState
    {
        Up,
        Down
    }

    [SerializeField] private ButtonState initialState = ButtonState.Up;
    [SerializeField] private ButtonState correctState = ButtonState.Up;

    [SerializeField] private Transform buttonVisual;
    [SerializeField] private Vector3 upLocalPosition;
    [SerializeField] private Vector3 downLocalPosition;

    public ButtonState CurrentState { get; private set; }
    public ButtonState CorrectState => correctState;
    public bool IsCorrect => CurrentState == correctState;

    private XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelected);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelected);
    }

    private void Start()
    {
        SetState(initialState, true);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        Toggle();
    }

    public void Toggle()
    {
        ButtonState newState = CurrentState == ButtonState.Up ? ButtonState.Down : ButtonState.Up;

        SetState(newState);
    }

    private void SetState(ButtonState newState, bool forceUpdate = false)
    {
        if (CurrentState == newState && !forceUpdate)
            return;

        CurrentState = newState;

        UpdateVisual();

        StateChanged?.Invoke(this);
    }

    private void UpdateVisual()
    {
        buttonVisual.localPosition = CurrentState == ButtonState.Up ? upLocalPosition : downLocalPosition;
    }
}