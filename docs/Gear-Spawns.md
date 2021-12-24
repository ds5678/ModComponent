> Coordinates, rotations, item, scene and loot table names can be obtained in-game by using the [Coordinates-Grabber](https://github.com/ds5678/Coordinates-Grabber/releases/latest)

Gear items (items that can be picked up) can be configured to appear additionally to the ones already present in the vanilla game.

The `GearSpawnReader` will look for configuration files in the `gear-spawns` internal directory of the zip file.

Any files in that directory ending with `.txt` will be treated as a gear spawn configuration file and processed accordingly. The name of the files does not matter.

## Configuration

Item spawns and loot table entries can be defined in any order, but it might be helpful to keep the files nice and tidy.

Separating individual scenes or themes into individual files might make it easier to experiment and configure. This will have no adverse effect on performance or resource consumption.


### Scene
The [[scene|Scenes]] of the item spawn must be defined first:
```
scene=<SceneName>
```
All following item spawn definitions will use that scene, until another scene is defined.

### Item Spawn

An item spawn definition consists of the item's name and its position. Optionally a rotation and spawn chance can be configured:
```
item=<ItemName> p=<Position> r=<Rotation> c=<SpawnChance>
```
* ItemName is the item's name as it appears in the DeveloperConsole. Items introduced by mods can be used as well.<br/>E.g.: `BasicBoots` or `CannedMangoes`. `GEAR_BasicBoots` and `GEAR_CannedMangoes` also work.
* The position is a Vector with 3 mandatory components separated by comma: x,y,z.<br/>E.g.: `1.23,2.34567,-123.456789`
* The rotation is a Vector with 3 mandatory components, representing the euler angles of the corresponding Quaternion.<br/>E.g. `0,-34.0374,0`<br/>The default value is `0,0,0`
* Spawn Chance is a numeric value representing the probability in per cent that the item actually spawns, so '100' or above means "will definitely spawn" and '0' or below means "will definitely not spawn". E.g.: `80.5`<br/>The default value is `100`


### Loot Table
The [[loot table|Loot Tables]] of the loot table entry must be defined first:
```
loottable=<LootTableName>
```
All following loot table entry definitions will use that loot table, until another loot table is defined.

### Loot Table Entry
A loot table entry definition consists of the item's name and its weight:
```
item=<ItemName> w=<Weight>
```
* ItemName is the item's name as it appears in the DeveloperConsole. Items introduced by mods can be used as well.<br/>E.g.: `BasicBoots` or `CannedMangoes`. `GEAR_BasicBoots` and `GEAR_CannedMangoes` also work.
* The weight represents the "size" of the item in the loot table. "Bigger" items are more likely to be selected, "smaller" items are less likely to be selected. `0` means the item cannot be selected. There is no upper limit. For more details see [https://en.wikipedia.org/wiki/Roulette-wheel_selection](https://en.wikipedia.org/wiki/Roulette-wheel_selection)

> It is also possible to redefine the weight of pre-existing loot table entries that way and make items more likely or less likely ot be selected. Setting an item's weight to `0` will disable it completely in the chosen loot table.


## Example
```
# this line is a comment and will not be processed, neither will the empty line below
    
# Pleasant Valley Farmstead
scene=FarmhouseA
item=Water500ml p=3.5729,0.8793,6.9064
item=BeerBottle p=3.5851,0.8793,7.0168
    
# Pleasant Valley
scene=RuralRegion
item=Rope p=1453.5633,48.7212,1029.07641 r=0,186.75,0 c=80

# Glove boxes in cars
loottable=LootTableVehicleGloveBox
item=WhiskyFlask w=1

# no more Beef Jerky in glove boxes!
item=BeefJerky w=0
```