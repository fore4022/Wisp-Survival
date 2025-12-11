using System.Collections.Generic;
using UnityEngine;
public class DamageLog_Manage
{
    public Dictionary<PoolingObject, DamageLog> damageLogs = new();

    public const string prefabName = "DamageLog";

    public bool isSet = false;

    public void Set()
    {
        foreach(PoolingObject obj in Managers.Game.objectPool.GetObjects(prefabName))
        {
            damageLogs.Add(obj, obj.GetComponent<DamageLog>());
        }

        isSet = true;
    }
    public void Show(Vector3 position, float damage)
    {
        PoolingObject obj = Managers.Game.objectPool.GetObject(prefabName);

        damageLogs[obj].SetInformation(position, damage);
        obj.SetActive(true);
    }
    public void Clear()
    {
        foreach(PoolingObject obj in damageLogs.Keys)
        {
            if(obj.activeSelf)
            {
                obj.Transform.Kill();
            }
        }
    }
}