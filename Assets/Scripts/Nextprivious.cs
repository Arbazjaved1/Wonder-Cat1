using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nextprivious : MonoBehaviour
{
    public int maxpage;
    int currentpage;
    Vector3 targetpos;
    public Vector3 pagestep;
    public RectTransform LevelSelection;
    public float tweentime;
    public LeanTweenType tweentype;
    // Start is called before the first frame update

    private void Awake()
    {
        currentpage = 1;
        targetpos = LevelSelection.localPosition;
    }
    public void Next()
    {
        if(currentpage < maxpage)
        {
            currentpage++;
            targetpos += pagestep;
            MovePage();
        }
    }
    public void Privious()
    {
        if(currentpage > 1)
        {
            currentpage--;
            targetpos -= pagestep;
            MovePage();
        }
    }
    void MovePage()
    {
        LevelSelection.LeanMoveLocal(targetpos, tweentime).setEase(tweentype);
    }
}
