using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ExpandShrinkLevels : MonoBehaviour
{

    public LayoutElement layoutElement;

    public Sprite spriteMenuNormal;
    public Sprite spriteMenuExpanded;

    public Image headerImage;
    public LayoutElement headerLayoutElement;

    public bool expanded;
    public bool finishedAnim = true;
    public bool smallList;

    void Start()
    {
        
    }
    public void MoveButtons(bool movement)
    {
        
        if (movement && finishedAnim)
        {
            finishedAnim = false;
            headerImage.sprite = spriteMenuExpanded;
            headerLayoutElement.preferredHeight = 70;

            if (smallList) layoutElement.DOPreferredSize(new Vector2(570, 200), 0.5f).OnComplete(() =>{
                finishedAnim = true;
                expanded = true;
            });
            else layoutElement.DOPreferredSize(new Vector2(570, 400), 0.5f).OnComplete(() => {
                finishedAnim = true;
                expanded = true;
            });
        }
        else if (!movement && finishedAnim)
        {
            finishedAnim = false;
            headerImage.sprite = spriteMenuNormal;
            headerLayoutElement.preferredHeight = 86;

            layoutElement.DOPreferredSize(new Vector2(570, 0), 0.5f).OnComplete(() => {
                finishedAnim = true;
                expanded = false;
            }); ;
        }
    }
    // Update is called once per frame
    /*public IEnumerator LerpLayout(bool expand)
    {
        layoutElement.DOPreferredSize(new Vector2(570,400), 2);
        layoutElement.preferredHeight = 200 * lerpValue;
        yield return new WaitForEndOfFrame();
    }*/
}
