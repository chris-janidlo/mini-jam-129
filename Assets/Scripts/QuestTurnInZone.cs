using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTurnInZone : MonoBehaviour
{
    public QuestBox QuestBox;

    public void TurnInQuest(List<Monsters> monsters)
    {
        QuestBox.CheckQuest(monsters);
    }
}
