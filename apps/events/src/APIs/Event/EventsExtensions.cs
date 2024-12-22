using Events.APIs.Dtos;
using Events.Infrastructure.Models;

namespace Events.APIs.Extensions;

public static class EventsExtensions
{
    public static Event ToDto(this EventDbModel model)
    {
        return new Event
        {
            CreatedAt = model.CreatedAt,
            Group = model.GroupId,
            Id = model.Id,
            Message = model.Message,
            UpdatedAt = model.UpdatedAt,

        };
    }

    public static EventDbModel ToModel(this EventUpdateInput updateDto, EventWhereUniqueInput uniqueId)
    {
        var event = new EventDbModel { 
               Id = uniqueId.Id,
Message = updateDto.Message
};

     if(updateDto.CreatedAt != null) {
     event.CreatedAt = updateDto.CreatedAt.Value;
}
if(updateDto.Group != null) {
     event.GroupId = updateDto.Group;
}
if(updateDto.UpdatedAt != null) {
     event.UpdatedAt = updateDto.UpdatedAt.Value;
}

    return event; }

}
