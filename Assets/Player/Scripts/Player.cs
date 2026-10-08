using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Player : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor LeftHandInteractor;
    [SerializeField] private XRDirectInteractor RightHandInteractor;

    public void EnableBothHandsInteractionMasks(string maskName)
    {
        int mask = InteractionLayerMask.GetMask(maskName);

        LeftHandInteractor.interactionLayers |= 1 << mask;

        RightHandInteractor.interactionLayers |= 1 << mask;
    }

    public void DisableBothHandsInteractionMasks(string maskName)
    {
        int mask = InteractionLayerMask.GetMask(maskName);

        LeftHandInteractor.interactionLayers &= ~(1 << mask);

        RightHandInteractor.interactionLayers &= ~(1 << mask);
    }
}
