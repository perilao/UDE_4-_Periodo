using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class InteractablePicture : PuzzleObject
{
    [SerializeField] private PictureDirections pictureDirection;
    public PictureDirections PictureDirection => pictureDirection;

    public enum PictureDirections
    {
        Up,
        Down
    }
}
