# LeaugeOfWindowsKeyNotBeingStolen




LeaugeOfWindowsKeyNotBeingStolen is a lightweight utility designed to prevent League of Legends from hijacking the Windows key, ensuring its functionality remains intact while a game is active. 



## How It Works

- The app hooks into keyboard input and detects when the Windows key is pressed.
- It finds a background process (like the desktop or a system process) and temporarily brings it to the foreground.
- After that, it allows the keystroke to pass normally

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/DamienTaubman/LeagueOfWindowsKeyNotBeingStolen
   ```
2. Open the project in your favorite IDE (e.g., Visual Studio).
3. Build the solution and run the executable.



## Usage

1. Set the app to run on startup by adding it to the Windows Startup folder or using the Task Scheduler.							
2. It is hard coded to league of legends, and will only hook the windows key while the league window is active.



## Contributions

Contributions, issues, and feature requests are welcome! Feel free to check the issues page if you have any questions or ideas.


## Author

[Damien Taubman](https://github.com/DamienTaubman)
