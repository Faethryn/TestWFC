# TestWFC
A test project creating a functioning wave function collapse system for labyrinth generation

This project was made to implement a tile based wave function collapse algorithm, with an easy manner of adding rules to each tile possibility.
To use the project you have to first add the labyrinth generator script to a game object, then add all of the possible tile components to its list in the form of tilecomponent objects.
Tilecomponents take a prefab for said tile, as well as 4 contact type objects for each side of the tile. The project is laid out where Z is north and X is east.
ContactTypes take lists of different compatible contactTypes that can be adjacent to them. 


# Map Generation
The map Generates using the wave function collapse algorithm, it first picks the tile with the least possible states/TileComponents in its possible ID's
Once it picks this first tile, it attempts to assign it a random id from its possible ID list, then propagates the ruleset of this tile to others. 
If this chain is successfull and never returns a false value, the changes are pushed to each tile, and then the generator moves to the next tile with the least ID's.
If the chain is unsuccessful, the generator takes a step back and does not push changes, retrying with another possible ID. 

# Map Serialization
The map saves the ID's of each tile to a texture map, using the red channel as its save position, Due to using a texture to save the map, you are limited to resolutions that are multiples of 2. 

![Map screenshot](https://github.com/Faethryn/TestWFC/assets/97239542/df81c362-0c78-4f39-b463-ba4a22c8fab6)

![Grid](https://github.com/Faethryn/TestWFC/assets/97239542/11f62d79-314b-4230-ab8d-312d51a494a9)
