using UnityEngine;

public class CronometerManager : MonoBehaviour
{
    [SerializeField] private BasePuzzle initialPuzzle;
    [SerializeField] private BasePuzzle finalPuzzle;
    private bool isPuzzleActive = false;
    private float elapsedTime = 0f;

    void Awake()
    {
        initialPuzzle.PuzzleInitialized += OnPuzzleInitialized;
        finalPuzzle.PuzzleCompleted += OnPuzzleCompleted;
    }

    void Update()
    {
        if (isPuzzleActive)
            elapsedTime += Time.deltaTime;
    }

    private void OnPuzzleInitialized(BasePuzzle puzzle)
    {
        isPuzzleActive = true;
    }

    private void OnPuzzleCompleted(BasePuzzle puzzle)
    {
        isPuzzleActive = false;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt(elapsedTime * 1000f % 1000f);

        Debug.Log($"Completou em {minutes:00}:{seconds:00}:{milliseconds:000}");
    }
}
