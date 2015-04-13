# OctoDump
A dump-to-JSON utility for [Octopus Deploy](http://octopusdeploy.com)

# Usage
    octopusdump -server:http://myserver -apiKey:API-XXXXXXXXX > octo.txt

# Description
I needed a way to examine all of the configuration data in Octopus Deploy (i.e. excluding any logging information) and
a way to be able to diff any changes I made to understand precisely what had changed.

This quick command line utility does that, exporting all (actually "most" as I'm still working on it) of the settings
to the console output. From there you can pipe it to a file and add that file to your version control system. 

This utility uses Octopus Deploy's REST API and the Octopus.Client .NET wrapper for it. It also uses Automapper 
to map each of the Octopus Resource types to a DTO type selectively excluding any fields that are not relevant
as part of the configuration.  Various 'state' properties for example are excluded as are any
objects related to logging deployments.

This is just an initial commit for today, more to follow.

# Contributions
Bug fixes welcome, send a PR.

# License
This code is MIT licensed, but please check any of the Nuget packages it utilizes for possible additional
licensing requirements.