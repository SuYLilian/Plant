using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynGrid_Item : MonoBehaviour
{
    public int flowerNum;
    public Image flowerImage;

    public GameObject backpackGroup, flowerGroup;
    public GameObject backpackItem_prefab, synFlowerListItem_prefab;

    public SynManager synManager;

    private void Awake()
    {
        backpackGroup = JasonManager.jasonManager.backpackGroup;
        flowerGroup = JasonManager.jasonManager.flowerListGroup;
        synManager = SynManager.synManager;
    }

    public void ClickSynGridFlower()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        bool haveFlowerInBackpack = false;
        GameObject flowerGrid_t=null;
        GameObject flowerGrid_flowerGroup = null;

        for (int i = 0; i < backpackGroup.transform.childCount; i++)
        {
            if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemType == "flower")
            {
                if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemNum == flowerNum)
                {
                    flowerGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                    haveFlowerInBackpack = true;
                    break;
                }
            }
            else
            {
                //propsGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                haveFlowerInBackpack = false;

            }
        }

        if (haveFlowerInBackpack)
        {
            //在背包新增物件數量
            flowerGrid_t.GetComponent<Item_Backpack>().itemAmount++;
            if (flowerGrid_t.GetComponent<Item_Backpack>().itemAmount >= 2)
            {
                flowerGrid_t.GetComponent<Item_Backpack>().itemAmount_text.text = flowerGrid_t.GetComponent<Item_Backpack>().itemAmount.ToString();
            }
            for(int i=0;i<flowerGroup.transform.childCount;i++)
            {
                if(flowerGroup.transform.GetChild(i).GetComponent<Item_SynthesisRoom>().flowerNum==flowerNum)
                {
                    flowerGrid_flowerGroup = flowerGroup.transform.GetChild(i).gameObject;
                    flowerGrid_flowerGroup.GetComponent<Item_SynthesisRoom>().flowerAmount++;
                    if(flowerGrid_flowerGroup.GetComponent<Item_SynthesisRoom>().flowerAmount>=2)
                    {
                        flowerGrid_flowerGroup.GetComponent<Item_SynthesisRoom>().flowerAmount_text.text = flowerGrid_flowerGroup.GetComponent<Item_SynthesisRoom>().flowerAmount.ToString();
                    }
                    break;
                }                
            }
        }
        else
        {
            GameObject flower_backpack = Instantiate(backpackItem_prefab, backpackGroup.transform);//在背包新增物件
            flower_backpack.GetComponent<Item_Backpack>().itemType = "flower";
            flower_backpack.GetComponent<Item_Backpack>().itemNum = flowerNum;
            flower_backpack.GetComponent<Item_Backpack>().itemAmount++;
            flower_backpack.GetComponent<Item_Backpack>().itemAmount_text.text = "";
            flower_backpack.GetComponent<Item_Backpack>().itemImage.sprite = flowerImage.sprite;

            GameObject flower_synList= Instantiate(synFlowerListItem_prefab, flowerGroup.transform);//在合成室列表新增花
            flower_synList.GetComponent<Item_SynthesisRoom>().flowerNum = flowerNum;
            flower_synList.GetComponent<Item_SynthesisRoom>().flowerAmount++;
            flower_synList.GetComponent<Item_SynthesisRoom>().flowerAmount_text.text = "";
            flower_synList.GetComponent<Item_SynthesisRoom>().flowerImage.sprite = flowerImage.sprite;
        }
        synManager.rate_text.text = "";
        Destroy(gameObject);
    }
}

