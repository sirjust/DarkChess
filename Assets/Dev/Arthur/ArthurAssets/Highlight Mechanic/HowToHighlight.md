## How To Use
1. Attach this script to the game object you want to highlight.

2. Create a highlight prefab and assign it to the game object like so. Keep clone empty.

![](https://i.ibb.co/FB33YT4/Screenshot-2020-12-05-141843.png)


## How It Works

This script clones a prefab called `highlight` on top of the GameObject this script is attached to using the `OnMouseEnter()` event. 

In this case, I am cloning a quad that is emissive (looks like a highlight).

![](https://i.ibb.co/6vX1CkF/Screenshot-2020-12-05-144732.png)

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
###### If I tried to destroy the original "highlight" it would attempt to delete the prefab from our assets which would not work.

