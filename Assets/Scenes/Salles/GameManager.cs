using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomManager : MonoBehaviour
{




    public string currentRoomName; // Salle actuelle
    public string nextRoomName;    // Salle suivante
    private List<string> allRooms = new List<string> { "Salle_1", "Salle_2", "Salle_3", "Salle_4", "Salle_5", "Salle_6", "Salle_7", "Salle_9" };
    private int currentRoomIndex = 0;
    private List<string> roomsSequence = new List<string>();

    



    void Start()
    {

        DontDestroyOnLoad(this);

        // DifficultyManager.LoadDifficulty();
        // Debug.Log("Difficulté actuelle : " + DifficultyManager.CurrentDifficulty);

        if (SceneManager.GetActiveScene().name == "Menu")
        {   
            GenerateRoomSequence();
        }
        else
        {
            LoadExistingRoomSequence(); // Récupère la séquence déjà définie
        }

        Debug.Log("Tu es dans la salle : " + GetCurrentRoom());
        Debug.Log("La prochaine salle est : " + GetNextRoom());
    }

    void LoadExistingRoomSequence()
    {
        Debug.Log("Chargement de la séquence existante...");
        
        // Si la liste des salles est déjà générée, on ne change rien.
        if (roomsSequence.Count > 0)
        {
            Debug.Log("Séquence existante : " + string.Join(", ", roomsSequence));
            return;
        }

        Debug.LogError("Aucune séquence trouvée, risque d'erreur !");
    }



    void GenerateRoomSequence()
    {
        roomsSequence.Clear();
        roomsSequence.Add("Salle_Début"); // Salle 1 (fixe)

        List<string> availableRooms = new List<string>(allRooms); // Copie des salles disponibles

        for (int i = 1; i < 10; i++)
        {
            if (i == 2 || i == 5) // Salle 3 et 6 sont toujours "Salle_Achat"
            {
                roomsSequence.Add("Salle_Achat");
            }
            else if (i == 7) // Salle 8 a 1 chance sur 2 d'être une Salle_Achat
            {
                if (Random.value < 0.5f) roomsSequence.Add("Salle_Achat");
                else roomsSequence.Add(GetUniqueRandomRoom(ref availableRooms));
            }
            else
            {
                roomsSequence.Add(GetUniqueRandomRoom(ref availableRooms));
            }
        }

        roomsSequence.Add("Salle_Fin"); // On ajoute la salle finale

        Debug.Log("Séquence des salles : " + string.Join(", ", roomsSequence));



        // Initialiser currentRoomName et nextRoomName
        if (roomsSequence.Count > 0)
        {
            currentRoomName = roomsSequence[0]; // Première salle
            if (roomsSequence.Count > 1)
                nextRoomName = roomsSequence[1]; // Salle suivante
            else
                nextRoomName = "Fin du parcours"; // Dernière salle
        }
        else
        {
            Debug.LogError("Aucune salle générée !");
        }
    }

    string GetUniqueRandomRoom(ref List<string> availableRooms)
    {
        if (availableRooms.Count == 0)
        {
            Debug.LogError("Plus de salles disponibles !");
            return "Salle_Début"; // Sécurité en cas d'erreur
        }

        int index = Random.Range(0, availableRooms.Count);
        string chosenRoom = availableRooms[index];
        availableRooms.RemoveAt(index); // On enlève la salle pour éviter qu'elle ne réapparaisse

        return chosenRoom;
    }

    public void LoadNextRoom()
    {
        Debug.Log("Tu es dans la salle : " + GetCurrentRoom());
        Debug.Log("La prochaine salle est : " + GetNextRoom());

        if (currentRoomIndex < roomsSequence.Count - 1)
        {
            currentRoomIndex++;
            currentRoomName = roomsSequence[currentRoomIndex]; // Salle actuelle

            if (currentRoomIndex < roomsSequence.Count - 1)
                nextRoomName = roomsSequence[currentRoomIndex + 1]; // Salle suivante
            else
                nextRoomName = "Salle_Fin"; // Dernière salle atteinte

            SceneManager.LoadScene(currentRoomName);
        }
        else
        {
            Debug.Log("Fin du parcours !");
        }
    }


    public string GetCurrentRoom()
    {
        return currentRoomName;
    }

    public string GetNextRoom()
    {
        if (currentRoomIndex < roomsSequence.Count - 1)
            return roomsSequence[currentRoomIndex + 1];
        else
            return "Salle_Fin"; // On force à retourner la salle finale
    }

}
