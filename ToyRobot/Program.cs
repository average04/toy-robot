﻿using ToyRobot;

CommandManager commandManager = new CommandManager();
while (true)
{
    Console.Write("Input Command (EXIT to terminate): ");
    string? command = Console.ReadLine();
    commandManager.ExecuteCommand(command);
    if (command == "EXIT") break;
}

Console.ReadLine();