using UnityEditor;
using UnityEngine;

namespace DamageSystem.Editor
{
    [CustomEditor(typeof(Damage))]
    public class DamageEditor : UnityEditor.Editor
    {
        public Damage Target => (Damage) target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (Target.gameObject.layer == LayerMask.NameToLayer("DamageEnemy") ||
                Target.gameObject.layer == LayerMask.NameToLayer("DamagePlayer"))
            {
                return;
            }

            EditorGUILayout.HelpBox("The Layer Must Be Damage Enemy Or Damage Player", MessageType.Error);
            if (GUILayout.Button("To Damage PLAYER PRESS HERE"))
            {
                Target.gameObject.layer = LayerMask.NameToLayer("DamagePlayer");
            }
            
            if (GUILayout.Button("To Damage ENEMY PRESS HERE"))
            {
                Target.gameObject.layer = LayerMask.NameToLayer("DamageEnemy");
            }
        }
    }
}