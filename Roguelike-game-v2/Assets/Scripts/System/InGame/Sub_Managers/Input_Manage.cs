using System.Collections.Generic;
using UnityEngine.InputSystem;
public static class Input_Manage
{
    public static List<IInputActionCollection> inputActionList = new();

    public static T CreateAndGetInputAction<T>() where T : IInputActionCollection, new()
    {
        if(GetInputAction(out T inputAction) != null)
        {
            return inputAction;
        }
        else
        {
            T newInputAction = new();

            inputActionList.Add(newInputAction);

            return newInputAction;
        }
    }
    public static T GetInputAction<T>() where T : IInputActionCollection, new()
    {
        IInputActionCollection instance = inputActionList.Find(input => input.GetType() == typeof(T));

        bool isInclude = instance != null;

        if(inputActionList.Count == 0)
        {
            Managers.Scene.onLoad -= ClearActions;
            Managers.Scene.onLoad += ClearActions;
        }

        if(isInclude)
        {
            return (T)instance;
        }

        return default;
    }
    public static T GetInputAction<T>(out T inputAction) where T : IInputActionCollection, new()
    {
        inputAction = GetInputAction<T>();

        return inputAction;
    }
    public static void EnableInputAction<T>() where T : IInputActionCollection, new()
    {
        if(GetInputAction(out T inputAction) != null)
        {
            inputAction.Enable();
        }
    }
    public static void DisableInputAction<T>() where T : IInputActionCollection, new()
    {
        if(GetInputAction(out T inputAction) != null)
        {
            inputAction.Disable();
        }
    }
    public static void ClearActions()
    {
        foreach(IInputActionCollection act in inputActionList)
        {
            act.Disable();
        }

        inputActionList = new();

        Managers.Scene.onLoad -= ClearActions;
    }
}