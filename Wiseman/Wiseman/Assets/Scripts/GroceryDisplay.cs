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

    public List<CollectibleData> groceries;
    

    public List<GameObject> displayedObjects;
    public List<Transform> displayedParents;
    public float displayParentDistance;

    public GameObject parent;
    public GameObject baseParent;

    public void Initialize()
    {
        displayedParents = new List<Transform>();
        displayedObjects = new List<GameObject>();
        groceries = new List<CollectibleData>();

    }

    public void AddDisplay(CollectibleData data)
    {
        groceries.Add(data);
        if(displayedParents.Count < groceries.Count)
        {
            for(int i = displayedParents.Count; i < groceries.Count; i++)
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
        groceries = new List<CollectibleData>();
        RefreshDisplays();
    }

    public void RefreshDisplays()
    {
        for(int i =0; i < displayedObjects.Count; i++)
        {
            Destroy(displayedObjects[i]);
        }

        displayedObjects = new List<GameObject>();

        for(int i = 0; i < groceries.Count; i++)
        {
            displayedObjects.Add(groceries[i].Depool());

            displayedObjects[i].transform.parent = displayedParents[i].transform;
            displayedObjects[i].transform.localPosition = Vector3.zero;
            displayedObjects[i].transform.localScale = Vector3.one;
            displayedObjects[i].transform.localEulerAngles = Vector3.zero;
            displayedObjects[i].gameObject.SetActive(true);
        }
    }
}

