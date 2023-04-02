using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuestBox : MonoBehaviour
{

    private SpriteRenderer bgLeftUp;
    private SpriteRenderer bgMiddleUpLeft;
    private SpriteRenderer bgMiddleUpRight;
    private SpriteRenderer bgRightUp;
    private SpriteRenderer bgLeftDown;
    private SpriteRenderer bgMiddleDownLeft;
    private SpriteRenderer bgMiddleDownRight;
    private SpriteRenderer bgRightDown;
    private TextMeshPro description;

    private float timer;
    private bool alive;
    public IconMap IconMap;

    private float ttlSeconds;
    private string questtext;
    private Dictionary<Monsters, int> conditions;
    public int minTime;
    public int timePerMonster;

    public void Awake()
    {
        bgLeftUp = transform.Find("Background").Find("BgLeftUp").GetComponent<SpriteRenderer>();
        bgMiddleUpLeft = transform.Find("Background").transform.Find("BgMiddleUpLeft").GetComponent<SpriteRenderer>();
        bgMiddleUpRight = transform.Find("Background").transform.Find("BgMiddleUpRight").GetComponent<SpriteRenderer>();
        bgRightUp = transform.Find("Background").transform.Find("BgRightUp").GetComponent<SpriteRenderer>();
        bgLeftDown = transform.Find("Background").transform.Find("BgLeftDown").GetComponent<SpriteRenderer>();
        bgMiddleDownLeft = transform.Find("Background").transform.Find("BgMiddleDownLeft").GetComponent<SpriteRenderer>();
        bgMiddleDownRight = transform.Find("Background").transform.Find("BgMiddleDownRight").GetComponent<SpriteRenderer>();
        bgRightDown = transform.Find("Background").transform.Find("BgRightDown").GetComponent<SpriteRenderer>();

        description = transform.Find("Questtext").GetComponent<TextMeshPro>();

        generateQuest();

    }

    private void generateQuest()
    {
        int totalAmount = 0;
        while (totalAmount == 0)
        {
            questtext = "Defend!<br>";
            conditions = new Dictionary<Monsters, int>();

            foreach (Monsters monster in Enum.GetValues(typeof(Monsters)))
            {
                int amount = UnityEngine.Random.Range(0, 3);
                amount = 1;
                conditions.Add(monster, amount);
                //Debug.Log(IconMap.GetSprite(monster));
                if (amount > 0)
                {
                    string monsterstring = IconMap.GetSprite(monster).ToString();

                    questtext += "<nobr><b>" + 1 + "</b> <size=10px><sprite=\"tilemap\" name=\"" +
                        monsterstring.Substring(0, monsterstring.IndexOf(" ")) + "\"></nobr></size> ";
                }
                totalAmount += amount;

            }
        }

        ttlSeconds = minTime + totalAmount * timePerMonster;
    }

    public void OnEnable()
    {
        generateQuest();
        initDescription();
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && timer < ttlSeconds)
        {
            timer = timer + Time.deltaTime;
            setText();
        }
        else
        {
            alive = false;
            timer = 0;
            gameObject.SetActive(false);
            Debug.Log("destroy");
        }

    }


    private void setText()
    {
        float timeLeft = ttlSeconds - timer;

        string text = questtext + "<br>";

        if (timeLeft < 10)
        {
            text += "<color=\"red\">{0:0}</color> seconds";
        }
        else
        {
            text += "{0:0} seconds";
        }

        description.SetText(text, timeLeft);
    }




    private void initDescription()
    {

        setText();
        description.ForceMeshUpdate();

        Vector2 descriptionSize = description.GetRenderedValues(false);
        Vector2 padding = new Vector2(0f, 0.5f);
        descriptionSize.x = 1;
        descriptionSize.y = descriptionSize.y / 2;

        float bgSize = bgLeftUp.size.y;

        bgLeftUp.size = descriptionSize + padding;
        bgMiddleUpLeft.size = descriptionSize + padding;
        bgMiddleUpRight.size = descriptionSize + padding;
        bgRightUp.size = descriptionSize + padding;
        bgLeftDown.size = descriptionSize + padding;
        bgMiddleDownLeft.size = descriptionSize + padding;
        bgMiddleDownRight.size = descriptionSize + padding;
        bgRightDown.size = descriptionSize + padding;

        float top = bgLeftUp.transform.position.y;
        
        bgLeftDown.transform.position = new Vector2(bgLeftDown.transform.position.x, top - descriptionSize.y - padding.y);
        bgMiddleDownLeft.transform.position = new Vector2(bgMiddleDownLeft.transform.position.x, top - descriptionSize.y - padding.y);
        bgMiddleDownRight.transform.position = new Vector2(bgMiddleDownRight.transform.position.x, top - descriptionSize.y - padding.y);
        bgRightDown.transform.position = new Vector2(bgRightDown.transform.position.x, top - descriptionSize.y - padding.y);

        float bottom = bgLeftDown.transform.position.y;

        float scale = Mathf.Max(1, bgLeftUp.size.y / bgSize);

        description.transform.position = new Vector2(description.transform.position.x, top - (top - bottom)/2 + descriptionSize.y - padding.y/2);

    }

}
