using System;
using UnityEngine;

public abstract class BasePuzzle : MonoBehaviour
{
    public event Action<BasePuzzle> PuzzleInitialized;
    public event Action<BasePuzzle> PuzzleCompleted;

    public bool IsCompleted { get; protected set; } = false;

    public int PuzzleID { get; private set; }

    public virtual void InitializePuzzle()
    {
        IsCompleted = false;
        
        PuzzleInitialized?.Invoke(this);
    }

    public abstract void ResetPuzzle();

    public virtual void CompletePuzzle()
    {
        if (IsCompleted)
            return;

        IsCompleted = true;
        PuzzleCompleted?.Invoke(this);
    }

    public abstract void ActivatePuzzle();

    public abstract void DisablePuzzle();
}
