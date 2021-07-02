using UnityEngine;

public class XmGUI : MonoBehaviour
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
    public static bool Button(string text, int width, int height)
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
            fixedHeight = height,
            fixedWidth = width,
            margin = new RectOffset(5, 7, (40 - height) / 2, 5),
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
    public static bool Button(string text, bool stat)
    {
        Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        if (stat)
        {
            texture2D.SetPixel(0, 0, new Color32(51, 51, 51, 255));    // rgba(30, 144, 255,1.0)
        }
        else
        {
            texture2D.SetPixel(0, 0, new Color32(69, 69, 69, 255));    // rgba(30, 144, 255,1.0)

        }
        texture2D.Apply();

        GUIStyle guistyle = new GUIStyle()
        {
            normal = new GUIStyleState  // 正常样式
            {
                textColor = Color.white,
                background = texture2D
            },
            wordWrap = true,
            alignment = TextAnchor.MiddleCenter,
            fixedHeight = 40,
            fixedWidth = 80,
            margin = new RectOffset(5, 7, 0, 0),
        };

        return GUILayout.Button(text, guistyle);
    }
    public static bool Button(string text, Texture2D normal, Texture2D active)
    {
        GUIStyle guistyle = new GUIStyle()
        {
            normal = new GUIStyleState  // 正常样式
            {
                textColor = Color.white,
                background = normal
            },
            active = new GUIStyleState  // 点击样式
            {
                textColor = Color.white,
                background = active
            },
            wordWrap = true,
            alignment = TextAnchor.MiddleCenter,
            fixedHeight = 40,
            fixedWidth = 80,
            margin = new RectOffset(5, 7, 0, 5),
        };
        return GUILayout.Button(text, guistyle);
    }
    public static bool Button(string text, Texture2D img)
    {
        GUIStyle guistyle = new GUIStyle()
        {
            normal = new GUIStyleState  // 正常样式
            {
                textColor = Color.white,
                background = img
            },
            wordWrap = true,
            alignment = TextAnchor.MiddleCenter,
            fixedWidth = 70,
            fixedHeight = 82,
            margin = new RectOffset(5, 7, 0, 5),
        };

        return GUILayout.Button(text, guistyle);
    }

    public static void Label(string text)
    {
        GUIStyle guistyle = new GUIStyle()
        {
            fixedWidth = 50,
            fixedHeight = 40,
            alignment = TextAnchor.MiddleRight,
            normal = new GUIStyleState
            {
                textColor = Color.white
            }
        };
        GUILayout.Label(text, guistyle);
    }
    public static void Label(string text, int width,int height)
    {
        GUIStyle guistyle = new GUIStyle()
        {
            fixedWidth = width,
            fixedHeight = height,
            alignment = TextAnchor.MiddleRight,
            normal = new GUIStyleState
            {
                textColor = Color.white
            }
        };
        GUILayout.Label(text, guistyle);
    }

    public static string TextField(string text)
    {
        GUIStyle guistyle = new GUIStyle()
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
        return GUILayout.TextField(text, guistyle);
    }
    public static string TextField(string text, int width, int height)
    {
        GUIStyle guistyle = new GUIStyle()
        {
            fixedWidth = width,
            fixedHeight = height,
            alignment = TextAnchor.MiddleLeft,
            margin = new RectOffset(5, 0, 0, 0),
            normal = new GUIStyleState
            {
                textColor = Color.white
            }
        };
        return GUILayout.TextField(text, guistyle);
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

