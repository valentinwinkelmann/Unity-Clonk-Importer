using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ClonkImporter : EditorWindow
{
    public Texture2D[] sprites;
    private TextAsset actMap;
    private TextAsset defCore;

    //Options
    private SpriteAlignment alignment = SpriteAlignment.BottomCenter;
    [MenuItem("Tools/Unity Clonk Importer")]
    public static void ShowWindow()
    {
        GetWindow<ClonkImporter>("Unity Clonk Importer");
    }

    private void OnGUI()
    {
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("sprites");
        EditorGUILayout.HelpBox("Simply drag and drop your Clonk graphics onto the sprites field. You can also drag and drop multiple files at once. You also need to specify an ActMap.txt. Optionally you can also specify a DefCore.txt if you want the title image of the graphic to be recognized as well. ", MessageType.Info);
        GUILayout.Label("Sprites", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children

        
        GUILayout.Label("ActMap", EditorStyles.boldLabel);
        actMap = (TextAsset)EditorGUILayout.ObjectField(actMap, typeof(TextAsset), false);
        GUILayout.Label("DefCore", EditorStyles.boldLabel);
        defCore = (TextAsset)EditorGUILayout.ObjectField(defCore, typeof(TextAsset), false);


        // Optionals
        EditorGUILayout.Space(20f);
        EditorGUILayout.LabelField("Optionals",EditorStyles.largeLabel);

        EditorGUILayout.LabelField("Alignment", EditorStyles.boldLabel);
        alignment = (SpriteAlignment)EditorGUILayout.EnumPopup(alignment);


        if (GUILayout.Button("Generate Spritesheet"))
        {
            foreach(Texture2D tex in sprites)
            {
                Debug.Log("Try convert: " + tex.name);
                CalculateSprite(tex);
                
            }
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("RenderClonk Unity Importer by Valentin Winkelmann", EditorStyles.miniLabel);
        if(EditorGUILayout.LinkButton("More on Github"))
        {
            // Open link in browser
            Application.OpenURL("https://github.com/valentinwinkelmann/Unity-Clonk-Importer");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("'Clonk' is a registered trademark of Matthes Bender.", EditorStyles.centeredGreyMiniLabel);

        so.ApplyModifiedProperties();
    }

    public void CalculateSprite(Texture2D tex)
    {
        // Set sprite import settings to Multiple mode
        string path = AssetDatabase.GetAssetPath(tex);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
        importer.isReadable = true;
        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        //importer.spritePixelsPerUnit = 73;
        //importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;

        List<SpriteMetaData> newData = new List<SpriteMetaData>();


        foreach (Action action in ParseActMap())
        {
            for (int i = 0; i < action.Length; i++)
            {
                SpriteMetaData smd = new SpriteMetaData();
                smd.pivot = new Vector2(0.5f, 0.5f);
                smd.alignment = (int)alignment;
                smd.name = action.Name + "_" + i;
                Rect unityRect = ConvertRectToUnity(new Rect(action.Facet.x + i * action.Facet.width, action.Facet.y, action.Facet.width, action.Facet.height), tex.height);
                smd.rect = unityRect;
                newData.Add(smd);
            }
        }
        if (defCore != null)
        {
            SpriteMetaData title = new SpriteMetaData();
            title.pivot = new Vector2(0f, 0f);
            title.alignment = (int)alignment;
            title.name = "Title";
            RectInt titleRect = ParseTitleRect();
            Rect unityRect = ConvertRectToUnity(new Rect(titleRect.x, titleRect.y, titleRect.width, titleRect.height), tex.height);
            title.rect = unityRect;
            newData.Add(title);
        }

        importer.spritesheet = newData.ToArray();
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate); // this takes time, approx. 3s per Asset
    }

    public static Rect ConvertRectToUnity(Rect rect, int spriteHeight)
    {
        float y = spriteHeight - (rect.y + rect.height);
        return new Rect(rect.x, y, rect.width, rect.height);
    }
    private void TestActMap()
    {
        List<Action> actions = new List<Action>();
        actions = ParseActMap();
    }
    public RectInt ParseTitleRect()
    {
        string iniString = defCore.text;
        RectInt pictureRect = new RectInt();
        string[] lines = iniString.Split('\n');
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("Picture="))
            {
                string[] rectValues = trimmedLine.Substring(8).Split(',');
                if (rectValues.Length == 4)
                {
                    int x, y, w, h;
                    if (int.TryParse(rectValues[0], out x) &&
                        int.TryParse(rectValues[1], out y) &&
                        int.TryParse(rectValues[2], out w) &&
                        int.TryParse(rectValues[3], out h))
                    {
                        pictureRect = new RectInt(x, y, w, h);
                    }
                    else
                    {
                        Debug.LogError("Invalid RectInt value in Picture: " + trimmedLine);
                    }
                }
                else
                {
                    Debug.LogError("Invalid RectInt value in Picture: " + trimmedLine);
                }
                break;
            }
        }
        return pictureRect;
    }
public List<Action> ParseActMap()
    {
        string iniString = actMap.text;
        List<Action> actions = new List<Action>();
        Action currentAction = null;
        string[] lines = iniString.Split('\n');
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("[Action]"))
            {
                currentAction = new Action();
                actions.Add(currentAction);
            }
            else if (currentAction != null && trimmedLine.Contains("="))
            {
                string[] parts = trimmedLine.Split('=');
                string key = parts[0].Trim().ToLower();
                string value = parts[1].Trim();
                switch (key)
                {
                    case "name":
                        currentAction.Name = value;
                        break;
                    case "length":
                        int length;
                        if (int.TryParse(value, out length))
                        {
                            currentAction.Length = length;
                        }
                        else
                        {
                            Debug.LogError("Invalid length value: " + value);
                        }
                        break;
                    case "facet":
                        string[] rectValues = value.Split(',');
                        if (rectValues.Length == 4)
                        {
                            int x, y, w, h;
                            if (int.TryParse(rectValues[0], out x) &&
                                int.TryParse(rectValues[1], out y) &&
                                int.TryParse(rectValues[2], out w) &&
                                int.TryParse(rectValues[3], out h))
                            {
                                currentAction.Facet = new RectInt(x, y, w, h);
                            }
                            else
                            {
                                Debug.LogError("Invalid RectInt value: " + value);
                            }
                        }
                        else
                        {
                            Debug.LogError("Invalid RectInt value: " + value);
                        }
                        break;
                    default:
                        Debug.LogWarning("Unknown key: " + key);
                        break;
                }
            }
        }
        return actions;
    }

    [System.Serializable]
    public class Action
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public RectInt Facet { get; set; }
    }
}
