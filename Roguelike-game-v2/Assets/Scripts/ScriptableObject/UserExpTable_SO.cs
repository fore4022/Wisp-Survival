using UnityEngine;
[CreateAssetMenu(fileName = "UserExpTable", menuName = "Create New SO/Create New UserExpTable_SO")]
public class UserExpTable_SO : ScriptableObject
{
    [SerializeField] private int[] requiredEXP = new int[GameUtil.maxLevel - 1];

    public int[] RequiredEXP { get { return requiredEXP; } }
    // 유저 경험치 표의 항목 수가 maxLevel과 같도록 유지
    private void OnValidate()
    {
        ArrayUtil.ResizeArray(ref requiredEXP, GameUtil.maxLevel - 1);
    }
}