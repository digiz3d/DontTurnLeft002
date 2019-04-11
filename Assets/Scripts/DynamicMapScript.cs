using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMapScript : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int blocksAhead = 2;
    [SerializeField] private int blocksBehind = 2;
    [SerializeField] private GameObject[] blocksToSpawn;
    [SerializeField] private SwipeDetection player;

    [Header("Debug: Some dynamic stuff")]
    private List<GameObject> spawnedBlocks;
    private List<GameObject> blocksToDelete;

    private float longuestBlock = 0f;
    private float currentAngle = 0;

    private GameObject currentBlock;
    private BlockScript currentBlockScript;

    private bool newBlock = false;
    // Use this for initialization
    void Start()
    {
        spawnedBlocks = new List<GameObject>();
        blocksToDelete = new List<GameObject>();

        foreach (GameObject g in blocksToSpawn)
        {
            BlockScript b = g.GetComponent<BlockScript>();
            float size = b.GetEndPoint().z - b.GetStartPoint().z;

            longuestBlock = Mathf.Max(longuestBlock, size);
        }
        NewLevel();
    }

    void Update()
    {
        Vector3 movement = new Vector3(0, 0, -speed * Time.deltaTime);
        movement = Quaternion.AngleAxis(currentAngle, Vector3.up) * movement;
        blocksToDelete.Clear();
        foreach (GameObject block in spawnedBlocks)
        {
            Transform blockTransform = block.transform;
            blockTransform.Translate(movement);
            BlockScript blockScript = block.GetComponent<BlockScript>();

            Vector3 endPosition = blockScript.GetEndPoint();
            newBlock = false;
            if (blockTransform.localPosition.z < player.gameObject.transform.position.z && player.gameObject.transform.position.z < (blockTransform.localPosition.z + endPosition.z))
            {
                if (currentBlock != block)
                {
                    currentBlock = block;
                    currentBlockScript = blockScript;
                    newBlock = true;
                }
            }

            if (blockTransform.localPosition.z < player.gameObject.transform.position.z - longuestBlock * blocksBehind)
            {
                blocksToDelete.Add(block);
            }
        }

        foreach (GameObject g in blocksToDelete)
        {
            DeleteBlock(g);
            SpawnRandomBlock();
        }
    }

    void SpawnRandomBlock()
    {
        int i = Random.Range(0, blocksToSpawn.Length);
        GameObject blockToSpawn = blocksToSpawn[i];
        BlockScript blockToSpawnScript = blockToSpawn.GetComponent<BlockScript>();

        GameObject lastBlock = spawnedBlocks[spawnedBlocks.Count - 1];
        BlockScript lastBlockScript = lastBlock.GetComponent<BlockScript>();

        Vector3 endPosition = lastBlockScript.GetEndPoint();
        GameObject spawnedBlock = Instantiate(blockToSpawn, endPosition, transform.localRotation, transform);
        spawnedBlocks.Add(spawnedBlock);
    }

    void DeleteBlock(GameObject g)
    {
        spawnedBlocks.Remove(g);
        Destroy(g);
    }

    public void NewLevel()
    {
        if (spawnedBlocks.Count > 0)
        {
            for (int x = spawnedBlocks.Count - 1; x >= 0; x--)
            {
                GameObject g = spawnedBlocks[x];
                DeleteBlock(g);
            }
        }

        spawnedBlocks.Add(Instantiate(blocksToSpawn[0], Vector3.zero, Quaternion.identity, transform));

        while (blocksAhead + blocksBehind + 1 > spawnedBlocks.Count)
        {
            SpawnRandomBlock();
        }
    }
}
