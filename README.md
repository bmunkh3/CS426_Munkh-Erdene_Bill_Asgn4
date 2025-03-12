# Assignment 04: Physics, Textures, and Lights

**Memory Employees** is a cooperative game, where players have the goal to prevent data overflow in memory due to data thieves attempting to shut the system down to steal data. Players have the goal to keep processing input data spheres to the best of their ability, as the farther the game progresses the faster the data spheres will start spawning. 

---
## New Design Changes:
Looking over my group's Assignment 2, I thought we lacked clear instructions on what the player's goal should be to win the game. So, I implemented some changes to the lightning to guide the player on the steps they need to take by color coding the different lights based on the Devices. This design change aims to assist the player visually without direct instruction. Additionally, aside from the quick pace of our game being the challenge of it, I thought the map could use more navigational challenges and physics constructs that collide with the player, as well as the spheres. This proved to be a good design choice, as these physics constructs collide with the Devices as well, which can block the CPU devices from accepting any Input spheres, as well as blocking the player from the Output/CPU devices.

### Physics Constructs:
Hazardous walls, being leftovers from cached in memory, that bounce and react upon collision with the player, as well as the data spheres. When the player throws a data sphere at one of these constructs, the walls recoil and bounce back, and the spheres obviously bounce away from the wall as well.

### Billboards:
I used a 3d object: quads and a texture for a lightbulb to represent a light source to the player. The quad is set up as a billboard, meaning it always faces the camera. This technique not only enhances the visual appeal of the game but also reinforces the theme of Computer Architecture by symbolizing a functional, dynamic light source that guides the player through the level.

### Lights:
Since the concept of the game is that the player is an employee working in the Memory of a computer device, and monitors being output devices. I used RGB colors to represent each device with a distinctly colored light source (lightbulb), since modern displays use the RGB color model to render images, making RGB an important part of how computers visualize data. This naturally tied into my idea of using light and color to represent different components in Computer Architecture. To sum it up, I used the lights to color code the objectives. (Input Devices = lightbulb billboard with blue light, CPU Devices = lightbulb billboard with green light, Output Devices = lightbulb billboard with red light.)

---

### Serious Objective:
Learn about computer architecture, specifically input data overflow in memory, and understand its effects on system performance.

---

## Player Interaction Pattern
- **Single Player VS. Game**: For this game, the player experience is intended to just be single player.
  
---

## Procedures and Controls

### Controls:
- **E**: Pick up item.
- **Left Mouse Click**: Throw picked-up item.
- **W A S D**: Move forward, back, left, and right.

### Procedures:
1. Pick up unprocessed data spheres (blue) emitted from input devices
2. Process the unprocessed data spheres at the CPU interpreter, turning them into processed data spheres (green).
3. Deliver the processed data spheres to the correct output device.

---

## Rules
1. **Matching Rule**: Output devices only accept CPU processed data, will reject unprocessed data.
2. **Time Limit**: Players have limited time and need to keep up with the pace that the input data spheres get spawned by the input devices.
3. **Memory Management**: A limit exists on how much unprocessed data can be on the map simultaneously. Exceeding this limit causes data overflow, leading to system slowdown and a game over screen.

---

## Resources
- **Map Area**: Enclosed rectangular map.
- **Scoreboards**: 
  - Progress towards memory leak.
  - Player contributions.
- **Players**: Capsules with different colors.
- **Input Devices**: Machines that emit unprocessed data spheres.
- **CPU Interpreter**: Processes unprocessed data spheres and outputs processed data spheres.
- **Output Devices**: Accept CPU processed data spheres only.
- **Billboards**: Light source with a lightbulb texture always facing the player.
- **Hazardous Walls**: Obstacles that collide with the objectives and the player.

---

## Non-Plain-Vanilla Rule
Players must process unprocessed input data spheres into CPU-processed data spheres before they can be delivered to the output devices. Output devices will not accept input data spheres.

---

## Educational Component

### Test Question:
What happens when a computer's memory is loaded with more data than it can handle, and how does this affect the computer's performance?

### Expected Answer:
If memory is overwhelmed with excessive data, it can lead to a memory leak, causing system slowdown and potentially resulting in a system crash if not managed.

---



## Credits
Developed by Bill Munkh-Erdene @ CS426, Spring 2025 by Liz Marai.
