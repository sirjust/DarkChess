# **Dark Chess**

A Unity 3D souls-like game based on chess

## Table of Contents

- [**Dark Chess**](#dark-chess)
	- [Table of Contents](#table-of-contents)
	- [**Authors**](#authors)
	- [**Technologies Used**](#technologies-used)
	- [**Developer Notes**](#developer-notes)
		- [**Git Notes**](#git-notes)
	- [**Mechanics**](#mechanics)
		- [**Title Screen**](#title-screen)
		- [**Highlight Mechanic**](#highlight-mechanic)
			- [**How To Use**](#how-to-use)
			- [**How It Works**](#how-it-works)

## **Authors**

- **Justin Moore** - [Github](https://github.com/sirjust)
  
- **Arthur** - [Github](https://github.com/arthur-schevey)

- **Damian Dorosz** - [Github](https://github.com/exostin)

- **Hans** - [Github](https://github.com/unbekanntunity)
  
## **Technologies Used**

- Unity 2019.4.15f1
- ProBuilder 4.4.0

## **Developer Notes**

There is a dev folder and various folders beneath it. While in development, work within these folders, where you'll have complete freedom. When a scene or prefab is ready to be brought into the main project, submit a **Pull Request** with the asset in the correct folder.

Make many small **Pull Requests** rather than packing too much code into one branch.

We want every prefab or asset to be able to be used in any scene. If there are steps needed to get it working (initializing variables, etc), document them here under the [Mechanics](#mechanics) section.

We plan to demo all features weekly.

To keep our code homogenous, we'll use camel case for our variables (fields etc), like so: `private bool canAttack;`

Variables for lists and arrays should be pluralized, even if it makes the variable read awkwardly, like so: `List<string> attackTypes` or `IEnumerable<PlayerStatistics> playerStats`

Of particular note, we learned in Week 1 that due to how Unity works with `.meta` files, we should never commit an empty folder to source control. If we do, every other developer will have conflicts.

### **Git Notes**

We are using Github Projects to track issues, which we were previously calling tickets. The link is found [here](https://github.com/sirjust/DarkChess/issues). Each ticket is automatically given a number. When you start working on an issue, create a feature branch in git with the following syntax. You'll also need to assign yourself to the issue in the `Issues` tab.

``` git
git checkout -b 10-short-description-of-issue
git push -u origin 10-short-description-of-issue
```

At this point, the branch is both local and is tracked in your fork. You can then freely make changes to the branch and your `main` branch will be unaffected.

To keep your fork's `main` branch up to date with the central repository, initiate a pull request on Github that brings the current code on `sirjust:main` to `your-fork:main`. When that is complete, pull the changes into your local `main` branch and then merge the `main` branch into your feature branch. There are more streamlined ways to do this, but this is the simplest way.

We need to keep track of our issues, and close them when they are merged in. When we figure out how to close them automatically, we will implement that, but currently they sit there until we manually close them.

## **Mechanics**

### **Title Screen**

- GetVersion.cs - A script that pulls the current build version (We set that in Project Settings under Player tab) and puts it into a transparent text in the bottom left corner. It can for example indicate which version is a screenshot from.

### **Highlight Mechanic**

#### **How To Use**

1. Attach this script to the game object you want to highlight.

2. Create a highlight prefab and assign it to the game object like so. Keep clone empty.

![prefab image](https://i.ibb.co/FB33YT4/Screenshot-2020-12-05-141843.png)

#### **How It Works**

This script clones a prefab called `highlight` on top of the GameObject this script is attached to using the `OnMouseEnter()` event.

In this case, I am cloning a quad that is emissive (looks like a highlight).

![highlight image](https://i.ibb.co/6vX1CkF/Screenshot-2020-12-05-144732.png)

First I define my two variables.

```cs
public GameObject highlight;
public GameObject clone;
```

Here I am taking the position of the game object and assigning it to `objectPosition` with a position slightly above it. `0.02f`

I then `Instantiate` my highlight into the scene with the new `objectPosition` and set it equal to `clone` so I can destroy it later.

```cs
OnMouseEnter() {
    Vector3 objectPosition = new Vector3(transform.position.x, 0.02f, transform.position.z);
    clone = (GameObject)Instantiate(highlight, objectPosition, Quaternion.Euler(Vector3.right * 90));
}
```

Destroy clone when mouse isn't hovering over object anymore.

```cs
OnMouseExit() {
    Destroy(clone);
}
```

If I tried to destroy the original "highlight" it would attempt to delete the prefab from our assets which would not work.

### **Audio Manager**

#### **What is it?**

![audio manager](https://i.ibb.co/4KKx6BL/image.png)

A library of audio clips that we can call on demand throughout our game. By developing a strong audio manager, we should be able to control different sounds in various ways.

#### **How To Use**

The `Audio Manager` prefab should be placed in every scene. It comes with  `AudioManager.cs` preloaded. 

To add your audio clip:
1. Increase the size by +1
2. Name the added audio element
3. Drag your audio clip into `Clip`

To play your audio clip on demand, use:

`FindObjectOfType<AudioManager>().Play("INSERT_NAME");`

We can alter the properties of each audio clip, such as `Volume`, `Pitch`, or `Loop`. If necessary, we can add more parameters to each audio source to have more control. This can be done by altering the `Sound.cs` script which displays the parameters in the inspector for each sound.

![sound.cs](https://i.ibb.co/99xMqf0/image.png)

