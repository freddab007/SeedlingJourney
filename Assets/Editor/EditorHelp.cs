using System.Linq;
using System;
using UnityEditor;
using UnityEngine;

public class EditorHelp
{
    public static void PrintLabelInColor(string _text, Color _baseColor, Color _contentColor, bool _center = false)
    {
        GUI.contentColor = _baseColor;

        if (_center)
        {
            GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
            centeredStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUILayout.LabelField(_text, centeredStyle, GUILayout.ExpandWidth(true));
        }
        else
        {
            EditorGUILayout.LabelField(_text);
        }
        GUI.contentColor = _contentColor;
    }

    public static void PrintLabelInColor(string _text, Color _baseColor, Color _contentColor, int _widthLayout)
    {
        GUI.contentColor = _baseColor;

        EditorGUILayout.LabelField(_text, GUILayout.Width(_widthLayout));
        GUI.contentColor = _contentColor;
    }

    
    public static void ShowFilteredEnum<T>(T[] _ignored, ref T _return)
    {
        T[] hiddenValues = _ignored;

        // Filtrer les valeurs de l'enum
        T[] displayedValues = Enum.GetValues(typeof(T))
            .Cast<T>()
            .Where(action => !hiddenValues.Contains(action))
            .ToArray();

        // Afficher le popup avec les valeurs filtrées
        int selectedIndex = Array.IndexOf(displayedValues, _return);
        if (selectedIndex < 0) selectedIndex = 0; // Sécurité au cas où la valeur actuelle est masquée

        selectedIndex = EditorGUILayout.Popup(selectedIndex, displayedValues.Select(a => a.ToString()).ToArray());
        _return = displayedValues[selectedIndex];
    }



}
