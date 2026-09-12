using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Manager : BasePuzzle
{
     [SerializeField] private List<CardSocket> cardSockets = new();

     void Start()
     {
          InitializePuzzle();
     }

     public override void InitializePuzzle()
     {
          IsCompleted = false;

          SetupCardSockets();
     }

     public override void CompletePuzzle()
     {
          if (IsCompleted)
               return;

          IsCompleted = true;

          Debug.Log("Puzzle 1 concluído");
     }

     public override void ResetPuzzle()
     {
          IsCompleted = false;

          foreach (CardSocket socket in cardSockets)
          {
               if (socket.CurrentCard == null)
                    continue;

               socket.CurrentCard.SwitchColor( socket.CurrentCard.DefaultCardColor, true );
          }
     }

     private void SetupCardSockets()
     {
          foreach (CardSocket socket in cardSockets)
          {
               socket.CardPlaced -= OnCardPlaced;
               socket.CardPlaced += OnCardPlaced;

               socket.CardRemoved -= OnCardRemoved;
               socket.CardRemoved += OnCardRemoved;
          }
     }

     private void OnCardPlaced(CardSocket initialSocket)
     {
          UpdateAdjacentSockets(initialSocket);

          CheckPuzzleCompletion();
     }

     private void OnCardRemoved(CardSocket socket)
     {
          CheckPuzzleCompletion();
     }

     private void UpdateAdjacentSockets(CardSocket initialSocket)
     {
          if (initialSocket.CurrentCard == null)
               return;
          
          foreach (CardSocket adjacentSocket in initialSocket.AdjacentSockets)
          {
               if (adjacentSocket == null)
                    continue;

               // Socket vazio interrompe a propagação.
               if (adjacentSocket.CurrentCard == null)
                    continue;

               adjacentSocket.CurrentCard.SwitchColor(initialSocket.CurrentCard.CurrentCardColor);
          }
     }

     private void CheckPuzzleCompletion()
     {
          if (CheckPuzzle())
               CompletePuzzle();
     }

     private bool CheckPuzzle()
     {
          if (cardSockets.Count == 0)
               return false;

          foreach (CardSocket socket in cardSockets)
          {
               if (!socket.IsCorrect)
                    return false;
          }

          return true;
     }

     private void OnDestroy()
     {
          foreach (CardSocket socket in cardSockets)
          {
               if (socket == null)
                    continue;

               socket.CardPlaced -= OnCardPlaced;
               socket.CardRemoved -= OnCardRemoved;
          }
     }
}