using UnityEngine;

public abstract class PuzzleObject : MonoBehaviour
{
    [SerializeField] protected int puzzleID;
    public int PuzzleID => puzzleID;

    public Transform OriginalTransform { get; private set; }

    void Awake()
    {
        OriginalTransform = transform;
    }
}
