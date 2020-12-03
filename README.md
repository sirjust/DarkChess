# **About**

A Unity 3D souls-like game based on chess

## **Authors**

- **Justin Moore** - [Github](https://github.com/sirjust)
  
- **Arthur** - [Github](https://github.com/Soleis)

- **Damian Dorosz** - [Github](https://github.com/exostin)

- **Hans** - [Github](https://github.com/unbekanntunity)
  
## **Technologies Used**

- Unity 2019.4.15f1
- ProBuilder 4.4.0

### **Notes**

There is a dev folder and various folders beneath it. While in development, work within these folders, where you'll have complete freedom. When a scene or prefab is ready to be brought into the main project, submit a **Pull Request** with the asset in the correct folder.

Make many small **Pull Requests** rather than packing too much code into one branch.

We want every prefab or asset to be able to be used in any scene. If there are steps needed to get it working (initializing variables, etc), document them here.

We plan to demo all features weekly.

## Documentation
### Title Screen

- GetVersion.cs - A script that pulls the current build version (We set that in Project Settings under Player tab)
				and puts it into a transparent text in the bottom left corner. It can for example indicate which version is a screenshot from.