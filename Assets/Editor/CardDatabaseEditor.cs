using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CardDatabaseEditor : Editor
{
    [MenuItem("Cards/Update Card Database")]
    public static void UpdateCardDatabase()
    {
        string collectionPath = "Assets/CardDatabase.asset"; // Adjust path as needed

        // Try to load the existing CardDatabase
        CardDatabase cardDatabase = AssetDatabase.LoadAssetAtPath<CardDatabase>(collectionPath);

        // If the CardCollection doesn't exist, create a new one
        if (cardDatabase == null)
        {
            cardDatabase = ScriptableObject.CreateInstance<CardDatabase>();
            AssetDatabase.CreateAsset(cardDatabase, collectionPath);
        }

        // Clear the existing list
        cardDatabase.Cards.Clear();

        string[] cardGUIDs = AssetDatabase.FindAssets("t:Card"); // This will get all Cards in the project
        foreach (string guid in cardGUIDs)
        {
            string cardPath = AssetDatabase.GUIDToAssetPath(guid);
            Card card = AssetDatabase.LoadAssetAtPath<Card>(cardPath);
            cardDatabase.Cards.Add(card);
        }

        EditorUtility.SetDirty(cardDatabase);  // Mark the cardDatabase as dirty to ensure changes are saved
        AssetDatabase.SaveAssets();  // Save changes
        AssetDatabase.Refresh();
    }
}