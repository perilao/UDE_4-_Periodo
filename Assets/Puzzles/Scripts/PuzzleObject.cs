using UnityEngine;

public abstract class PuzzleObject : MonoBehaviour
{
    [SerializeField] protected int puzzleID;
    public int PuzzleID => puzzleID;
}
