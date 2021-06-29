using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Script : MonoBehaviour
{
    // 获取玩家数据
    public static void GetPlyaerData()
    {
        //var playerData = ScriptTrainer.ScriptTrainer.playerData;

        PlayerData data = new PlayerData();

        KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家      

        data.age = (int)player.age;
        data.ZiZhi = player.ZiZhi;
        data._shengShi = player._shengShi;
        data.wuXin = (int)player.wuXin;
        data._dunSu = player._dunSu;



        ScriptTrainer.ScriptTrainer.playerData = data;

    }

    // 修改数据保存到游戏
    public static void ChangeToGame()
    {

    }

}

