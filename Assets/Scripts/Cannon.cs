using UnityEngine;

public class Cannon : MonoBehaviour
{
    private float timer;
    public float rate;
    public GameObject prefab;
    public Transform firepoint;
    public Transform target;
    private float scaleMultiplier = 1;
    public float height;
    public float timeToDestroy;

    private void Update()
    {
        SetTargetPosition();
        Timer(Fire);
        ChangeRotation();
        ChangeScaleBullet();
    }

    public delegate void TimeDelegate();

    private void Timer(TimeDelegate timedAction)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timedAction();
            timer = rate;
        }
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        }
        target.position = hit.point + Vector3.up * 0.1f;
    }

    private void Fire()
	{
        var instance = Instantiate(prefab, firepoint.position, Quaternion.identity).GetComponent<CannonBall>();
        instance.Setup(scaleMultiplier, target.position, height, timeToDestroy);
    }

    private void ChangeScaleBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            scaleMultiplier *= 2;
    }

    private void ChangeRotation()
    {
        Vector3 rotation = target.position - transform.position;
        rotation.y = 0;
        transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
    }
}
