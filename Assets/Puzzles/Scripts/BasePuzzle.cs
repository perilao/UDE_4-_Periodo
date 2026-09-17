using System;
using UnityEngine;

public abstract class BasePuzzle : MonoBehaviour
{
    public event Action<BasePuzzle> PuzzleCompleted;

    public bool IsCompleted { get; protected set; } = false;

    public int PuzzleID { get; private set; }

    [SerializeField] private GameObject puzzleRoom;
    public GameObject PuzzleRoom => puzzleRoom;

    public abstract void InitializePuzzle();

    public abstract void ResetPuzzle();

    public virtual void CompletePuzzle()
    {
        if (IsCompleted)
            return;

        IsCompleted = true;
        PuzzleCompleted?.Invoke(this);
    }
}
