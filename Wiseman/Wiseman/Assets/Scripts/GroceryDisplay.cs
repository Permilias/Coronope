using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryDisplay : MonoBehaviour
{
    public static GroceryDisplay Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<CollectibleData> displayedGroceries;
    public List<GameObject> displayedObjects;
    public List<Transform> displayedParents;
    public float displayParentDistance;

    public GameObject parent;
    public GameObject baseParent;

    public void Initialize()
    {
        displayedParents = new List<Transform>();
        displayedObjects = new List<GameObject>();
        displayedGroceries = new List<CollectibleData>();
    }

    public void AddDisplay(CollectibleData data)
    {
        displayedGroceries.Add(data);
        if(displayedParents.Count < displayedGroceries.Count)
        {
            for(int i = displayedParents.Count; i < displayedGroceries.Count; i++)
            {
                displayedParents.Add(Instantiate(baseParent, transform).transform);
                displayedParents[i].parent = parent.transform;
                displayedParents[i].localPosition += new Vector3(i * displayParentDistance, 0, 0);
            }
        }
        RefreshDisplays();
    }

    public void RemoveAllDisplays()
    {
        displayedGroceries = new List<CollectibleData>();
        RefreshDisplays();
    }

    public void RefreshDisplays()
    {
        int count = 0;
        for(int i = 0; i < displayedGroceries.Count; i++)
        {
            if(displayedObjects.Count <= i)
            {
                displayedObjects.Add(displayedGroceries[i].Depool());
            }

            count++;
        }

        int displayedCount = displayedObjects.Count;
        if (displayedCount > count)
        {
            for(int i = displayedCount-1; i >= count; i--)
            {
                displayedObjects.RemoveAt(i);
            }
        }

        if (displayedCount <= 0) return;

        for(int i = 0; i < displayedObjects.Count; i++)
        {
            displayedObjects[i].transform.parent = displayedParents[i].transform;
            displayedObjects[i].transform.localPosition = Vector3.zero;
            displayedObjects[i].transform.localScale = Vector3.one;
            displayedObjects[i].transform.localEulerAngles = Vector3.zero;
            displayedObjects[i].gameObject.SetActive(true);
        }
    }
}

