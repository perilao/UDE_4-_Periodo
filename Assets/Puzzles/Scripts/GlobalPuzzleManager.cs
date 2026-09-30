using System.Collections.Generic;
using UnityEngine;

public class GlobalPuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [SerializeField] private List<BasePuzzle> PuzzleManagers = new();

    private int CurrentPuzzleIndex = 0;

    public BasePuzzle CurrentPuzzle => PuzzleManagers[CurrentPuzzleIndex];
    
    void Start()
    {
        SwitchPuzzle(0);

        foreach (BasePuzzle puzzle in PuzzleManagers)
        {
            if (puzzle != CurrentPuzzle) 
                puzzle.DisablePuzzle();
        }
    }

    private void SwitchPuzzle(int index)
    {
        if (index < 0 || index >= PuzzleManagers.Count)
            return;

        CurrentPuzzle.DisablePuzzle();

        CurrentPuzzle.PuzzleCompleted -= PuzzleCompleted;

        CurrentPuzzleIndex = index;

        CurrentPuzzle.InitializePuzzle();

        CurrentPuzzle.ActivatePuzzle();

        CurrentPuzzle.PuzzleCompleted += PuzzleCompleted;
    }

    private void PuzzleCompleted(BasePuzzle puzzle)
    {
        if (puzzle != CurrentPuzzle)
        {
            Debug.LogError("Puzzle completed is not the current puzzle.");
            return;
        }

        SwitchPuzzle(CurrentPuzzleIndex + 1);
    }
}
