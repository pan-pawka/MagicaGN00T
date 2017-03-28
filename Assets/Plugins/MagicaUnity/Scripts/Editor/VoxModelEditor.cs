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
                    MagicaVoxelParser parser = new MagicaVoxelParser(model.modelScale);
                    parser.LoadModel(filePath, model);
                    EditorUtility.SetDirty(model);
                    string path = AssetDatabase.GetAssetPath(model);
                    string name = Path.GetFileNameWithoutExtension(path);
                    Object[] subAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(model));
                    int max = model.meshes.Count;
                    bool update = false;
                    Mesh m,temp; 
                    for (int i = max-1; i >=0; i--)
                    {
                        //they are stored backwards
                        if (i < subAssets.Length - 1)
                        {
                            update = false;
                            m = subAssets[i] as Mesh;
                            //asset wasn't a mesh create new
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
                                AssetDatabase.AddObjectToAsset(m, target);
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
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}
