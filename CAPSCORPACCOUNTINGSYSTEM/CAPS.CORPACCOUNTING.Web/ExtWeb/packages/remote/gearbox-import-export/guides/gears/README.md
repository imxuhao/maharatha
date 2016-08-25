#initialise local package repo
sencha repository init --name "2gears sarl"

#in package dir:
sencha package build
cd ../../build/<packagename>/
sencha package add <packagename>.pkg
sencha package install <packagename>

Add the package name to your applications requires in app.json

build your application using
sencha app build

And we're all done.