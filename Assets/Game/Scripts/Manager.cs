using UnityEngine;

public class Manager : MonoBehaviour
{
    public int LeftJoy;
    public int RightJoy;

    public void Down_LeftJoy()
    {
        LeftJoy = 1;
    }
    public void Up_LeftJoy()
    {
        LeftJoy = 0;
    }
    public void Down_RightJoy()
    {
        RightJoy = 1;
    }
        public void Up_RightJoy()
    {
        RightJoy = 0;
    }
}
