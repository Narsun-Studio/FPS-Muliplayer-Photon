using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public Transform shotPoint;
    [Range(0, 500)] public float force = 100;

    public GameObject bulletImpact;

    public PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    public void Fire()
   {
        RaycastHit hit;
        if(Physics.Raycast(shotPoint.position,shotPoint.forward,out hit, force))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.Damage(30,PV.ViewID);
            PV.RPC("RPC_DrawBulletImpact", RpcTarget.All, hit.point, hit.normal);
        }
   }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(shotPoint.position, shotPoint.forward * 300);
    }

    [PunRPC]
    void RPC_DrawBulletImpact(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {

            GameObject bullectImpactObj = Instantiate(bulletImpact, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpact.transform.rotation);
            Destroy(bullectImpactObj, 5f);   
            bullectImpactObj.transform.SetParent(colliders[0].transform); 
        }
    }
}
