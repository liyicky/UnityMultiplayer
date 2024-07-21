using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAiming : NetworkBehaviour
{
    [SerializeField] private Transform turretTransform;
    [SerializeField] private InputReader inputReader;

    private void LateUpdate()
    {
        if (!IsOwner) { return; }

        Vector2 aimPos = Camera.main.ScreenToWorldPoint(inputReader.AimPosition);
        turretTransform.up = aimPos - (Vector2) turretTransform.position;
    }
}
