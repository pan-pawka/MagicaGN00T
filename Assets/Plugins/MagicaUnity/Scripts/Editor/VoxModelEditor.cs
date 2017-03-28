using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace GN00T.MagicaUnity
{
    [CustomEditor(typeof(VoxModel))]
    public class VoxModelEditor : Editor
    {
        private string filePath = string.Empty;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            VoxModel model = target as VoxModel;
            GUILayout.BeginHorizontal();
            GUILayout.Label("File:", GUILayout.ExpandWidth(false));
            GUILayout.TextArea(filePath.Replace(Application.dataPath, ""));
            if (GUILayout.Button("Open file", GUILayout.ExpandWidth(false)))
                filePath = EditorUtility.OpenFilePanel("Open model", Application.dataPath, "vox");
            GUILayout.EndHorizontal();
            if (filePath != string.Empty)
            {
                if (GUILayout.Button("Generate Model", GUILayout.ExpandWidth(false)))
                {
                    Debug.Log("Generate model.");
                    MagicaVoxelParser parser = new MagicaVoxelParser(model.modelScale);
                    parser.LoadModel(filePath, model);
                    EditorUtility.SetDirty(model);
                    string path = AssetDatabase.GetAssetPath(model);
                    string name = Path.GetFileNameWithoutExtension(path);
                    string dir = Path.GetDirectoryName(path);
                    string framePath;
                    Debug.Log(name + " " + path+" "+dir);
                    AssetDatabase.StartAssetEditing();
                    for (int i = 0; i < model.frames.Count; i++)
                    {
                        Mesh m = model.meshes[i];
                        m.name = name + "_frame_" + i;
                        framePath = Path.Combine(dir, name + "_frame_" + i + ".asset");
                        model.meshes[i] = CreateOrReplaceAsset<Mesh>(m,framePath);
                        model.meshes[i].colors = m.colors;
                        model.meshes[i].normals = m.normals;
                        model.meshes[i].vertices = m.vertices;
                        model.meshes[i].triangles = m.triangles;
                        model.meshes[i].name = m.name;
                        model.meshes[i].uv = m.uv;
                        EditorUtility.SetDirty(model.meshes[i]);
                    }
                    AssetDatabase.StopAssetEditing();
                    AssetDatabase.Refresh();
                    AssetDatabase.SaveAssets();
                  
                }
            }
        }

        T CreateOrReplaceAsset<T>(T asset, string path) where T : Object
        {
            T existingAsset = AssetDatabase.LoadAssetAtPath<T>(path);

            if (existingAsset == null)
            {
                AssetDatabase.CreateAsset(asset, path);
                existingAsset = asset;
            }
            else
            {
                EditorUtility.CopySerialized(asset, existingAsset);
            }

            return existingAsset;
        }
    }
}
