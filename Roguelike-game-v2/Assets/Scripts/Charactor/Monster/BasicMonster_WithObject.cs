/// <summary>
/// so를 통해서 추가적인 객체를 사용하는, 플레이어를 향해서 이동하는 몬스터
/// </summary>
public class BasicMonster_WithObject : BasicMonster
{
    protected MonsterStat_WithObject_SO monsterSO { get { return _monsterSO as MonsterStat_WithObject_SO; } }
}