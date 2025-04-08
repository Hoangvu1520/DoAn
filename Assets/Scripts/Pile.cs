using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pile : MonoBehaviour
{
    public List<DataPile> piles;
    public Transform firstPost;
    public List<Nuts> nutList;
    private bool isCompleted;
    private void Start()
    {
        nutList = piles.Where(p => p.nuts != null)
                   .Select(p => p.nuts)
                   .ToList();
    }
    private void Update()
    {
        for (int i = 0; i < piles.Count; i++)
        {
            if (i < nutList.Count)
            {
                piles[i].nuts = nutList[i]; 
            }
            else
            {
                piles[i].nuts = null; 
            }
        }
        nutList = piles.Where(p => p.nuts != null)
                   .Select(p => p.nuts)
                   .ToList();
    }
    public void AddNut(Nuts nut)
    {
        nutList.Add(nut);
    }
    public void RemoveNut()
    {
        //nutList.RemoveAll(item => item.id == nutList[nutList.Count - 1].id);
        nutList.RemoveAt(nutList.Count - 1);
    }
    public Nuts GetNut()
    {
        for (int i = piles.Count - 1; i >= 0; i--)
        {
            if (piles[i].nuts != null)
            {
                return piles[i].nuts;

            }
        }
        return null;
    }
    public Nuts GetNutWhenPileNull() // check if pile list is empty
    {
                return piles[0].nuts;
    }
    public Transform GetPostData(Nuts param)
    {
        for (int i = piles.Count - 1; i >= 0; i--)
        {
            if (piles[i].nuts == param)
            {
                return piles[i].post;

            }
        }
        return null;
    }


    public DataPile GetTargetPile() //get the top null
    {
        for(int i = 0; i< piles.Count; i++)
        {
            if (piles[i].nuts == null)
            {
                return piles[i];
            }
        }
        return null;
    }
}

[System.Serializable]
public class DataPile
{
    public Nuts nuts;
    public Transform post;
}

