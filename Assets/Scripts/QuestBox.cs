using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBox : MonoBehaviour
{

    private SpriteRenderer bgLeftUp;
    private SpriteRenderer bgMiddleUp;
    private SpriteRenderer bgRightUp;
    private SpriteRenderer bgLeftDown;
    private SpriteRenderer bgMiddleDown;
    private SpriteRenderer bgRightDown;
    private float timer;

    private TextMeshPro description;

    private Quest quest;

    public void Awake()
    {
        bgLeftUp = transform.Find("Background").Find("BgLeftUp").GetComponent<SpriteRenderer>();
        bgMiddleUp = transform.Find("Background").transform.Find("BgMiddleUp").GetComponent<SpriteRenderer>();
        bgRightUp = transform.Find("Background").transform.Find("BgRightUp").GetComponent<SpriteRenderer>();
        bgLeftDown = transform.Find("Background").transform.Find("BgLeftDown").GetComponent<SpriteRenderer>();
        bgMiddleDown = transform.Find("Background").transform.Find("BgMiddleDown").GetComponent<SpriteRenderer>();
        bgRightDown = transform.Find("Background").transform.Find("BgRightDown").GetComponent<SpriteRenderer>();

        description = transform.Find("Questtext").GetComponent<TextMeshPro>();

        quest = new Quest();

    }


    public void Start()
    {
        initDescription(quest.getText());
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < quest.getTtlSeconds())
        {
            timer = timer + Time.deltaTime;
            setText(quest.getText());
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("destroy");
        }

    }


    private void setText(string questtext)
    {
        float timeLeft = quest.getTtlSeconds() - timer;

        questtext += "<br><br><align=\"center\">Time remaining: ";

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
        Vector2 padding = new Vector2(0f, 0.25f);
        descriptionSize.x = 1;
        descriptionSize.y = descriptionSize.y / 2;

        float bgSize = bgLeftUp.size.y;

        bgLeftUp.size = descriptionSize + padding;
        bgMiddleUp.size = descriptionSize + padding;
        bgRightUp.size = descriptionSize + padding;
        bgLeftDown.size = descriptionSize + padding;
        bgMiddleDown.size = descriptionSize + padding;
        bgRightDown.size = descriptionSize + padding;

        float top = bgLeftUp.transform.position.y;
        
        bgLeftDown.transform.position = new Vector2(bgLeftDown.transform.position.x, top - descriptionSize.y - padding.y);
        bgMiddleDown.transform.position = new Vector2(bgMiddleDown.transform.position.x, top - descriptionSize.y - padding.y);
        bgRightDown.transform.position = new Vector2(bgRightDown.transform.position.x, top - descriptionSize.y - padding.y);

        float bottom = bgLeftDown.transform.position.y;

        float scale = Mathf.Max(1, bgLeftUp.size.y / bgSize);

        description.transform.position = new Vector2(description.transform.position.x, top - (top - bottom)/2 + descriptionSize.y - padding.y * scale);

    }

}
