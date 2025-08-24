# Police Simulator V for FiveM

> ⚠️ **WARNING**  
> This is an early version/prototype release and is **NOT recommended for use on public servers**. Use at your own risk in development or private testing environments only. Features may be unstable and subject to significant changes.

Police Simulator V (PSV) delivers an immersive, realistic PvE policing experience for [FiveM](https://fivem.net/), the popular Grand Theft Auto V multiplayer modification platform. Transform your roleplay server into a dynamic law enforcement environment with comprehensive police simulation features.

## 🚨 Key Features

### Core Policing Experience
- **🎯 Dynamic Callout System** - Respond to incidents across **379+ unique locations** with varied scenarios
- **💾 Zero External Dependencies** - No external database required - seamless plug-and-play installation
- **🎛️ Fully Customizable** - Configure callout frequency, postal ranges, NPC profiles, search items, and UI elements

### Advanced Law Enforcement Tools
- **📋 Comprehensive Background Checks** - Access detailed NPC profiles including firearms licenses, active warrants, and criminal history
- **📊 Integrated Reporting System** - Track and document all law enforcement activities with built-in arrest reports
- **💻 Mobile Data Computer (MDC)** - Modern interface for callout management, background checks, and report submission

### Realistic Traffic & Pursuit Systems
- **🚗 Intelligent Traffic Stops** - NPCs realistically observe traffic laws and pull over safely
- **🏃 Dynamic Vehicle Pursuits** - Suspects exhibit realistic evasion behavior with collision avoidance
- **🎲 Random Traffic Events** - Unpredictable scenarios including vehicle breakdowns and equipment failures
- **🚔 Felony Stop Procedures** - Tactical suspect extraction system for high-risk situations

### Professional Documentation & Integration
- **📄 Digital Driver Documents** - Request and verify licenses and vehicle registration
- **📍 Postal Code Integration** - Advanced dispatching with location-based callout management  
- **👮 EUP Menu Integration** - Customizable uniform and equipment management
- **🚛 NPC Tow Services** - Automated impound system with AI tow truck dispatch

## 📸 Screenshots

![Police Simulator V Interface](https://forum-cfx-re.akamaized.net/original/5X/f/1/f/2/f1f2532b0fc481ea99442f45a9f2ded4e691bce7.jpeg)
![Mobile Data Computer](https://i.imgur.com/sy4QRtp.png)
![Callout System](https://i.imgur.com/3MyAwCE.png)
![Traffic Stop Interface](https://i.imgur.com/ByXa3rX.png)

## 🛠️ Installation & Setup

- For information about the .json config files, check the [Wiki](https://github.com/laroche-a/PoliceSimulatorV/wiki) page.

### Prerequisites
- [FiveM Server](https://runtime.fivem.net/artifacts/fivem/build_server_windows/master/) (latest version recommended)

### Building the Resource

1. **Build the Resource**
   ```bash
   run build.cmd
   ```

2. **Deploy to Server**
   - Compiled files will be generated in the `dist/` folder
   - Copy the entire `dist/` folder contents to your FiveM server resources directory
   - Add the following line to your `server.cfg`:
   ```
   ensure [resource-name]
   ```
