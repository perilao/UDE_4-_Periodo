using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRSocketInteractor))]
public class PictureSocket : PuzzleObject
{
    public event Action<PictureSocket> PicturePlaced;
    public event Action<PictureSocket> PictureRemoved;

    [SerializeField] private int socketID;
    public int SocketID => socketID;

    [Tooltip("O quadro que deve ser colocado neste socket para que ele seja considerado correto. Se for nulo, significa que o socket não deve ter nenhum quadro colocado nele para ser considerado correto.")]
    [SerializeField] private InteractablePicture targetPicture;
    public InteractablePicture TargetPicture => targetPicture;

    public InteractablePicture CurrentPicture { get; private set; }

    private XRSocketInteractor socketInteractor;

    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
    }

    /// <summary>
    /// Verifica se o quadro atual no socket está correto em relação ao alvo.
    /// </summary>
    /// <returns><c>true</c> se o quadro atual está correto, isso inclui o caso em que ambos são nulos.</returns>
    public bool IsCorrect()
    {
        if (TargetPicture == null && CurrentPicture == null)
            return true;

        if (CurrentPicture == null || TargetPicture == null)
            return false;

        return CurrentPicture == TargetPicture;
    }

    private void OnEnable()
    {
        socketInteractor.selectEntered.AddListener(OnPictureEntered);
        socketInteractor.selectExited.AddListener(OnPictureExited);
    }

    private void OnDisable()
    {
        socketInteractor.selectEntered.RemoveListener(OnPictureEntered);
        socketInteractor.selectExited.RemoveListener(OnPictureExited);
    }

    private void OnPictureEntered(SelectEnterEventArgs args)
    {
        if (!args.interactableObject.transform.TryGetComponent<InteractablePicture>(out var picture))
            return;

        CurrentPicture = picture;

        PicturePlaced?.Invoke(this);
    }

    private void OnPictureExited(SelectExitEventArgs args)
    {
        if (!args.interactableObject.transform.TryGetComponent<InteractablePicture>(out var picture))
            return;

        if (picture != CurrentPicture)
            return;

        CurrentPicture = null;

        PictureRemoved?.Invoke(this);
    }
}
