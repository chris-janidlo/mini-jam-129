using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

[CreateAssetMenu(fileName = "newIconMap.asset", menuName = "Icon Map")]
public class IconMap : ScriptableObject
{
    public EnumMap<Monsters, Sprite> Map;

    public Sprite GetSprite(Monsters monster)
    {
        return Map[monster];
    }
}
