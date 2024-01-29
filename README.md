# Perfect Maze Generator

## Description
The Perfect Maze Generator is a Unity-based minimum viable product (MVP), designed to create perfect mazes using the randomized Kruskal's algorithm. A perfect maze is defined as a maze which has one and only one path from any point in the maze to any other point, having no loops or closed circuits, and no inaccessible areas. This MVP was done as part of the assignment for the technical assessment for the Unity Developer Intern position at DTT.

## Features
- Generates perfect mazes of customizable sizes.
- Utilizes randomized Kruskal's algorithm for maze generation.
- Interactive camera controls for zooming and panning across the maze.
- User-friendly UI for adjusting maze dimensions and generation speed.
- Option to reset and generate new mazes on demand.
- A game mode in which the player can navigate the generate mazes as a cute little blue cube.

## Getting Started

### Prerequisites
- Unity Editor (Version 2020.1 LTS or later recommended).

### Instructions
1. Clone the repository:
   ```bash
   git clone git@github.com:fcortevargas/Perfect-Maze.git
   ```
2. Open the project in Unity.
3. Load the Start scene and hit the play button to run the project.
4. Have fun!

## Usage

- Use the UI sliders to adjust the width and height of the maze.
- Press the 'Generate' button to create a new maze.
- Zoom and pan the camera to explore the maze.
- Press the 'Reset' button to clear the maze and generate a new one.
- For small mazes, you can enter the game mode and explore the maze you just generated.

## Script Assets Overview
This is an overview of the key script assets included in the Perfect Maze Generator project. Each script has also been thoroughly commented for the sake of clarity. I inteded to use the four pillars of object-oriented progamming (encapsulation, abstraction, inheritance, and polymorphism) as much as possible to create elegant and efficient code: 

### `GameManager.cs`
- Manages the overall game state, including scene transitions and global game settings.
- Handles the loading of different scenes and maintains static properties representing the game's state.

### `Maze.cs`
- Responsible for the procedural generation and management of the maze.
- Implements the maze generation algorithm and controls the maze's visual representation.

### `PlayerController.cs`
- Manages the player's movement and interaction within the maze environment.
- Handles user input for navigation (zooming and panning) within the maze.

### `Wall.cs`
- Represents the individual walls of the maze.
- Contains logic for initializing the wall properties and their relation to the maze cells.

### `Cell.cs`
- Represents the individual cells of the maze grid.
- Used in maze generation and to keep track of the player's position within the maze.

### `DisjointSet.cs`
- Implements the disjoint set (also known as Union-Find) data structure, a crucial component in the maze generation algorithm.
- Used to efficiently manage sets of elements and determine whether two elements are in the same set. This is essential for ensuring the maze's perfect generation by preventing cycles.

### UI Scripts
- `ButtonController.cs`, `SliderController.cs`, etc.: Provide user interface functionalities like buttons and sliders to interact with the maze generator.

### Cinematics
- `CameraController.cs`: Manages camera behaviors, such as zooming and panning, for cinematic effects.

## Contact

For any questions related to this repository or the Unity project, please don't hesitate to contact me at f.cortevargas@gmail.com.