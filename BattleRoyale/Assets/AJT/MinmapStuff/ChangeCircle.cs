using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(LineRenderer))]
public class ChangeCircle : NetworkBehaviour
{
    [SyncVar]
    float xScale;
    [SyncVar]
    float zScale;

	[Range(0, 360)]
	public int Segments;
	[Range(0,5000)]
	public float XRadius;
	[Range(0,5000)]
	public float YRadius;
	public GameObject ZoneWall;
	public bool Shrinking;
    public bool OutsideOfCircle;
    public GameObject OutsideZoneImage;

    NetworkManager networkManager;
    NetworkDiscoveryScript networkDiscoveryScript;

    #region Private Members
    private WorldCircle circle;
	private LineRenderer renderer;
	private float [] radii = new float[2];
	#endregion

	void Start ()
	{
		renderer = gameObject.GetComponent<LineRenderer>();
		radii[0] = XRadius;  radii[1] = YRadius;
		circle = new WorldCircle(ref renderer, Segments, radii);
        //ZoneWall = GameObject.FindGameObjectWithTag ("ZoneWall");

        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
    }

	// Update is called once per frame
	void Update ()
	{
	/*	if (Input.GetMouseButtonDown (0))
		{
			Shrinking = true;
		}*/
        if(ZoneWall == null)
        {
            ZoneWall = GameObject.FindGameObjectWithTag("ZoneWall");
            if (ZoneWall == null)
                return;
        }
        
        if(networkDiscoveryScript.isServer == false)
        {
            ZoneWall.transform.localScale = new Vector3(xScale, ZoneWall.transform.localScale.y, zScale);
            return;
        }


		if(GameManager.instance.zoneShrinking)
		{
			XRadius = Mathf.Lerp(XRadius, ShrinkCircle(XRadius)[0], Time.deltaTime * 0.05f);
			circle.Draw(Segments, XRadius, XRadius);
		}

		ZoneWall.transform.localScale = new Vector3 ((XRadius * 0.01f), 1, (XRadius * 0.01f));

        xScale = ZoneWall.transform.localScale.x;
        zScale = ZoneWall.transform.localScale.z;

        if (XRadius <= 0)
        {
            GameManager.instance.zoneShrinking = false;
            GameManager.instance.zoneShrunk = false;
        }
        /*if (OutsideOfCircle)
        {
            OutsideZoneImage.SetActive(true);
        }
        else
        {
            OutsideZoneImage.SetActive(false);
        }*/
	}

	private float[] ShrinkCircle(float amount)
	{
		float newXR = circle.radii[0] - amount;
		float newYR = circle.radii[1] - amount;
		float [] retVal = new float[2];
		retVal[0] = newXR;
		retVal[1] = newYR;
		return retVal;
	}
}