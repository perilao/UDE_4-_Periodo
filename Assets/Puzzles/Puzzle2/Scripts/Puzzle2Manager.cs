using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Manager : BasePuzzle
{
    [SerializeField] private List<PictureSocket> pictureSockets = new();

    public override void InitializePuzzle()
    {
        base.InitializePuzzle();

        SetupPictureSockets();
    }

    public override void ResetPuzzle()
    {
        IsCompleted = false;
    }

    public override void CompletePuzzle()
    {
        base.CompletePuzzle();

        Debug.Log("Puzzle 2 concluído!");
    }

    private void SetupPictureSockets()
    {
        foreach (PictureSocket socket in pictureSockets)
        {
            socket.PicturePlaced -= OnPicturePlaced;
            socket.PicturePlaced += OnPicturePlaced;

            socket.PictureRemoved -= OnPictureRemoved;
            socket.PictureRemoved += OnPictureRemoved;
        }
    }

    private void OnPicturePlaced(PictureSocket initialSocket)
    {
        CheckPuzzleCompletion();
    }

    private void OnPictureRemoved(PictureSocket socket)
    {
        CheckPuzzleCompletion();
    }

    private void CheckPuzzleCompletion()
    {
        if (IsCompleted)
            return;

        if (CheckPuzzle())
            CompletePuzzle();
    }

    private bool CheckPuzzle()
    {
        if (pictureSockets.Count == 0)
            return false;

        foreach (PictureSocket socket in pictureSockets)
        {
            if (!socket.IsCorrect())
                return false;
        }

        return true;
    }

    public override void ActivatePuzzle()
    {
        gameObject.SetActive(true);
    }

    public override void DisablePuzzle()
    {
        gameObject.SetActive(false);
    }
}
