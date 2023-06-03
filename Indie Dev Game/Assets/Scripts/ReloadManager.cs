using UnityEngine;

public class ReloadManager : MonoBehaviour
{
    public GameObject reloadBarTick;
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject line;
    public float duration = 10f;

    private float timePassed = 0f;
    private bool isReloading = false;

    private void Start()
    {
        //Setting all of the interface elements to inactive upon start so that they are not being displayed
        reloadBarTick.SetActive(false);
        startPoint.SetActive(false);
        endPoint.SetActive(false);
        line.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //If the weapon is currently being reloaded...
        if (isReloading)
        {
            //Set all interface elements to active so that they are being displayed
            reloadBarTick.SetActive(true);
            startPoint.SetActive(true);
            endPoint.SetActive(true);
            line.SetActive(true);
            //If the duration of the reload is greater than the amount of time passed (or equal to)
            if (timePassed <= duration)
            {
                timePassed += Time.deltaTime;
                //Smoothly transition the reload tick between the start and end point, based on how far into the reload we are
                reloadBarTick.transform.position = Vector3.Lerp(startPoint.transform.position, endPoint.transform.position, timePassed / duration);
            //Once reloaded...
            } else
            {
                //Set isReloading to false to not loop the interface
                isReloading = false;
                //Set the progress bar back to it's starting position
                reloadBarTick.transform.position = startPoint.transform.position;
                //Reset the time passed during the reload (we are no longer reloading)
                timePassed = 0f;
                //Turning off all of the ui elements again
                reloadBarTick.SetActive(false);
                startPoint.SetActive(false);
                endPoint.SetActive(false);
                line.SetActive(false);
            }
        }
    }

    //Public func so that other scripts can access an otherwise private var
    public void SetisReloadingToTrue()
    {
        isReloading = true;
    }
}
