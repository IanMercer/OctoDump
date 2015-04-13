using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OctopusDump
{
    // DTO classes for mapping the useful configuration values out of Octopus Deploy and into something
    // that can be cleanly serialized to JSON (or other formats)

    internal class RepositoryDto
    {
        public TeamDto[] Teams { get; set; }
        public UserDto[] Users { get; set; }
        public CertificateDto[] Certificates { get; set; }
        public EnvironmentDto[] Environments { get; set; }
        public FeedDto[] Feeds { get; set; }
        public LibraryVariableSetDto[] LibraryVariableSets { get; set; }
        public LifeCycleDto[] LifeCycles { get; set; }
        public string[] MachineRoles { get; set; }
        public MachineDto[] Machines { get; set; }
        public ProjectGroupDto[] ProjectGroups { get; set; }
        public ProjectDto[] Projects { get; set; }
//            public RetentionPolicyDto[] RetentionPolicies { get; set; }
        public UserRoleDto[] UserRoles { get; set; }
        public ReleaseDto[] Releases { get; set; }
        public VariableSetDtos[] VariableSets { get; set; }
    }

    internal class CertificateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Thumbprint { get; set; }
    }

    internal class EnvironmentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool UseGuidedFailure { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }              // Why is this not a UserId?
    }

    internal class FeedDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FeedUri { get; set; }
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }

    internal class LibraryVariableSetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VariableSetId { get; set; }
        public int ContentType { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }              // Why is this not a UserId?
    }

    internal class LifeCycleDto
    {
        public string Id { get; set; }
        public PhaseDto[] Phases { get; set; }
    }

    internal class MachineDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Thumbprint { get; set; }
        public string Uri { get; set; }
        public bool IsDisabled { get; set; }
        public string[] EnvironmentIds { get; set; }
        public string[] Roles { get; set; }
        public string Squid { get; set; }
        public int CommunicationStyle { get; set; }
// not part of configuration, so not included for now
//            public int Status { get; set; }
//            public string StatusSummary { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }              // Why is this not a UserId?
    }

    internal class ProjectGroupDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] EnvironmentIds { get; set; }
        public string RetentionPolicyId { get; set; }
    }

    internal class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string VariableSetId { get; set; }
        public string DeploymentProcessId { get; set; }

        public DeploymentProcessDto DeploymentProcess { get; set; }     // inserted manually

        public string[] IncludedLibraryVariableSetIds { get; set; }
        public bool DefaultToSkipIfAlreadyInstalled { get; set; }
        public VersioningStrategyDto VersioningStrategy { get; set; }
        public ReleaseCreationStrategyDto ReleaseCreationStrategy { get; set; }

        public bool IsDisabled { get; set; }

        public string ProjectGroupId { get; set; }
        public string LifeCycleId { get; set; }
        public bool AutoCreateRelease { get; set; }

        public DateTimeOffset LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }              // Why is this not a UserId?
    }

    internal class ReleaseDto
    {
        public string Id { get; set; }
        public DateTimeOffset Assembled { get; set; }
        public string ReleaseNotes { get; set; }
        public string ProjectId { get; set; }
        public string ProjectVariableSetSnapshotId { get; set; }
        public string[] LibraryVariableSetSnapshotIds { get; set; }
        public string ProjectDeploymentProcessSnapshotId { get; set; }
        public SelectedPackageDto[] SelectedPackages { get; set; }
        public string Version { get; set; }

        public DateTimeOffset LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }              // Why is this not a UserId?
    }

    internal class SelectedPackageDto
    {
        public string StepName { get; set; }
        public string Version { get; set; }
    }

    internal class RetentionPolicyDto
    {
    }

    internal class VersioningStrategyDto
    {
        public string DonorPackageStepId { get; set; }
        public string Template { get; set; }                // is this a string??
    }

    internal class ReleaseCreationStrategyDto
    {
        public string ReleaseCreationPackageStepId { get; set; }
    }

    internal class PhaseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        // TODO: Other Phase information here
    }

    internal class TeamDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] MemberUserIds { get; set; }
        public string[] ExternalSecurityGroups { get; set; }
        public string[] UserRoleIds { get; set; }
        public string[] ProjectIds { get; set; }
        public string[] EnvironmentIds { get; set; }
        public bool CanBeDeleted { get; set; }
        public bool CanBeRenamed { get; set; }
        public bool CanChangeRoles { get; set; }
        public bool CanChangeMembers { get; set; }
    }

    internal class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public bool IsService { get; set; }
        public string EmailAddress { get; set; }
        public bool IsRequestor { get; set; }
    }

    internal class UserRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SupportedRestrictions { get; set; }
        public string[] PermissionDescriptions { get; set; }
        public int[] GrantedPermissions { get; set; }
        public bool CanBeDeleted { get; set; }
    }

    internal class VariableSetDtos
    {
        public string OwnerId { get; set; }
        public VariableScopeValuesDto ScopeValues { get; set; }
        public IList<VariableResourceDto> Variables { get; set; }
        public int Version { get; set; }
    }

    internal class VariableScopeValuesDto
    {
        public List<ReferenceDataItemDto> Actions { get; set; }
        public List<ReferenceDataItemDto> Environments { get; set; }
        public List<ReferenceDataItemDto> Machines { get; set; }
        public List<ReferenceDataItemDto> Roles { get; set; }
    }

    internal class ReferenceDataItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    internal class VariableResourceDto
    {
        public string Id { get; set; }
        public bool IsEditable { get; set; }
        public bool IsSensitive { get; set; }
        public string Name { get; set; }
        public VariablePromptOptionsDto Prompt { get; set; }
        public ScopeSpecificationDto Scope { get; set; }
        public string Value { get; set; }
    }

    internal class ScopeSpecificationDto : Dictionary<ScopeFieldDto, ScopeValueDto>
    {
    }

    public enum ScopeFieldDto
    {
        Project = 0,
        Environment = 1,
        Machine = 2,
        Role = 3,
        TargetRole = 4,
        Action = 5,
        User = 6,
        Private = 7,
    }

    internal class ScopeValueDto : HashSet<string> // A Hashset (inherits) : Octopus.Platform.Model.ReferenceCollection
    {
    }

    internal class VariablePromptOptionsDto
    {
        public string Description { get; set; }
        public string Label { get; set; }
        public bool Required { get; set; }
    }

    internal class DeploymentProcessDto
    {
        public string LastSnapshotId { get; set; }
        public string ProjectId { get; set; }
        public IList<DeploymentStepDto> Steps { get; set; }
        public int Version { get; set; }
    }

    internal enum DeploymentStepConditionDto
    {
        Success = 0,
        Failure = 1,
        Always = 2,
    }

    internal class DeploymentActionDto
    {
        public string ActionType { get; set; }
        public string[] Environments { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public IDictionary<string, string> SensitiveProperties { get; set; }
    }

    internal class DeploymentStepDto
    {
        public List<DeploymentActionDto> Actions { get; set; }
        public DeploymentStepConditionDto Condition { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public bool RequiresPackagesToBeAcquired { get; set; }
        public IDictionary<string, string> SensitiveProperties { get; set; }
        public DeploymentStepStartTriggerDto StartTrigger { get; set; }
    }

    internal enum DeploymentStepStartTriggerDto
    {
        StartAfterPrevious = 0,
        StartWithPrevious = 1,
    }

}
