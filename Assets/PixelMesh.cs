using System.Collections.Generic;
public sealed class PixelMesh
{
    public int Pixels_Per_Unit = 1;
    public List<ColorData> Color_Pallet = new List<ColorData>();
    public List<PixelData> Mesh_Data = new List<PixelData>();
}
public sealed class ColorData
{
    private ColorData() { }
    public int r = 0;
    public int g = 0;
    public int b = 0;
    public static ColorData Create()
    {
        ColorData output = new ColorData();
        output.r = 0;
        output.g = 0;
        output.b = 0;
        return output;
    }
    public static ColorData Create(int r, int g, int b)
    {
        ColorData output = new ColorData();
        output.r = r;
        output.g = g;
        output.b = b;
        return output;
    }
}
public sealed class PixelData
{
    public int x = 0;
    public int y = 0;
    public int z = 0;
    public int c = 0;
    private PixelData() { }
    public static PixelData Create()
    {
        PixelData output = new PixelData();
        output.x = 0;
        output.y = 0;
        output.z = 0;
        output.c = 0;
        return output;
    }
    public static PixelData Create(int x, int y, int z, int c)
    {
        PixelData output = new PixelData();
        output.x = x;
        output.y = y;
        output.z = z;
        output.c = c;
        return output;
    }
    public PixelData Clone()
    {
        PixelData output = new PixelData();
        output.x = x;
        output.y = y;
        output.z = z;
        output.c = c;
        return output;
    }
}