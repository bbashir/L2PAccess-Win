Welcome at the **L<sup>2</sup>PAccess** project!
===================


The aim of this project to support the development of **Windows Phone** and other Windows third-party apps for **RWTH Aachen**'s new **L<sup>2</sup>P** teaching and learning platform. This project will provide an easy way to call methods on the L<sup>2</sup>P API without the need to worry about authentication. **Authentication**, which means everything from getting user code, verifying it, fetching and refreshing access tokens, will be handled automatically by the provided L<sup>2</sup>P client.

----------


Usage
-------------
### Add to your project
As the project is only available in source form, you have to add the whole project to your solution. Do not worry, that's simpler than you think, just follow the steps below!
#### Step 1 - [**<i class="icon-download"></i>Download**](https://github.com/gavronek/L2PAccess-Win/archive/master.zip)
Click on the link above or below, **download** the project as a Zip and **extract** it somewhere on your computer.
[**<i class="icon-download"></i> Download as Zip**](https://github.com/gavronek/L2PAccess-Win/archive/master.zip)
#### Step 2 - Include in solution
Ok, now you have to **copy the whole project folder in the working directory of your app**. Assuming DemoWPApp is a project created by Visual Studio 2013 Pivot App template it would look like something like this:

![Copy from Zip to App](http://i.imgur.com/qM5xXXv.png)

Now **open Visual Studio** and on the **Solution Explorer** right click on your solution and press **Add > Existing project...** then navigate to your project folder and **add L2PAccess.csproj**

![enter image description here](http://i.imgur.com/BvsNg2r.png)

#### Step 3 - Reference it
The last step you need to do is to add a reference to this newly added project from your main project. **Right click on your Windows Phone project** and press **Add > Reference** then select the L2PAccess project:

![enter image description here](http://i.imgur.com/mVPkavZ.png)

### How to use
The goal was to create something very simple to use, so you can concentrate more on actually what you want to do.
#### Configure
To instantiate an ***L2PClient*** you will need a ***OAuthConfig*** object. There are two ways to do this:

 - Using an ***RwthConfig*** object with your **ClientID**, like this: 
  ```csharp
  var config = RwthConfig.Create("YOUR_CLIENT_ID");
  ```
 
 - Using the ***OAuthConfig*** directly, like I did in the ***RwthConfig***:
```csharp
  new OAuthConfig
            {
                ClientId = clientId,
                OauthServerUrl = "https://oauth.campus.rwth-aachen.de/",
                InjectQueryParam = true,
                Scopes = new List<string>
                {
                    "l2p.rwth", 
                    "campus.rwth",
                    "l2p2013.rwth"
                }
            };
```
#### Your first call
Oh, this is simple. Just create an ***L2PClient*** instance using the config object we just did and call the L<sup>2</sup>P method of choice. Voilà!
```csharp
l2PClient = new L2PClient(config);
var viewAllCourseInfo = await l2PClient.ViewAllCourseInfo();
```
#### Enjoy
Really, just only this step left :) Hope you will have to get your hands dirty somewhere else in your project. Good luck!

----------


Extend
-------------------
### Adding L<sup>2</sup>P methods
The list of available L<sup>2</sup>P methods on the client is very limited as this was not the main goal of the project. But the way to extend the list is very easy only two things is needed:

#### Step 1 - Add to the API interface
Just go to the ***IL2PApi*** interface and add a line like this:

```csharp
        [Get("/viewCourseInfo")]
        Task<L2PResponse<Course>> ViewCourseInfo([AliasAs("cid")] string courseId);
```

The annotation both for the method and the parameter uses the [**<i class="icon-link"></i> REFIT**](https://github.com/paulcbetts/refit) project, so it is ***highly recommended*** to take a look at that.
As most of the L<sup>2</sup>P methods returns with the same outer structure, a generic ***L2PResponse*** class is provided. If you need to add your own model classes (like Wiki, Announcement, etc.) i recommend to store it under the **API.Model folder**.

> **Note:** You don't need an access_token parameter on your L<sup>2</sup>P method call, it will be injected autmagically

#### Step 2 - Extend the proxy client
----------
Now you have added the method to the [Refit](https://github.com/paulcbetts/refit) interface, you need to **add a new method** in the ***L2PClient***, which corresponds to the one on your interface:
```csharp
        public async Task<L2PResponse<Course>> ViewCourseInfo(string courseId)
        {
            var l2PResponse = await client.ViewCourseInfo(courseId);
            return l2PResponse;
        }
```

Why do you need this? Because ***L2PClient*** makes sure that you don't need the access_token parameter on your method and that it will be injected at runtime.
Problems
-------------------
If you found some problems with the library or have some ideas what to develop, feel free to leave an issue here: [**Issues page**](https://github.com/gavronek/L2PAccess-Win/issues/)

Leave one even if you just don't understand something and need somewhere more detailed documentation.

> **Note:** This is an open source project, I work on it in my barely existing freetime, so feel free to actually fix things yourself if you have to.

----------
