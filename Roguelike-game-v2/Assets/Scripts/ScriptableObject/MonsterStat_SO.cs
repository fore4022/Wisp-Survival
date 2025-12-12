using UnityEngine;
[CreateAssetMenu(fileName = "BasicMonsterStat", menuName = "Create New SO/Monster/Create New BasicMonsterStat_SO")]
public class MonsterStat_SO : ScriptableObject
{
    [SerializeField] private DefaultStat _stat;

    [SerializeField] private int _userExperience = 1;
    [SerializeField] private int _inGameExperience = 1;

    public DefaultStat Stat { get { return _stat; } }
    public int User_Experience { get { return _userExperience; } }
    public int InGame_Experience { get {return _inGameExperience; } }
}