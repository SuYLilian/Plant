using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SeedListGrid : MonoBehaviour
{
    public string seedType;
    public int seedNum;
    public int seedAmount;
    public TextMeshProUGUI seedAmount_text;
    public Image seedImage;
    public GameObject[] plantItem;
    public GameObject plantGroup,backpackGroup;

    private void Awake()
    {
        plantGroup = JasonManager.jasonManager.plantGroup;
        backpackGroup = JasonManager.jasonManager.backpackGroup;
    }

    public void ClickSeedToPlant()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        for (int i=0;i<plantGroup.transform.childCount;i++)
        {
            if(plantGroup.transform.GetChild(i).transform.childCount<=0)
            {
                GameObject item=Instantiate(plantItem[seedNum], plantGroup.transform.GetChild(i).transform);
                item.GetComponent<PlantItem>().canCaculate = true;
                for(int j=0;j<backpackGroup.transform.childCount;j++)
                {
                    if(backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemType=="seed"&& backpackGroup.transform.GetChild(j).GetComponent<Item_Backpack>().itemNum==seedNum)
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
                seedAmount--;
                if(seedAmount<=0)
                {
                    Destroy(gameObject);
                }
                else if(seedAmount>0)
                {
                    if(seedAmount==1)
                    {
                        seedAmount_text.text = "";
                    }
                    else
                    {
                        seedAmount_text.text = seedAmount.ToString();
                    }
                }
                break;
            }
        }
    }
}
