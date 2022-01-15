using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapWindow :EditorWindow
{
    [MenuItem("Tools/地图编辑器")]
    public static void Init()
    {
        MapWindow mapWindow = EditorWindow.GetWindow(typeof(MapWindow)) as MapWindow;
        mapWindow.Show();
    }

    private void OnEnable()
    {
        
    }

    [MenuItem("CONTEXT/Image/Init")]
    private static void ImageInit()
    {
        Debug.Log("ImageInit");
    }

    [ContextMenu("CONTEXT/Image/ImageReset")]
    private static void ImageReset()
    {
        Debug.Log("ImageReset");

    }

}
  
