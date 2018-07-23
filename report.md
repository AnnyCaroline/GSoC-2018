-------------------------------------------------------------------------------
`[23-Jul] -> [29-Jul]`
-------------------------------------------------------------------------------
### `[23-Jul]` What will I do this week?
- translate the tutorials;
- create a pico-Céu example (with tutorial) that covers the remaining resources;
- list arduino functionalities and create an example, with tutorial, that covers its features;
- close the remaining issues

### `[29-Jul]` What did I do last week?

-------------------------------------------------------------------------------
`[16-Jul] -> [22-Jul]`
-------------------------------------------------------------------------------
### `[16-Jul]` What will I do this week?
- Create tutorial and an example showing arduino functionalities
- Create a tutorial and an example showing the network communication

### `[22-Jul]` What did I do last week?
- postponed to next week
- pico-ceu/examples/move-net (version pre-v0.40)
- update the pico-ceu/examples/move-net to cover more Céu features

-------------------------------------------------------------------------------
`[09-Jul] -> [15-Jul]`
-------------------------------------------------------------------------------
### `[09-Jul]` What will I do this week?
- Disable the option to use IDE configuration
- Install Linux in a Virtual Box 
- Create a tutorial and an example about pico-céu graphics

### `[15-Jul]` What did I do last week?
- The IDE configuration was disabled ([ceu-lang/ceu-maker#13](https://github.com/ceu-lang/ceu-maker/issues/13))
- ok
- The tutorial and the example are in pico-ceu repository, inside examples/race folder (version pre-v0.40)

-------------------------------------------------------------------------------
`[02-Jul] -> [08-Jul]`
-------------------------------------------------------------------------------
### `[02-Jul]` What will I do this week?
- Fix a bug in SelectDevice.exe - CPU comboBox was displaying all CPUs in one line
- Stop compiling when close SelectDevice Window [ceu-lang/ceu-maker#12](https://github.com/ceu-lang/ceu-maker/issues/12)
- Start writing the tutorial
    - The tutorials must include
        - graphical interaction (pico-Céu)
        - Arduino programming (Céu-Arduino)
        - Both of then, including communication via Serial
        - Network
    - Implement the examples while writing the tutorials
- Fix a bug when use the IDE configuration in SelectDevice
- Fix issue [ceu-lang/ceu-maker#10](https://github.com/ceu-lang/ceu-maker/issues/10)
    - pico-Céu.exe and Céu-Arduino.exe don't compile files with space in name
    - compile a folder is still not working
- Usart
    - replace await with spawn, NEVER with finalize
    - allow the user to pass _ as parameter to `Usart` function. In this case, the connected port is used. If more than one port is connected, then the first detect is choosen.
- Check if the modifications in Céu-Arduino and pico-Céu repositories don't produce any error

### `[08-Jul]` What did I do last week?
- SelectDevice bug fixed in [ceu-maker-gsoc cd832c2](https://github.com/AnnyCaroline/ceu-maker-gsoc/commit/cd832c2c76b59447e0f22e34ea8cfa9323131ae4) and [ceu-maker 6a167d2](https://github.com/ceu-lang/ceu-maker/commit/6a167d29f8be45d8095144231de80660107e4572)
- Fixed [ceu-lang/ceu-maker#12](https://github.com/ceu-lang/ceu-maker/issues/12) "Stop compiling when close SelectDevice Window"
- 
- The bug when use the IDE configuration was not solved yet. The arduino Makefile was changed to support different compilation process when using the IDE configs, but I didn't figure out what is causing the bug
- The issue [ceu-lang/ceu-maker#10](https://github.com/ceu-lang/ceu-maker/issues/10) was partially fixed. Files with space in names can now be compilled, but this created a error when compiling a folder (with main.ceu file in it).
- Usart
    - the replacements were made in [ceu-maker 8f58b08](https://github.com/ceu-lang/ceu-maker/commit/8f58b080a3df1c3842e1e1f642366d76d2cfbb75) and in [pico-ceu fe94587](https://github.com/ceu-lang/pico-ceu/commit/fe94587aac2cf8ab187c69233e232ae86f91cd85)
    - still working in it
- need help in "Check if the modifications in Céu-Arduino and pico-Céu repositories don't produce any error"

-------------------------------------------------------------------------------
`[25-Jun] -> [01-Jun]`
-------------------------------------------------------------------------------
### `[25-Jun]` What will I do this week?
- Fix issue [ceu-lang/ceu-maker#10](https://github.com/ceu-lang/ceu-maker/issues/10)
    - pico-Céu.exe and Céu-Arduino.exe don't compile files with space in name
- Pico-Céu usart
  - include in pico-céu repository
      - as explaned in , string.ceu was also modified. This file should be added in Céu repository
  - change *await* to *spawn* and use *finalize* (consequently is necessary to use *NEVER*)
  - allow the user to use the default port number when calling *usart*

### `[01-Jun]` What did I do last week?
- The issue was mostly fixed. Some things that We still need to do:
    - Check if *Storm* example still works after the changes and if it works with whitespaces in its filepath.
- usart 


-------------------------------------------------------------------------------
`[18-Jun] -> [24-Jun]`
-------------------------------------------------------------------------------
### `[18-Jun]` What will I do this week?
- Implement an application that uses Usart
- Document Usart
- Fix issue [ceu-lang/ceu-maker#9](https://github.com/ceu-lang/ceu-maker/issues/9)
    - Executing pico-Céu code by double click

### `[24-Jun]` What did I do last week?
- The app is in [https://github.com/AnnyCaroline/ceu-maker-gsoc/tree/master/usart](https://github.com/AnnyCaroline/ceu-maker-gsoc/tree/master/usart). This folder also contains the usart.ceu and an explanation of the application;
- The [ceu-lang/ceu-maker#9](https://github.com/ceu-lang/ceu-maker/issues/9) was fixed.

-------------------------------------------------------------------------------
`[11-Jun] -> [17-Jun]`
-------------------------------------------------------------------------------
### `[11-Jun]` What will I do this week?
- Fix the issue [ceu-lang/ceu-maker#4](https://github.com/ceu-lang/ceu-maker/issues/4)
    - ```[pico-ceu] API for Serial ports```
- Fix the issue [ceu-lang/ceu-maker#8](https://github.com/ceu-lang/ceu-maker/issues/8)
    - ```'tr' is not recognized as an internal command ```
    - The error above shows up when a Céu-Arduino file is compiled. See the issue for more information.
- Fix a refresh bug in SelectDevice.exe
- Check if SelectDevice.exe can use IDE options properly, including compile throw a Network port
    - In the IDE this is done like that:
    ![arduino ide: choose network port](https://uploaddeimagens.com.br/images/001/468/246/full/arduino-network-port.png?1529165817)

### `[17-Jun]` What did I do last week?
- Usart is working. Still need to implement Usart_TX and Usart_RX;
    - The current work is in [Google Drive](https://drive.google.com/open?id=1T1n-H-LqzXxNJpxF597A3M9Q1vOAu5FP)
- Fixed the issue [ceu-lang/ceu-maker#8](https://github.com/ceu-lang/ceu-maker/issues/8) by adding ```tr.exe``` bin from [GnuWin32](http://gnuwin32.sourceforge.net/) in Céu-Maker. This bin needed two dlls. One of them, ```libiconv2.dll``` is already present in ```ceu-maker/mingw/bin/``` with the name ```libiconv-2.dll```. Would be interesting to find a way to not have the same file twice.
- The SelectDevice.exe refresh bug was fixed with the commits [50c1827](https://github.com/AnnyCaroline/ceu-maker-gsoc/commit/50c1827b825e300b55a95250ee492c8de8926427) and [f9796ac](https://github.com/ceu-lang/ceu-maker/commit/f9796ac26241a901c0f13ed9bc7bb5f9f97de25f)
-

-------------------------------------------------------------------------------
`[04-Jun] -> [10-Jun]`
-------------------------------------------------------------------------------
### `[04-Jun]` What will I do this week?
- Fix the issue [ceu-lang/ceu-maker#4](https://github.com/ceu-lang/ceu-maker#4)

### `[10-Jun]` What did I do last week?
- I found difficulties to fix the issue [ceu-lang/ceu-maker#4](https://github.com/ceu-lang/ceu-maker#4)

-------------------------------------------------------------------------------
`[28-May] -> [03-Jun]`
-------------------------------------------------------------------------------
### `[28-Jun]` What will I do this week?
- Fix the issues from the last pull request ([ceu-lang/ceu-maker#4](https://github.com/ceu-lang/ceu-maker#4), [ceu-lang/ceu-maker#5](https://github.com/ceu-lang/ceu-maker#5), [ceu-lang/ceu-maker#6](https://github.com/ceu-lang/ceu-maker#6), [ceu-lang/ceu-maker#7](https://github.com/ceu-lang/ceu-maker#7))
- Study the modifications in Céu-Maker 

### `[03-Jun]` What did I do last week?
- Fixed the issues ([ceu-lang/ceu-maker#5](https://github.com/ceu-lang/ceu-maker#5), [ceu-lang/ceu-maker#6](https://github.com/ceu-lang/ceu-maker#6), [ceu-lang/ceu-maker#7](https://github.com/ceu-lang/ceu-maker#7))
- The issue [ceu-lang/ceu-maker#4](https://github.com/ceu-lang/ceu-maker#4) was postponed to next week

-------------------------------------------------------------------------------
`[21-May] -> [27-May]`
-------------------------------------------------------------------------------
### `[21-May]` What will I do this week?
- Fix the last pull request
- Test and continue the implementation of the User interface to chosse arduino board and port
    - allow the user to choose the cpu if necessary
    - document
    - pull request
- Propose a aplication to use as example

### `[27-May]` What did I do last week?
- Fixed the last pull request and made a [new one](https://github.com/fsantanna/ceu-maker/pull/3);
- SelectDevice updates
    - Implemented CPU selection in SelectDevice;
    - Fixed a bug in SelectDevice - Reference [AnnyCaroline/ceu-maker-gsoc#1](AnnyCaroline/ceu-maker-gsoc#1);
    - Implemented a workaround in SelectDevice App so that users can choose to select a board or use the IDE configurations (see [more](https://github.com/AnnyCaroline/ceu-maker-gsoc/tree/master/SelectDevice));
- Changed WinRAR 64-bit to 32-bit;
- Updated and fixes Céu-maker intructions;
- Tested Céu-Maker with 3 Arduino boards: [video](https://youtu.be/U1Rzs9SMOyA);
- VSCode with Céu-Arduino: [instructions](https://github.com/AnnyCaroline/ceu-maker-gsoc/blob/master/VSCode%20with%20C%C3%A9u-Maker/VSCode-with-ceu-maker.md) and [video](https://www.youtube.com/watch?v=iNG42KRNjjI);

-------------------------------------------------------------------------------
`[14-May] -> [20-May]`
-------------------------------------------------------------------------------
### `[14-May]` What will I do this week?
- Pull request
- Study pico-ceu
- User-friendly interface for end-user choose arduino board and port (céu-arduino)

### `[20-May]` What did I do last week?
- Pull request: https://github.com/fsantanna/ceu-maker/pull/1
- Started the studies of pico-ceu
- Developed a Windows Forms Application with GUI to auxiliate the board selection
    - Application: https://github.com/AnnyCaroline/ceu-maker-gsoc/tree/master/SelectDevice
    - Integration with Céu-Maker: https://github.com/AnnyCaroline/ceu-maker/commit/305d559256df8fb909a07137a0930ca4e75dfef9
-------------------------------------------------------------------------------
`[07-May] -> [13-May]`
-------------------------------------------------------------------------------
### `[07-May]` What will I do this week?
- Continue the study of Package Generation of Céu-Maker
    - add make.exe in céu-maker repository
        - change instructions
            - remove download section
            - reinforce to use Git Bash
    - change Makefile to automate the winrar section
- Search for Arduino projects to compose the project's set of examples

### `[13-May]` What did I do last week?
- Completed the tasks nested in the study of Package Generation
    - updated instructions
    - generate script to use WinRAR via command line
- Created a prototype of a GUI app for Céu-maker package generator
- Postponed the search for Arduino projects

-------------------------------------------------------------------------------
`[30-Apr] -> [06-May]`
-------------------------------------------------------------------------------

### `[30-Apr]` What will I do this week?

- Ask and search about some parts of the paper
- Understand and execute Package Generation of Céu-Maker

### `[06-May]` What did I do last week?
- Deeper study of the paper and the features of Céu
- I found difficulties to reproduce the Céu-Maker package generation
-------------------------------------------------------------------------------
`[23-Apr] -> [29-Apr]`
-------------------------------------------------------------------------------

### `[23-Apr]` What will I do this week?

- Mailing list of Céu
    - https://groups.google.com/forum/#!forum/ceu-lang
    - Tasks:
        1. register
        2. take a glance at previous messages
        3. post small introduction about the project with related links

- Papers about Céu
    - Anny:
        - http://www.ceu-lang.org/chico/ceu_sensys13_pre.pdf
    - Naveen:
        - http://www.ceu-lang.org/chico/ceu_sensys13_pre.pdf
        - http://www.ceu-lang.org/chico/ceu_lctes18_short_pre.pdf
    - Tasks:
        - Read the suggested papers
            - (not everything is exactly the same as in the current implementation)
        - Ask questions
        - It may take time (weeks)

- Fork repository
    - Anny
        - https://github.com/fsantanna/ceu-maker
    - Naveen
        - https://github.com/fsantanna/ceu-arduino
    - Add this file
    - Add your project proposal submitted to GSoC
    - Share the link among us
    - Keep it up-to-date with the original
        - https://stackoverflow.com/questions/7244321/how-do-i-update-a-github-forked-repository

- Be proactive! Suggest and discuss tasks with mentor

### `[30-Apr]` What did I do last week?
    - Everything expected, but the paper needs more work. There are some parts that I should search and ask about. 

`(1-2 small paragraphs for each of the planned tasks)`
