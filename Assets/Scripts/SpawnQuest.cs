using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class SpawnQuest : MonoBehaviour
{

    private List<GameObject> quests;


    public float spawnRate;
    public IntVariable difficulty;

    private float timer = 0;

    public void Awake()
    {
        quests = new List<GameObject>();

        quests.Add(transform.Find("QuestBoxTop").gameObject);
        quests.Add(transform.Find("QuestBoxLeft").gameObject);
        quests.Add(transform.Find("QuestBoxBottom").gameObject);
        quests.Add(transform.Find("QuestBoxRight").gameObject);

    }

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
            
            spawnQuest();
            
            //Debug.Log("Fire");
            timer = 0;
        }

    }
    [ContextMenu("Quest")]
    void spawnQuest()
    {

        List<int> ind = new List<int>();

        for(int i = 0; i < 4; i++)
        {
            if (!quests[i].activeSelf)
            {
                ind.Add(i);
            }
        }

        if(ind.Count > 4 - difficulty.Value)
        {
            GameObject q = quests[ind[Random.Range(0, ind.Count)]];
            q.SetActive(true);
        }
        


    }
}
