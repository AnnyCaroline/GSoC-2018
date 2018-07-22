![](https://uploaddeimagens.com.br/images/001/412/739/original/gsoc2018.png?1526046053)
# Céu-Maker: easy-to-install, up-to-date integrated environment for Céu-Arduino and pico-Céu development
Céu-Maker is a programming environment that integrates Céu-Arduino and pico-Céu in a single package for Windows. The project already existed before Google Summer of Code 2018, but lacked documentation and examples of use and required too much work from developers to generate a new release. Also, some features needed improvement.

Céu-Maker integrates 4 repositories, and the work done during the Google Summer of Code were added in the ```pre-v0.40``` branch:
- Céu: https://github.com/ceu-lang/ceu/tree/pre-v0.40
- Céu-Arduino: https://github.com/ceu-lang/ceu-arduino/tree/pre-v0.40
- pico-Céu: https://github.com/ceu-lang/pico-ceu/tree/pre-v0.40
- Céu-Maker: https://github.com/ceu-lang/ceu-maker/tree/pre-v0.40

The Céu-Maker version developed in Google Summer of Code can be found in [Céu-Maker releases page](https://github.com/ceu-lang/ceu-maker/releases).

Next, we will present the contributions to Céu-Maker, explaning the motivation and implementation.

## Automate the generation of a new release of Céu-Maker
To create a new release of Céu-Maker, developers needed too manually clone the repositories, checkout the target version for each of them and use a Makefile to copy the needed folders to the release. They also needed to:
- do some configuration in the Makefile (edit ```Makefile.dirs``` to point to the source repositories);
- donwload third-pary binaries if the command ```make``` does not exist;
- manually use WinRAR to generate a self-extracting executable.

Now, changes in Makefile are no longer necessary, make binary was included in Céu-Maker repository and the winrar process was automated. The developer only need to execute a single command using GitBash, that is normally availlable to anyone who uses Git on Windows.

## Upgrade in Céu-Arduino compilation
Now the user can select the Arduino board and port using Céu-Maker. We developed a GUI application (located in [/SelectDevice](SelectDevice)) that, when the user compiles a source code, list all the connected Arduino boards and allow the user to select one. 

ToDo: add SelectDevice print
![]()

## 
## 

