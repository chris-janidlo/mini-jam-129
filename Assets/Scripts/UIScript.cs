using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityAtoms.BaseAtoms;
using UnityEngine.SceneManagement;


public class UIScript : MonoBehaviour
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

    private int maxLife;

    public IntVariable gold;
    public IntVariable life;

    private int oldLife;
    private int oldGold;


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

        description = transform.Find("UIText").GetComponent<TextMeshPro>();

        oldGold = gold.Value;
        oldLife = life.Value;
        maxLife = life.Value;

        initUI();


    }

    // Update is called once per frame
    void Update()
    {
        if(life.Value < 1)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (gold.Value != oldGold || life.Value != oldLife)
        {
            updateUI();
        }

    }


    private void updateUI()
    {

        

        string text = "Lives <br><size=10px>";

        for(int i = 1; i <= maxLife; i++)
        {
            if(i <= life.Value)
            {
                text += "<sprite=\"tilemap\" name=\"tilemap_114\">";
            }
            else
            {
                text += "<sprite=\"tilemap\" name=\"tilemap_64\">";
            }
        }
        text += "</size><br>Gold<br>";
        text += "<size=10px><sprite=\"tilemap\" name=\"tilemap_89\"></size>";
        text += " <b>" + gold.Value + "</b>";

        // uesttext += "<nobr><b>" + 1 + "</b>  "; 
        description.SetText(text);

        oldGold = gold.Value;
        oldLife = life.Value;
    }




    private void initUI()
    {

        updateUI();
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

        description.transform.position = new Vector2(description.transform.position.x, top - (top - bottom) / 2 + descriptionSize.y - padding.y / 2);

    }

    
}
