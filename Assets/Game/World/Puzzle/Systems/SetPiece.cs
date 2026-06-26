using System.Collections.Generic;
using UnityEngine;

namespace Game.World.Puzzle.Systems
{
    public class SetPiece : MonoBehaviour
    {
        [SerializeField]
        private string SetPieceName;
        [SerializeField]
        public List<SetPieceItem> items = new List<SetPieceItem>();
        public void assignSetPieceName(string name)
        {
            SetPieceName = name;
        }
    }
}