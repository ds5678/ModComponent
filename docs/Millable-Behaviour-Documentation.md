# Template
```
{
    "ModMillableComponent" : {
                                "RepairDurationMinutes" : 15,
                                "RepairRequiredGear" : ["GEAR_ScrapMetal"],
                                "RepairRequiredGearUnits" : [2],
                                "CanRestoreFromWornOut" : false,
                                "RecoveryDurationMinutes" : 60,
                                "RestoreRequiredGear" : ["GEAR_ScrapMetal","Gear_Cloth"],
                                "RestoreRequiredGearUnits" : [4,2],
                                "Skill" : "None"
                            }
}
```

# Parameters

> Note: cannot be used without a [[Component|Basic Information about Components]].

## RepairDurationMinutes
*int*<br/>
The number of minutes required to repair the item.

## RepairRequiredGear
*Array of string text*<br/>
The Gear Items required for repairing the item.

## RepairRequiredGearUnits
*Array of int numbers*<br/>
The numbers of each Gear Item required for repairing the item.

## CanRestoreFromWornOut
*bool*<br/>
Can the item be restored from a ruined state?

## RecoveryDurationMinutes
*int*<br/>
The number of minutes required to restore the item.

## RestoreRequiredGear
*Array of string text*<br/>
The Gear Items required for restoring the item.

## RestoreRequiredGearUnits
*Array of int numbers*<br/>
The numbers of each Gear Item required for restoring the item.

## Skill
*[[Skill Type|Skill Type List]]*<br/>
The skill associated with repairing/restoring this item. 