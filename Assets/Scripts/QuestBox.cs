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
    private float ttlSeconds;

    private TextMeshPro text;

    public void Awake()
    {
        bgLeftUp = transform.Find("Background").Find("BgLeftUp").GetComponent<SpriteRenderer>();
        bgMiddleUp = transform.Find("Background").transform.Find("BgMiddleUp").GetComponent<SpriteRenderer>();
        bgRightUp = transform.Find("Background").transform.Find("BgRightUp").GetComponent<SpriteRenderer>();
        bgLeftDown = transform.Find("Background").transform.Find("BgLeftDown").GetComponent<SpriteRenderer>();
        bgMiddleDown = transform.Find("Background").transform.Find("BgMiddleDown").GetComponent<SpriteRenderer>();
        bgRightDown = transform.Find("Background").transform.Find("BgRightDown").GetComponent<SpriteRenderer>();

        text = transform.Find("Questtext").GetComponent<TextMeshPro>();

        timer = 0;
        ttlSeconds = 30;
    }


    public void Start()
    {
        setText("this is a sample text but it will grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow and grow");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < ttlSeconds)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void setText(string questtext)
    {
        text.SetText(questtext);
        text.ForceMeshUpdate();
        Vector2 textSize = text.GetRenderedValues(false);
        Vector2 padding = new Vector2(0f, 0.25f);
        textSize.x = 1;
        textSize.y = textSize.y / 2;

        float bgSize = bgLeftUp.size.y;

        bgLeftUp.size = textSize + padding;
        bgMiddleUp.size = textSize + padding;
        bgRightUp.size = textSize + padding;
        bgLeftDown.size = textSize + padding;
        bgMiddleDown.size = textSize + padding;
        bgRightDown.size = textSize + padding;

        float top = bgLeftUp.transform.position.y;
        

        bgLeftDown.transform.position = new Vector2(bgLeftDown.transform.position.x, top - textSize.y - padding.y);
        bgMiddleDown.transform.position = new Vector2(bgMiddleDown.transform.position.x, top - textSize.y - padding.y);
        bgRightDown.transform.position = new Vector2(bgRightDown.transform.position.x, top - textSize.y - padding.y);

        float bottom = bgLeftDown.transform.position.y;

        //Debug.Log(top);
        //Debug.Log(bottom);
        //Debug.Log(textSize.y);

        //Debug.Log(top - (top - bottom) / 2);

        //Debug.Log(bgLeftUp.size.y / bgSize);

        float scale = Mathf.Max(1, bgLeftUp.size.y / bgSize);

        //Debug.Log(scale);

        text.transform.position = new Vector2(text.transform.position.x, top - (top - bottom)/2 + textSize.y - padding.y * scale);

    }

}
