dotnet version -f .\RtfPipe\RtfPipe.csproj build `
  && dotnet build .\RtfPipe\RtfPipe.csproj -o -c Release `
  && dotnet pack .\RtfPipe\RtfPipe.csproj -o ".nupkgs" -c Release `
  && Copy-Item ".nupkgs\*.nupkg" $env:GAUSS_NUGET_FOLDER `
  && dotnet nuget push ".nupkgs\*.nupkg" --source gauss --api-key $env:GAUSS_NUGET_API_KEY --skip-duplicate `
  && Remove-Item ".nupkgs" -Recurse `
  && git push
