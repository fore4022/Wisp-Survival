using UnityEngine;
[CreateAssetMenu(fileName = "BasicMonsterStat", menuName = "Create New SO/Monster/Create New BasicMonsterStat_SO")]
public class MonsterStat_SO : ScriptableObject
{
    [SerializeField] private DefaultStat stat;

    [SerializeField] private int user_Experience = 1;
    [SerializeField] private int inGame_Experience = 1;

    public DefaultStat Stat { get { return stat; } }
    public int User_Experience { get { return user_Experience; } }
    public int InGame_Experience { get {return inGame_Experience; } }
}