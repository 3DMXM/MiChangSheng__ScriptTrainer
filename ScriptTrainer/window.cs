using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class window : MonoBehaviour
{
    public static bool MenPaiWindowStat = false;
    public static bool ShiLiChengHaoStat = false;

    /// <summary>
    /// 显示右侧窗口
    /// </summary>
    /// <param name="position">坐标</param>
    /// <param name="content">窗体内容</param>
    /// <param name="title">窗体标题</param>
    public static void RightWindow(Rect position, string title, WindowContent content)
    {
        if (MenPaiWindowStat || ShiLiChengHaoStat)
        {
            GUILayout.BeginHorizontal();
            {
                Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                texture2D.SetPixel(0, 0, new Color32(51, 51, 51, 255));
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
                GUILayout.BeginArea(position, guistyle);
                {
                    if (MenPaiWindowStat) MenPaiWindow(new Rect(15, 15, position.width - 30, position.height - 30), content);
                    if (ShiLiChengHaoStat) ShiLiChengHao();

                }
                GUILayout.EndArea();
            }
            GUILayout.EndHorizontal();
        }       
    }

    public static void ShowContent(Rect position, WindowContent content)
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

        GUILayout.BeginArea(position, guistyle);
        {
            GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
            {

                content();

            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();
    }


    /// <summary>
    /// 门派列表窗口
    /// </summary>
    public static void MenPaiWindow(Rect position, WindowContent content)
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

        GUILayout.BeginArea(position, guistyle);
        {
            GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
            {

                KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家
                int num = 0;

                foreach (JSONObject jsonobject in jsonData.instance.CyShiLiNameData.list)
                {
                    if (MyGui.Button(jsonobject["name"].Str))
                    {
                        player.menPai = (ushort)jsonobject["id"].I;
                        window.MenPaiWindowStat = false;
                    }
                    num++;
                    if (num >=3 )
                    {
                        MyGui.hr();
                        num = 0;
                    }
                }

            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();

    }

    /// <summary>
    /// 职位/称号 列表窗口
    /// </summary>
    public static void ShiLiChengHao()
    {
        GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
        {
            KBEngine.Avatar player = Tools.instance.getPlayer();    // 获取玩家
            int num = 0;
            foreach (JSONObject jsonobject in jsonData.instance.ChengHaoJsonData.list)
            {
                if (MyGui.Button(jsonobject["Name"].Str))
                {
                    PlayerEx.SetShiLiChengHaoLevel(player.menPai, jsonobject["id"].I + 1);

                    window.ShiLiChengHaoStat = false;
                }
                num++;
                if (num >= 3)
                {

                    MyGui.hr();
                    num = 0;
                }
            }
        }
        GUILayout.EndHorizontal();
    }




    public delegate void WindowContent();
}

