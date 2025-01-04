using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain : MonoBehaviour {
    public GameObject[] cityBuildings; // B�timents pour la ville
    public GameObject[] natureElements; // �l�ments pour la nature
    public float tileSize = 10f; // Taille des carreaux
    public int initialSpawnDistance = 10; // Distance initiale de g�n�ration
    public int renderAheadDistance = 20; // Distance de pr�-g�n�ration devant la voiture
    public int cleanupDistance = 30; // Distance � partir de laquelle les tuiles sont supprim�es
    public Transform vehicle; // Le v�hicule � suivre
    private float noiseSeed;

    private HashSet<Vector2> spawnedTiles = new HashSet<Vector2>();
    private Dictionary<Vector2, GameObject> tileObjects = new Dictionary<Vector2, GameObject>();

    void Start() {
        // G�n�re les b�timents au d�part
        noiseSeed = Random.Range(0f, 1000f);
        GenerateInitialBuildings();
    }

    void Update() {
        // G�n�re dynamiquement des b�timents au fur et � mesure
        GenerateBuildingsAhead();

        // Supprime les b�timents derri�re le v�hicule
        CleanupDistantTiles();
    }

    void GenerateInitialBuildings() {
        // G�n�re les b�timents dans un certain rayon autour de la position de d�part
        Vector2 vehiclePos = new Vector2(vehicle.position.x, vehicle.position.z);

        for (int x = -initialSpawnDistance; x <= initialSpawnDistance; x++) {
            for (int z = 0; z <= initialSpawnDistance; z++) // Ne g�n�re pas derri�re le v�hicule
            {
                Vector2 tileCoord = new Vector2(
                    Mathf.Floor((vehiclePos.x + x * tileSize) / tileSize),
                    Mathf.Floor((vehiclePos.y + z * tileSize) / tileSize)
                );

                if (Mathf.Abs(x) > 0 && Mathf.Abs(x) <= 2 && !spawnedTiles.Contains(tileCoord)) {
                    SpawnTile(tileCoord);
                }
            }
        }
    }

    void GenerateBuildingsAhead() {
        // Position actuelle de la voiture
        Vector2 vehiclePos = new Vector2(vehicle.position.x, vehicle.position.z);

        // G�n�re uniquement des b�timents devant la voiture
        for (int x = -renderAheadDistance; x <= renderAheadDistance; x++) {
            for (int z = 0; z <= renderAheadDistance; z++) // Seulement en avant
            {
                Vector2 tileCoord = new Vector2(
                    Mathf.Floor((vehiclePos.x + x * tileSize) / tileSize),
                    Mathf.Floor((vehiclePos.y + z * tileSize) / tileSize)
                );

                // G�n�re des b�timents uniquement sur les c�t�s (�vite la route)
                if (Mathf.Abs(x) > 0 && Mathf.Abs(x) <= 2 && !spawnedTiles.Contains(tileCoord)) {
                    SpawnTile(tileCoord);
                }
            }
        }
    }

    void SpawnTile(Vector2 coord) {
        Vector3 tileCenter = new Vector3(coord.x * tileSize, 0, coord.y * tileSize);

        // Calcul de la hauteur et s�lection de l'environnement � l'aide de Mathf.PerlinNoise
        float perlinValue = Mathf.PerlinNoise(coord.x * 0.1f + noiseSeed, coord.y * 0.1f + noiseSeed);

        GameObject spawnedObject;
        if (perlinValue < 0.5f) {
            // Nature
            spawnedObject = SpawnNatureElement(tileCenter, perlinValue);
        } else {
            // Ville
            spawnedObject = SpawnCityBuilding(tileCenter, perlinValue);
        }

        // Enregistre l'objet g�n�r�
        spawnedTiles.Add(coord);
        tileObjects[coord] = spawnedObject;
    }

    GameObject SpawnCityBuilding(Vector3 position, float perlinValue) {
        int buildingIndex = Mathf.FloorToInt(perlinValue * 2f * cityBuildings.Length) % cityBuildings.Length;
        buildingIndex = Mathf.Clamp(buildingIndex, 0, cityBuildings.Length - 1);

        return Instantiate(cityBuildings[buildingIndex], position, Quaternion.identity);
    }

    GameObject SpawnNatureElement(Vector3 position, float perlinValue) {
        int elementIndex = Mathf.FloorToInt(perlinValue * 2f * natureElements.Length) % natureElements.Length;
        elementIndex = Mathf.Clamp(elementIndex, 0, natureElements.Length - 1);

        return Instantiate(natureElements[elementIndex], position, Quaternion.identity);
    }

    void CleanupDistantTiles() {
        Vector2 vehiclePos = new Vector2(vehicle.position.x, vehicle.position.z);
        Vector2 vehicleDirection = new Vector2(vehicle.forward.x, vehicle.forward.z).normalized;

        List<Vector2> tilesToRemove = new List<Vector2>();

        foreach (var tile in spawnedTiles) {
            Vector2 tileCenter = tile * tileSize;
            Vector2 toTile = tileCenter - vehiclePos;

            // V�rifie si la tuile est derri�re le v�hicule
            if (Vector2.Dot(toTile.normalized, vehicleDirection) < 0 &&
                toTile.magnitude > cleanupDistance * tileSize) {
                tilesToRemove.Add(tile);
            }
        }

        foreach (var tile in tilesToRemove) {
            if (tileObjects.TryGetValue(tile, out GameObject tileObject)) {
                Destroy(tileObject);
                tileObjects.Remove(tile);
            }
            spawnedTiles.Remove(tile);
        }
    }
}
