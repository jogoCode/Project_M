using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Terrain : MonoBehaviour
{

    [SerializeField] private GameObject _brique;
    [SerializeField] private GameObject _brique2;
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
        //afficherLaby();
        _terrain.SetHeights(0, 0, _theWorldHeight);
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
        _activeTerrain.GetComponent<TerrainCollider>().;
        
        
        
        //Parcourir les 2 dimensions du tableau (faire toutes la longueur (0,1 ; 0,2 ..) puis revenir et augmenter (1,1 ; 1,2 ..))
        //Fait la premiere boucle puis reste dans la deuxieme boucle jusqu'� la condition puis retourne a la premiere
        for (int i = 0; i < _longueur-1; i++)
        {
            for (int j = 0; j < _largeur-1; j++)
            {
                float die = Mathf.PerlinNoise(i * 0.02f+randx , j * 0.02f + randy);
                die *= Mathf.PerlinNoise(i * 0.03f + randx, j * 0.03f + randy);
                die *= Mathf.PerlinNoise(i * 0.04f + randx, j * 0.04f + randy);
                die *= Mathf.PerlinNoise(i * 0.05f + randx, j * 0.05f + randy);
                _theWorldHeight[i , j] = die *4; // le * impacte la hauteur/profondeur du PerlinNoise plus le chiffre est haut plus les collines seront haute
                //Debug.Log(_theWorldHeight[i, j] * 20);
                
                if(_theWorldHeight[i, j] * 20 >= 0.5f)
                {
                    
                    float PropsIntensity = Random.Range(1, 100);
                    if (PropsIntensity <= 10)
                    {

                        int randomProps = Random.Range(0, _terrainProps.Length);
                        //Instantiate(_terrainProps[randomProps], _activeTerrain.GetPosition()+new Vector3 (i * _terrain., _theWorldHeight[i, j] * 20, j * _terrain.) , Quaternion.identity);
                        
                        TreeInstance arbre = new TreeInstance();
                        arbre.position = new Vector3((float)i / _terrain.heightmapResolution, 0, (float)j / _terrain.heightmapResolution);
                        arbre.prototypeIndex = randomProps;
                        //Debug.Log(arbre.position);
                        arbre.color = Color.white;
                        arbre.widthScale = Random.Range(1f, 2f);
                        arbre.heightScale = Random.Range(2f, 3f);
                        arbre.lightmapColor=Color.white;

                        _activeTerrain.AddTreeInstance(arbre);
                        
                        _activeTerrain.Flush();

                    }

                }
                
                if (_theWorldHeight[i,j] * 20 <= _tauxSand)
                {
                    float variation = Mathf.Lerp(0, 1, _theWorldHeight[i, j] * 20 / _tauxSand);
                    texture[i, j, 0] = variation;
                    texture[i, j, 1] = 1 - variation;
                    texture[i, j, 2] = 0;
                }
                else 
                {
                    float variation = Mathf.Lerp(0, 1, _theWorldHeight[i, j] * 20 / _tauxHill);
                    texture[i, j, 0] = 1 - variation;
                    texture[i, j, 1] = 0;
                    texture[i, j, 2] = variation;
                }
            }
        }
        _terrain.SetAlphamaps(0, 0, texture);
        _activeTerrain.GetComponent<TerrainCollider>().enabled = true;
    }
    
    public void afficherLaby()
    {
        for (int i = 0; i < _longueur; i++)
        {
            for (int j = 0; j < _largeur; j++)
            {
                //J'affiche si 0 ou 1
                if (_theWorldHeight[i, j] == 0)
                {

                }
                if (_theWorldHeight[i, j] == 1)
                {
                    /* Changement en Y
                    int posY = Random.Range(0, 2);
                    Vector3 pos = new Vector3(i, posY, j);                    
                    Instantiate(_brique, pos, Quaternion.identity);
                    /*/
                    float y = Mathf.PerlinNoise(i / 5f, j / 5f) * 4;
                    Vector3 pos = new Vector3(i * 4, y, j * 4);
                    GameObject briqueGo = Instantiate(_brique, pos, Quaternion.identity);
                    briqueGo.transform.parent = transform.parent;
                    briqueGo.name = "Case" + i + " " + j;
                    //*/
                }
                //Mur
                if (_theWorldHeight[i, j] == 3)
                {
                    Vector3 pos = new Vector3(i, 0, j);
                    Instantiate(_brique2, pos, Quaternion.identity);
                }
            }
        }
    }
    

}
