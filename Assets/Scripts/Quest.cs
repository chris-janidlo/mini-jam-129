using System.Collections;
using System.Collections.Generic;
using System;


public class Quest 
{
    private float ttlSeconds;
    private string text;
    private Dictionary<Heros,int> conditions;

    public Quest()
    {
        conditions = new Dictionary<Heros, int>();

        int totalAmount = 0;
        while (totalAmount == 0)
        {
            text = "Yelp! The heros are attacking again!<br>";
            text += "To counter their raid, we need";

            foreach (Heros hero in Enum.GetValues(typeof(Heros)))
            {
                int amount = UnityEngine.Random.Range(0, 3);
                conditions.Add(hero, amount);
                if (amount > 0)
                {
                    text += "<br>at least " + 1 + " " + hero.ToString();
                }
                totalAmount += amount;

            }
        }

        ttlSeconds = 25 * totalAmount * 5;
        

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
