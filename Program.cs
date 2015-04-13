using Newtonsoft.Json;
using Octopus.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Client.Model;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Mappers;
using AutoMapper.QueryableExtensions;
using Octopus.Client.Repositories;
using System.Text.RegularExpressions;
using System.IO;

namespace OctopusDump
{
    class Program
    {
        static int Main(string[] args)
        {
            Dictionary<string, string> parameters = null;
            string server, apiKey, directory;

            try
            {
                Regex regex = new Regex(@"-{1,2}(?<switch>[^=:]+)([=:]|\s)(?<value>[^\s]+)(?=\s+[-\/]|$)");
                string commands = string.Join(" ", args);
                parameters = regex.Matches(commands).Cast<Match>()
                    .ToDictionary(m => m.Groups["switch"].Value.ToLowerInvariant(), m => m.Groups["value"].Value);

                server = parameters["server"];
                apiKey = parameters["apikey"];
                parameters.TryGetValue("directory", out directory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Usage:");
                Console.WriteLine(@"This utility dump the configuration data from an Octopus Deploy Server.");
                Console.WriteLine(@"You can then check that data into a revision control system.");
                Console.WriteLine();
                Console.WriteLine(@"  octopusdump -server:http://myserver -apiKey:API-XXXXXXXXX");
                Console.WriteLine();
                Console.WriteLine(@"outputs to the console which you can then pipe to a file");
                Console.WriteLine();
                Console.WriteLine(@"  or, to dump to individual files in a directory structure:");
                Console.WriteLine();
                Console.WriteLine(@" octopusdump -server:http://myserver -apiKey:API-XXXXXXXXX -directory:./configXXX");
                Console.WriteLine();
                Console.WriteLine(ex);
                return 1;
            }

            var endpoint = new OctopusServerEndpoint(server, apiKey);
            var repository = new OctopusRepository(endpoint);

            AutomapperConfiguration.CreateMap();
            var expanded = Mapper.Map<OctopusRepository, RepositoryDto>(repository);

            if (string.IsNullOrWhiteSpace(directory))
            {
                var dump = JsonConvert.SerializeObject(expanded, Formatting.Indented);
                Console.WriteLine(dump);
            }
            else
            {
                WriteFile(directory, "Teams", expanded.Teams);
                WriteFile(directory, "Users", expanded.Users);
                WriteFile(directory, "Certificates", expanded.Certificates);
                WriteFile(directory, "Environments", expanded.Environments);
                WriteFile(directory, "Feeds", expanded.Feeds);
                WriteFile(directory, "LibraryVariableSets", expanded.LibraryVariableSets);
                WriteFile(directory, "Lifecycles", expanded.LifeCycles);
                WriteFile(directory, "Machines", expanded.Machines);
                WriteFile(directory, "ProjectGroups", expanded.ProjectGroups);
                WriteFile(directory, "Projects", expanded.Projects);
                WriteFile(directory, "UserRoles", expanded.UserRoles);
                WriteFile(directory, "Releases", expanded.Releases);

                // Remaining special cases go in a single JSON file:
                WriteFile(directory, ".", "repository.json", new { expanded.MachineRoles, expanded.VariableSets });

//        public LibraryVariableSetDto[] LibraryVariableSets { get; set; }
//        public LifeCycleDto[] LifeCycles { get; set; }
//        public string[] MachineRoles { get; set; }
//        public MachineDto[] Machines { get; set; }
//        public ProjectGroupDto[] ProjectGroups { get; set; }
//        public ProjectDto[] Projects { get; set; }
////            public RetentionPolicyDto[] RetentionPolicies { get; set; }
//        public UserRoleDto[] UserRoles { get; set; }
//        public ReleaseDto[] Releases { get; set; }
//        public VariableSetDtos[] VariableSets { get; set; }

            }
            return 0;
        }

        private static void WriteFile (string directory, string subdirectory, IId[] objects)
        {
            foreach (var obj in objects)
            {
                WriteFile(directory, subdirectory, obj.Id + ".json", obj);
            }
        }

        private static void WriteFile(string directory, string subdirectory, string filename, object obj)
        {
            string directoryPath = Path.Combine(Environment.CurrentDirectory, directory, subdirectory);
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Creating directory  " + directoryPath);
                Directory.CreateDirectory(directoryPath);
            }
            string path = Path.Combine(directoryPath, filename);
            var dump = JsonConvert.SerializeObject(obj, Formatting.Indented);

            if (File.Exists(path) && File.ReadAllText(path) == dump)
            {
                Console.WriteLine("Skipping " + path + " unchanged");
            }
            else
            {
                Console.WriteLine("Writing file  " + path);
                File.WriteAllText(path, dump);
            }
        }

    }
}
