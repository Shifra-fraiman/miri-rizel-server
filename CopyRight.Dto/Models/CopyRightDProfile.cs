using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using CopyRight.Dto.Models;

namespace CopyRight.Dto.Models
{
    public class CopyRightDProfile : Profile
    {
        public CopyRightDProfile()


        {
            CreateMap<Dal.Models.Project, Projects>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusNavigation))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => new Dto.Models.Customers { CustomerId = src.CustomerId,
                    FirstName = src.Customer.FirstName,
                    LastName = src.Customer.LastName,
                    Phone = src.Customer.Phone,
                    Email = src.Customer.Email,
                    BusinessName = src.Customer.BusinessName,
                    Source = src.Customer.Source,
                    Status = new Dto.Models.StatusCodeUser { Id = src.Customer.StatusNavigation.Id, Description = src.Customer.StatusNavigation.Description },
                    CreatedDate = src.Customer.CreatedDate,
                }))
                .ReverseMap()
                .ForMember(dest => dest.StatusNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
           .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.CustomerId));


            CreateMap<Dal.Models.Task, Tasks>()
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusNavigation))
               .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedToNavigation))
               .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
               .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.PriorityNavigation))
               .ReverseMap()
               .ForMember(dest => dest.StatusNavigation, opt => opt.Ignore())
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
               .ForMember(dest => dest.AssignedToNavigation, opt => opt.Ignore())
               .ForMember(dest => dest.Project, opt => opt.Ignore())
               .ForMember(dest => dest.PriorityNavigation, opt => opt.Ignore())
               .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Id))
               .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo.UserId))
               .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Project.ProjectId));

            //CreateMap<Dal.Models.User, User>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleNavigation)).ReverseMap();
            CreateMap<Dal.Models.User, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleNavigation))
            .ReverseMap()
            .ForMember(dest => dest.RoleNavigation, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Id));
                 
            CreateMap<Dal.Models.Lead, Leads>().ReverseMap();
            CreateMap<Dal.Models.Document, Documents>().ReverseMap();

            CreateMap<Dal.Models.Customer, Customers>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusNavigation))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
                .ForMember(dest => dest.StatusNavigation, opt => opt.Ignore());



            CreateMap<Dal.Models.RoleCode, RoleCode>().ReverseMap();
            CreateMap<Dal.Models.Communication, Communications>()
                            .ForMember(dest => dest.RelatedTo, opt => opt.MapFrom(src => src.RelatedToNavigation))
                            .ForMember(dest => dest.name, opt => opt.Ignore())
                            .ReverseMap()
                            .ForMember(dest => dest.RelatedToNavigation, opt => opt.Ignore()); CreateMap<Dal.Models.StatusCodeProject, StatusCodeProject>().ReverseMap();
            CreateMap<Dal.Models.PriorityCode, PriorityCode>().ReverseMap();
            CreateMap<Dal.Models.StatusCodeUser, StatusCodeUser>().ReverseMap();
            CreateMap<Dal.Models.RelatedToCode, relatedToCode>().ReverseMap();
            

        }
    }
}
