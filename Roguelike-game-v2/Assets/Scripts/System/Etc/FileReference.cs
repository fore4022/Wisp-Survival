using System;
using System.Reflection;
using UnityEngine;
/// <summary>
/// <para>
/// 컴포넌트 필드에 접근하고 Get/Set할 수 있는 기능을 가진 Serializable
/// </para>
/// 접근 제한자와 상관 없이 인스펙터의 필드로 선택 가능
/// </summary>
[Serializable]
public class FileReference
{
    [ShowInInspector] public UnityEngine.Object component;
    [ShowInInspector] public string fieldName;

    [HideInInspector] public Action GetAction = null;
    [HideInInspector] public Action SetAction = null;

    // 이벤트 호출, component에서 fieldName 필드를 리플렉션으로 접근해 값을 반환
    public object GetValue()
    {
        if(component ==  null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var field = component.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        GetAction?.Invoke();

        return field?.GetValue(component);
    }
    // 이벤트 호출, component에서 fieldName 필드를 리플렉션으로 접근해 값을 수정
    public void SetValue(object value)
    {
        if(component == null || string.IsNullOrEmpty(fieldName))
        {
            return;
        }

        var field = component.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if(field != null && CanConvert(value, field.GetValue(component).GetType()))
        {
            field.SetValue(component, value);
        }

        SetAction?.Invoke();
    }
    // 목포 타입으로 값 변경 가능 여부를 반환
    private bool CanConvert(object value, Type targetType)
    {
        try
        {
            var converted = Convert.ChangeType(value, targetType);

            return true;
        }
        catch
        {
            return false;
        }
    }
}