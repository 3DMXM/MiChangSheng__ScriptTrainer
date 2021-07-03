using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using GUIPackage;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScriptTrainer
{
    [BepInPlugin("aoe.top.MiChangSheng.ScriptTrainer", "【觅长生】内置修改器", "1.0.1.0")]
    public class ScriptTrainer : BaseUnityPlugin
    {
        //public static PlayerData playerData = new PlayerData(); // 玩家属性
        public static KBEngine.Avatar player;   //获取玩家
        public static List<item> itemList = new List<item>();  // 物品列表

        // 窗口相关
        public static bool DisplayingWindow;

        private Rect HeaderTitleRect;
        private Rect windowRect;
        private Vector2 scrollPosition;

        private Rect DisplayArea;
        private Rect TableRect;

        // 启动按键
        private ConfigEntry<BepInEx.Configuration.KeyboardShortcut> ShowCounter { get; set; }

        // 一些要用的参数
        private int ItemID = 0; // 物品ID
        private int count = 1;  // 物品数量
        private string search = ""; // 搜索

        // 在插件启动时会直接调用Awake()方法
        void Awake()
        {
            // 允许用户自定义启动快捷键
            ShowCounter = Config.Bind("修改器快捷键", "Key", new BepInEx.Configuration.KeyboardShortcut(KeyCode.F9));

            // 清空之前的日志
            Debug.EmptyLog();
            // 日志输出
            Debug.addLog("脚本已启动");

            // 获取游戏数据
            Script.GetGameData();
            // 计算区域
            ComputeRect();
        }

        void Update()
        {
            if (ShowCounter.Value.IsDown())
            {
                DisplayingWindow = !DisplayingWindow;
                if (DisplayingWindow)
                {
                    Debug.addLog("打开窗口");
                }
                else
                {
                    Debug.addLog("关闭窗口");
                }
            }

        }

        // 初始样式
        void ComputeRect()
        {

            // 主窗口居中
            int num = Mathf.Min(Screen.width, 740);
            int num2 = (Screen.height < 400) ? Screen.height : (400);
            int num3 = Mathf.RoundToInt((float)(Screen.width - num) / 2f);
            int num4 = Mathf.RoundToInt((float)(Screen.height - num2) / 2f);
            this.windowRect = new Rect((float)num3, (float)num4, (float)num, (float)num2);

            this.DisplayArea = new Rect(15, 15, (float)num - 30, (float)num2 - 30);

            // 头部
            this.HeaderTitleRect = new Rect(5, 5, (float)num - 40, (float)num2 - 40);

            // 中间窗口
            this.TableRect = new Rect(0, 40, (float)num - 30, 300);

            
        }

        // GUI函数
        private void OnGUI()
        {
            if (DisplayingWindow)
            {
                try
                {

                    player = Tools.instance.getPlayer();    // 获取玩家 
                    if (player.ToString() == "")
                    {
                        Debug.addLog("未进入游戏");
                        return;
                    }

                    Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                    // rgba(116, 125, 140,1.0)
                    texture2D.SetPixel(0, 0, new Color32(51, 51, 51, 255));
                    texture2D.Apply();
                    GUIStyle myWindowStyle = new GUIStyle
                    {
                        normal = new GUIStyleState  // 正常样式
                        {
                            textColor = new Color32(47, 53, 66, 1),
                            background = texture2D
                        },
                        wordWrap = true,    // 自动换行
                                            //alignment = TextAnchor.UpperCenter,  //对齐方式
                    };
                    // 定义一个新窗口
                    int winId = 20210630;
                    windowRect = GUI.Window(winId, windowRect, DoMyWindow, "", myWindowStyle);

                    float windowW = 210f;
                    window.LeftWindow(new Rect(windowRect.x - windowW, windowRect.y, windowW, windowRect.height));      // 左侧菜单
                    window.RightWindow(new Rect(windowRect.x + windowRect.width, windowRect.y + 40, windowW, windowRect.height));   // 右侧菜单
                    window.CloseButton(new Rect(windowRect.x + windowRect.width, windowRect.y, 80, 40)); // 关闭按钮
                }
                catch (Exception e)
                {
                    Debug.addLog(String.Format("错误：{0}.", e.Message));
                }


            }
        }


        // 显示窗口
        void DoMyWindow(int winId)
        {
            GUILayout.BeginHorizontal();
            {
                Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                texture2D.SetPixel(0, 0, new Color32(69, 69, 69, 255));
                texture2D.Apply();
                GUIStyle guistyle = new GUIStyle
                {
                    normal = new GUIStyleState  // 正常样式
                    {
                        textColor = new Color32(47, 53, 66, 1),
                        background = texture2D
                    },
                    wordWrap = true,    // 自动换行
                    alignment = TextAnchor.UpperCenter,  //对齐方式
                };
                GUILayout.BeginArea(DisplayArea, guistyle);
                {
                    // 渲染头部标题
                    this.HeaderTitle(HeaderTitleRect);

                    // 基础功能
                    if (window.TabButtonStaty.GetWindowStat<windowsStat>("BasicScripts")) this.BasicScriptsTable(TableRect);

                    // 玩家资料
                    if (window.TabButtonStaty.GetWindowStat<windowsStat>("PlayerAttributes")) this.UserDataTable(TableRect);

                    // 玩家悟道
                    if (window.TabButtonStaty.GetWindowStat<windowsStat>("PlayerWuDao")) this.UserWuDaoTable(TableRect);

                    // 玩家物品
                    if (window.TabButtonStaty.GetWindowStat<windowsStat>("playerItem")) this.UserItemTable(TableRect);

                }
                GUILayout.EndArea();
            }
            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }



        // 窗口标题
        void HeaderTitle(Rect HeaderTitleRect)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginArea(HeaderTitleRect);
                {
                    Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                    // rgba(255, 99, 72,1.0)
                    texture2D.SetPixel(0, 0, new Color32(51, 51, 51, 255));
                    texture2D.Apply();
                    GUIStyle guistyle = new GUIStyle
                    {
                        normal = new GUIStyleState
                        {
                            textColor = Color.white,
                            background = texture2D
                        },
                        wordWrap = true,
                        alignment = TextAnchor.MiddleCenter,
                        fixedHeight = 30,
                        fontSize = 16
                    };

                    GUILayout.Label("[觅长生] 内置修改器 By:小莫", guistyle);
                }
                GUILayout.EndArea();
            }
            GUILayout.EndHorizontal();
        }

        // 基础功能
        void BasicScriptsTable(Rect TableRect)
        {
            GUILayout.BeginArea(TableRect);
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(700), GUILayout.Height(300));
                {
                    // KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家                  

                    XmGUI.Title("常用功能");
                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        {
                            // 现金
                            XmGUI.Label("现金");
                            if (XmGUI.Button("-1000", 50, 20))
                            {
                                player.money -= 1000;
                            }
                            var ItemText = XmGUI.TextField(player.money.ToString());
                            player.money = (ulong)Script.CheckIsInt(ItemText);
                            if (XmGUI.Button("+1000", 50, 20))
                            {
                                player.money += 1000;
                            }
                        }
                        {
                            // 宁州声望
                            XmGUI.Label("宁州声望", 70, 40);
                            if (XmGUI.Button("-100", 50, 20))
                            {
                                PlayerEx.AddNingZhouShengWang(-100);
                            }
                            XmGUI.Label(PlayerEx.GetNingZhouShengWang().ToString(), 30, 40);
                            if (XmGUI.Button("+100", 50, 20))
                            {
                                PlayerEx.AddNingZhouShengWang(100);
                            }
                        }
                        {
                            // 海域声望
                            XmGUI.Label("海域声望", 70, 40);
                            if (XmGUI.Button("-100", 50, 20))
                            {
                                PlayerEx.AddSeaShengWang(-100);
                            }
                            XmGUI.Label(PlayerEx.GetSeaShengWang().ToString(), 30, 40);
                            if (XmGUI.Button("+100", 50, 20))
                            {
                                PlayerEx.AddSeaShengWang(100);
                            }
                        }
                    }
                    XmGUI.hr();
                    {
                        if (XmGUI.Button("修为全满"))
                        {
                            player.exp = (ulong)jsonData.instance.LevelUpDataJsonData[player.level.ToString()]["MaxExp"].I;
                        }
                        if (XmGUI.Button("血量全满"))
                        {
                            player.HP = player.HP_Max;
                        }
                    }
                    GUILayout.EndHorizontal();

                    XmGUI.Title("抽卡");
                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        if (XmGUI.Button("随机1张卡", 90, 40))
                        {
                            RoundManager.instance.drawCard(player);
                        }
                        if (XmGUI.Button("随机3张卡", 90, 40))
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                RoundManager.instance.drawCard(player);
                            }
                        }
                        if (XmGUI.Button("随机3张相同卡", 90, 40))
                        {

                            int a = Random.Range(0, 4);
                            for (int i = 0; i < 3; i++)
                            {
                                RoundManager.instance.drawCard(player, a);
                            }
                        }                        
                    }
                    XmGUI.hr();
                    {
                        string[] CardList = { "金", "木", "水", "火", "土" };
                        int CardID = 0;
                        foreach (var item in CardList)
                        {
                            if (XmGUI.Button(String.Format("抽3张{0}卡", item), 90, 40))
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    RoundManager.instance.drawCard(player, CardID);
                                }
                            }
                            CardID++;
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();

            }
            GUILayout.EndArea();
        }

        // 修改玩家资料
        void UserDataTable(Rect TableRect)
        {
            GUILayout.BeginArea(TableRect);
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(700), GUILayout.Height(300));
                {

                    // KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家 
                    XmGUI.Title("玩家");
                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        {
                            XmGUI.Label("年龄");
                            var ItemText = XmGUI.TextField(player.age.ToString());
                            player.age = (uint)Script.CheckIsInt(ItemText);
                        }
                        {
                            XmGUI.Label("寿元");
                            var ItemText = XmGUI.TextField(player.shouYuan.ToString());
                            player.shouYuan = (uint)Script.CheckIsInt(ItemText);
                        }
                        {
                            XmGUI.Label("资质");
                            var ItemText = XmGUI.TextField(player.ZiZhi.ToString());
                            player.ZiZhi = Script.CheckIsInt(ItemText);
                        }
                        {
                            XmGUI.Label("神识");
                            window.PlayerShengShi(player);
                            
                        }
                        {
                            XmGUI.Label("悟性");
                            var ItemText = XmGUI.TextField(player.wuXin.ToString());
                            player.wuXin = (uint)Script.CheckIsInt(ItemText);
                        } 
                        {
                            XmGUI.Label("遁速");
                            window.PlayerDunSu(player);
                        }                       

                    }
                    XmGUI.hr(); // 换行
                    {
                        {
                            XmGUI.Label("心境");
                            var ItemText = XmGUI.TextField(player.xinjin.ToString());
                            player.xinjin = Script.CheckIsInt(ItemText);
                        }
                        {
                            XmGUI.Label("丹毒");
                            var ItemText = XmGUI.TextField(player.Dandu.ToString());
                            player.Dandu = Script.CheckIsInt(ItemText);
                        }
                        {
                            XmGUI.Label("灵感");
                            var ItemText = XmGUI.TextField(player.LingGan.ToString());
                            //player.LingGan = Script.CheckIsInt(ItemText);
                            Script.ChangeLingGan(Script.CheckIsInt(ItemText));
                        }                       
                        {
                            XmGUI.Label("修为");
                            var ItemText = XmGUI.TextField(player.exp.ToString());
                            player.exp = (ulong)Script.CheckIsInt(ItemText);
                        }
                        {
                            XmGUI.Label("生命");
                            window.PlayerHP();
                        }
                    }
                    GUILayout.EndHorizontal();


                    XmGUI.Title("门派宗门");
                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        {
                            XmGUI.Label("门派");
                            string menPaiName = Tools.Code64(jsonData.instance.ShiLiHaoGanDuName[player.menPai.ToString()]["ChinaText"].str);
                            if (XmGUI.Button(menPaiName))
                            {
                                window.windowStaty.ChangeWindowStat<windowsStat>("MenPaiWindowStat", !window.windowStaty.GetWindowStat<windowsStat>("MenPaiWindowStat"));
                            }
                        }
                        {
                            XmGUI.Label("职位");

                            if (XmGUI.Button(PlayerEx.GetMenPaiChengHao()))
                            {
                                window.windowStaty.ChangeWindowStat<windowsStat>("ShiLiChengHaoStat", !window.windowStaty.GetWindowStat<windowsStat>("ShiLiChengHaoStat"));
                            }
                        }
                        {
                            XmGUI.Label("声望");
                            var ItemText = XmGUI.TextField(PlayerEx.GetMenPaiShengWang().ToString());
                            Script.ChangeMenPaiShengWang(Script.CheckIsInt(ItemText));
                        }
                        //{
                        //    MyGui.Label("俸禄");
                        //    var ItemText = MyGui.TextField(player.chenghaomag.GetAllFengLuMoney().ToString());
                        //}
                    }
                    GUILayout.EndHorizontal();


                    XmGUI.Title("灵根属性");
                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        string[] LingGengList = {"金","木","水","火","土" };
                        for (int i = 0; i < player.LingGeng.Count; i++)
                        {
                            XmGUI.Label(LingGengList[i]);
                            var ItemText = XmGUI.TextField(player.LingGeng[i].ToString());
                            player.LingGeng[i] = Script.CheckIsInt(ItemText);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();
        }

        // 玩家悟道修改
        void UserWuDaoTable(Rect TableRect)
        {
            GUILayout.BeginArea(TableRect);
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(700), GUILayout.Height(300));
                {
                    // KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家                    

                    XmGUI.Title("悟道属性");
                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        {
                            int num = 0;
                            foreach (var item in jsonData.instance.WuDaoAllTypeJson.list)
                            {
                                XmGUI.Label(Tools.Code64(item["name"].str));
                                var ItemText = XmGUI.TextField(player.wuDaoMag.getWuDaoEx(item["id"].I).ToString());
                                player.wuDaoMag.SetWuDaoEx(item["id"].I, Script.CheckIsInt(ItemText));

                                num++;
                                if (num >= 6)
                                {
                                    XmGUI.hr();
                                    num = 0;
                                }
                            }
                        }
                    }
                    GUILayout.EndHorizontal();


                    XmGUI.Title("悟道修改");                    
                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        {
                            XmGUI.Label("悟道点");
                            var ItemText = XmGUI.TextField(player._WuDaoDian.ToString());
                            player._WuDaoDian = Script.CheckIsInt(ItemText);
                        }
                        {
                            if (XmGUI.Button("一键全满"))
                            {
                                foreach (var item in jsonData.instance.WuDaoAllTypeJson.list)
                                {
                                    player.wuDaoMag.SetWuDaoEx(item["id"].I, 150000);
                                }
                            }
                        }
                        XmGUI.hr(); // 换行
                        {
                            int num = 0;
                            foreach (var item in jsonData.instance.WuDaoAllTypeJson.list)
                            {
                                string str = String.Format("{0} 全满", Tools.Code64(item["name"].str));
                                if (XmGUI.Button(str))
                                {
                                    player.wuDaoMag.SetWuDaoEx(item["id"].I, 150000);
                                }
                                num++;
                                if (num >= 6)
                                {
                                    XmGUI.hr();
                                    num = 0;
                                }
                            }
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();

        }

        // 玩家物品修改
        void UserItemTable(Rect TableRect)
        {
            GUILayout.BeginArea(TableRect);
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(700), GUILayout.Height(300));
                {
                    // KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家                  

                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        XmGUI.Label("获取物品", 80, 40);
                        XmGUI.Label("ID", 30, 40);
                        //int ItemID = 0;
                        ItemID = Script.CheckIsInt(XmGUI.TextField(ItemID.ToString(), 40, 40));
                        XmGUI.Label("数量", 30, 40);
                        //int count = 1;
                        count = Script.CheckIsInt(XmGUI.TextField(count.ToString(), 40, 40));
                        if (XmGUI.Button("获取", 50, 20))
                        {
                            if (ItemID != 0)
                            {
                                player.addItem(ItemID, Tools.CreateItemSeid(ItemID), count);
                                Singleton.inventory.AddItem(ItemID);
                            }
                        }
                        //XmGUI.Label("搜索");
                        //search = XmGUI.TextField(search);
                    }
                    XmGUI.hr();
                    {
                        //window.ItemWindow(itemList, player, count, search);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();
        }
    }
}