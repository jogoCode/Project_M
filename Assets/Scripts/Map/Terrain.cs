using UnityEditor;
using UnityEngine;

public class Terrain : MonoBehaviour
{

    [SerializeField] private GameObject _brique;
    [SerializeField] private GameObject _brique2;
    [SerializeField] private int _longueur;
    [SerializeField] private int _largeur;
    [SerializeField] private TerrainData _terrain;

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
        //Louer la mémoire, connaître la dimension de la matrix
        _theWorldHeight = new float[_longueur, _largeur];
    }

    public void genererLaby()
    {
        float randx = Random.Range(-10000f,10000f);
        float randy = Random.Range(-10000f, 10000f);
        float[,,] texture = _terrain.GetAlphamaps(0,0,_terrain.alphamapResolution,_terrain.alphamapResolution);
        
        
        //Parcourir les 2 dimensions du tableau (faire toutes la longueur (0,1 ; 0,2 ..) puis revenir et augmenter (1,1 ; 1,2 ..))
        //Fait la première boucle puis reste dans la deuxième boucle jusqu'à la condition puis retourne a la première
        for (int i = 0; i < _longueur-1; i++)
        {
            for (int j = 0; j < _largeur-1; j++)
            {
                float die = Mathf.PerlinNoise(i * 0.02f+randx , j * 0.02f + randy);
                die *= Mathf.PerlinNoise(i * 0.03f + randx, j * 0.03f + randy);
                die *= Mathf.PerlinNoise(i * 0.04f + randx, j * 0.04f + randy);
                die *= Mathf.PerlinNoise(i * 0.05f + randx, j * 0.05f + randy);
                _theWorldHeight[i , j] = die *4; // le * impacte la hauteur/profondeur du PerlinNoise plus le chiffre est haut plus les collines seront haute
                if (_theWorldHeight[i,j] * 20 <= 0.5)
                {
                    texture[i, j, 0] = 0;
                    texture[i, j, 1] = Mathf.Lerp(0, 1,0.1f); 
                    texture[i, j, 2] = 0;
                }
                else if (_theWorldHeight[i,j] * 20 >= 10 )
                {
                    texture[i, j, 0] = 0;
                    texture[i, j, 1] = 0;
                    texture[i, j, 2] = 1;
                }
                else
                {
                    texture[i, j, 0] = 1;
                    texture[i, j, 1] = 0;
                    texture[i, j, 2] = 0;
                }
            }
        }
        _terrain.SetAlphamaps(0, 0, texture);
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
