# BirdFlight_VR_Game
Gamification project

**Tutorial videos** :
  1. Terrain and Scene build (realistic): "https://youtu.be/2UFvOHlI7BY?si=Wd5zc_qXsZXYpSrS"
  2. Terrain and Scene build( Animation tool):"https://youtu.be/bAHhDh19B7o?si=mZ6jC-JcorErbxND"
  3. Bg audio: https://youtu.be/DqewBvd-bAA?si=ZVUVGe06-FOIn2Is
  4. VR-Headset setup: https://youtu.be/NV9WzAfRFz4?si=_lAlOPsZK9_LTgY_


## Project Overview- Useful guide

### Technical Specifications 
| Detail | Value |
| :--- | :--- |
| **Engine** | Unity 2022.3.62f1 LTS |
| **Render Pipeline** | Universal Render Pipeline (URP) 14.0.11  |
| **Input System** | Unity New Input System 1.7.0  |
| **Project Name** | realistic forest |
| **Bird Character** | Sparrow_Low (imported asset) |
| **Theme** | Red Kite nest-building simulation |
| **Platform Target** |Desktop (PC) + Meta Quest VR  |

### Project Architecture & Scripts [cite: 217]
* [cite_start]**`BirdController.cs`**: Handles bird flight using the New Input System (keyboard and VR inputs)[cite: 217]. [cite_start]Manages flight mechanics [cite: 66, 67, 68][cite_start], twig collection triggers [cite: 217][cite_start], audio playback (wing flaps, twig pickups, success chirps) [cite: 218][cite_start], and caps the max twig count[cite: 73].
* [cite_start]**`GameManager.cs`**: A Singleton pattern manager handling core game states, a survival timer, twig UI counters, educational info panel transitions, and win conditions[cite: 218, 219].
* [cite_start]**`StartScreenManager.cs`**: Listens for user confirmation inputs to seamlessly cycle from the start menu to the main gameplay loop[cite: 219, 220].
* [cite_start]**`BirdCamera.cs`**: Attached to the first-person camera inside the bird to add realistic head bobbing, wind shaking, and banking/tilt adjustments[cite: 221].
* [cite_start]**`NestMarker.cs`**: Controls a glowing, golden destination beacon that slowly scales and rotates[cite: 222].
* [cite_start]**`RiverFlow.cs`**: Scrolls water textures and shifts depth colors to simulate active river environments[cite: 223].

---

## Core Gameplay Mechanics

### [cite_start]1. Flight Dynamics (`BirdController.cs`) [cite: 64, 65]
* [cite_start]**Movement Controls**: Forward/backward (W/S) [cite: 66][cite_start], banking/turns (A/D) [cite: 67][cite_start], and vertical elevation changes (Q/E)[cite: 68].
* [cite_start]**Realism Tweaks**: Includes progressive forward pitch during diving/climbing [cite: 70][cite_start], natural banking angles when turning [cite: 69][cite_start], and a rigid body setup (`useGravity = false`, `drag = 2`) for smooth momentum deceleration[cite: 73].
* [cite_start]**Safeguards**: Hard boundary clamp at $Y = 0.5$ prevents the player from slipping beneath the terrain geometry[cite: 72].

### 2. Collection & Progression System
* [cite_start]**Twig Primitives**: 10 distinct cylinder models scattered across the forest floor with custom collider triggers tagged as `Twig`[cite: 90, 91, 93, 94, 95].
* [cite_start]**The Nest Zone**: An elevated marker target hidden near trees ($Y \ge 5$) tagged as `NestZone`[cite: 96, 97, 99]. [cite_start]A golden emissive `NestBeacon` pulses visually at this location until the nest criteria ($5/5$ twigs) are met[cite: 73, 103, 105, 106].
* [cite_start]**Educational Intermissions**: Gamification intervals that showcase real-world Red Kite behavior facts (e.g., nest construction materials, structural weight, breeding statistics)[cite: 119, 120, 121, 122, 123].

---

## User Interface Configuration [cite: 124]

[cite_start]The project utilizes a unified Canvas system set to **Screen Space - Overlay** for desktop deployment[cite: 126].

* [cite_start]**`TwigCountText`**: Top-left anchor, tracks collected items against goals[cite: 128].
* [cite_start]**`TimerText`**: Top-right anchor, tracks active game time[cite: 128].
* [cite_start]**`InstructionText`**: Bottom-center anchor, guides current objectives[cite: 128].
* [cite_start]**Screen Panels (`StartPanel`, `InfoPanel`, `WinPanel`)**: Full-screen layouts configured with alpha-blended black backgrounds ($200$–$220$ opacity) for high typographic legibility[cite: 128].

---

## Step-by-Step VR Conversion Pipeline [cite: 133]

Follow these procedures to port the desktop build over to standalone **Meta Quest 2 / Quest 3** hardware[cite: 136]:

