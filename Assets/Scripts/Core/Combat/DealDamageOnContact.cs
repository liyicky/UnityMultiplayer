using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DealDamageOnContact : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private int damagePerAttack = 5;

    private ulong ownerClientId;
    private ulong otherOwnerClientId;

    public void SetOwner(ulong ownerClientId)
    {
        this.ownerClientId = ownerClientId;
        Debug.Log("Owner Client Id Set: " + ownerClientId);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.attachedRigidbody == null) { return; }


        if (other.attachedRigidbody.TryGetComponent<NetworkObject>(out NetworkObject no))
        {
            if (no.OwnerClientId == ownerClientId) { return; }
            else
            {
                otherOwnerClientId = no.OwnerClientId;
            }
        }

        if (other.attachedRigidbody.TryGetComponent<Health>(out Health otherHealth))
        {   
            Debug.Log(ownerClientId + "dealth " + damagePerAttack + "to " + otherOwnerClientId);
            otherHealth.TakeDamage(damagePerAttack);
        }
    }
}
