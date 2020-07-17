using UnityEngine;
public class Prescictentdtata : MonoBehaviour
{

	#region Varibles

	public static Prescictentdtata PD;

	public bool[] allskins;

	public int myskin;

    private void OnEnable() => Prescictentdtata.PD = this;

	#endregion


    public void SkinsStringtodata(string skinsIn)
	{
		for (int i = 0; i < skinsIn.Length; i++)
		{
			if(int.Parse(skinsIn[i].ToString()) > 0) allskins[i] = true;
			else allskins[i] = false;
		}
		MenuController.Mc.SetUpStore();
	}

	public string SkinsDataToString()
	{
		string tostring = "";
		for (int i = 0; i < allskins.Length; i++)
		{
			if (allskins[i] == true) tostring += "1";
			else tostring += "0";
		}
		return tostring;
	}
}