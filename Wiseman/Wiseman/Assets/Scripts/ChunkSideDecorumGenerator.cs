using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSideDecorumGenerator : MonoBehaviour
{
    Transform leftParent, rightParent;

    List<GameObject> currentObjects;
    public void Awake()
    {
        leftParent = new GameObject("Chunk_SideDecorum_LeftParent").transform;
        leftParent.parent = transform;
        leftParent.localPosition = new Vector3(-DecorumManager.Instance.config.decorumDistanceFromCenter, 0, 0);

        rightParent = new GameObject("Chunk_SideDecorum_RightParent").transform;
        rightParent.parent = transform;
        rightParent.localPosition = new Vector3(DecorumManager.Instance.config.decorumDistanceFromCenter, 0, 0);
        rightParent.transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Initialize()
    {
        currentObjects = new List<GameObject>();
        float currentDistance = 0f;
        while(currentDistance < ChunkManager.Instance.config.chunkSize)
        {
            GameObject newDecorum = DecorumManager.Instance.RandomDecorum();
            currentObjects.Add(newDecorum);
            newDecorum.transform.parent = leftParent;
            newDecorum.transform.localScale = Vector3.one;
            newDecorum.transform.localPosition = Vector3.zero + new Vector3(0, 0, currentDistance);
            newDecorum.gameObject.SetActive(true);

            newDecorum = DecorumManager.Instance.RandomDecorum();
            currentObjects.Add(newDecorum);
            newDecorum.transform.parent = rightParent;
            newDecorum.transform.localScale = Vector3.one;
            newDecorum.transform.localPosition = Vector3.zero + new Vector3(0, 0, currentDistance);
            newDecorum.gameObject.SetActive(true);

            currentDistance += DecorumManager.Instance.config.decorumDistanceFromEachOther;
        }
    }

    public void Deactivate()
    {
        for (int i = 0; i < currentObjects.Count; i++)
        {
            DecorumManager.Instance.ReturnDecorum(currentObjects[i]);
        }
    }
}
