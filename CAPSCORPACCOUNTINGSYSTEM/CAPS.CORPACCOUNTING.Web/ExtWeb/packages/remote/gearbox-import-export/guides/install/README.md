#Installation Guide
This Gear is a Sencha package, which makes it easy to install. Just 
follow these steps and you'll be up and running in no time.

###Prerequisites
We'll assume you have already set up Sencha Cmd. If not, please refer to Sencha's
own [excellent guide](http://docs.sencha.com/cmd/index.html) for how to do this. 

##Step 1: Set up a Local repository
Sencha needs a local repository to store its packages, so let's set one up! 

Fire up your favorite terminal emulator and issue the following command. Of course,
you can change the name of your local repository to anything you like.

```bash
sencha repository init --name "myAwesomeRepo"
```

Good. Now that you're all set, it's time to add the Gear to your repository. 

##Step 2: Add the Gear
Switch back to your console and find your .pkg file.

Issue the following command to add it to your local repository:

```bash
sencha package add package.pkg
```

Alright! Now you're ready to use the Gear in your own projects.

##Step 3: Use the Gear
Now, the above may seem like a lot of hassle to just install your Gear, but you
only need to add this Gear once. From now on, all you need to do to use it in 
your projects is add it to your requires in your applications app.json:

```javascript
requires: [
	"gearbox-import-export"
]
```

##Step 4: Update your application
To tell Sencha Cmd to fetch packages and use them in your application, tell it to
refresh the required packages like so:

```bash
sencha app refresh --packages
```

That's it! Sencha Cmd will take care of everything else for you.

##Step 5: Feel the power
Check out the [examples](#!/example) and the other [guides](#!/guide) on how to 
quickly get started with this Gear and go code awesomeness.