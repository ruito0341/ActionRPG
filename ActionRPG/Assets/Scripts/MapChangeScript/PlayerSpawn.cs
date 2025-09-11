using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        Debug.Log("PlayerSpawn Start");
        // "SpawnPoint" という名前のオブジェクトを探す
        GameObject spawn = GameObject.Find("SpawnPoint");
        if (spawn != null)
        {
            transform.position = spawn.transform.position;
        }
    }
}