### Step 1: Headset Preparation [cite: 141]
1. [cite_start]Download and open the **Meta Quest Mobile App** on your smartphone[cite: 142].
2. [cite_start]Power on the headset and ensure it pairs via Bluetooth[cite: 144].
3. [cite_start]Go to **Menu > Devices > Headset Settings > Developer Mode** and switch the toggle **ON**[cite: 144, 145].
4. [cite_start]Put on the headset and accept the developer confirmation prompt[cite: 146].

### Step 2: XR Framework Configuration [cite: 147]
1. [cite_start]Open the Unity Editor and navigate to `Window > Package Manager`[cite: 148].
2. [cite_start]Locate and install **XR Plugin Management**[cite: 149].
3. [cite_start]Locate and install **XR Interaction Toolkit** (import the **Starter Assets** sample bundle)[cite: 150].
4. [cite_start]Open `Edit > Project Settings > XR Plugin Management`[cite: 151].
5. [cite_start]Under both the **Android** and **PC** tabs, select **Oculus** as the target plugin provider[cite: 152].

### Step 3: Platform Setup [cite: 153]
1. [cite_start]Go to `File > Build Settings`[cite: 155].
2. [cite_start]Choose **Android** from the platform selector menu and click **Switch Platform**[cite: 156, 157].
3. [cite_start]Set **Texture Compression** to **ASTC**[cite: 157].

### Step 4: Camera to XR Rig Migration [cite: 159]
1. [cite_start]Locate the `Main Camera` child nested underneath the `Sparrow_Low` player hierarchy and **delete it**[cite: 162].
2. [cite_start]Right-click the Hierarchy window and select **XR > XR Origin (VR)** to instantiate a tracked spatial node structure[cite: 163].
3. [cite_start]Drag the new **XR Origin** object directly onto `Sparrow_Low` to parent it to the bird model[cite: 164].
4. [cite_start]Update the local position transform values to match the old tracking data: `X: 0`, `Y: 0.1`, `Z: 0.2`[cite: 164].

### Step 5: Mapping Controller Joystick Inputs [cite: 167]
[cite_start]Open `BirdController.cs` and incorporate XR input parsing blocks alongside your standard keyboard controls to map axes to both inputs simultaneously[cite: 169, 187]:

```csharp
using UnityEngine.XR;

// Inside Update() after keyboard validation:
List<XRNodeState> nodeStates = new List<XRNodeState>();
InputTracking.GetNodeStates(nodeStates);

foreach (var state in nodeStates) 
{
    // Left Joystick controls Forward and Turn vectors
    if (state.nodeType == XRNode.LeftHand) 
    {
        state.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftStick);
        forward += leftStick.y;
        turn += leftStick.x;
    }
    // Right Joystick controls Elevation (Up/Down)
    if (state.nodeType == XRNode.RightHand) 
    {
        state.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightStick);
        up += rightStick.y;
    }
}
```

### Step 6: Spatial UI Modifications
1. [cite_start]Click the core UI `Canvas` root node within the hierarchy pane[cite: 198].
2. [cite_start]Change the **Render Mode** dropdown selection from *Screen Space Overlay* to **World Space**[cite: 199].
3. [cite_start]Re-scale the Transform properties to uniform sub-units: `X: 0.001`, `Y: 0.001`, `Z: 0.001`[cite: 200].
4. [cite_start]Parent this canvas frame directly to the bird framework, calculating a functional forward offset position (`Y: 0.3`, `Z: 2`) to make it act as a floating HUD[cite: 201, 202].

### Step 7: Optimization Guidelines for VR
[cite_start]To preserve a consistent frame rate ($\ge 72\text{ FPS}$) and avoid simulation sickness, configure the following deployment settings[cite: 190]:

* [cite_start]**Quality Settings**: Create an independent **VR Profile** applied to Android targets[cite: 191]. [cite_start]Cap the global `Render Scale` at `0.9`, reduce `Shadow Distance` limits down to `30`, use a maximum of `1` cascade count layer, and disable MSAA passes[cite: 192].
* [cite_start]**URP Profiles**: Disable Screen Space Ambient Occlusion (SSAO) and HDR properties, and set Render Scale to 0.9[cite: 193].
* [cite_start]**Terrain Renderers**: Check the **GPU Instancing** options across tree materials, drop the total density of the detail layouts down, and disable `Draw Instanced` tasks if hardware bottlenecks arise[cite: 193].

### Step 8: Build and Deploy
1. [cite_start]Connect the Meta Quest headset directly to your workstation computer using a verified USB-C link cable[cite: 204].
2. [cite_start]Put the headset on and accept the **Allow USB Debugging** prompt inside the interface[cite: 205].
3. [cite_start]Navigate to `File > Build Settings` in Unity[cite: 206].
4. [cite_start]Click **Add Open Scenes** to confirm your forest level is actively queued[cite: 207].
5. [cite_start]Click **Build and Run**[cite: 208].

> **Note**: Initial builds can take between 10 to 20 minutes to compile asset libraries for Android deployment[cite: 210]. [cite_start]If your hardware isn't detected by Unity, ensure you have the official *Oculus ADB Drivers* installed on your host system[cite: 211].



