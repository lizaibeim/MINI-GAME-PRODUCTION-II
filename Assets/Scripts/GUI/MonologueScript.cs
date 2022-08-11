using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueScript : MonoBehaviour
{
    public bool displayText;
    public GameObject audioSource;
    public GameObject textField;
    public GameObject monologueAudio;
    [SerializeField] float monologueDuration = 10f;
    bool alreadyPlayed = false;
    GameObject instantiatedAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyPlayed) return;
        if (!other.gameObject.CompareTag("Player")) return;
        alreadyPlayed = true;
        instantiatedAudio = Instantiate(monologueAudio, audioSource.transform.position, audioSource.transform.rotation);
        instantiatedAudio.transform.parent = this.transform;
        textField.SetActive(displayText);
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(monologueDuration);
        Destroy(this.gameObject);
    }
}
