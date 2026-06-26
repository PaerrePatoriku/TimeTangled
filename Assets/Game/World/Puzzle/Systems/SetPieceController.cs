using UnityEngine;
using UnityEditor;
namespace Game.World.Puzzle.Systems
{
    public class SetPieceController : MonoBehaviour
    {
        [SerializeField] 
        private string GroupName;

        public string GetGroupName()
        {
            return GroupName;
        }
    }
}