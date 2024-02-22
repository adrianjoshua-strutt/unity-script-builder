# Unity Script Builder
#### A simple visual scripting tool for Unity3D

# Background

A small but extendable and modular low code scripting engine for Unity. 
You can easily add events and so called executables to any gameobject. Executables are code snipptes with properties exposed in the editor.
The executables are are found in the folder `/Assets/ScriptBuilder/Executable/`
Events can be added in `/Assets/ScriptBuilder/Events/`

Note: This is only a proof and concept for educational purposes. Most of the Unity Events are not covered, and would need to be added manually.

# Key Takeaways
### Use the Unity `PropertyDrawer` Class
Using this class allows for custom editor extensions. 
### Heavy use of `ScriptableObjects` 
This projects involves heavy use of ScriptableObjects and many edge cases

# Pictures

### Deleting a cube after 3 seconds and printing "test" to the console

![Deleting a cube after 3 seconds and printing "test" to the console](./Docs/preview.gif?raw=true "Deleting a cube after 3 seconds and printing test to the console")
