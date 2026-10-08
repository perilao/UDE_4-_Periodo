using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRSocketInteractor))]
public class CardSocket : PuzzleObject
{
    public event Action<CardSocket> CardPlaced;
    public event Action<CardSocket> CardRemoved;

    [SerializeField] private int socketID;
    public int SocketID => socketID;

    [SerializeField] private InteractableCard.CardColors targetCardCurrentColor;

    public InteractableCard.CardColors TargetCardCurrentColor => targetCardCurrentColor;

    [Header("Adjacência")]
    [SerializeField] private List<CardSocket> adjacentSockets = new();

    public IReadOnlyList<CardSocket> AdjacentSockets => adjacentSockets;

    public InteractableCard CurrentCard { get; private set; }

    public bool IsCorrect => CurrentCard != null && CurrentCard.CurrentCardColor == TargetCardCurrentColor;

    public XRSocketInteractor SocketInteractor { get; private set; }

    private void Awake()
    {
        SocketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        SocketInteractor.selectEntered.AddListener(OnCardEntered);
        SocketInteractor.selectExited.AddListener(OnCardExited);
    }

    private void OnDisable()
    {
        SocketInteractor.selectEntered.RemoveListener(OnCardEntered);
        SocketInteractor.selectExited.RemoveListener(OnCardExited);
    }

    private void OnCardEntered(SelectEnterEventArgs args)
    {
        if (!args.interactableObject.transform.TryGetComponent<InteractableCard>(out var card))
            return;

        CurrentCard = card;

        CardPlaced?.Invoke(this);
    }

    private void OnCardExited(SelectExitEventArgs args)
    {
        if (!args.interactableObject.transform.TryGetComponent<InteractableCard>(out var card))
            return;

        if (card != CurrentCard)
            return;

        card.SwitchColor(card.DefaultCardColor);

        CurrentCard = null;

        CardRemoved?.Invoke(this);
    }
}