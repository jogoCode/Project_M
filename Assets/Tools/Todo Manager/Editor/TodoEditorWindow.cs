using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TodoEditorWindow : EditorWindow
{
    private const string EDITOR_PREFS_KEY = "TodoManagerWindow";
    private const string ICON_PATH = "Assets/Tools/Todo Manager/Icon.png";

    [MenuItem("Window/Todo Manager")]
    public static void ShowWindow()
    {
        TodoEditorWindow window = GetWindow<TodoEditorWindow>();

        Texture2D icon = EditorGUIUtility.Load(ICON_PATH) as Texture2D;
        window.titleContent = new GUIContent("TODO Manager", icon);
    }

    private string _title = "";
    private string _description = "";

    [SerializeField] private List<TodoElement> _todos = new();

    private static GUIStyle boldTitle
    {
        get
        {
            var style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            return style;
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
        HorizontalLine();
        EditorGUILayout.LabelField("Create New", boldTitle);
        HorizontalLine();
        EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

        _title = EditorGUILayout.TextField("Title", _title);
        EditorGUILayout.LabelField("Description");
        _description = EditorGUILayout.TextArea(_description, 
            GUILayout.MinHeight(EditorGUIUtility.singleLineHeight * 2f));

        var enabled = GUI.enabled;
        GUI.enabled = string.IsNullOrEmpty(_title) is false;

        EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
        if (GUILayout.Button("Add"))
        {
            _todos.Add(new TodoElement(_title, _description));

            _title = string.Empty;
            _description = string.Empty;
        }
        GUI.enabled = enabled;

        EditorGUILayout.EndVertical();
        
        EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
        HorizontalLine();
        EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

        List<TodoElement> elementsToRemove = new();
        foreach (var todo in _todos)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

            todo.m_completed = EditorGUILayout.Toggle(todo.m_title, todo.m_completed);
            if (string.IsNullOrEmpty(todo.m_description) is false)
                EditorGUILayout.LabelField(todo.m_description);

            if (todo.m_completed)
            {
                if (GUILayout.Button("Remove"))
                {
                    elementsToRemove.Add(todo);
                }
            }

            EditorGUILayout.EndVertical();
        }

        foreach (var toRemove in elementsToRemove)
        {
            _todos.Remove(toRemove);
        }
    }

    private void OnEnable()
    {
        var data = EditorPrefs.GetString(EDITOR_PREFS_KEY, JsonUtility.ToJson(this, false));
        JsonUtility.FromJsonOverwrite(data, this);
    }

    private void OnDisable()
    {
        var data = JsonUtility.ToJson(this, false);
        EditorPrefs.SetString(EDITOR_PREFS_KEY, data);
    }

    private static void HorizontalLine(Color color, int height = 1)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, height);
        rect.height = height;

        EditorGUI.DrawRect(rect, color);
    }

    private static void HorizontalLine(int height = 1)
    {
        HorizontalLine(new Color(0.5f, 0.5f, 0.5f, 1f), height);
    }
}
