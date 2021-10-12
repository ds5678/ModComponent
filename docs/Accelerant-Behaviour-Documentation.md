> Not compatible with FireStarter Behaviour

# Template
```
{
    "ModAccelerantComponent": {
                                "DestroyedOnUse" : true,
                                "DurationOffset" : 0,
                                "SuccessModifier" : 40
                            }
}
```

# Parameters

> Note: cannot be used without a [[Component|Basic Information about Components]].

## DestroyedOnUse
*bool*<br/>
Is the item destroyed immediately after use?

## DurationOffset
*float*<br/>
In-game seconds offset for fire starting duration from this accelerant. NOT scaled by fire starting skill. Positive values mean 'slower', negative values mean 'faster'.

## SuccessModifier
*float*<br/>
Does this item affect the chance for success? Represents percentage points. Positive values increase the chance, negative values reduce it.