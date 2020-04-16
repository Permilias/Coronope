using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorumManager : MonoBehaviour
{
    public static DecorumManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        decorumList = new List<GameObject>();
        FillDecorumPool();
    }

    public DecorumConfig config;

    List<GameObject> decorumList;

    public void FillDecorumPool()
    {
        for (int i = 0; i < config.poolAmount; i++)
        {
            GameObject newDecorum = Instantiate(config.decorumObjects[Random.Range(0, config.decorumObjects.Length)], transform);
            decorumList.Add(newDecorum);
            newDecorum.gameObject.SetActive(false);
        }
    }

    public GameObject RandomDecorum()
    {
        if (decorumList.Count < 1) FillDecorumPool();
        int index = Random.Range(0, decorumList.Count);
        GameObject returned = decorumList[index];
        decorumList.RemoveAt(index);
        return returned;
    }

    public void ReturnDecorum(GameObject decorum)
    {
        decorum.SetActive(false);
        decorumList.Add(decorum);
    }
}
