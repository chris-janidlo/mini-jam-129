using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private float timer;

    private TextMeshPro description;

    private Quest quest;

    private bool alive;

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

        quest = new Quest();

    }


    public void OnEnable()
    {
        quest = new Quest();
        initDescription(quest.getText());
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && timer < quest.getTtlSeconds())
        {
            timer = timer + Time.deltaTime;
            setText(quest.getText());
        }
        else
        {
            alive = false;
            timer = 0;
            gameObject.SetActive(false);
            Debug.Log("destroy");
        }

    }


    private void setText(string questtext)
    {
        float timeLeft = quest.getTtlSeconds() - timer;

        questtext += "<br><br><align=\"center\">";

        if (timeLeft < 10)
        {
            questtext += "<color=\"red\">{0:0}</color> seconds";
        }
        else
        {
            questtext += "{0:0} seconds";
        }

        description.SetText(questtext, timeLeft);
    }




    private void initDescription(string questtext)
    {

        setText(questtext);
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
