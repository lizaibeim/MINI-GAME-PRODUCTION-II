using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRail : MonoBehaviour
{
    [SerializeField] GameObject Robo;
    [SerializeField] bool TimerBased = false;
    [SerializeField] float Timer = 2f;
    [SerializeField] bool TriggerBased = false;

    private GameObject standin;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        standin = Instantiate(Robo);
        standin.transform.SetParent(this.transform);
        standin.transform.position = Robo.transform.position;

        var components = standin.GetComponents(typeof(Component));
        foreach (var c in components)
        {
            if (c.GetType() != typeof(SkinnedMeshRenderer)){
                if(c.GetType() != typeof(MeshRenderer))
                    if(c.GetType() != typeof(MeshFilter))
                        if (c.GetType() != typeof(Transform))
                            Destroy(c);
            }
        }

        Robo.SetActive(false);
        boxCollider = GetComponent<BoxCollider>();

        if(TimerBased)
            StartCoroutine(releaseRobo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator releaseRobo()
    {
        yield return new WaitForSeconds(Timer);
        deployRobot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerBased)
        {
            if (other.CompareTag("Player"))
                deployRobot();
        }
    }

    void deployRobot()
    {
        Robo.SetActive(true);
        standin.SetActive(false);
    }
}
