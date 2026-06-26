
using Game.World.Puzzle.Systems;
using Unity.VisualScripting;
using UnityEngine;

namespace UnityEditor
{
    [CustomEditor(typeof(SetPiece), true)]
    public class SetPieceControllerEditor : Editor
    {
        public SetPieceController GetParentController(GameObject go)
        {
            if (go.GetComponent<SetPieceController>() != null)
                return go.GetComponent<SetPieceController>();
            else
                return GetParentController(go.transform.parent.gameObject);
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var setPiece = target as SetPiece;
            SetPieceController parent = GetParentController(target.GameObject());
            if(GUILayout.Button("Generate name"))
            {
                setPiece.assignSetPieceName($"{parent.GetGroupName()}.{target.name}");
            }
        }
    }
}