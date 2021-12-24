# Supported Item Types

### Beds
All types of item that can be used to sleep on/in.

### Clothing
Any and all types of clothing, including accessories.

Clothing items can bring their own implementation, allowing for completely new game mechanics, e.g. the Ski Goggles from [Clothing-Pack](https://github.com/ds5678/Clothing-Pack) reduce the brightness while worn.

### First Aid
Items that treat afflications or restore condition.

### Food / Drink
Consumable food and drink items, including things that can / need to be cooked.  
These items may contain [[alcohol]] that can affect the player character.

### Generic Items
Items that don't have a primary function, but can still be carried around in the players inventory.  
These items can be configured to be harvestable, required for crafting or suitable for making fires.

### Generic Equippable Items

These items are equippable and bring their own implementation, allowing for completely new game mechanics, e.g. the [Binoculars](https://github.com/ds5678/Binoculars) actually allow you to see far away things.

### Liquid Items
Items that can hold liquids.

### Tools
Either items that can be equipped to perform a specific function (like the rifle) or items that simply sit in the inventory and are required for a certain task (like the sewing kit).


# Supported Item Behaviours

All behaviours can be attached to all items.

### Accelerant
Configure which items can be used as accelerants to increase the chance to successfully start a fire.

### Burnable
Configure which items can be burnt in a fire and how much temperature and time they provide.

### Evolve
Configure which items can transition into another item, e.g. gut curing.

### Fire Starter
Configure which items can be used to start fires and how effective they are.

### Harvestable
Configure which items can be broken down and which resources they yield.

### Millable
Configure which items can be repaired at the milling machine in Bleak Inlet.

### Repairable
Configure which items can be repaired and which tools and items are required for this.

### Scent
Configure which items emit a smell and increase animal detection.

### Sharpenable
Configure which items can be sharpened to restore their condition.

### Stackable
Configure which items can be stacked in the inventory.


# [Blueprints](Blueprints.md)

New [[blueprints|Blueprints]] can be defined for crafting existing or modded items.

# Loot Tables and Item Spawns

New items can be added to the existing loot tables and then spawn randomly while the player searches containers.  
It is also possible to change the spawn chance for existing items.

New items can be made to spawn in locations with a configurable probability ("fixed" / "chance-based").

Everything is configured by creating [[gear-spawn configurations|Gear-Spawns]]

# [Alternative Item Actions](Alternative-Actions.md)

Additional click actions can be bound to all interactive things, providing additional interactions.