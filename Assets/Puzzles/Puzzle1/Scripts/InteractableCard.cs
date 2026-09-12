using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class InteractableCard : PuzzleObject
{
    [SerializeField] private CardColors defaultCardColor;
    public CardColors DefaultCardColor => defaultCardColor;
    public CardColors CurrentCardColor { get; private set; }

    private MeshRenderer meshRenderer;

    [Header("Materiais")]
    [SerializeField] private CardMaterialDatabase CardMaterialDatabase;

    public enum CardColors
    {
        Red,
        Orange,
        Green,
        Violet
    }

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        SwitchColor( defaultCardColor, true );
    }

    public void SwitchColor(CardColors newColor, bool isForceUpdate = false)
    {
        if (CurrentCardColor == newColor && !isForceUpdate)
            return;
        
        CurrentCardColor = newColor;
        UpdateColorMaterial();
    }

    private void UpdateColorMaterial()
    {
        Material targetMaterial = CardMaterialDatabase.GetMaterial(CurrentCardColor);

        if (targetMaterial == null)
        {
            Debug.LogWarning($"Material não definido para a cor {CurrentCardColor}.", this);
            return;
        }

        meshRenderer.sharedMaterial = targetMaterial;
    }
}
