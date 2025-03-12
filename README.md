# Memory Employees

**Memory Employees** is a cooperative game, where players have the goal to prevent data overflow in memory due to data thieves attempting to shut the system down to steal data. Players have the goal to keep processing input data spheres to the best of their ability, as the farther the game progresses the faster the data spheres will start spawning.

---

## Gameplay Overview

### Objective:
Rescue. Avoid data overflow in memory by successfully processing and delivering data.

### Serious Objective:
Learn about computer architecture, specifically input data overflow in memory, and understand its effects on system performance.

---

## Player Interaction Pattern
- **Cooperative Play**: Work together to prevent memory leaks.
  
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
Developed by Bill Munkh-Erdene, Habeeb Rehman, Sai Samith Reddy Sudini @ CS426, Spring 2025 by Liz Marai.
