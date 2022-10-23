using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] public AudioSource suaraufo;

    Player player;

    void Update()
    {
        if (this.transform.position.z <= player.CurrentTravel - 20)
            return;

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (this.transform.position.z <= player.CurrentTravel && player.gameObject.activeInHierarchy)
        {
            player.transform.SetParent(this.transform);
            suaraufo.Play();
        }
            

    }

    public void SetUpTarget(Player target)
    {
        this.player = target;
    }
}
