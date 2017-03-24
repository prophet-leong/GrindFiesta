using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class UpgradeTabManager : Singleton<UpgradeTabManager> 
{
    public List<GameObject> tabs = new List<GameObject>();
    GameObject UpgradeTabPrefab;
    GameObject Options;
    float UpgradeTabUnitPos;
    //DragDetect dragDetect;
    //int currentTabIndex;
    //public float tabMoved;
    //public float TabIndexFloat;
    public UpgradeTabManager()
    {
        //TabIndexFloat = 0;
        //currentTabIndex = 0;
        /******Init options***/
        UserSingleton.GetInstance().Options.AddComponent<Mask>();
        Options = new GameObject();
        Options.name = "UpgradeTabs";
        Options.transform.SetParent(UserSingleton.GetInstance().Options.transform);
        Options.transform.SetAsFirstSibling();
        Options.transform.localPosition = new Vector3(0, UserSingleton.GetInstance().Options.GetComponent<RectTransform>().rect.height/2, 0);
        Options.transform.localScale = new Vector3(1, 1, 1);
        Options.AddComponent<RectTransform>();
        Options.GetComponent<RectTransform>().sizeDelta =new Vector2( UserSingleton.GetInstance().Options.GetComponent<RectTransform>().rect.width,UserSingleton.GetInstance().Options.GetComponent<RectTransform>().rect.height);
        //Options.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f,1.0f);
        //Options.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1.0f);
        Options.AddComponent<BoxCollider>();
        Options.GetComponent<BoxCollider>().size = new Vector3(Options.GetComponent<RectTransform>().rect.width, Options.GetComponent<RectTransform>().rect.height,1);
        Options.GetComponent<BoxCollider>().isTrigger = true;
        UserSingleton.GetInstance().Options.AddComponent<ScrollRect>();
        UserSingleton.GetInstance().Options.GetComponent<ScrollRect>().content = Options.GetComponent<RectTransform>();
        UserSingleton.GetInstance().Options.GetComponent<ScrollRect>().horizontal = false;
        UserSingleton.GetInstance().Options.GetComponent<ScrollRect>().scrollSensitivity = 10;
        //Options.AddComponent<DragDetect>();
        //dragDetect = Options.GetComponent<DragDetect>();

        /******UpgradetabPrefab****/
        UpgradeTabPrefab = Resources.Load("Prefabs/UpgradeCharacterTab") as GameObject;
        UpgradeTabUnitPos = (Options.GetComponent<RectTransform>().rect.height / 4);
    }
    public void CreateTab(Hero hero)
    {
        /************Options Holder Transform***************/
        float OptionsHolderheight = (tabs.Count+2) * UpgradeTabPrefab.GetComponent<RectTransform>().rect.height;
        Options.GetComponent<RectTransform>().sizeDelta = new Vector2(UpgradeTabPrefab.GetComponent<RectTransform>().rect.width,OptionsHolderheight );
        Options.transform.localPosition = new Vector3(0, UserSingleton.GetInstance().Options.GetComponent<RectTransform>().rect.height - OptionsHolderheight,0);
        /******************Basic transform*********************/
        GameObject newTab ;
        newTab = GameObject.Instantiate(UpgradeTabPrefab);
        newTab.transform.SetParent(Options.transform);
        newTab.transform.localPosition = new Vector3(0,  UpgradeTabUnitPos * (-tabs.Count), 0);
        newTab.transform.localScale = new Vector3(1, 1, 1);
        //resize the upgrade tab holder so it can scroll
        /*********************Button Scripts for the UpgradeTab*****************************/
        //a popup will appear when pressed
        newTab.AddComponent<CharacterStats>();
        newTab.GetComponent<CharacterStats>().SetHero(hero);
        //upgrade the character
        newTab.transform.FindChild("Price").gameObject.AddComponent<CharacterPurchase>();
        newTab.transform.FindChild("Price").gameObject.GetComponent<CharacterPurchase>().SetHero(hero,tabs.Count);
        /******************Create the upgrade tab********************/
        newTab.transform.FindChild("Price/PriceTag").GetComponent<Text>().text = hero.price.ToString() + " Gold";
        newTab.transform.FindChild("UnitFace").GetComponent<Image>().sprite = hero.spriteRenderer.sprite;
        newTab.transform.FindChild("Name").GetComponent<Text>().text = hero.name;
        newTab.transform.FindChild("Attack").GetComponent<Text>().text = "Attack :" + hero.attack.ToString();
        tabs.Add(newTab);
        Vector3 temp;
        float heightOfTab = UpgradeTabPrefab.GetComponent<RectTransform>().rect.height;
        float StartOfUpgrade = Options.GetComponent<RectTransform>().rect.height/2 - 200.0f;
        for(int i =0;i<tabs.Count;++i)
        {
            temp = tabs[i].transform.localPosition;
            temp.y = StartOfUpgrade -(heightOfTab * i);
            tabs[i].transform.localPosition = temp;

        }
    }
    public void UpdageTab(Hero hero,int tabNumber)
    {
        tabs[tabNumber].transform.FindChild("Price/PriceTag").GetComponent<Text>().text = hero.price.ToString() + " Gold";
        tabs[tabNumber].transform.FindChild("Attack").GetComponent<Text>().text = "Attack :" + hero.attack.ToString();
    }
    //public void DragResponse()
    //{
    //    if(dragDetect.dragging == true)
    //    {
    //        if(tabs.Count>4)
    //        {
    //            moveTabs();
    //        }
    //    }
    //}
    //void moveTabs()
    //{
    //    float distanceMoved = dragDetect.startPosition.y - Input.mousePosition.y;
    //    tabMoved = distanceMoved / UpgradeTabPrefab.GetComponent<RectTransform>().rect.height + TabIndexFloat;
    //    if(distanceMoved > 0)
    //    {
    //        //moving part
    //        if(currentTabIndex != (int)tabMoved)
    //        {
    //            for (int i = 0; i < tabs.Count;++i)
    //            {
    //                tabs[currentTabIndex + i].gameObject.SetActive(false);
    //            }
    //            currentTabIndex = (int)tabMoved;
    //        }
    //        float heightOfOptionsTab = (Options.GetComponent<RectTransform>().rect.height);
    //        for (int i = currentTabIndex; i < tabs.Count; ++i)
    //        {
    //            if (i-currentTabIndex > 4)
    //                return;
    //            //change this so that it will calculate its correct position and add the distanceMoved to the Y coord
    //            float placement = (1.0f - (float)(i - tabMoved + currentTabIndex));
    //            Vector3 temp = new Vector3(0, (heightOfOptionsTab / 4 * placement)+ distanceMoved, 0);
    //            tabs[i].gameObject.transform.localPosition = temp;
    //            tabs[i].gameObject.SetActive(true);
    //        }
    //    }
    //}
}
