using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public static class BroadcastSystem
    {
        public static UnityAction<int> CardSelected;
        public static UnityAction<Vector2Int> BoardPieceFilled;

        public static UnityAction<GameState> GameStateChanged;
        public static UnityAction PlayerWon;
        public static UnityAction RestartGame;
    }
}
