using System.Collections.Generic;
using UnityEngine;

public class Puzzle3Manager : BasePuzzle
{
    [SerializeField] private List<PuzzleToggleButton> puzzleButtons = new();

    public override void InitializePuzzle()
    {
        base.InitializePuzzle();

        SetupButtons();
    }

    public override void CompletePuzzle()
    {
        base.CompletePuzzle();

        Debug.Log("Puzzle 3 concluído!");
    }

    public override void ResetPuzzle()
    {
        IsCompleted = false;
    }

    private void SetupButtons()
    {
        foreach (PuzzleToggleButton button in puzzleButtons)
        {
            if (button == null)
                continue;

            button.StateChanged -= OnButtonStateChanged;
            button.StateChanged += OnButtonStateChanged;
        }
    }

    private void OnButtonStateChanged(PuzzleToggleButton changedButton)
    {
        if (CheckPuzzle())
            CompletePuzzle();
    }

    private bool CheckPuzzle()
    {
        if (puzzleButtons.Count == 0)
            return false;

        foreach (PuzzleToggleButton button in puzzleButtons)
        {
            if (button == null || !button.IsCorrect)
                return false;
        }

        return true;
    }

    private void OnDestroy()
    {
        foreach (PuzzleToggleButton button in puzzleButtons)
        {
            if (button != null)
                button.StateChanged -= OnButtonStateChanged;
        }
    }

    public override void ActivatePuzzle()
    {
        puzzleButtons.ForEach(button => button.gameObject.SetActive(true));
    }

    public override void DisablePuzzle()
    {
        puzzleButtons.ForEach(button => button.gameObject.SetActive(false));
    }
}