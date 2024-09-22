using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedStore : MonoBehaviour
{
    public Image seedImage;
    public int seedIndex = 0;
    //public string itemType;
    public Sprite[] seedSprites;
    public Sprite[] seedIntroSprites;
    public GameObject seedStoreCanvas;
    public int[] seedPrice;
    public GameObject backpackItem_prefab, seedList_prefab;
    public GameObject backpackGroup, seedListGroup;
    public GameObject seedGroup;
    public float seedGroudMovingDistance;
    public TextMeshProUGUI laboratoryCoin_text, storeCoin_text;

    public Animator[] storeSeed_ani;

    public GameObject seedStorePanel, propsStorePanel;

    public int propsIndex;
    public Image propsImage;
    public Sprite[] propsSprites;
    public Sprite[] propsIntroSprites;
    public int[] propsPrice;
    public Animator[] storeProps_ani;

    public Animator seedTitle_ani, propsTitle_ani,buy_seed_ani,buy_props_ani;

    public bool propsTitleIsShow=false; 
    public void ClickNextButton()
    {
        if (seedIndex < 2)
        {
            seedGroup.transform.localPosition -= new Vector3(seedGroudMovingDistance, 0, 0);
            seedIndex++;
            seedImage.sprite = seedSprites[seedIndex];
        }
    }
    public void ClickPreviousButton()
    {
        if (seedIndex > 0)
        {
            seedGroup.transform.localPosition += new Vector3(seedGroudMovingDistance, 0, 0);
            seedIndex--;
            seedImage.sprite = seedSprites[seedIndex];
        }
    }
    public void ClickBackButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        seedStoreCanvas.SetActive(false);
    }
    public void ClickStoreSeed(int seedNum)
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        //itemType = "seed";
        seedIndex = seedNum;
        seedImage.sprite = seedIntroSprites[seedIndex];
        for (int i=0;i<storeSeed_ani.Length;i++)
        {
            if(i!=seedNum)
            {
                storeSeed_ani[i].SetBool("ClickItem",false);
            }
            else
            {
                storeSeed_ani[i].SetBool("ClickItem", true);
            }
        }
    }

    public void ClickBuyButton_Seed()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        Debug.Log("buy");

        if (JasonManager.jasonManager.coinNum >= seedPrice[seedIndex])
        {
            buy_seed_ani.SetTrigger("isBuying");
            JasonManager.jasonManager.coinNum -= seedPrice[seedIndex];
            laboratoryCoin_text.text = JasonManager.jasonManager.coinNum.ToString();
            storeCoin_text.text = JasonManager.jasonManager.coinNum.ToString();
            bool haveSeedInBackpack = false;
            GameObject seedGrid_t = null;
            for (int i = 0; i < backpackGroup.transform.childCount; i++)
            {
                if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemType == "seed")
                {
                    if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemNum == seedIndex)
                    {
                        seedGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                        haveSeedInBackpack = true;
                        break;
                    }
                }
                else
                {
                    seedGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                    haveSeedInBackpack = false;

                }
            }

            if (haveSeedInBackpack)
            {
                //在背包新增物件數量
                seedGrid_t.GetComponent<Item_Backpack>().itemAmount++;
                if (seedGrid_t.GetComponent<Item_Backpack>().itemAmount >= 2)
                {
                    seedGrid_t.GetComponent<Item_Backpack>().itemAmount_text.text = seedGrid_t.GetComponent<Item_Backpack>().itemAmount.ToString();
                }
                for(int i=0;i<seedListGroup.transform.childCount;i++)
                {
                    if(seedListGroup.transform.GetChild(i).GetComponent<SeedListGrid>().seedNum==seedIndex)
                    {
                        seedListGroup.transform.GetChild(i).GetComponent<SeedListGrid>().seedAmount++;
                        if (seedListGroup.transform.GetChild(i).GetComponent<SeedListGrid>().seedAmount >= 2)
                        {
                            seedListGroup.transform.GetChild(i).GetComponent<SeedListGrid>().seedAmount_text.text = seedListGroup.transform.GetChild(i).GetComponent<SeedListGrid>().seedAmount.ToString();
                        }
                        break;
                    }
                }

            }
            else
            {
                GameObject seed_backpack = Instantiate(backpackItem_prefab, backpackGroup.transform);//在背包新增物件
                seed_backpack.GetComponent<Item_Backpack>().itemType = "seed";
                seed_backpack.GetComponent<Item_Backpack>().itemNum = seedIndex;
                seed_backpack.GetComponent<Item_Backpack>().itemAmount++;
                seed_backpack.GetComponent<Item_Backpack>().itemAmount_text.text = "";
                seed_backpack.GetComponent<Item_Backpack>().itemImage.sprite = seedSprites[seedIndex];
                GameObject seed_seedList = Instantiate(seedList_prefab,seedListGroup.transform);//在種子列表新增物件
                seed_seedList.GetComponent<SeedListGrid>().seedType = "seed";
                seed_seedList.GetComponent<SeedListGrid>().seedNum = seedIndex;
                seed_seedList.GetComponent<SeedListGrid>().seedAmount++;
                seed_seedList.GetComponent<SeedListGrid>().seedAmount_text.text = "";
                seed_seedList.GetComponent<SeedListGrid>().seedImage.sprite = seedSprites[seedIndex];
            }
        }

    }
    public void ClickBuyButton_Props()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        if (JasonManager.jasonManager.coinNum >= propsPrice[propsIndex])
        {
            buy_props_ani.SetTrigger("isBuying");
            JasonManager.jasonManager.coinNum -= propsPrice[propsIndex];
            laboratoryCoin_text.text = JasonManager.jasonManager.coinNum.ToString();
            storeCoin_text.text = JasonManager.jasonManager.coinNum.ToString();
            bool havePropsInBackpack = false;
            GameObject propsGrid_t = null;
            if(propsIndex==0)
            {
                JasonManager.jasonManager.waterPropsNum++;
            }
            for (int i = 0; i < backpackGroup.transform.childCount; i++)
            {
                if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemType == "props")
                {
                    if (backpackGroup.transform.GetChild(i).GetComponent<Item_Backpack>().itemNum == propsIndex)
                    {
                        propsGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                        havePropsInBackpack = true;
                        break;
                    }
                }
                else
                {
                    //propsGrid_t = backpackGroup.transform.GetChild(i).gameObject;
                    havePropsInBackpack = false;

                }
            }

            if (havePropsInBackpack)
            {
                //在背包新增物件數量
                propsGrid_t.GetComponent<Item_Backpack>().itemAmount++;
                if (propsGrid_t.GetComponent<Item_Backpack>().itemAmount >= 2)
                {
                    propsGrid_t.GetComponent<Item_Backpack>().itemAmount_text.text = propsGrid_t.GetComponent<Item_Backpack>().itemAmount.ToString();
                }

            }
            else
            {
                GameObject props_backpack = Instantiate(backpackItem_prefab, backpackGroup.transform);//在背包新增物件
                props_backpack.GetComponent<Item_Backpack>().itemType = "props";
                props_backpack.GetComponent<Item_Backpack>().itemNum = propsIndex;
                props_backpack.GetComponent<Item_Backpack>().itemAmount++;
                props_backpack.GetComponent<Item_Backpack>().itemAmount_text.text = "";
                props_backpack.GetComponent<Item_Backpack>().itemImage.sprite = propsSprites[propsIndex];
        
            }
        }
    }
    public void ClickStoreProps(int propsNum)
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        propsIndex = propsNum;
        propsImage.sprite = propsIntroSprites[propsIndex];
        for (int i = 0; i < storeProps_ani.Length; i++)
        {
            if (i != propsNum)
            {
                storeProps_ani[i].SetBool("ClickItem", false);
            }
            else
            {
                storeProps_ani[i].SetBool("ClickItem", true);
            }
        }
    }
    public void ClickSeedStoreTypeButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        propsTitleIsShow = false;
        int order = seedTitle_ani.gameObject.transform.GetSiblingIndex();
        seedTitle_ani.gameObject.transform.SetSiblingIndex(order + 1);
        seedTitle_ani.SetBool("isShow", true);
        seedStorePanel.SetActive(true);
        for (int i = 0; i < storeSeed_ani.Length; i++)
        {
            if (i != seedIndex)
            {
                storeSeed_ani[i].SetBool("ClickItem", false);
            }
            else
            {
                storeSeed_ani[i].SetBool("ClickItem", true);
            }
        }
        propsStorePanel.SetActive(false);
    }
    public void ClickPropsStoreTypeButton()
    {
        FindObjectOfType<SoundManager>().PlayClip_BGM(FindObjectOfType<SoundManager>().seClips[0]);

        propsTitleIsShow = true;
        int order = propsTitle_ani.gameObject.transform.GetSiblingIndex();
        propsTitle_ani.gameObject.transform.SetSiblingIndex(order + 1);
        propsTitle_ani.SetBool("isShow", true);
        propsStorePanel.SetActive(true);
        for (int i = 0; i < storeProps_ani.Length; i++)
        {
            if (i != propsIndex)
            {
                storeProps_ani[i].SetBool("ClickItem", false);
            }
            else
            {
                storeProps_ani[i].SetBool("ClickItem", true);
            }
        }
        seedStorePanel.SetActive(false);

    }
}
