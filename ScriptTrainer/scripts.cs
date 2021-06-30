using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public class Script : MonoBehaviour
{
    /// <summary>
    /// 获取玩家数据
    /// </summary>
    public static void GetPlyaerData()
    {
        try
        {
            //var playerData = ScriptTrainer.ScriptTrainer.playerData;

            PlayerData data = new PlayerData();

            KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家      

            data.age = (int)player.age;
            data.ZiZhi = player.ZiZhi;
            data.shouYuan = (int)player.shouYuan;
            data._shengShi = player._shengShi;
            data.wuXin = (int)player.wuXin;
            data._dunSu = player._dunSu;

            ScriptTrainer.ScriptTrainer.playerData = data;
        }
        catch (Exception)
        {
            Debug.Log("未进入游戏");
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

    public static void ChangeLingGan(int change)
    {
        KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家 
        player.AddLingGan(change - player.LingGan);
    }

   
}
