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


    private float startTime; // Temps de début de la scène
    private int baseReward = 50; // Récompense de base



    public static RoomManager instance;
    public int credits = 0; // 🔹 L'argent global du joueur

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Évite les doublons
        }
    }
    
    public void AddCredits(int amount)
    {
        credits += amount;
        Debug.Log("💰 Crédits ajoutés : " + amount + " | Total : " + credits);
        UIManager.instance?.UpdateCreditsText(); // Mettre à jour l'UI
    }

    public void RemoveCredits(int amount)
    {
        if (credits >= amount)
        {
            credits -= amount;
            Debug.Log("💸 Crédits dépensés : " + amount + " | Restant : " + credits);
            UIManager.instance?.UpdateCreditsText(); // Mettre à jour l'UI
        }
        else
        {
            Debug.Log("❌ Pas assez de crédits !");
        }
    }

    public int GetCredits()
    {
        return credits;
    }


    void Start()
    {

        DontDestroyOnLoad(this);

        startTime = Time.timeSinceLevelLoad; // On enregistre le temps de départ

        // DifficultyManager.LoadDifficulty();
        // Debug.Log("Difficulté actuelle : " + DifficultyManager.CurrentDifficulty);

        if (SceneManager.GetActiveScene().name == "Menu")
        {   
            GenerateRoomSequence();
        }
        else
        {
            LoadExistingRoomSequence(); 
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

        List<string> availableRooms = new List<string>(allRooms); 

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

        roomsSequence.Add("Salle_Fin"); 

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
        availableRooms.RemoveAt(index);

        return chosenRoom;
    }

    private int CalculateReward(float timeTaken)
    {   
        if (currentRoomName == "Salle_Achat" || currentRoomName == "Salle_Début" || currentRoomName == "Salle_Fin")
        {
            return 0; // Très lent : petite récompense
        }
        else
        {
            if (timeTaken <= 10) return baseReward * 2; // Très rapide : x2
            if (timeTaken <= 30) return baseReward; // Normal : baseReward
            if (timeTaken <= 60) return baseReward / 2; // Lent : moitié
            if (timeTaken <= 120) return baseReward / 4; // Lent : moitié
            return baseReward / 5; // Très lent : petite récompense
        }
    }


    public void LoadNextRoom()
    {

        float timeTaken = Time.timeSinceLevelLoad; // Temps passé dans la scène
        int reward = CalculateReward(timeTaken); // Calcul de la récompense

        AddCredits(reward); // Ajoute les crédits gagnés
        Debug.Log("🏆 Récompense pour " + currentRoomName + " : " + reward + " crédits");


        Debug.Log("Tu es dans la salle : " + GetCurrentRoom());
        Debug.Log("La prochaine salle est : " + GetNextRoom());

        if (currentRoomIndex < roomsSequence.Count - 1)
        {
            currentRoomIndex++;
            currentRoomName = roomsSequence[currentRoomIndex]; 

            if (currentRoomIndex < roomsSequence.Count - 1)
                nextRoomName = roomsSequence[currentRoomIndex + 1]; 
            else
                nextRoomName = "Salle_Fin"; 

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
            return "Salle_Fin"; 
    }

    public static string GetSalle(int num){
        return "Salle_" + num.ToString();
    }

}
