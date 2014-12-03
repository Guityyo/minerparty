using UnityEngine;
using System.Collections;

public class Perspective : Sense
{
    public int FieldOfView = 45;
    public int ViewDistance = 100;

    private Transform playerTrans, mineTrans;
    private Vector3 rayDirection;

    protected override void Initialise() 
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		mineTrans = GameObject.Find ("Mine").transform;
    }

	// Update is called once per frame
    protected override void UpdateSense() 
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
            DetectAspect();
	}
	
	//Detect perspective field of view for the AI Character
	void DetectAspect()
	{
		RaycastHit hit;
		rayDirection = playerTrans.position - transform.position;
		
		if ((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
		{
			// Detect if player is within the field of view
			if (Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
			{
				Aspect aspect = hit.collider.GetComponent<Aspect>();
				if (aspect != null)
				{
					//Check the aspect
					if (aspect.aspectName == aspectName)
					{
						//						enemy = true;
						Debug.Log("Enemy Detected!!");
					}
				}
			}
		}
	}

	//Detect perspective field of view for the AI Character
	public bool IsWithinViewfield(GameObject obj) {
		RaycastHit hit;
		rayDirection = obj.transform.position - transform.position;
		
		if ((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView) {
			// Detect if mine is within the field of view
			if (Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance)) {
				Debug.Log ("Mine visible!");
				return true;
			}
		}
		Debug.Log ("Mine not visible :(");
		return false;
	}

    /// <summary>
    /// Show Debug Grids and obstacles inside the editor
    /// </summary>
    void OnDrawGizmos()
    {
        if ( playerTrans == null)
            return;

        Debug.DrawLine(transform.position, playerTrans.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * ViewDistance);

        //Approximate perspective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += FieldOfView * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= FieldOfView * 0.5f;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
}