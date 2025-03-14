using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum DifficultyLevel { Facile, Moyen, Difficile }
    public static DifficultyLevel CurrentDifficulty { get; private set; } = DifficultyLevel.Moyen; // Défaut : Moyen

    public static void SetDifficulty(int level)
    {
        CurrentDifficulty = (DifficultyLevel)level;
        PlayerPrefs.SetInt("Difficulty", level); // Sauvegarde la difficulté
        PlayerPrefs.Save();
    }

    public static void LoadDifficulty()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            CurrentDifficulty = (DifficultyLevel)PlayerPrefs.GetInt("Difficulty");
        }
    }
}
