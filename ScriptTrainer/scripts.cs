using System;
using System.Text.RegularExpressions;
using UnityEngine;
using GUIPackage;

public class Script : MonoBehaviour
{
    /// <summary>
    /// 获取游戏数据
    /// </summary>
    public static void GetGameData()
    {
        try
        {
            foreach (var item in jsonData.instance.ItemJsonData)
            {
                item i = new item(item.Value["id"].I);
                ScriptTrainer.ScriptTrainer.itemList.Add(i);
            }

            Debug.addLog("数据导入完成");
        }
        catch (Exception)
        {
            Debug.addLog("未进入游戏");
        }


    }

    /// <summary>
    /// 检查是否是int
    /// </summary>
    /// <param name="ItemText"></param>
    public static int CheckIsInt(string ItemText)
    {
        int newCount = 0;
        ItemText = Regex.Replace(ItemText, @"[^0-9.]", "");
        try
        {
            if (ItemText != null && ItemText.Length < 10 && ItemText.Length != 0)
            {
                newCount = Int32.Parse(ItemText);
            }
            else
            {
                ItemText = newCount.ToString();
            }
        }
        catch (Exception) { throw; }

        return newCount;
    }

    /// <summary>
    /// 修改门派声望
    /// </summary>
    /// <param name="change"></param>
    public static void ChangeMenPaiShengWang(int change)
    {
        int shengW = PlayerEx.GetMenPaiShengWang();

        PlayerEx.AddMenPaiShengWang(change - shengW);

    }
    /// <summary>
    /// 修改俸禄
    /// </summary>
    /// <param name="change"></param>
    public static void ChangeFengLuMoney(int change)
    {
        KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家 
        int fengLu = player.chenghaomag.GetAllFengLuMoney();
        player.chenghaomag.AddFengLu(change - fengLu);
    }

    /// <summary>
    /// 修改灵感
    /// </summary>
    /// <param name="change"></param>
    public static void ChangeLingGan(int change)
    {
        KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家 
        player.AddLingGan(change - player.LingGan);
    }
    /// <summary>
    /// 修改悟道点
    /// </summary>
    /// <param name="change"></param>
    public static void ChangeWuDaoDian(int change)
    {
        KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家 
        player._WuDaoDian = change;
    }
   
    /// <summary>
    /// 修改宁州声望
    /// </summary>
    /// <param name="change"></param>
    public static void ChangeNingZhouShengWang(int change)
    {
        if (change < 0)
        {
            PlayerEx.AddNingZhouShengWang((change - PlayerEx.GetNingZhouShengWang()));
        }
        else
        {
            PlayerEx.AddNingZhouShengWang(change - PlayerEx.GetNingZhouShengWang());
        }
    }
}
