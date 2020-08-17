using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;

public class EditorPixelMeshFilter : MonoBehaviour
{
    private string Loaded_Mesh_Json = "";
    public TextAsset Pixel_Mesh_Json = null;
    public GameObject PixelPrefab;
    void Start()
    {
        Regenerate_Mesh();
    }
    public void Regenerate_Mesh()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        if (Pixel_Mesh_Json != null && Pixel_Mesh_Json.text != "")
        {
            if (Pixel_Mesh_Json.text == Loaded_Mesh_Json)
            {
                return;
            }
            else
            {
                Loaded_Mesh_Json = Pixel_Mesh_Json.text;
                try
                {
                    byte[] JsonToBytes = Encoding.ASCII.GetBytes(Pixel_Mesh_Json.text);
                    MemoryStream memoryStream = new MemoryStream(JsonToBytes);
                    DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(PixelMesh));
                    object Output = ser1.ReadObject(memoryStream);
                    if (Output != null && Output.GetType() == typeof(PixelMesh))
                    {
                        PixelMesh Loaded_Mesh = (PixelMesh)Output;
                        foreach (PixelData PD in Loaded_Mesh.Mesh_Data)
                        {
                            GameObject Pixel = Instantiate(PixelPrefab, transform);
                            Pixel.transform.position = new Vector3(PD.x / (float)Loaded_Mesh.Pixels_Per_Unit, PD.y / (float)Loaded_Mesh.Pixels_Per_Unit, PD.z / (float)Loaded_Mesh.Pixels_Per_Unit);
                            Mesh Custom_Mesh = new Mesh();
                            Custom_Mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(0, 1, 1) };
                            Custom_Mesh.triangles = new int[] { 2, 1, 0, 0, 3, 2, 4, 5, 6, 6, 7, 4, 0, 1, 5, 0, 5, 4, 0, 4, 7, 7, 3, 0, 7, 2, 3, 6, 2, 7, 1, 2, 6, 6, 5, 1 };
                            Color MeshColor = new Color(Loaded_Mesh.Color_Pallet[PD.c].r / 255f, Loaded_Mesh.Color_Pallet[PD.c].g / 255f, Loaded_Mesh.Color_Pallet[PD.c].b / 255f, 1f);
                            Custom_Mesh.colors = new Color[] { MeshColor, MeshColor, MeshColor, MeshColor, MeshColor, MeshColor, MeshColor, MeshColor };
                            Custom_Mesh.name = "Pixel Mesh";
                            Custom_Mesh.RecalculateBounds();
                            Custom_Mesh.RecalculateNormals();
                            Pixel.GetComponent<MeshFilter>().sharedMesh = Custom_Mesh;
                            Pixel.GetComponent<MeshFilter>().mesh = Custom_Mesh;
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}
