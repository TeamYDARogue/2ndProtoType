[System.Serializable]
public struct FadeTimeData
{
    public FadeTimeData(float inTime,float outTime):this()
    {
        this.inTime = inTime;
        this.outTime = outTime;
    }

    public static FadeTimeData Zero
    {
        get { return new FadeTimeData(0, 0); }
    }

    public float inTime;
    public float outTime;
}