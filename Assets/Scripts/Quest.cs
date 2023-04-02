using System.Collections;
using System.Collections.Generic;
using System;


public class Quest 
{
    private float ttlSeconds;
    private string text;
    private Dictionary<Monsters,int> conditions;

    public Quest()
    {
        

        int totalAmount = 0;
        while (totalAmount == 0)
        {
            text = "Yelp! The heroes are attacking again!<br>";
            text += "To counter their raid, we need";
            conditions = new Dictionary<Monsters, int>();

            foreach (Monsters monster in Enum.GetValues(typeof(Monsters)))
            {
                int amount = UnityEngine.Random.Range(0, 3);
                amount = 1;
                conditions.Add(monster, amount);
                if (amount > 0)
                {
                    text += "<br>at least <b>" + 1 + "</b> " + monster.ToString();
                }
                totalAmount += amount;

            }
        }

        ttlSeconds = 0 + totalAmount * 5;
        

    }

    public string getText()
    {
        return text;
    }

    public float getTtlSeconds()
    {
        return ttlSeconds;
    }

}
