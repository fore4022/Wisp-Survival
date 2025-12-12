using UnityEditor;
/// <summary>
/// <para>
/// 필드를 토글 방식으로 표시/숨기는 것이 가능
/// </para>
/// Skill_SO의 필드 이름이 변경 사항이 똑같이 적용되어야 함
/// </summary>
[CustomEditor(typeof(Skill_SO))]
public class SkillSOEditor : Editor
{
    private SerializedProperty show_1;
    private SerializedProperty value_1;

    private SerializedProperty show_2;
    private SerializedProperty value_2;

    private void OnEnable()
    {
        show_1 = serializedObject.FindProperty("_isProjectile");
        value_1 = serializedObject.FindProperty("_projectileInfo");

        show_2 = serializedObject.FindProperty("_isMultiCast");
        value_2 = serializedObject.FindProperty("_multiCast");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, "_projectileInfo", "_multiCast");

        if(show_1.boolValue)
        {
            EditorGUILayout.PropertyField(value_1);
        }

        if(show_2.boolValue)
        {
            EditorGUILayout.PropertyField(value_2);
        }

        serializedObject.ApplyModifiedProperties();
    }
}