using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkMigrationManagerScript : NetworkMigrationManager {

    public override bool FindNewHost(out PeerInfoMessage newHostInfo, out bool youAreNewHost)
    {
        return base.FindNewHost(out newHostInfo, out youAreNewHost);
    }

}
