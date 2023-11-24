using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor;
using SchoolRoomTemperature.Context;
using SchoolRoomTemperature.Entities;

namespace SchoolRoomTemperature.Components.Pages;

public partial class Home
{
    [Inject] private DataContext Context { get; set; }

    private IEnumerable<Classroom> _serverTable;
    private RadzenDataGrid<Classroom> _dataGrid;

    private InputModel _classroom = new();
    private bool _messageVisible;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _serverTable = Context.Classrooms;
    }

    private async Task AddClassroom()
    {
        if (!ValidateTemperature(_classroom.Celsius)) return;

        var classroom = await Context.Classrooms.FirstOrDefaultAsync(x => x.RoomNumber == _classroom.RoomNumber);
        if (classroom is not null)
        {
            classroom.Celsius = float.Parse(_classroom.Celsius);
            classroom.DateTime = DateTime.Now;
            Context.Classrooms.Update(classroom);
        }
        else
        {
            classroom = new Classroom
            {
                RoomNumber = _classroom.RoomNumber,
                Celsius = float.Parse(_classroom.Celsius),
                DateTime = DateTime.Now
            };
            Context.Classrooms.Add(classroom);
        }

        await Context.SaveChangesAsync();
        _classroom = new InputModel();
        _messageVisible = true;
        await _dataGrid.Reload();
    }


    private bool ValidateTemperature(object temperature)
    {
        var isValid = float.TryParse(Convert.ToString(temperature), out var result);
        _messageVisible = false;
        return isValid && result is >= -50 and <= 50;
    }

    private class InputModel
    {
        public string RoomNumber { get; set; }
        public string Celsius { get; set; }
    }
}
