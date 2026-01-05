using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
/// <summary>
/// <para>
/// FileReference에서 지정한 컴포넌트의 모든 직렬화 가능한 필드를 팝업 형식으로 선택 가능
/// </para>
/// 직접적인 컴포넌트로만 동작, 게임 오브젝트로 동작하지 않음
/// </summary>
[CustomPropertyDrawer(typeof(FileReference))]
public class FieldReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)//
    {
        var componentProp = property.FindPropertyRelative("component");
        var fieldNameProp = property.FindPropertyRelative("fieldName");

        EditorGUI.BeginProperty(position, label, property);// property

        float halfWidth = position.width / 2f;

        Rect objectRect = new Rect(position.x, position.y, halfWidth - 5, position.height);
        EditorGUI.PropertyField(objectRect, componentProp, GUIContent.none);

        if(componentProp.objectReferenceValue != null)
        {
            var type = EditorUtility.EntityIdToObject(componentProp.objectReferenceValue.GetInstanceID()).GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if(fields.Count() > 0)
            {
                string[] options = fields.Select(o => o.Name).ToArray();
                Rect dropdownRect = new Rect(position.x + halfWidth + 5, position.y, halfWidth - 5, position.height);
                int currentIndex = 0;

                if(fieldNameProp.stringValue != "")
                {
                    currentIndex = System.Array.IndexOf(options, fieldNameProp.stringValue);
                }

                int selected = EditorGUI.Popup(dropdownRect, currentIndex, options);

                if(selected != -1)
                {
                    fieldNameProp.stringValue = options[selected];
                }
            }
        }

        EditorGUI.EndProperty();
    }
}