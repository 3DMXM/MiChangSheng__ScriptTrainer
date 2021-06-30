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
        //public static PlayerData newPlayerData = new PlayerData(); // 新玩家属性

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

            // 保存修改
            //Script.ChangeToGame();
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
            this.HeaderTitleRect = new Rect(5, 5, (float)num - 40, (float)num2 - 40);
            // 选项卡按钮
            this.HeaderTableRect = new Rect(0, 40, 700, 40);
            // 玩家资料修改
            this.UserDataTableRect = new Rect(0, 40, 700, 300);

            
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

        // 修改玩家资料
        void UserDataTable(Rect UserDataTableRect)
        {
            GUIStyle Labelguistyle = new GUIStyle()
            {
                fixedWidth = 50,
                fixedHeight = 40,
                alignment = TextAnchor.MiddleRight,
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            };
            GUIStyle TextFieldguistyle = new GUIStyle()
            {
                fixedWidth = 60,
                fixedHeight = 40,
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(5, 0, 0, 0),
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            };

            GUILayout.BeginArea(UserDataTableRect);
            {


                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(700), GUILayout.Height(300));
                {

                    KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家 基础属性
                    MyGui.Title("玩家基础属性");
                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        {
                            GUILayout.Label("年龄", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.age.ToString(), TextFieldguistyle);
                            player.age = (uint)Script.CheckIsInt(ItemText);
                        }
                        {
                            GUILayout.Label("寿元", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.shouYuan.ToString(), TextFieldguistyle);
                            player.shouYuan = (uint)Script.CheckIsInt(ItemText);
                        }
                        {
                            GUILayout.Label("资质", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.ZiZhi.ToString(), TextFieldguistyle);
                            player.ZiZhi = Script.CheckIsInt(ItemText);
                        }
                        {
                            GUILayout.Label("神识", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.shengShi.ToString(), TextFieldguistyle);
                            player.shengShi = Script.CheckIsInt(ItemText);
                        }
                        {
                            GUILayout.Label("悟性", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.wuXin.ToString(), TextFieldguistyle);
                            player.wuXin = (uint)Script.CheckIsInt(ItemText);
                        } 
                        {
                            GUILayout.Label("遁速", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.dunSu.ToString(), TextFieldguistyle);
                            player.dunSu = Script.CheckIsInt(ItemText);
                        }

                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        {
                            GUILayout.Label("心境", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.xinjin.ToString(), TextFieldguistyle);
                            player.xinjin = Script.CheckIsInt(ItemText);
                        }
                        {
                            GUILayout.Label("丹毒", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.Dandu.ToString(), TextFieldguistyle);
                            player.Dandu = Script.CheckIsInt(ItemText);
                        }
                        {
                            GUILayout.Label("灵感", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.LingGan.ToString(), TextFieldguistyle);
                            player.LingGan = Script.CheckIsInt(ItemText);
                        }
                        
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
                    {
                        {
                            GUILayout.Label("门派", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.menPai.ToString(), TextFieldguistyle);
                            player.menPai = (ushort)Script.CheckIsInt(ItemText);
                        }
                        {
                            GUILayout.Label("门派声望", Labelguistyle);
                            var ItemText = GUILayout.TextField(PlayerEx.GetMenPaiShengWang().ToString(), TextFieldguistyle);
                            Script.ChangeMenPaiShengWang(Script.CheckIsInt(ItemText));
                        }
                        {
                            GUILayout.Label("俸禄", Labelguistyle);
                            var ItemText = GUILayout.TextField(player.chenghaomag.GetAllFengLuMoney().ToString(), TextFieldguistyle);
                            //Script.ChangeMenPaiShengWang(Script.CheckIsInt(ItemText));
                            Script.ChangeFengLuMoney(Script.CheckIsInt(ItemText));
                        }

                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();
        }
    }
}