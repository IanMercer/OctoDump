# OctoDump
A dump-to-JSON utility for [Octopus Deploy](http://octopusdeploy.com)

# Usage
    octopusdump -server:http://myserver -apiKey:API-XXXXXXXXX > octo.txt

or

    octopusdump -server:http://myserver -apiKey:API-XXXXXXXXX -directory:./configXXX

# Description
This utility dumps most of the configuration data from an Octopus Deploy Server into a JSON file or a directory
containing separate JSON files for each feature. It avoids any
collections that are logs of activity and any properties that are status related not configuration related.

Once in a JSON file you can easily check it into source control. If you rerun the utility after each change
and commit those changes to revision control you will have a complete history of changes made and you can use
diff to see what changed between any pair of revisions.

This is not a sanctioned tool, and it's not a substitute for the Export and Import capabilities provided already
by Octopus Deploy's command line utilities. I made it for my own purposes but am providing it here so that others
can use it and hopefully improve it.

This utility uses Octopus Deploy's REST API and the Octopus.Client .NET wrapper for it. It also uses Automapper 
to map each of the Octopus Resource types to a DTO type selectively excluding any fields that are not relevant
as part of the configuration. And, of course, it also uses JSON.NET for serialization.

# Contributions
Bug fixes welcome, send a PR.

# License
This code is MIT licensed, but please check any of the Nuget packages it utilizes for possible additional
licensing requirements.