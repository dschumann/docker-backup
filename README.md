# docker-inspect
A quick and dirty solution to make backups of your docker containers. Uses the Docker API through Docker.DotNet.

![Screenshot of the application](https://raw.githubusercontent.com/bmsimons/docker-backup/master/docker-backup/artwork/screenshot-1.png)

## Getting started
This project comes with a Visual Studio solution file. Open this file in VS, retrieve all NuGet packages and build the project. This should result in a working binary.

## How does it work?
Fill in your endpoint in the endpoint text field (http://localhost:2375 for example), specify your basic credentials if needed, and click on the get containers button.