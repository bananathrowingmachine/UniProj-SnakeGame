UniProj-SnakeGame
=
This is *a client* for a multiplayer snake game, a game where you are a snake and you eat fruits to increase your score but you get longer as well and running into something causes you to lose.

The server was provided for use as a pre compiled executable (which I do not have, and it's also copyrighted anyways). Additionally I was unable to grab the assignment details so I'll do my best to describe what the goal was below.

The assignment:
=
It was divided into 3 parts. Part 1 was to to simply learn the basics of sending and receiving data using packets, just on a functional high level. You can see the files for that in "ChatClient" and "ChatServer" directories.

Part 2 was to then make the client (mostly). Essentially take all data emitted from the compiled server, translate it into visuals, and then transmit back controls and stuff. Unfortunately without the server you won't be able to see how it actually worked.

Part 3 was to then integrate a database into the client, that stored game information as well as individual player scoring information. Due to the instructions having one of us (as this was a partner project) put down our information in a string for user and password to our university provided database, that has also been redacted and will not work. The database integration code can be found in the "WebServer" directory however.
