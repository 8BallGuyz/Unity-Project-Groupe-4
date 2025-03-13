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
        GenerateRoomSequence();
        Debug.Log("Tu es dans la salle : " + GetCurrentRoom());
        Debug.Log("La prochaine salle est : " + GetNextRoom());
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
        

        menu_manager menu = FindObjectOfType<menu_manager>();
        if (menu != null)
        {
            menu.LesSalles(); // Appelle la fonction LesSalles() après la génération des salles
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
        Debug.Log("Salle actuelle : " + currentRoomName + " | Prochaine salle : " + nextRoomName);

        if (currentRoomIndex < roomsSequence.Count - 1)
        {
            currentRoomIndex++;
            currentRoomName = roomsSequence[currentRoomIndex]; // Salle actuelle

            if (currentRoomIndex < roomsSequence.Count - 1)
                nextRoomName = roomsSequence[currentRoomIndex + 1]; // Salle suivante
            else
                nextRoomName = "Fin du parcours"; // Dernière salle atteinte

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
        return nextRoomName;
    }

}
