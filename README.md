# Vorze-PlayerHelper
Allows control of VORZE Cyclone SA via external players

Currently only Zoom Player is supported as that has an easy to use Control API over network, others can be supported upon request so long as they have the ability to read playback positions and status either locally or over network.

This version is still in the BETA phase as it needs better debug logging and perhaps some performance improvements, however it's fully functional already.

The Vorze brand and the SA product listed here are the property of RENDS Co.,Ltd.

This program merely expands their limited video player capabilities, this is for non-profit and can be used free of charge.

# Requirements

Zoom Player  - http://www.inmatrix.com/zplayer/

Microsoft .NET framework 4.5.2  - http://www.microsoft.com/en-us/download/details.aspx?id=42642

Vorze Player Helper - https://github.com/Net005/Vorze-PlayerHelper/releases

# HOWTO

##### Step 1 - Enable Control API in Zoom Player setings

Enable it in the options dialog under "Advanced Options / System".
The default TCP/IP port is "4769"

Alternatively, with Zoom Player v8.1 or newer, you can instruct Zoom Player to connect to your server using the
"/CONNECTTCP:[address or ip]:[port]", for example "/CONNECTTCP:127.0.0.1:4770".

##### Step 2 - Start Vorze-Player helper and change settings

If IP / PORT is different change it otherwise leave it default.

Change the "Vorze CSV root directory" to the root directory where you keep your CSV files, the files need to be named as follows:

> your video filename without extension.csv

for example if your video filename is "movie.avi" your csv file would be named "movie.csv"

##### Step 3 - Enable "Start monitoring for Zoom player activities" to start Vorze support

If it detects Zoom player is running and it finds an valid CSV file it will automatically start sending commands to the Vorze device.

If playback is paused it will stop the Vorze device and on resume enable it again with very little delay, on playback stop it will set an stop command as well.

##### Optional Vorze CSV / scripts

Below link contains a few Vorze movie csv/scripts and will be adding more there in the future:

https://mega.co.nz/#F!Qd5kHbBJ!M-xCXy6XY1hF_uoQe_K0Ow

These were created using the awesome OneTouch scripter: http://onetouchscripter.com
