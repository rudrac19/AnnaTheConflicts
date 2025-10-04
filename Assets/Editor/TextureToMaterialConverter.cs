using UnityEngine;
using UnityEditor;
using System.IO;

public class BaseColorMaterialConverter : EditorWindow
{
    private string textureFolder = "Assets/Textures";   // Where your PNGs are
    private string materialFolder = "Assets/Materials"; // Where materials will be saved

    [MenuItem("Tools/Convert BaseColor PNGs to Materials")]
    public static void ShowWindow()
    {
        GetWindow<BaseColorMaterialConverter>("BaseColor → Materials");
    }

    private void OnGUI()
    {
        GUILayout.Label("Convert ONLY BaseColor Textures to Materials", EditorStyles.boldLabel);

        textureFolder = EditorGUILayout.TextField("Texture Folder", textureFolder);
        materialFolder = EditorGUILayout.TextField("Material Folder", materialFolder);

        if (GUILayout.Button("Convert"))
        {
            ConvertBaseColorTextures();
        }
    }

    private void ConvertBaseColorTextures()
    {
        if (!Directory.Exists(materialFolder))
        {
            Directory.CreateDirectory(materialFolder);
        }

        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { textureFolder });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

            if (texture == null) continue;

            // Only accept textures that look like BaseColor/Albedo
            string fileName = Path.GetFileNameWithoutExtension(path).ToLower();

            if (!(fileName.Contains("basecolor") || fileName.Contains("albedo") || fileName.Contains("diffuse")))
                continue;

            string materialPath = Path.Combine(materialFolder, texture.name + ".mat");
            materialPath = materialPath.Replace("\\", "/");

            Material mat = new Material(Shader.Find("HDRP/Lit"));
            mat.mainTexture = texture;

            AssetDatabase.CreateAsset(mat, materialPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("✅ Conversion complete! Only BaseColor/Albedo textures converted.");
    }
}
