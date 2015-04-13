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

namespace OctopusDump
{
    class Program
    {
        static int Main(string[] args)
        {
            Dictionary<string, string> parameters = null;
            string server, apiKey;

            try
            {
                Regex regex = new Regex(@"-{1,2}(?<switch>[^=:]+)([=:]|\s)(?<value>[^\s]+)(?=\s+[-\/]|$)");
                string commands = string.Join(" ", args);
                parameters = regex.Matches(commands).Cast<Match>()
                    .ToDictionary(m => m.Groups["switch"].Value.ToLowerInvariant(), m => m.Groups["value"].Value);

                server = parameters["server"];
                apiKey = parameters["apikey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Usage: octopusdump -server:http://myserver -apiKey:API-XXXXXXXXX");
                Console.WriteLine();
                Console.WriteLine(ex);
                return 1;
            }

            var endpoint = new OctopusServerEndpoint(server, apiKey);
            var repository = new OctopusRepository(endpoint);

            AutomapperConfiguration.CreateMap();
            var expanded = Mapper.Map<OctopusRepository, RepositoryDto>(repository);

            var dump = JsonConvert.SerializeObject (expanded, Formatting.Indented);

            Console.WriteLine(dump);
            return 0;
        }
    }
}
