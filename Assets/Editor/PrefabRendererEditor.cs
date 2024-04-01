using UnityEngine;
using UnityEditor;

public class PrefabRendererEditor : EditorWindow
{
    [MenuItem("Tools/Render Prefabs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PrefabRendererEditor));
    }

    void OnGUI()
    {
        GUILayout.Label("Render Prefabs", EditorStyles.boldLabel);

        if (GUILayout.Button("Render Selected Prefabs"))
        {
            RenderSelectedPrefabs();
        }
    }

    void RenderSelectedPrefabs()
    {
        /*
        // Obtener los prefabs seleccionados en el panel de proyecto
        Object[] selectedPrefabs = Selection.objects;

        foreach (Object prefab in selectedPrefabs)
        {
            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            // Colocar el prefab en una posición y rotación predeterminadas
            prefabInstance.transform.position = Vector3.zero;
            prefabInstance.transform.rotation = Quaternion.identity;
            */
        string objectName = null;

        if (Selection.activeGameObject != null)
        {
            objectName = Selection.activeGameObject.name;
        }
            // Renderizar el prefab con transparencia
            RenderTexture renderTexture = new RenderTexture(512, 512, 32);
            Camera renderCamera = Camera.main;
            renderCamera.targetTexture = renderTexture;
            renderCamera.Render();

            // Guardar el render con el nombre del prefab
            RenderTexture.active = renderTexture;
            Texture2D prefabRender = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
            prefabRender.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            prefabRender.Apply();
            byte[] bytes = prefabRender.EncodeToPNG();
            //string prefabName = prefab.name.Replace("(Clone)", ""); // Eliminar el "(Clone)" del nombre
            System.IO.File.WriteAllBytes(Application.dataPath + "/RenderedPrefabs/" + objectName + ".png", bytes);
            EditorApplication.RepaintHierarchyWindow();
            // Descartar el prefab
            //DestroyImmediate(prefabInstance);
            //PrefabUtility.UnloadPrefabContents(prefabInstance);
        //}
    }
}
