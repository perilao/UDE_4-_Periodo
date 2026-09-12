using UnityEngine;

public abstract class BasePuzzle : MonoBehaviour
{
    public bool IsCompleted { get; protected set; } = false;

    public int PuzzleID { get; private set; }

    public abstract void InitializePuzzle();

    public abstract void ResetPuzzle();

    public abstract void CompletePuzzle();
}
