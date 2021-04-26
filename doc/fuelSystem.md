# FuelSystem

Most of this mod is taken from https://github.com/thers/FRFuel .


## Changes
- Subtract the UI component
- Add a HTML UI
- subtracted the manual refuel
- added a lock system for vehicles from (https://github.com/winject/NoCarJack)

## Lifecycle
TODO add an image

Until then for reference: Check if in car --> check if viable for fuel & init --> consume Fuel && check for gas station --> if in gasstation (turn off engine/refuel prompts) 

Car Lock --> check if trying to enter car && lock it if not owned/has key 
Flag on car: car locked
if tying to enter car && has flag==true --> unlock car

## Registered Commands
Commands: 
    - ToggleCarKeys(set keys flag to 0/1)
    - SearchKeys(set keys flag to -1/0/1)
    - Unlock(set flag to true)

These commands will be changed into Item interactions once the CharacterInterface is done.

## Reference
https://github.com/thers/FRFuel  
https://github.com/winject/NoCarJack