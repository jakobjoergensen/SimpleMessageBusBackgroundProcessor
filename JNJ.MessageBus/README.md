### Build a new package version
Remember to update the project file with new version number. Then:

	dotnet pack -c Release

### Push package to local package store

	dotnet nuget push ./bin/Release/JNJ.MessageBus.{VersionNumber}.nupkg -s LocalNuget