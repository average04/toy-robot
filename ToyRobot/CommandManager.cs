using System.Text.RegularExpressions;

namespace ToyRobot;
public class CommandManager
{
    public (int, int) _position;
    public Direction? _direction;

    public void ExecuteCommand(string? commandText)
    {
        if (commandText == null) return;

        var command = ToEnumCommand(commandText.Split(" ")[0]);
        if (command == null) return;

        switch (command)
        {
            case CommandEnum.Place:
                PlaceCommand(commandText.Split(" ")[1]);
                break;
            case CommandEnum.Move:
                MoveCommand(_direction);
                break;
            case CommandEnum.Left:
                LeftCommand(_direction);
                break;
            case CommandEnum.Right:
                RightCommand(_direction);
                break;
            case CommandEnum.Report:
                ReportCommand(_position, _direction);
                break;
        }
    }
    private static CommandEnum? ToEnumCommand(string commandText)
      => commandText switch
      {
          "PLACE" => CommandEnum.Place,
          "MOVE" => CommandEnum.Move,
          "LEFT" => CommandEnum.Left,
          "RIGHT" => CommandEnum.Right,
          "REPORT" => CommandEnum.Report,
          _ => null
      };

    private void PlaceCommand(string location)
    {
        if (ValidateLocation(location))
        {
            var chopLocation = location.Split(',');

            _position.Item1 = Convert.ToInt32(chopLocation[0]);
            _position.Item2 = Convert.ToInt32(chopLocation[1]);
            _direction = chopLocation[2] switch
            {
                "NORTH" => Direction.North,
                "SOUTH" => Direction.South,
                "EAST" => Direction.East,
                "WEST" => Direction.West,
                _ => null,
            };
        }
    }

    private void MoveCommand(Direction? direction)
    {
        if (direction == null) return;
        if (direction == Direction.North && _position.Item1 < 5) _position.Item1++;
        if (direction == Direction.East && _position.Item2 < 5) _position.Item2++;
        if (direction == Direction.South && _position.Item2 > 0) _position.Item2--;
        if (direction == Direction.West && _position.Item1 > 0) _position.Item1--;
    }

    private void LeftCommand(Direction? direction)
    {
        if (direction == null) return;
        switch (direction)
        {
            case Direction.North:
                _direction = Direction.West;
                break;
            case Direction.West:
                _direction = Direction.South;
                break;
            case Direction.South:
                _direction = Direction.East;
                break;
            case Direction.East:
                _direction = Direction.North;
                break;
        }
    }

    private void RightCommand(Direction? direction)
    {
        if (direction == null) return;
        switch (direction)
        {
            case Direction.North:
                _direction = Direction.East;
                break;
            case Direction.East:
                _direction = Direction.South;
                break;
            case Direction.South:
                _direction = Direction.West;
                break;
            case Direction.West:
                _direction = Direction.North;
                break;
        }
    }

    private void ReportCommand((int, int) position, Direction? direction)
    {
        if (direction != null) Console.WriteLine($"{position.Item1}, {position.Item2} {direction.Value.ToString().ToUpper()}");
        else Console.WriteLine("Place the toy first");
    }

    private bool ValidateLocation(string location)
    {
        string pattern = @"^\d+,\d+,(NORTH|SOUTH|EAST|WEST)$";
        return Regex.IsMatch(location, pattern);
    }

}

public enum CommandEnum
{
    Place,
    Move,
    Left,
    Right,
    Report
}

public enum Direction
{
    North,
    South,
    East,
    West
}