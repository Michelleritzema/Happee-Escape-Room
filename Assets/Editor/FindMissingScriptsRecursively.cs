using UnityEngine;
using UnityEditor;

/*
 * Original from http://wiki.unity3d.com/index.php?title=FindMissingScripts
 * Edited by Michelle Ritzema.
 * 
 * Class that checks for missing scripts.
 */

public class FindMissingScriptsRecursively : EditorWindow
{

    static int go_count = 0, components_count = 0, missing_count = 0;

    [MenuItem("Tests/FindMissingScriptsRecursively")]

    /*
     * Creates a window of type FindMissingScriptsRecursively.
     */
    public static void ShowWindow()
    {
        GetWindow(typeof(FindMissingScriptsRecursively));
    }

    /*
     * Creates a button that starts the search.
     */
    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
        {
            FindInSelected();
        }
    }

    /*
     * Starts searching the selected GameObjects.
     */
    private static void FindInSelected()
    {
        GameObject[] go = Selection.gameObjects;
        go_count = 0;
        components_count = 0;
        missing_count = 0;
        foreach (GameObject g in go)
        {
            FindInGO(g);
        }
        Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
    }

    /*
     * Finds all the components of a game and shows when there is an empty script.
     * Also recurses through each child GameObject if there are any.
     */
    private static void FindInGO(GameObject g)
    {
        go_count++;
        Component[] components = g.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            components_count++;
            if (components[i] == null)
            {
                missing_count++;
                string s = g.name;
                Transform t = g.transform;
                while (t.parent != null)
                {
                    s = t.parent.name + "/" + s;
                    t = t.parent;
                }
                Debug.Log(s + " has an empty script attached in position: " + i, g);
            }
        }
        foreach (Transform childT in g.transform)
        {
            //Debug.Log("Searching " + childT.name  + " " );
            FindInGO(childT.gameObject);
        }
    }

}