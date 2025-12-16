using UnityEditor;
[CustomEditor(typeof(StageInformation_SO))]
public class StageInformationSOEditor : Editor
{
    private SerializedProperty show;
    private SerializedProperty value;

    private void OnEnable()
    {
        show = serializedObject.FindProperty("isDefaultColor");
        value = serializedObject.FindProperty("_skillRangeVisualizerColor");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "_skillRangeVisualizerColor");

        if(!show.boolValue)
        {
            EditorGUILayout.PropertyField(value);
        }

        serializedObject.ApplyModifiedProperties();
    }
}