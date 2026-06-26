using UnityEngine;

namespace Game.World.Puzzle.Systems
{
    public class SetPieceItem : MonoBehaviour
    {
        [SerializeField]
        SetPieceState State = SetPieceState.Incomplete;
    }
}