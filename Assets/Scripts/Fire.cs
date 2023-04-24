using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Fire : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    private ParticleSystem muzzleFlash;

    private PhotonView pv;
    private bool isMouseClick => Input.GetMouseButtonDown(0);
    // Start is called before the first frame update
    void Start()
    {
        pv = this.GetComponent<PhotonView>();
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine && isMouseClick)
        {
            FireBullet(pv.Owner.ActorNumber);
            pv.RPC("FireBullet", RpcTarget.Others, pv.Owner.ActorNumber);
        }
    }
    [PunRPC]
    void FireBullet(int actorNo)
    {
        if (!muzzleFlash.isPlaying) muzzleFlash.Play(true);

        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}
