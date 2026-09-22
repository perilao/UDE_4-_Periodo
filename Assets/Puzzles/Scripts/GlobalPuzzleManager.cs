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
    }

    private void SwitchPuzzle(int index)
    {
        if (index < 0 || index >= PuzzleManagers.Count)
            return;

        CurrentPuzzle.PuzzleCompleted -= PuzzleCompleted;

        CurrentPuzzleIndex = index;

        ReparentPlayer( CurrentPuzzle.PuzzleRoom.transform );

        CurrentPuzzle.InitializePuzzle();

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

    private void ReparentPlayer(Transform newParent, bool keepLocalPosition = true)
    {
        Vector3 prevPlayerTransform = Player.transform.localPosition;

        Player.transform.parent = newParent;

        Player.transform.localPosition = prevPlayerTransform;
    }
}
