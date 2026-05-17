using UnityEngine;
[CreateAssetMenu(fileName = "IconSprite", menuName = "Create New SO/Game Stage/Create New IconSprite_SO")]
public class IconSO : ScriptableObject
{
    [SerializeField] private Sprite _ground;
    [SerializeField] private Sprite _cover;
    [SerializeField] private Sprite _banner;
    [SerializeField] private Sprite _monster;

    public Sprite Ground { get { return _ground; } }
    public Sprite Cover { get { return _cover; } }
    public Sprite Banner { get { return _banner; } }
    public Sprite Monster { get { return _monster; } }
}