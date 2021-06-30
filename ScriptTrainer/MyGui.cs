using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MyGui : MonoBehaviour
{
    public static void Title(string text)
    {
        GUILayout.BeginHorizontal();
        {
            GUIStyle guistyle = new GUIStyle()
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
            UILabel newLabel = new UILabel();
            GUILayout.Label(text, guistyle);
        }
        GUILayout.EndHorizontal();       
    }

    public static bool Button(string text)
    {
        Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture2D.SetPixel(0, 0, new Color32(30, 144, 255, 255));    // rgba(30, 144, 255,1.0)
        texture2D.Apply();
        Texture2D texture2D2 = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture2D2.SetPixel(0, 0, new Color32(112, 161, 255, 255));  // rgba(112, 161, 255,1.0)
        texture2D2.Apply();

        GUIStyle guistyle = new GUIStyle()
        {
            normal = new GUIStyleState  // 正常样式
            {
                textColor = Color.white,
                background = texture2D
            },
            active = new GUIStyleState  // 点击样式
            {
                textColor = Color.white,
                background = texture2D2
            },
            wordWrap = true,
            alignment = TextAnchor.MiddleCenter,
            fixedHeight = 40,
            fixedWidth = 80,
            margin = new RectOffset(5, 7, 0, 5),
        };

        return GUILayout.Button(text, guistyle);
    }
    public static bool Button(Rect position, string text)
    {
        Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture2D.SetPixel(0, 0, new Color32(30, 144, 255, 255));    // rgba(30, 144, 255,1.0)
        texture2D.Apply();
        Texture2D texture2D2 = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture2D2.SetPixel(0, 0, new Color32(112, 161, 255, 255));  // rgba(112, 161, 255,1.0)
        texture2D2.Apply();

        GUIStyle guistyle = new GUIStyle()
        {
            normal = new GUIStyleState  // 正常样式
            {
                textColor = Color.white,
                background = texture2D
            },
            active = new GUIStyleState  // 点击样式
            {
                textColor = Color.white,
                background = texture2D2
            },
            wordWrap = true,
            alignment = TextAnchor.MiddleCenter,
            fixedHeight = 40,
            fixedWidth = 80,
            margin = new RectOffset(5, 7, 0, 5),
        };

        return GUI.Button(position, text, guistyle);
    }


    /// <summary>
    /// 换行
    /// </summary>
    public static void hr()
    {
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal(new GUIStyle { alignment = TextAnchor.UpperLeft });
    }
}

