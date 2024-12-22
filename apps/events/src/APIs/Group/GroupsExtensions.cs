using Events.APIs.Dtos;
using Events.Infrastructure.Models;

namespace Events.APIs.Extensions;

public static class GroupsExtensions
{
    public static Group ToDto(this GroupDbModel model)
    {
        return new Group
        {
            CreatedAt = model.CreatedAt,
            Events = model.Events?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static GroupDbModel ToModel(
        this GroupUpdateInput updateDto,
        GroupWhereUniqueInput uniqueId
    )
    {
        var group = new GroupDbModel { Id = uniqueId.Id, Name = updateDto.Name };

        if (updateDto.CreatedAt != null)
        {
            group.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            group.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return group;
    }
}
