
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private int _longueur;
    [SerializeField] private int _largeur;
    [SerializeField] private TerrainData _terrain;
    [SerializeField] private float _tauxSand;
    [SerializeField] private float _tauxHill;
    [SerializeField] private GameObject[] _terrainProps;
    [SerializeField] private UnityEngine.Terrain _activeTerrain;
    
    
    //Tableau 2 dimensions / Tableau dans un tableau / Matrix
    private float[,] _theWorldHeight;

    void Start()
    {
        init();
        genererLaby();
        _terrain.SetHeights(0, 0, _theWorldHeight);
        TerrainCollider col = _activeTerrain.gameObject.AddComponent<TerrainCollider>();
        col.terrainData = _terrain;
    }
    
    public void init()
    {
        //Louer la m�moire, conna�tre la dimension de la matrix
        _theWorldHeight = new float[_longueur, _largeur];
    }
    
    public void genererLaby()
    {
        float randx = Random.Range(-100000f, 100000f);
        float randy = Random.Range(-100000f, 100000f);
        float[,,] texture = _terrain.GetAlphamaps(0,0,_terrain.alphamapResolution,_terrain.alphamapResolution);
        _terrain.treeInstances = new TreeInstance[100];        
        //Parcourir les 2 dimensions du tableau (faire toutes la longueur (0,1 ; 0,2 ..) puis revenir et augmenter (1,1 ; 1,2 ..))
        //Fait la premiere boucle puis reste dans la deuxieme boucle jusqu'a la condition puis retourne a la premiere
        for (int i = 0; i < _longueur-1; i++)
        {
            for (int j = 0; j < _largeur-1; j++)
            {
                float die = Mathf.PerlinNoise(i * 0.02f+randx , j * 0.02f + randy);
                die *= Mathf.PerlinNoise(i * 0.03f + randx, j * 0.03f + randy);
                die *= Mathf.PerlinNoise(i * 0.04f + randx, j * 0.04f + randy);
                die *= Mathf.PerlinNoise(i * 0.05f + randx, j * 0.05f + randy);
                _theWorldHeight[i , j] = die *4; // le * impacte la hauteur/profondeur du PerlinNoise plus le chiffre est haut plus les collines seront haute
                
                float hauteurTerrain = _theWorldHeight[i, j] * 20;
                
                if (hauteurTerrain >= _tauxSand && hauteurTerrain <= 3 )
                {            
                    float PropsIntensity = Random.Range(1, 100);
                    if (PropsIntensity <= 1)
                    {
                        int randomProps = Random.Range(0, _terrainProps.Length);
                        TreeInstance arbre = new TreeInstance();
                        arbre.position = new Vector3((float)j / _terrain.heightmapResolution, 0, (float)i / _terrain.heightmapResolution);
                        
                        float PropsChance = Random.Range(1, 100);

                        if (PropsChance <= 70) { arbre.prototypeIndex = Random.Range(0,6); }
                        else if (PropsChance > 70 && PropsChance < 90) { arbre.prototypeIndex = 6; }
                        else if (PropsChance >= 90 && PropsChance < 95) { arbre.prototypeIndex = Random.Range(7,11); }
                        else if (PropsChance >= 95) { arbre.prototypeIndex = Random.Range(11,14); }

                        arbre.color = Color.white;
                        arbre.widthScale = Random.Range(1f, 1f);
                        arbre.heightScale = Random.Range(1f, 1f);
                        arbre.lightmapColor = Color.white;

                        _activeTerrain.AddTreeInstance(arbre);
                        
                        _activeTerrain.Flush();

                    }
                }                    
                if (hauteurTerrain > 3 && hauteurTerrain < _tauxHill)
                {
                    float PropsIntensity = Random.Range(1, 100);
                    if (PropsIntensity <= 5)
                    {
                        int randomProps = Random.Range(0, _terrainProps.Length);
                        TreeInstance arbre = new TreeInstance();
                        arbre.position = new Vector3((float)j / _terrain.heightmapResolution, 0, (float)i / _terrain.heightmapResolution);

                        float PropsChance = Random.Range(1, 100);

                        if (PropsChance <= 70) { arbre.prototypeIndex = Random.Range(0, 6); }
                        else if (PropsChance > 70 && PropsChance < 90) { arbre.prototypeIndex = 6; }
                        else if (PropsChance >= 90 && PropsChance < 95) { arbre.prototypeIndex = Random.Range(7, 11); }
                        else if (PropsChance >= 95) { arbre.prototypeIndex = Random.Range(11, 14); }

                        arbre.color = Color.white;
                        arbre.widthScale = Random.Range(1f, 1f);
                        arbre.heightScale = Random.Range(1f, 1f);
                        arbre.lightmapColor = Color.white;

                        _activeTerrain.AddTreeInstance(arbre);

                        _activeTerrain.Flush();

                    }
                }                   
                if (hauteurTerrain >= _tauxHill )
                { 
                    float PropsIntensity = Random.Range(1, 100);
                    if (PropsIntensity <= 10)
                    {
                        int randomProps = Random.Range(0, _terrainProps.Length);
                        TreeInstance arbre = new TreeInstance();
                        arbre.position = new Vector3((float)j / _terrain.heightmapResolution, 0, (float)i / _terrain.heightmapResolution);
                        
                        float PropsChance = Random.Range(1, 100);

                        if (PropsChance <= 70) { arbre.prototypeIndex = Random.Range(0, 6); }
                        else if (PropsChance > 70 && PropsChance < 90) { arbre.prototypeIndex = 6; }
                        else if (PropsChance >= 90 && PropsChance < 95) { arbre.prototypeIndex = Random.Range(7, 11); }
                        else if (PropsChance >= 95) { arbre.prototypeIndex = Random.Range(11, 14); }

                        arbre.color = Color.white;
                        arbre.widthScale = Random.Range(1f, 1f);
                        arbre.heightScale = Random.Range(1f, 1f);
                        arbre.lightmapColor = Color.white;

                        _activeTerrain.AddTreeInstance(arbre);
                        
                        _activeTerrain.Flush();

                    }
                }
                
                if (hauteurTerrain <= _tauxSand)
                {
                    float variation = Mathf.Lerp(0, 1, hauteurTerrain / _tauxSand);
                    texture[i, j, 0] = variation;
                    texture[i, j, 1] = 1 - variation;
                    texture[i, j, 2] = 0;
                }
                else 
                {
                    float variation = Mathf.Lerp(0, 1, hauteurTerrain / _tauxHill);
                    texture[i, j, 0] = 1 - variation;
                    texture[i, j, 1] = 0;
                    texture[i, j, 2] = variation;
                }
            }
        }
        _terrain.SetAlphamaps(0, 0, texture);
    }
}
