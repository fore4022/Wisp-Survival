using UnityEngine;
[CreateAssetMenu(fileName = "IconSprite", menuName = "Create New SO/Game Stage/Create New IconSprite_SO")]
public class Icon_SO : ScriptableObject
{
    [SerializeField] private Sprite ground;
    [SerializeField] private Sprite cover;
    [SerializeField] private Sprite banner;
    [SerializeField] private Sprite monster;

    public Sprite Ground { get { return ground; } }
    public Sprite Cover { get { return cover; } }
    public Sprite Banner { get { return banner; } }
    public Sprite Monster { get { return monster; } }
}