# Modular Level Editor

A 3D Modular Level Editor built in Unity 6 as a Final Year Mini Project.
Place, move, rotate, and scale modular 3D assets to build levels visually.

## Features
- 3D Grid Snapping System
- Ghost Preview before placement
- Click to place objects
- Undo / Redo (Command Pattern)
- Right click to select object
- Shift + Right click to delete object
- Object highlight on selection
- F key to focus camera on selected object
- Real-time Transform Inspector (Position, Rotation, Scale)
- Save / Load levels (JSON)
- Saved levels list panel
- Free camera control (WASD + Right Click)
- UI Panel for asset selection

## Tech Stack
- Unity 6000.4.9f1 (Built-In Render Pipeline)
- C#

## Controls

### Placement
| Key | Action |
|-----|--------|
| Left Click | Place object |
| Right Click | Select object |
| Shift + Right Click | Delete object |

### Undo / Redo
| Key | Action |
|-----|--------|
| Ctrl + Z | Undo |
| Ctrl + Y | Redo |

### Camera
| Key | Action |
|-----|--------|
| Right Click + WASD | Move camera |
| Right Click + Q | Move down |
| Right Click + E | Move up |
| Right Click + Shift | Sprint |
| Scroll Wheel | Zoom |
| F | Focus on selected object |

### Save / Load
| Key | Action |
|-----|--------|
| Ctrl + S | Save level |
| Ctrl + L | Load level |

### Transform Inspector
- Right click any placed object to select it
- Edit Position, Rotation, Scale values in right panel
- Changes apply in real time

## Team
- Programmer: Maharshi Trivedi
- 3D Artist: (Designer name yahan likho)

## Project Structure
```
Assets/
  _Project/
    Scripts/
    Prefabs/
    Materials/
    Resources/
    Scenes/
  _Designer/
```
