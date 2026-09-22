using System.Collections.Generic;
using UnityEngine;

public class CronometerManager : MonoBehaviour
{
    [SerializeField] private List<BasePuzzle> PuzzlesToDebbug = new();

    private List<BasePuzzle> puzzlesCompleted = new();

    private bool isPuzzleActive = false;

    private float globalElapsedTime = 0f;

    private float lastElapsedTime = 0f;

    void Awake()
    {
        if (PuzzlesToDebbug.Count == 0)
            return;
        
        PuzzlesToDebbug[0].PuzzleInitialized += OnPuzzleInitialized;
        
        foreach (BasePuzzle puzzle in PuzzlesToDebbug)
            puzzle.PuzzleCompleted += OnPuzzleCompleted;
    }

    void Update()
    {
        if (isPuzzleActive)
            globalElapsedTime += Time.deltaTime;
    }

    private void OnDestroy()
    {
        if (PuzzlesToDebbug.Count == 0)
            return;

        PuzzlesToDebbug[0].PuzzleInitialized -= OnPuzzleInitialized;

        foreach (BasePuzzle puzzle in PuzzlesToDebbug)
            puzzle.PuzzleCompleted -= OnPuzzleCompleted;
    }

    private void OnPuzzleInitialized(BasePuzzle puzzle)
    {
        isPuzzleActive = true;
    }

    private void OnPuzzleCompleted(BasePuzzle puzzle)
    {   
        puzzlesCompleted.Add(puzzle);

        Debug.Log($"Completou {puzzle.name} em {FormatTime(globalElapsedTime - lastElapsedTime)}");

        lastElapsedTime = globalElapsedTime;

        // Só para evitar que o cronômetro continue rodando caso o último puzzle seja completado
        if (puzzlesCompleted.Count < PuzzlesToDebbug.Count)
            return;

        isPuzzleActive = false;

        string allPuzzlesNames = string.Join(", ", PuzzlesToDebbug.ConvertAll(p => p.name));

        Debug.Log($"Completou {allPuzzlesNames} em {FormatTime(globalElapsedTime)}");

        puzzlesCompleted.Clear();
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt(time * 1000f % 1000f);

        return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
