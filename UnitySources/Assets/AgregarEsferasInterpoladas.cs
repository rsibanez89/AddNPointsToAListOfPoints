using UnityEngine;
using System.Collections.Generic;

public class AgregarEsferasInterpoladas : MonoBehaviour
{

    private List<Vector3> lista;
    public int cantidadAAgregar = 0;
    public bool agregar = false;

    private List<GameObject> esferasDibujadas;

    // Use this for initialization
    void Start()
    {
        Util.leerAnimacion("Assets\\trajectory.txt", out lista);
        esferasDibujadas = new List<GameObject>();
        dibujar(lista, new Color(0f, 0f, 0.3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (agregar)
        {
            //Elimino las agregadas anteriormente
            int cant = esferasDibujadas.Count;
            for (int i = 0; i < cant; i++)
            {
                GameObject eliminar = esferasDibujadas[0];
                esferasDibujadas.Remove(eliminar);
                Destroy(eliminar);
            }

            //Cargo las nuevas
            List<Vector3> nuevaLista = remuestrear(lista, lista.Count + cantidadAAgregar);
            dibujar(nuevaLista, new Color(0.3f, 0f, 0f));
            agregar = false;
        }
    }

    private void dibujar(List<Vector3> esferasADibujar, Color c)
    {
        for (int i = 0; i < esferasADibujar.Count; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new UnityEngine.Vector3(0.05f, 0.05f, 0.05f);
            Vector3 v = esferasADibujar[i];
            sphere.transform.position = new UnityEngine.Vector3(v.x, v.y, v.z);
            sphere.GetComponent<Renderer>().material.color = c;
            esferasDibujadas.Add(sphere);
        }
    }

    // Remuestrea devuelve una nueva trayectoria remuestreada de N puntos
    public List<Vector3> remuestrear(List<Vector3> trajectoria, int N)
    {
        float incremento = Util.getLongitud(trajectoria) / (N - 1);
        float D = 0;
        List<Vector3> trayectoriaRemuestreada = new List<Vector3>();
        List<Vector3> trayectoryAux = Util.copiarTrayectoria(trajectoria); //Para no modificar la trayectoria original
        trayectoriaRemuestreada.Add(trayectoryAux[0].clon());
        for (int i = 1; i < trayectoryAux.Count; i++)
        {
            float distancia = Vector3.getDistancia(trayectoryAux[i - 1], trayectoryAux[i]);
            if ((D + distancia) >= incremento)
            {
                Vector3 nuevo = new Vector3();
                nuevo.x = trayectoryAux[i - 1].x + ((incremento - D) / distancia) * (trayectoryAux[i].x - trayectoryAux[i - 1].x);
                nuevo.y = trayectoryAux[i - 1].y + ((incremento - D) / distancia) * (trayectoryAux[i].y - trayectoryAux[i - 1].y);
                nuevo.z = trayectoryAux[i - 1].z + ((incremento - D) / distancia) * (trayectoryAux[i].z - trayectoryAux[i - 1].z);
                trayectoriaRemuestreada.Add(nuevo);
                trayectoryAux.Insert(i, nuevo);
                D = 0;
            }
            else
                D += distancia;
        }
        if (trayectoriaRemuestreada.Count < N)
            trayectoriaRemuestreada.Add(trayectoryAux[trayectoryAux.Count - 1].clon()); // agrego el último

        return trayectoriaRemuestreada;
    }



}
