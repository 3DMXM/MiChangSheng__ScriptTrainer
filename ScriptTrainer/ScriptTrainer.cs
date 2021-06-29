using System;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace ScriptTrainer
{
    [BepInPlugin("aoe.top.MiChangSheng.ScriptTrainer", "【觅长生】内置修改器", "1.0.0.0")]
    public class ScriptTrainer : BaseUnityPlugin
    {
        public static PlayerData playerData = new PlayerData(); // 玩家属性
        public static PlayerData newPlayerData = new PlayerData(); // 新玩家属性

        // 窗口相关
        public bool DisplayingWindow;
        private bool ShowAddItemWindow = true;
        private bool ShowOtherWindow = false;
        private bool ShowAddlocktechWindow = false;
        private int AddItemNum = 1000;

        private string searchItem = "";

        private Rect HeaderTitleRect;
        private Rect HeaderTableRect;
        private Rect windowRect;
        private Rect AddItemTableRect;
        private Rect AddlocktechRect;
        private Rect OtherRect;
        private Vector2 scrollPosition;

        private Rect DisplayArea;
        private Rect UserDataTableRect;

        // 启动按键
        private ConfigEntry<BepInEx.Configuration.KeyboardShortcut> ShowCounter { get; set; }

        // 在插件启动时会直接调用Awake()方法
        void Awake()
        {
            // 允许用户自定义启动快捷键
            ShowCounter = Config.Bind("修改器快捷键", "Key", new BepInEx.Configuration.KeyboardShortcut(KeyCode.F9));

            // 日志输出
            Debug.Log("脚本已启动");

            // 计算区域
            ComputeRect();

        }

        void Update()
        {
            if (ShowCounter.Value.IsDown())
            {
                //Debug.Log("按下按键");
                DisplayingWindow = !DisplayingWindow;
                if (DisplayingWindow)
                {
                    Debug.Log("打开窗口");
                }
                else
                {
                    Debug.Log("关闭窗口");
                }
                Script.GetPlyaerData();
            }


            //var key = new BepInEx.Configuration.KeyboardShortcut(KeyCode.F9);
            //if (key.IsDown())
            //{
            //    var a = new ADDBUFF();
            //    KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家                        
            //    var skillList = jsonData.instance.skillJsonData;        // 获取技能列表
            //    foreach (var item in skillList)
            //    {
            //        var id = item.Key;
            //        var skill = item.Value;
            //    }
            //    var ItemList = jsonData.instance.ItemJsonData;          // 物品列表
            //    var BuffList = jsonData.instance.BuffJsonData;          // Buff列表
            //    foreach (var item in ItemList)
            //    {
            //        if (jsonData.instance.ItemJsonData.HasField(item.Key))
            //        {
            //            int id = Tools.instance.getSkillIDByKey(int.Parse(item.Key));
            //            player.addItem(id, 1, Tools.CreateItemSeid(id));            // 给玩家添加物品
            //        }
            //    }
            //player.money += 100;    // 给玩家+100块钱
            //}
        }

        // 初始样式
        void ComputeRect()
        {

            // 主窗口居中
            int num = Mathf.Min(Screen.width, 700);
            int num2 = (Screen.height < 400) ? Screen.height : (400);
            int num3 = Mathf.RoundToInt((float)(Screen.width - num) / 2f);
            int num4 = Mathf.RoundToInt((float)(Screen.height - num2) / 2f);
            this.windowRect = new Rect((float)num3, (float)num4, (float)num, (float)num2);

            this.DisplayArea = new Rect(15, 15, (float)num - 30, (float)num2 - 30);

            // 头部
            this.HeaderTitleRect = this.windowRect;
            this.HeaderTitleRect.position = new Vector2(0, 5);
            // 选项卡按钮
            //this.HeaderTableRect.width = 150f;
            this.HeaderTableRect = new Rect(0, 40, 700, 40);
            // 玩家资料修改
            this.UserDataTableRect = new Rect(0, 90, 700, 300);
            // 解锁科技
            this.AddlocktechRect = new Rect(0, 90, 700, 300);
            // 其他内容
            this.OtherRect = new Rect(0, 90, 700, 300);
        }

        // GUI函数
        private void OnGUI()
        {
            if (this.DisplayingWindow)
            {
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
                windowRect = GUI.Window(20210219, windowRect, DoMyWindow, "", myWindowStyle);
            }
        }


        // 显示窗口
        void DoMyWindow(int winId)
        {
            //Rect Gr = new Rect();
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

                    // 玩家资料
                    this.UserDataTable(UserDataTableRect);

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
                    texture2D.SetPixel(0, 0, new Color32(69, 69, 69, 255));
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

        void UserDataTable(Rect UserDataTableRect)
        {
            GUIStyle Labelguistyle = new GUIStyle()
            {
                fixedWidth = 80,
                fixedHeight = 40,
                alignment = TextAnchor.MiddleRight,
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            };
            GUIStyle TextFieldguistyle = new GUIStyle()
            {
                fixedWidth = 100,
                fixedHeight = 40,
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(5, 0, 0, 0),
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            };

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(700), GUILayout.Height(300));
            {
                GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                {
                    // 基础属性
                    {
                        GUILayout.Label("年龄", Labelguistyle);
                        var ItemText = GUILayout.TextField(playerData.age.ToString(), TextFieldguistyle);
                    }
                }
                GUILayout.EndHorizontal();

            }
            GUILayout.EndScrollView();
        }
    }
}
