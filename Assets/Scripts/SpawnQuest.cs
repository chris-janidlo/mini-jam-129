using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuest : MonoBehaviour
{

    public GameObject quest;
    public float spawnRate;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.childCount);
        spawnQuest();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
            //Debug.Log(timer);
        }
        else
        {
            if(transform.childCount < 5)
            {
                spawnQuest();
            }
            //Debug.Log("Fire");
            timer = 0;
        }

    }
    [ContextMenu("Quest")]
    void spawnQuest()
    {
        
        GameObject q = Instantiate(quest, new Vector3(0, 0, 0), transform.rotation);
        q.transform.SetParent(transform);
        


    }
}
