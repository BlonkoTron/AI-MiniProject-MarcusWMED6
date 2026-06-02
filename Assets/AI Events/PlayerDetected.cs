using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/PlayerDetected")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "PlayerDetected", message: "[Agent] has Spotted [Player]", category: "Events", id: "63d5a1c926c5d2ee9ad0022e2d964a06")]
public sealed partial class PlayerDetected : EventChannel<GameObject, GameObject> { }

