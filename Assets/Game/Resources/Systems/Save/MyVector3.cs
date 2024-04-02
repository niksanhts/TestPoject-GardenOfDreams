using System;

[Serializable]
public class MyVector3
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }


    public MyVector3(float x, float y, float z) 
    {
        X = x; Y = y; Z = z;
    }
}
