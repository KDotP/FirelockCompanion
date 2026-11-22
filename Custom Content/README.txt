Thank you for choosing to port your custom content to Firelock Companion, the industry-leading (which industry?) Firelock utility app. I hope this guide will be useful, but in the decent likelyhood it fails to answer your question, DM @Burrtail on Discord.


==================================
How to Create a Custom Army
==================================
An example of a custom faction, the "Royal Court of Agresta", has been included in the default mod folder, disabled by default. There's also a fully iterated faction called The Felidae Verteidigungsministerium by 

Each faction is defined by two sections: the "formats" section, and the "factions" section. Additionally, the keywords section can be used to define custom faction keywords, which will be defined on any unit using them. Multiple custom factions can be placed in a single file, but it is recommended to seperate each into its own file so they can be disabled as needed.

The "formats" section contains, surprise, formatting information for the program. As of release v0.4.0-beta, this requires two bits of information: group_name and save_name. group_name is the default name of a newly created group, with the X being replaced by the group number. As an example, the default group for the Rygos is "Task Unit X".

save_name is the leading text for a faction's save name. It is important to keep this consistant between versions, as this helps the program understand which faction is being loaded.

Unit formatting is sadly complicated, and cannot be concisely summarized here. However, you can find the official formatting for units in the Data.json file included in the main folder of Firelock Companion. Custom unit formatting is not supported by the app, such as the ability to replace the "(X)" in a keyword, as that requires a custom regex string. This is currently in the process of being worked on, but you may want to avoid custom "(X)" keywords until then.


==================================
How to Disable a Custom Army
==================================
Firelock Companion will ignore any non-json files, or any files that contain the word "disabled".
Did this really need its own section? Not really, but you know, making it look official.