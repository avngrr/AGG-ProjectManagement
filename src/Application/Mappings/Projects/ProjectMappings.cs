using Application.Projects.Commands;
using Application.Projects.Responses;
using AutoMapper;
using Domain.Entities.Projects;

namespace Application.Mappings.Projects;

public class ProjectMappings : Profile
{
    public ProjectMappings()
    {
        CreateMap<Ticket, TicketResponse>();
        CreateMap<Project, ProjectResponse>()
            .ForMember(dest
                => dest.Tickets, opt =>
                opt.MapFrom(src => src.Tickets)).ReverseMap();
        CreateMap<AddEditProjectCommand, Project>();
        CreateMap<AddEditTicketCommand, Ticket>();
    }
}