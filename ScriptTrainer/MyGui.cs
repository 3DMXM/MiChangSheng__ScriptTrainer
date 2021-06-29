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
}

