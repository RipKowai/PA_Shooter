In this project I made a game made with DOTS.
Begun with making the player and the Inputs first using the new unity input system. 
Made the player rotate and always face the direction of the mouse aswell as shoot from the direction that the sapceship is facing. 
I created saparate scripts for player input, player system(function and stuff that have to dow tih the player), bullet componenty(holding only information for component) and i instantiate that in the player system.
Once i was done with bullets i created enemy spawner that spawns enemies outside of camera size.
(All of that using entities and i create it in a sub scene)
After that i created a enemyAI system that simply makes the enemies move aswell as rotate thowards the player. Again making a EnemyAI component script that holds information about the enemies like speed ect.
Once i did that i made the bullets diactivate the enemies if they overlap with anything that has the layer called Enemies.
