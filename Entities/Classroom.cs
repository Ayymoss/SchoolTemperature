using System.ComponentModel.DataAnnotations;

namespace SchoolRoomTemperature.Entities;

/// <summary>
/// Defines classroom
/// </summary>
public class Classroom
{
    [Key] public int Id { get; set; }
    public string RoomNumber { get; set; }
    public float Celsius { get; set; }
    public DateTime DateTime { get; set; }

    public float Fahrenheit => 32 + Celsius / 0.5556f;
}
