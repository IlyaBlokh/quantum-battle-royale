# Quantum Battle Royale

A top-down multiplayer battle royale built with Unity 6 and Photon Quantum 3 — a deterministic networking framework that ensures consistent simulation across all clients.

## Tech Stack

| | |
|---|---|
| **Engine** | Unity 6000.3.10f1 |
| **Networking** | Photon Quantum 3.0.9 + Photon Realtime 5.1.0 |
| **Rendering** | Universal Render Pipeline (URP) |
| **Camera** | Cinemachine |
| **UI** | TextMeshPro, DOTween |

## Features

- **Character selection** — choose a character with unique stats (health and fire rate multipliers)
- **6 weapons** — AK, Pistol, Revolver, Shotgun, SMG, Sniper Rifle; auto & semi-auto firing modes
- **Pickup system** — weapon, health, and ammo pickups scattered across the map
- **Shrinking safe zone** — configurable damage-per-second outside the circle with countdown UI
- **HUD** — live ammo count, weapon icon, game state display
- **Deterministic simulation** — server-authoritative via Quantum; no desync between clients

## Project Structure

```
Assets/
├── Photon/              # Quantum engine, Photon Realtime, menu framework
└── QuantumUser/
    ├── Simulation/      # Deterministic game logic (ECS systems, components, assets)
    │   ├── Systems/     # Bullet, Weapon, Damageable, Movement, Spawn, GameManager…
    │   ├── AssetData/   # Weapon configs, character configs, damageable definitions
    │   └── Components/  # .qtn component definitions (auto-generated)
    └── View/            # Client-side rendering, UI, camera
        └── UI/          # HUD elements (ammo, weapon icon, countdown)
```

## Getting Started

1. **Requirements** — Unity 6000.3.10f1, Photon Quantum SDK 3.x
2. **Photon App ID** — create a Quantum app at the Photon dashboard and set the App ID in `PhotonServerSettings`
3. Open `Assets/QuantumUser/Scenes/GameLevel` as the main scene
4. Hit **Play** — the Quantum menu handles matchmaking and character selection automatically

## Sandbox Mode

Enable `GameManagerConfig.EnableSandbox` to bypass the player-count game-over condition — useful for solo testing.
