# TestWFC
A test project creating a functioning wave function collapse system for labyrinth generation

This project was made to implement a tile based wave function collapse algorithm, with an easy manner of adding rules to each tile possibility.
To use the project you have to first add the labyrinth generator script to a game object, then add all of the possible tile components to its list in the form of tilecomponent objects.
Tilecomponents take a prefab for said tile, as well as 4 contact type objects for each side of the tile. The project is laid out where Z is north and X is east.
ContactTypes take lists of different compatible contactTypes that can be adjacent to them. 
