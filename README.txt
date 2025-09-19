# Factory Reset

## Core Gameplay/ Game Type
The core gameplay is centered around controlling a rotationally based robot as it dodges obstacles, enemy attacks and advances towards the end of the level. Players will move between rooms together while fighting and looking for the exit, which will sometimes be blocked by a boss monster.


## Player Setup
There are up to 2 players at a time in 3rd person controlling their robots, with a single top down perspective, which changes only as the level changes. 


## AI Enemies
We want to have at least two enemy types, and will add more if time permits. 
Enemy 1: Turret
This enemy will be attached to the walls of the arena. It is unable to move in any direction, but will become active once the player comes within a certain distance of it. Once this happens, it will begin to aim at the player and shoot laser bullets at timed intervals
Enemy 2: Grunt
This enemy will be able to move, and will rotate between a variety of pre-defined attack patterns depending on player position and possibly enemy health. For example, they will have a ranged attack if the player is far away, and a melee attack if the player stays too close to the enemy for too long.


## Scripted Events
There are 2 Scripted Events we specifically have in mind at the moment, one is the spawning of new waves of enemies as the players descend through the level, and if there is enough time left without combat, new enemies spawn in.


## Environment
The environment will be a large spiral that descends into the ground, with various obstacles established to make more engaging terrain. The terrain itself will be a representation of layers of the planet as the players descend into its core.


## Basic Planning Factor
### Assets
We will make assets using Blender as well as source free assets from the Unreal Engine Marketplace

### Team Information
Group 17, Trajan Brown (20346627) , Laurie Yuzichuk (20260195), Zachary Kizell (20283184)

### Team roles
Animation, Visuals and Physics - Laurie
Communication and Enemy NPC  - Trajan
Networking and Player Control - Zach 


### Timeline/ Weekly Milestones
	
	Week 1 - Sept 14 to 20
Basic player movement completed


	Week 2 - Sept 21 to 27
Finish basic enemy AI with 3 distinct states


	Week 3 - Sept 28 to Oct 4
		- Assignment 2: Finite State Machine (FSM) Enemy due Oct 2
		- Implement basic combat mechanics


	Week 4 - Oct 5 to 11
Begin work on npc with pathfinding / decision making


	Week 5 - Oct 12 to 18
Design environment


	Week 6 - Oct 19 to 25
Create pathing mechanics for enemies


	Week  7 - Oct 26 to Nov 1
Begin implementing network capabilities
		Assignment 3: NPC Decision Making and Movement due Oct 30


	Week 8 - Nov 2 to 8
Network capabilities cont.


	Week 9 - Nov 9 to 15
Network capabilities cont.


	Week 10 - Nov 16 to 22
Smoothing lag effects


	Week 11 - Nov 23 to 29
Final touches
			Assignment 4: Game with Networking Component due Nov 27
