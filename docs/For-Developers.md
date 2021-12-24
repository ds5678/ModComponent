ModComponent is intended to be used for creating mods that introduce new items.

This page is supposed to contain information about how the get started, answer development related questions and show examples.

# Unity
Asset Bundles can be created with Unity 2019.4.19 (free).

Existing item mods make the best examples when starting out.

# Wwise
SoundBanks can be created with WWise 2018.1.11
> This exact version is required. You will get a wrong version error otherwise. (Needs verified for recent versions of the game. Might have increased.)

Registration is required and the non-commercial version is limited to 200 audio files per SoundBank, but using multiple SoundBanks should be possible.

I found this [tutorial](https://www.audiokinetic.com/courses/wwise101/?source=wwise101&id=quick_start_from_silence_to_sound#read) from Audiokinetic very helpful.

Audiokinetic also provides some great (although lengthy) tutorials on youtube, e.g. <https://www.youtube.com/watch?v=JEp7Un7Vj44>


# Architecture

A rough description of the architecture and the motivation / reasoning behind it.<br/>
It isn't necessary to understand it, but it might help to get the big picture.

[Architecture](Architecture.md)

# Auto Mapper

Eliminates the need to write glue code for mapping items.

[Auto Mapper](Auto-Mapper.md)

# Item Mod Tutorial

A tutorial for creating your own item mod with ModComponent

> Outdated

[Item Mod Tutorial](Item-Mod-Tutorial.md)