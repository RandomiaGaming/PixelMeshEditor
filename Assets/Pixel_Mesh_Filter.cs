using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Pixel_Mesh_Filter : MonoBehaviour
{
    private MeshFilter MF;
    private string Loaded_Mesh_Json = "";
    public TextAsset Pixel_Mesh_Json = null;
    void Start()
    {
        Regenerate_Mesh();
    }
    private void Regenerate_Mesh()
    {
        if (MF == null)
        {
            MF = GetComponent<MeshFilter>();
        }
        if (MF == null)
        {
            Loaded_Mesh_Json = "!NULL!";
            return;
        }
        else
        {
            if (Pixel_Mesh_Json == null || Pixel_Mesh_Json.text == "")
            {
                if (Loaded_Mesh_Json != "")
                {
                    Loaded_Mesh_Json = "";
                    Mesh Custom_Mesh = new Mesh();
                    Custom_Mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(0, 1, 1) };
                    Custom_Mesh.triangles = new int[] { 2, 1, 0, 0, 3, 2, 4, 5, 6, 6, 7, 4, 0, 1, 5, 0, 5, 4, 0, 4, 7, 7, 3, 0, 7, 2, 3, 6, 2, 7, 1, 2, 6, 6, 5, 1 };
                    Custom_Mesh.colors = new Color[] { Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta };
                    Custom_Mesh.name = "Pixel Mesh";
                    MF.sharedMesh = Custom_Mesh;
                    MF.mesh = Custom_Mesh;
                }
            }
            else
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
                        DataContractJsonSerializer ser1 = new DataContractJsonSerializer(typeof(Pixel_Mesh));
                        object Output = ser1.ReadObject(memoryStream);
                        if (Output == null || Output.GetType() != typeof(Pixel_Mesh))
                        {
                            Loaded_Mesh_Json = "";
                            Mesh Custom_Mesh = new Mesh();
                            Custom_Mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(0, 1, 1) };
                            Custom_Mesh.triangles = new int[] { 2, 1, 0, 0, 3, 2, 4, 5, 6, 6, 7, 4, 0, 1, 5, 0, 5, 4, 0, 4, 7, 7, 3, 0, 7, 2, 3, 6, 2, 7, 1, 2, 6, 6, 5, 1 };
                            Custom_Mesh.colors = new Color[] { Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta };
                            Custom_Mesh.name = "Pixel Mesh";
                            MF.sharedMesh = Custom_Mesh;
                            MF.mesh = Custom_Mesh;
                        }
                        else
                        {
                            Pixel_Mesh Loaded_Mesh = (Pixel_Mesh)Output;
                            Loaded_Mesh.Pixels_Per_Unit = Mathf.Clamp(Loaded_Mesh.Pixels_Per_Unit, 0, int.MaxValue);
                            Mesh Custom_Mesh = new Mesh();
                            List<Vector3> Vertices = new List<Vector3>();
                            List<int> Triangles = new List<int>();
                            List<Color> Colors = new List<Color>();
                            int Triangles_Offset = 0;
                            foreach (Pixel_Data PD in Loaded_Mesh.Mesh_Data)
                            {
                                List<Vector3> Cleaned_Vertices = new List<Vector3>();
                                foreach (Vector3 Vertice in new Vector3[] { new Vector3(PD.x, PD.y, PD.z), new Vector3(PD.x + 1, PD.y, PD.z), new Vector3(PD.x + 1, PD.y + 1, PD.z), new Vector3(PD.x, PD.y + 1, PD.z), new Vector3(PD.x, PD.y, PD.z + 1), new Vector3(PD.x + 1, PD.y, PD.z + 1), new Vector3(PD.x + 1, PD.y + 1, PD.z + 1), new Vector3(PD.x, PD.y + 1, PD.z + 1) })
                                {
                                    Cleaned_Vertices.Add(new Vector3(Vertice.x / Loaded_Mesh.Pixels_Per_Unit, Vertice.y / Loaded_Mesh.Pixels_Per_Unit, Vertice.z / Loaded_Mesh.Pixels_Per_Unit));
                                }
                                Vertices.AddRange(Cleaned_Vertices);
                                Triangles.AddRange(new int[] { Triangles_Offset + 2, Triangles_Offset + 1, Triangles_Offset, Triangles_Offset, Triangles_Offset + 3, Triangles_Offset + 2, Triangles_Offset + 4, Triangles_Offset + 5, Triangles_Offset + 6, Triangles_Offset + 6, Triangles_Offset + 7, Triangles_Offset + 4, Triangles_Offset, Triangles_Offset + 1, Triangles_Offset + 5, Triangles_Offset, Triangles_Offset + 5, Triangles_Offset + 4, Triangles_Offset, Triangles_Offset + 4, Triangles_Offset + 7, Triangles_Offset + 7, Triangles_Offset + 3, Triangles_Offset, Triangles_Offset + 7, Triangles_Offset + 2, Triangles_Offset + 3, Triangles_Offset + 6, Triangles_Offset + 2, Triangles_Offset + 7, Triangles_Offset + 1, Triangles_Offset + 2, Triangles_Offset + 6, Triangles_Offset + 6, Triangles_Offset + 5, Triangles_Offset + 1 });
                                Color_Data Pixel_Color = Loaded_Mesh.Color_Pallet[PD.c];
                                for (int i = 0; i < 8; i++)
                                {
                                    Colors.Add(new Color(Pixel_Color.r / 255f, Pixel_Color.g / 255f, Pixel_Color.b / 255f, 1f));
                                }
                                Triangles_Offset += 8;
                            }
                            Custom_Mesh.vertices = Vertices.ToArray();
                            Custom_Mesh.triangles = Triangles.ToArray();
                            Custom_Mesh.colors = Colors.ToArray();
                            Custom_Mesh.name = "Pixel Mesh";
                            MF.sharedMesh = Custom_Mesh;
                            MF.mesh = Custom_Mesh;
                        }
                    }
                    catch
                    {
                        Loaded_Mesh_Json = "";
                        Mesh Custom_Mesh = new Mesh();
                        Custom_Mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 1, 1), new Vector3(0, 1, 1) };
                        Custom_Mesh.triangles = new int[] { 2, 1, 0, 0, 3, 2, 4, 5, 6, 6, 7, 4, 0, 1, 5, 0, 5, 4, 0, 4, 7, 7, 3, 0, 7, 2, 3, 6, 2, 7, 1, 2, 6, 6, 5, 1 };
                        Custom_Mesh.colors = new Color[] { Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta, Color.magenta };
                        Custom_Mesh.name = "Pixel Mesh";
                        MF.sharedMesh = Custom_Mesh;
                        MF.mesh = Custom_Mesh;
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Regenerate_Mesh();
    }
}
public struct Color_Data
{
    public int r;
    public int g;
    public int b;
    public Color_Data(int r, int g, int b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}
public struct Pixel_Data
{
    public int x;
    public int y;
    public int z;
    public int c;
    public Pixel_Data(int x, int y, int z, int c)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.c = c;
    }
}
public class Pixel_Mesh
{
    public int Pixels_Per_Unit = 16;
    public List<Color_Data> Color_Pallet = new List<Color_Data>();
    public List<Pixel_Data> Mesh_Data = new List<Pixel_Data>();
}