using AutoMapper;
using Octopus.Client;
using Octopus.Client.Model;
using Octopus.Platform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusDump
{
    public class AutomapperConfiguration
    {
        private class FindAllResolver<U, T> : ValueResolver<U, List<T>>
        {
            private Func<U, List<T>> memberSelection;

            public FindAllResolver(Func<U, List<T>> memberSelection)
            {
                this.memberSelection = memberSelection;
            }
            protected override List<T> ResolveCore(U source)
            {
                return this.memberSelection(source);
            }
        }

        private static FindAllResolver<OctopusRepository, T> FindAll<T>(Func<OctopusRepository, List<T>> selector)
        {
            return new FindAllResolver<OctopusRepository, T>(selector);
        }

        public static void CreateMap()
        {
            Mapper.CreateMap<TeamResource, TeamDto>();
            Mapper.CreateMap<UserResource, UserDto>();
            Mapper.CreateMap<CertificateResource, CertificateDto>();
            Mapper.CreateMap<EnvironmentResource, EnvironmentDto>();
            Mapper.CreateMap<FeedResource, FeedDto>();
            Mapper.CreateMap<LibraryVariableSetResource, LibraryVariableSetDto>();
            Mapper.CreateMap<LifecycleResource, LifeCycleDto>();
            Mapper.CreateMap<MachineResource, MachineDto>();
            Mapper.CreateMap<ProjectGroupResource, ProjectGroupDto>();
            Mapper.CreateMap<ProjectResource, ProjectDto>();
            Mapper.CreateMap<UserRoleResource, UserRoleDto>();
            Mapper.CreateMap<PhaseResource, PhaseDto>();
            Mapper.CreateMap<VersioningStrategyResource, VersioningStrategyDto>();
            Mapper.CreateMap<ReleaseCreationStrategyResource, ReleaseCreationStrategyDto>();
            Mapper.CreateMap<VariableSetResource, VariableSetDtos>();
            Mapper.CreateMap<VariableResource, VariableResourceDto>();
            Mapper.CreateMap<ReleaseResource, ReleaseDto>();
            Mapper.CreateMap<SelectedPackage, SelectedPackageDto>();
            Mapper.CreateMap<ReferenceDataItem, ReferenceDataItemDto>();
            Mapper.CreateMap<VariableScopeValues, VariableScopeValuesDto>();
            Mapper.CreateMap<VariablePromptOptions, VariablePromptOptionsDto>();
            Mapper.CreateMap<ScopeSpecification, ScopeSpecificationDto>();
            Mapper.CreateMap<ScopeField, ScopeFieldDto>();
            Mapper.CreateMap<ScopeValue, ScopeValueDto>();




            Mapper.CreateMap<OctopusRepository, RepositoryDto>()
                .ForMember(x => x.Certificates, opt => opt.ResolveUsing(FindAll(x => x.Certificates.FindAll())))
                .ForMember(x => x.Environments, opt => opt.ResolveUsing(FindAll(x => x.Environments.FindAll())))
                .ForMember(x => x.Feeds, opt => opt.ResolveUsing(FindAll(x => x.Feeds.FindAll())))
                .ForMember(x => x.LibraryVariableSets, opt => opt.ResolveUsing(FindAll(x => x.LibraryVariableSets.FindAll())))

                .ForMember(x => x.VariableSets, opt => opt.ResolveUsing(FindAll(x => x.LibraryVariableSets.FindAll()
                    .Select(p => x.VariableSets.Get(p.VariableSetId)).ToList())))

                .ForMember(x => x.LifeCycles, opt => opt.ResolveUsing(FindAll(x => x.Lifecycles.FindAll())))
                .ForMember(x => x.Machines, opt => opt.ResolveUsing(FindAll(x => x.Machines.FindAll())))

                .ForMember(x => x.ProjectGroups, opt => opt.ResolveUsing(FindAll(x => x.ProjectGroups.FindAll())))
                .ForMember(x => x.Projects, opt => opt.ResolveUsing(FindAll(x => x.Projects.FindAll())))
                .ForMember(x => x.Releases, opt => opt.ResolveUsing(FindAll(x => x.Releases.FindAll())))

                .ForMember(x => x.UserRoles, opt => opt.ResolveUsing(FindAll(x => x.UserRoles.FindAll())))
                .ForMember(x => x.MachineRoles, opt => opt.ResolveUsing(y => y.MachineRoles.GetAllRoleNames()))     // odd discrepancy in naming
                .ForMember(x => x.Users, opt => opt.ResolveUsing(FindAll(x => x.Users.FindAll())))
                .ForMember(x => x.Teams, opt => opt.ResolveUsing(FindAll(x => x.Teams.FindAll())))

                //.ForMember(x => x.RetentionPolicies, opt => opt.ResolveUsing(FindAll(x => x.RetentionPolicies)))
                ;
                Mapper.AssertConfigurationIsValid();
        }
    }
}