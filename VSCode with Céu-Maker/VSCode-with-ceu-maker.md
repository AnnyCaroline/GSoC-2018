# Céu-Maker with VSCode 
## Usage
[![video](http://img.youtube.com/vi/iNG42KRNjjI/0.jpg)](http://www.youtube.com/watch?v=iNG42KRNjjI)

## Installation
1. Install [Open in Application](https://marketplace.visualstudio.com/items?itemName=fabiospampinato.vscode-open-in-application) extension  
2. *File > Preferences > Settings* and add this configurations:
```json
"openInApplication.applications": {
    "ceu": ["Céu-Arduino.exe", "pico-Céu.exe"]
},

"files.associations": {
    "*.ceu": "csharp"
}
```
3. Add ```ceu-maker/bin``` to PATH environment variable

## Add a shotcut for openInApplication
In VSCode, open ```File > Preferences > Keyboard Shortcuts``` and search for ```openInApplication.open``` command. Double click on it, press the desired key combination and click ENTER.

## References
- [Key Bindings for Visual Studio Code](https://code.visualstudio.com/docs/getstarted/keybindings#_keyboard-shortcuts-editor)
