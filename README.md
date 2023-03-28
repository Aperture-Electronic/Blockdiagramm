# Blockdiagramm 

Blockdiagramm is a graphical block design tool for integrated circuit (IC) design.  

## Main Features
* Add and analysis your HDL design files and create the visual component in block design enviroment;
* Use your mouse to connect modules and IPs in diagrams;
* Inferring the bus protocol (like AXI) and connect them;
* Make multi-layer diagram with multi design units, easy to manage and easy to configure;
* Build the diagram to entity sources in HDL!

## Development Status
**Our development is under intense progress. If you wish to join us, please contact us!**

|Part|Function|Status|
|--|--|--|
|Frontend|Main UI Design|In progress|
||Main UI Interactive|In progress|
||UI Testing|Not started|
|Backend|Server Framework|In process|
||APIs|In process|
||Unit Testing|Not started|
|HDL Library|Parser (System Verilog 2017)|Done|
||Parser(VHDL)|Not started|
||Pre-processor (System Verilog 2017)|Done|
||Pre-processor(VHDL)|Not started|
||Elaborator (System Verilog 2017)|Done
||Elaborator (VHDL)|Not started|

## Technical Stack
|Part|Technical Stack|Languages|
|--|--|--|
|Frontend|Vue 3|TypeScript, JavaScript, CSS, HTML5|
||Electron|TypeScript|
||Quasar|TypeScript, CSS, HTML5, SCSS|
|Backend|ASP.NET Core (.NET 7)|C#|
|HDL Library|.NET Standard 2.1|C#|
||.NET 7 (*for Elaborator*)|C#|
