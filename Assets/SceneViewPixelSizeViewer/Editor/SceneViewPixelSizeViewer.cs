using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class SceneViewPixelSizeViewer
{
    private static Vector2 PreviousSize;
    private static int     FrameCount = 1;

    static SceneViewPixelSizeViewer()
    {
        // NOTE:
        // When use ver.2018 or before.
        // SceneView.onSceneGUIDelegate += OnGUI;

        SceneView.duringSceneGui += OnGUI;
    }

    private static void OnGUI(SceneView sceneView)
    {
        // CAUTION:
        // A view ticked 1px size in same frame (bt. 1st and 2nd OnGUI).
        // So needs to consider that case.

        // NOTE:
        // Rect rect = sceneView.position;
        // position shows scaled size by OS and inclued SceneView's tool bar height.

        GUILayout.BeginArea(new Rect(0, 0, 500, 100));

        Vector2 size = new Vector2(sceneView.camera.pixelWidth,
                                   sceneView.camera.pixelHeight);

        if (PreviousSize != size)
        {
            FrameCount++;

            if (FrameCount > 1)
            {
                GUILayout.Label("Size : " + size.x + " x " + size.y);
            }
        }
        else
        {
            FrameCount = 0;
        }

        PreviousSize = size;

        GUILayout.EndArea();
    }
}

// NOTE:
// Window type.

//public class SceneViewInfo : EditorWindow
//{
//    [MenuItem("Window/SceneViewInfo")]
//    static void Init()
//    {
//        GetWindow<SceneViewInfo>().Show();
//    }

//    protected virtual void OnEnable()
//    {
//        EditorApplication.update += Repaint;
//    }

//    protected virtual void OnDisable()
//    {
//        EditorApplication.update -= Repaint;
//    }

//    protected virtual void OnGUI()
//    {
//        foreach (var sceneView in Resources.FindObjectsOfTypeAll<SceneView>())
//        {
//            GUILayout.Label("Position.Size : " + sceneView.position.width
//                                       + " x " + sceneView.position.height);

//            GUILayout.Label("Camera.Pixel  : " + sceneView.camera.pixelWidth
//                                       + " x " + sceneView.camera.pixelHeight);
//        }
//    }
//}