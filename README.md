# **Pawns Have Souls**

We're creating an exciting chess crawler cRPG with souls-like atmosphere and card game combat mechanics!

__Genres:__

- _Dungeon crawler_ - core gameplay idea

- _Chess_ - world setting

- _Card game_ - combat mechanics

- _cRPG_ - combat statistics, lore

- _Souls-like_ - atmosphere

__Inspired by games such as:__

- _Book of Demons_

- _Darkest Dungeon_

- _Dark Souls_

## Table of Contents

<<<<<<< HEAD
- [**Dark Chess**](#dark-chess)
	- [Table of Contents](#table-of-contents)
	- [**Authors**](#authors)
	- [**Technologies Used**](#technologies-used)
	- [**Developer Notes**](#developer-notes)
		- [**Git Notes**](#git-notes)
	- [**Mechanics**](#mechanics)
		- [**Title Screen**](#title-screen)
		- [**Highlight Mechanic**](#highlight-mechanic)
			- [**How To Use**](#how-to-use(Hightlight-Mechanic))
			- [**How It Works**](#how-it-works(Hightlight-Mechanic))
	- [**Battle Menu**](#Battle-Menu)
		- [**CharInfo**](#charInfo)
		- [**SkillInfo**](#skillInfo)
		- [**Health**](#health)
		- [**CardHolding**](#cardHolding)
		- [**ObjectDetection**](#objectDetection)
		- [**Important Notes**](#important-notes)
	- [**Movement System**](#Movement-System)
		- [**How to use**](#How-to-use(Movement-System))
		- [**How it works**](#How-it-works(Movement-System))
=======
- [Table of Contents](#table-of-contents)
- [**Authors**](#authors)
- [**Technologies Used**](#technologies-used)
- [**Developer Notes**](#developer-notes)
  - [**Git Notes**](#git-notes)
- [**Mechanics**](#mechanics)
  - [**Title Screen**](#title-screen)
  - [**Highlight Mechanic**](#highlight-mechanic)
  - [**Battle Menu**](#Battle-Menu)
    - [**CharInfo**](#charInfo)
    - [**SkillInfo**](#skillInfo)
    - [**Health**](#health)
    - [**CardHolding**](#cardHolding)
    - [**ObjectDetection**](#objectDetection)
    - [**Important Notes**](#important-notes)
  - [**Turn System**](#turn-system)
  - [**Movement System**](#Movement-System)
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef

## **Authors**

- **Justin Moore** - [Github](https://github.com/sirjust)
  
- **Arthur** - [Github](https://github.com/arthur-schevey)

- **Damian Dorosz** - [Github](https://github.com/exostin)

- **Hans** - [Github](https://github.com/unbekanntunity)
  
## **Technologies Used**

- Unity 2019.4.15f1
- ProBuilder 4.4.0
- Blender

## **Developer Notes**

There is a dev folder and various folders beneath it. While in development, work within these folders, where you'll have complete freedom. When a scene or prefab is ready to be brought into the main project, submit a **Pull Request** with the asset in the correct folder.

Make many small **Pull Requests** rather than packing too much code into one branch.

We want every prefab or asset to be able to be used in any scene. If there are steps needed to get it working (initializing variables, etc), document them here under the [Mechanics](#mechanics) section.

We plan to demo all features weekly.

To keep our code homogenous, we'll use camel case for our variables (fields etc), like so: `private bool canAttack;`

Variables for lists and lists should be pluralized, even if it makes the variable read awkwardly, like so: `List<string> attackTypes` or `IEnumerable<PlayerStatistics> playerStats`

Of particular note, we learned in Week 1 that due to how Unity works with `.meta` files, we should never commit an empty folder to source control. If we do, every other developer will have conflicts.

### **Git Notes**

We are using Github Projects to track issues, which we were previously calling tickets. The link is found [here](https://github.com/sirjust/DarkChess/issues). Each ticket is automatically given a number. When you start working on an issue, create a feature branch in git with the following syntax. You'll also need to assign yourself to the issue in the `Issues` tab.

``` git
git checkout -b 10-short-description-of-issue
git push -u origin 10-short-description-of-issue
```

At this point, the branch is both local and is tracked in your fork. You can then freely make changes to the branch and your `main` branch will be unaffected.

To keep your fork's `main` branch up to date with the central repository, initiate a pull request on Github that brings the current code on `sirjust:main` to `your-fork:main`. When that is complete, pull the changes into your local `main` branch and then merge the `main` branch into your feature branch. There are more streamlined ways to do this, but this is the simplest way.

Once you're done working on an issue submit a pull request, and link it to that issue so it can be automatically closed after merging.

![linking pull request to an issue](https://i.ibb.co/JpyX08X/Link-Pull-Request-To-Issue-Example.png)

## **Mechanics**

### **Title Screen**

- GetVersion.cs - A script that pulls the current build version (We set that in Project Settings under Player tab) and puts it into a transparent text in the bottom left corner. It can for example indicate which version is a screenshot from.

### **Highlight Mechanic**

<<<<<<< HEAD
#### **How to use(Hightlight Mechanic)**

1. Attach this script to the game object you want to highlight.

2. Create a highlight prefab and assign it to the game object like so. Keep clone empty.

![prefab image](https://i.ibb.co/fY3Rbrt/Edited-Grid-Generator.png)

=======
#### **How to use(Highlight Mechanic)**

1. Attach this script to the `GameObject` you want to highlight.

2. Create a highlight prefab and assign it to the `GameObject` like so. Keep clone empty.

![prefab image](https://i.ibb.co/fY3Rbrt/Edited-Grid-Generator.png)

>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
- `Main Cam`: The camera, whereby the player sees the scene
- `Player`: The y-component if the position of this object will be used when generating the grid and instantiate the  `highlight` object
- `Take Object Transform`: An option to take the position of the game object, which has the script, as the start position
- `Destroy`: An option to destroy the `highlight` objects
- `Gridstart` The start position of the grid(lower right corner)
- `GridSize`: The size of the grid
- `Layer`: Every object with this layer will be ignored
- `SelectionKey`: The button that must be pressed so that the selected tiles are not destroyed
- `Clear SelectionKey`: The button that muss be pressed to delete all selected tiles
<<<<<<< HEAD

Note: The y-value of the `Gridstart` variable will be ignored, because we using the y-value of the `Player`s position, if the `take object transform` option is false

Note: In sone function, you has to use a argument of the type `TypesofValue`. This is a enum set, which contains two values:
=======

Note: The y-value of the `Gridstart` variable will be ignored, because we using the y-value of the `Player`s position, if the `take object transform` option is false

Note: In sone function, you has to use a argument of the type `TypesofValue`. This is a enum set, which contains two values:

>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
- `relative`: the instantiated `highlight` objects adapt to the rotation of the player
- `absolute`: the instantiated `highlight` object wont adapt to the rotation of the player

#### **How it works(Hightlight Mechanic)**

Once the scene is started, a grid is created from EditedInvisGridTile objects. Now the script checks whether the player presses a certain mouse button. If this is the case, a raycast is sent from the camera. As soon as this raycast hits an object, it checks whether it is an "EditedInvisGridTile" object. If so, a clone of the 'Hightlight' object is instantiated to the position of the EditedInvisGridTile object.

In this case, I am cloning a quad that is emissive (looks like a highlight).

![highlight image](https://i.ibb.co/6vX1CkF/Screenshot-2020-12-05-144732.png)

### **Battle Menu**

#### **Components / Sections**

The battle menu contains several components, which can be used individually until a specific point. Each component adds a functionality to the Battle Menu

1. [**CharInfo**](#charInfo)
2. [**SkillInfo**](#skillInfo)
3. [**Health**](#health)
4. [**CardHolding**](#cardHolding)
5. [**ObjectDetection**](#objectDetection)
6. [**Important Notes**](#important-notes)

#### **CharInfo**

##### **Functionality**

Display the [Character](#Character) information(name, picture, health, mana, strength, critRate, dodgeRate, armor) of the last selected character in the scene.

##### **How to use**

1. Attach `CharInfo.cs` to the `GameObject` you want to use as a display for the character informations
2. then assign the different components to the variables under the `Required` header

![charInfo image](https://i.ibb.co/gT9Qh4D/CharInfo.png)

#### **skillInfo**

##### **Functionality**

Display the name and description of the last played [card](#Card) in the scene.

##### **How to use**

1. Attach `SkillInfo.cs` to the game object you want to use as a display for the [card](#Card) informations
2. Then assign the different components to the variables under the `Required` header

![skillInfo image](https://i.ibb.co/BfZhB15/Skill-Info.png)

#### **Health**

##### **Functionality**

Display the players healthbar and manabar.

##### **How to use**

1. Attach `GetBarInfo.cs` to the game object you want to use as a display the character's health and mana
2. Then assign the different components to the variables under the `Required` header  

![skillInfo image](https://i.ibb.co/s3Z64Fx/Health.png)

#### **cardHolding**

##### **Functionality**

Used to draw, display and play [cards objects](#card-object)

Note: There is a difference between [cards](#Card) and [card objects](#card-object).

- [cards](#Card): [ScriptableObject](#scriptableObject) which is linked to a [card object](#card-object)
- [card objects](#card-object): The whole game Object with additional game objects and scripts e.g UI elements or the `DragDrop.cs`  

![cardHolding image](https://i.ibb.co/qkcB0FD/Card-System.png)

##### **How to use**

1. Attach `CardSystem.cs` to the game object you want to use as container for the cards
2. Then assign the different components to the variables under the `Required` header

- `Starting Card Count`: Specify the amount of [card objects](#card-object), which will be created at the beginning
- `Max Card Count`: Specify the highest number of [card objects](#card-object) in the hand
- `Y_start`: Specify the y-coordinate of the instantiated [card objects](#card-object).
- `Gap`: The distance between every [card object](#card-object) on the hand
- `Selected Pos`: Specify the position which will add to the current Position of the [card objects](#card-object), if the [card object](#card-object) is selected

##### **Card spawning**

##### **How it works**

<<<<<<< HEAD
Once the scene starts, the `CardSystem.cs` instatiates empty objects. These empty objects (`place`) are saved in a list. Afterwards the script spawn a specific amount of [card object](#card-object) on the position of the empty objects in the list. In addition all card objects will receive a index which represent the index of the `place` which the [cards](#Card) are children of. These `place` object and the [cards](#Card) will be saved in a seperate list. 
=======
Once the scene starts, the `CardSystem.cs` instatiates empty objects. These empty objects (`place`) are saved in a list. Afterwards the script spawn a specific amount of [card object](#card-object) on the position of the empty objects in the list. In addition all card objects will receive a index which represent the index of the `place` which the [cards](#Card) are children of. These `place` object and the [cards](#Card) will be saved in a seperate list.
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef

Note: All possible [cards](#Card) which can be played/drawed are saved in the [scriptableObject](#ScriptableObject) of the player.

![CardArrays Image](https://i.ibb.co/PxSJRR3/Card-lists.png)

##### **Drag and Drop**

In order to use the unity drag and drop functionality, I have to import the different Interfaces( `IPointerDownHandler`, `IBeginDragHandler`, `IEndDragHandler`, `IDragHandler` ). Each Interface add a new method into the `DragDrop.cs` script, which will triggered in the different stages in the drag and drop process. Now I can modify the different methods and add new functionalities in it.

##### **Play a Card**

##### **How it works**

Once the [card object](#card-object) is moved the drag and drop process begin and the position of the [card object](#card-object) will be saved in a variable called `lastPos`. Since the [card object](#card-object) is always clicked and selected when moving it, I had to subtract the `SelectedPos` from the position.

```cs
public void OnBeginDrag(PointerEventData eventData)
{
        lastPos = this.transform.position - selectedPos;
}

```

Now the next stage begins and triggers the `OnDrag()` method as long as the player hold down the mouse button. This method add a the delta mouse position to the position of the dragged object. 

```cs
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }
```

When the y-coordinate of the [card object](#card-object) is higher than the variable `height UI`, then the `cast()` will triggered. If this method returns true, then a method named `PlayCard()` is triggered in the `CardSystem.cs`.  Otherwise the position of the [card object](#card-object) will be reset to the `lastPos`. The `PlayCard()` method destroys the [card object](#card-object) and moves all other [card objects](#card-object) one position to the left.

1. Destroys the played [card object](#card-object)
2. Saves the next [card object](#card-object) in a temporary variable called `old_cardObj`
3. Destroys the next [card object](#card-object)
4. Instatiated the next [card object](#card-object) to position of the new place
5. Decreased the index of the next [card object](#card-object)

```cs
var old_cardObj = places[i].GetComponentInChildren<DragDrop>().gameObject;

Destroy(old_cardObj);

var new_cardObj = Instantiate(old_card.template, places[i - 1].transform);
```

This process goes through each card until all have been moved

##### **Use a card**

##### **How it works**

In order to let the `cast()` method returns the bool value `true`, the player has to select the right tiles and there has to be at least one object which can be [detected](#objectDetection) by a tile e.g a other [character object](#character-object).

After the player [selected](#select-a-tile) some tiles and [played](#Play-a-Card) a [card](#Card). A method called `cast()` triggers. This method compares the positions of the `ranges` list with every positions of the [selected](#select-a-tile) tile list. Now if the compared tiles has the same position, then method of the skill will be triggered and the `cast()` method returns the bool value `true`. When there is no matches in the comparison, then the method will return the bool value `false`.

##### **Draw a Card**

##### **How it works**

As soon as the player presses on the `DrawButton`, it is checked whether the maximum number of [cards](#Card) is exceeded or not. If this is not the case, then the `PickRandCard()` method is called. This takes an list of [cards](#Card) and randomly picks one. This [card](#Card) is then returned. The linked card object of the returned [card](#Card) will then instantiated in first free `place` object. 

Note: "Free" means that the object hasnt any [card object](#card-object) as a children

##### **Select a Card**

##### **How it works**

<<<<<<< HEAD
The range of every [card](#Card) are saved in the [scriptableObject](#ScriptableObject) in a list. If the player only clicks on the [card object](#card-object) and does not move, it will be selected. This selected [card object](#card-object) then will trigger a method called ` GenerateTiles()`. This method read the saved relative positions in the list of the [cards](#Card), add them to the current position of the user and adapt these based on the current rotation of the user. After the calculations the method instantiates the `highlight` object to the calculated position and save them into another list called `rangeTiles`. If the player deselect a [card](#Card) the method called `DestroyTiles()` will be triggered, which clears all lists and destroy all `highlight` objects.
=======
The range of every [card](#Card) are saved in the [scriptableObject](#ScriptableObject) in a list. If the player only clicks on the [card object](#card-object) and does not move, it will be selected. This selected [card object](#card-object) then will trigger a method called `GenerateTiles()`. This method read the saved relative positions in the list of the [cards](#Card), add them to the current position of the user and adapt these based on the current rotation of the user. After the calculations the method instantiates the `highlight` object to the calculated position and save them into another list called `rangeTiles`. If the player deselect a [card](#Card) the method called `DestroyTiles()` will be triggered, which clears all lists and destroy all `highlight` objects.
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef

#### **objectDetection**

##### **How to use**

1. Attach the `GetObjectonTile.cs` to the game object which should detect object above him

![objectDetection Image](https://i.ibb.co/P65X4q2/Get-Objecton-Tile.png)

- `Layer`: Every object with this layer will be ignored

##### **How it works**

The script will send a raycast upwards every frame  and once the raycast hits a collider, the game object will be saved in a variable called `gameObjectOnTile`.

In our case we using this script for the `EditedHighlight Quad` object and the `EditedInvisGridTile` object 

### **Important notes**

### ScriptableObject

- In this project work with scriptableObjects
- Currently there are two types of scriptableObjects([Character](#Character), [Card](#Card))
- ScriptableObject are containers for different values e.g health or mana cost
- These scriptableObject also contains some other scripts

### Card

![CardScObject Image](https://i.ibb.co/3rQ7j1N/Strike-Sc-Object.png)

- `Skill Name`: The name which will displayed on the [skillInfo](#skillInfo) display(the name doesnt have to be the same as the name of the object)
- `Mana Cost`: The amount of mana which will be consumed if the card Object is played
- `Damage`: The amount of basedamage. This number will be added to the 	strength of the user
- `Skill Pic`: The picture which is displayed on the card object 
- `Template`: The linked [card object](#card-object)
- `Skill`: The skill which will triggered if the [card object](#card-object) is played
- `Max Amount Of Targets`: The highest number of targets(If the player select more targets, only the first selected will be count)
- `Ranges`: An list of the relative position of the user e.g (1 | 0 | -1) means the tile before the user on the left side  
- `Skill description`: A short description, which will be displayed on the [skillInfo](#skillInfo) display

### Character

![CardScObject Image](https://i.ibb.co/1mtRLVQ/Pawn.png)

- `Char Name`: The name, which is displayed on the [charInfo](#charInfo) display(the name doesnt have to be the same as the name of the object)

- `Skill Pic`: The picture, which is displayed on the [charInfo](#charInfo) display

- `Health Representation`: Specify the way how the haelth be showed in the game. Currently there are two options: None, Healthbar

- `Realtion`: Specify the relation to the player. Currently there are three options: friendly, enenmy, neutral 

##### game objects(card object, character object)

- Game objects in the scene has to have specific scripts to work with the Card System or Combat System

### card object

![card object Image](https://i.ibb.co/hV0XFSK/Strike-Prefab-New.png)

- `Card`: The linked [scriptableObject](#ScriptableObject)
- `height UI`: The y-coordinate, which has to be exceeded to count the [card object](#card-object) as played

### character object

![character object Image](https://i.ibb.co/P4D8pmG/charcacter-Object.png)

- `Character`: The linked [scriptableObject](#ScriptableObject)
- `Have Body`: If the Prefab has a Body like in this example then make a check mark. Otherwise the `GetStats.cs` create the prefab, which is saved in the variable `model`
- `Normal Skills`: Collection of drawable [cards](#Card)
<<<<<<< HEAD
- `Unique Skills`: Collection of unique drawable [cards](#Card), which can be only one time at the same time on the hand(has to be in the `Normal Skills` list to) 
=======
- `Unique Skills`: Collection of unique drawable [cards](#Card), which can be only one time at the same time on the hand(has to be in the `Normal Skills` list to)
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef

Note: Every [character object](#character-object) has to have a collider in order to be [detected](#objectDetection)

##### Player

<<<<<<< HEAD
##### Other notes about the scripts
- Be sure that you have one game Object in the scene which the `allSkills.cs`, `EditedScriptgenerator.cs`, `TurnSystem.cs` script is attached to
- The name of the method and the name of the enums has to be the same e.g `strike()` and `strike`

=======
- This variable has to contain a [scriptableObject](#ScriptableObject), which was created with the `character.cs`

##### Other notes about the scripts

- Be sure that you have one `GameObject` in the scene which the `allSkills.cs`, `EditedScriptgenerator.cs`, `TurnSystem.cs` script is attached to
- The name of the method and the name of the enums has to be the same e.g `strike()` and `strike`

### **Turn System**

There is a script called `TurnSystem.cs` which governs all player and enemy turns. They are currently divided into four sections which are kept in an enum datatype.

  1. Player Move
  2. Player Combat
  3. Enemy Move
  4. Enemy Combat

The `TurnSystem.cs` script is referenced by the `AllSkills.cs` script. When the player moves or strikes on their turn, it invokes the `NextTurn()` method in the `TurnSystem.cs` script, which increments the `BattleStatus` enum.

The turns are cycled through step by step. Since we don't currently have enemy functionality, we log the `Enemy Move` and `Enemy Combat` steps in the console. When we get to the end of the last step in the enum, `NextTurn()` brings us back to the beginning.

>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef
### **Movement System**

#### **How To Use(Movement-System)**

1. Attach this script to the game object which should be able to move

2. Be sure that you implemented the `TurnSystem.cs` and the `EditedGridGenerator.cs` correctly

![prefab image](https://i.ibb.co/mFMyTBw/Movement.png)

- `Main Cam`: The camera, whereby the player sees the scene

#### **How it works(Movement System)**

<<<<<<< HEAD
The movement system interacts with the `TurnSystem.cs` and as soon as the `status` variable is equal to the value `PlayerMove`, almost the same thing happens as when [selecting a card](#Select-a-Card). The only difference is, that we use the relative position, which saved in the [character](#Character), instead the position, which are saved in the [cards](#Card). we using this movement system for the enenmies too, but we have to adapt some point. The enemies will use the same method to move, but the way how to trigger this method will be different. 

##### **Player rotation(Movement Systm)**
To adapt the movement and the instantiated `highlight` objects to the current rotation of the [player](#Player), the `Update()` method triggers a method called `Rotate()` every frame. This method checks wheather the players press specific button, in order to rotate the [character object](#character-object) and is this the case, the `DestroyTiles()` in the `EditedGridGenerator.cs` will be triggered and clears all list and destroys all `hightlight` object. Afterwards the [character object](#character-object) will be rotate based on the pressed button and the `GenerateTiles()` will be method triggered again.  
=======
The movement system interacts with the `TurnSystem.cs` and as soon as the `status` variable is equal to the value `PlayerMove`, almost the same thing happens as when [selecting a card](#Select-a-Card). The only difference is, that we use the relative position, which saved in the [character](#Character), instead the position, which are saved in the [cards](#Card). we using this movement system for the enenmies too, but we have to adapt some point. The enemies will use the same method to move, but the way how to trigger this method will be different.

##### **Player rotation(Movement Systm)**
>>>>>>> b509ddc8d8322b376f23c874791c023ceeeae1ef

To adapt the movement and the instantiated `highlight` objects to the current rotation of the [player](#Player), the `Update()` method triggers a method called `Rotate()` every frame. This method checks wheather the players press specific button, in order to rotate the [character object](#character-object) and is this the case, the `DestroyTiles()` in the `EditedGridGenerator.cs` will be triggered and clears all list and destroys all `hightlight` object. Afterwards the [character object](#character-object) will be rotate based on the pressed button and the `GenerateTiles()` will be method triggered again.  
