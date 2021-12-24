> Not compatible with Accelerant Behaviour

# Template
```
{
    "ModFireStarterBehaviour" : {
                                "DestroyedOnUse" : false,
                                "NumberOfUses" : 100,
                                "OnUseSoundEvent" : "",
                                "RequiresSunLight" : false,
                                "RuinedAfterUse" : false,
                                "SecondsToIgniteTinder" : 1,
                                "SecondsToIgniteTorch" : 1,
                                "SuccessModifier" : 0
                            }
}
```

# Parameters

> Note: cannot be used without a [Component](Basic-Information-about-Components.md).

## DestroyedOnUse
*bool*<br/>
Is the item destroyed immediately after use?

## NumberOfUses
*float*<br/>
How many times can this item be used?

## OnUseSoundEvent
*string*<br/>
What sound to play during usage.

## RequiresSunLight
*bool*<br/>
Does the item require sunlight to work?

## RuinedAfterUse
*bool*<br/>
Set the condition to 0% after the fire starting finished (either successful or not).

## SecondsToIgniteTinder
*float*<br/>
How many in-game seconds this item will take to ignite tinder.

## SecondsToIgniteTorch
*float*<br/>
How many in-game seconds this item will take to ignite a torch.

## SuccessModifier
*float*<br/>
Does this item affect the chance for success? Represents percentage points. Positive values increase the chance, negative values reduce it.