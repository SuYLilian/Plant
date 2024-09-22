using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Item_SynthesisRoom : MonoBehaviour
{
    public int flowerNum;
    public int flowerAmount;
    public Image flowerImage;
    public TextMeshProUGUI flowerAmount_text;

    public GameObject synGridItem_prefab;
    public GameObject synFrame;
    public GameObject backpackGroup,flowerGroup;
    public SynManager synManager;

    public void Start()
    {
        synManager = SynManager.synManager;
        synFrame = SynManager.synManager.synFrame;
        backpackGroup = JasonManager.jasonManager.backpackGroup;
        flowerGroup = JasonManager.jasonManager.flowerListGroup;
    }

    public void ClickFlower()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        for (int i = 0; i < synFrame.transform.childCount; i++)
        {
            if (synFrame.transform.GetChild(i).transform.childCount < 1)
            {
                GameObject _synGridItem = Instantiate(synGridItem_prefab, synFrame.transform.GetChild(i).transform);
                _synGridItem.GetComponent<SynGrid_Item>().flowerNum = flowerNum;
                _synGridItem.GetComponent<SynGrid_Item>().flowerImage.sprite = flowerImage.sprite;
                Debug.Log("flowerNum:" + _synGridItem.GetComponent<SynGrid_Item>().flowerNum);
                synManager.HaveEmptyGrid();

                for (int j = 0; j < backpackGroup.transform.childCount; j++)
                {
                    if (backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemType == "flower" && backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemNum == flowerNum)
                    {
                        backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount--;
                        if (backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount <= 0)
                        {
                            Destroy(backpackGroup.transform.GetChild(j).gameObject);
                        }
                        else if (backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount > 0)
                        {
                            if (backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount == 1)
                            {
                                backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount_text.text = "";
                            }
                            else
                            {
                                backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount_text.text = backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemAmount.ToString();
                            }
                        }
                    }
                }
                flowerAmount--;
                if(flowerAmount<=0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    if(flowerAmount==1)
                    {
                        flowerAmount_text.text = "";
                    }
                    else if(flowerAmount>=2)
                    {
                        flowerAmount_text.text = flowerAmount.ToString();
                    }
                }


                break;
            }
        }
    }
}
