using UnityEngine;
using UnityEngine.Events;

namespace GameEvents
{
    public static class BroadcastSystem
    {
        public static UnityAction<Card> CardSelected;
        public static UnityAction<Vector2Int> BoardPieceFilled;

        public static UnityAction<GameState> GameStateChanged;
        public static UnityAction<bool> CanInput;
        public static UnityAction UnFlipAllCards;
        public static UnityAction PlayerWon;
        public static UnityAction RestartGame;
    }
}
