using UnityEditor;
[CustomEditor(typeof(MonsterStat_WithObjectSO))]
public class MonsterStatWithObjectSOEditor : Editor
{
    private SerializedProperty show;
    private SerializedProperty value;

    private void OnEnable()
    {
        show = serializedObject.FindProperty("_hasExtraObject");
        value = serializedObject.FindProperty("_extraObjects");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "_extraObjects");

        if(show.boolValue)
        {
            EditorGUILayout.PropertyField(value);
        }

        serializedObject.ApplyModifiedProperties();
    }
}