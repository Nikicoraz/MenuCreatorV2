# MenuCreatorV2
because it's better than the CUI Menu I created
## Install
Import [the dll](https://github.com/Nikicoraz/MenuCreatorV2/releases/latest) in the dependencies of the C# project
## Usage
Create the menu with 
```cs
Menu.CreateMenu(<Menu options>, [args]);
```
The return value of the function is the index of the option selected
### Args
- x: Set the x from the left of the terminal
- y: Set the y from the top of the terminal
- alignment: Align the text of the options
- fg: The foreground color of the options
- bg: The background color of the options
- selectedFg: The foreground color of the selected option
- selectedBg: The background color of the selected option
- reset: If the menu dissapears after it's lived it's life