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
        private VoxModel targetModel;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            VoxModel model = target as VoxModel;
            if (targetModel == null || model != targetModel)
            {
                filePath = model.modelSource;
                targetModel = model;
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("File:", GUILayout.ExpandWidth(false));
            GUILayout.TextArea(filePath.Replace(Application.dataPath, ""));
            if (GUILayout.Button("Open file", GUILayout.ExpandWidth(false)))
            {
                filePath = EditorUtility.OpenFilePanel("Open model", EditorPrefs.GetString("LastVoxPath",Application.dataPath), "vox");
                if (File.Exists(filePath))
                    EditorPrefs.SetString("LastVoxPath", filePath);
            }
            GUILayout.EndHorizontal();
            if (filePath != string.Empty)
            {
                if (GUILayout.Button("Generate Model", GUILayout.ExpandWidth(false)))
                {
                    MagicaVoxelParser parser = new MagicaVoxelParser();
                    model.modelSource = filePath;
                    parser.LoadModel(filePath, model);
                    EditorUtility.SetDirty(model);
                    string path = AssetDatabase.GetAssetPath(model);
                    string name = Path.GetFileNameWithoutExtension(path);
                    Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(model));
                    //load asset meshes
                    List<Mesh> assetMeshes = new List<Mesh>();
                    for (int i = 0; i < subAssets.Length; i++)
                        if (subAssets[i] is Mesh)
                            assetMeshes.Add(subAssets[i] as Mesh);
                    int max = model.meshes.Count;
                    bool update = false;
                    Mesh m,temp; 
                    for (int i = 0; i <max; i++)
                    {
                        //get mesh
                        if (i < assetMeshes.Count)
                        {
                            update = false;
                            m = assetMeshes[i];
                            if (m == null)
                            {
                                update = true;
                                m = new Mesh();
                            }
                        }
                        else
                        {
                            m = new Mesh();
                            update = true;
                        }
                        //turn temp meshes into assets
                        if (i < model.meshes.Count)
                        {
                            m.name = name + "_frame_" + i;
                            temp = model.meshes[i];
                            m.Clear();
                            m.vertices = temp.vertices;
                            m.colors = temp.colors;
                            m.normals = temp.normals;
                            m.triangles = temp.triangles;
                            m.uv = Unwrapping.GeneratePerTriangleUV(m);
                            Unwrapping.GenerateSecondaryUVSet(m);
                            //new mesh
                            if (update)
                                AssetDatabase.AddObjectToAsset(m, targetModel);
                            model.meshes[i] = m;
                        }
                    }
                    //destroy un needed meshes
                    for (int i = 0; i < subAssets.Length; i++) {
                        m = subAssets[i] as Mesh;
                        if (m != null) {
                            if (model.meshes.Contains(m) == false) {
                                DestroyImmediate(m, true);
                            }
                        }
                    }
                    EditorUtility.SetDirty(targetModel);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}
