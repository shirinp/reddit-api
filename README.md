# reddit-api
test reddit api

# Setup
Application uses Reddit "Script Application Type" for consuming reddit posts and requires ClientID, ClientSecret, UserName, Password and redirectURI to Generate Access Token and eventually calling the API. Ideally we could have put the secrets in the Environment Variables of computer, but to keep it simple and test it easily I have kept the secrets in the appsettings.json file. 

Please replace below setting in appsettings.json file to run the application.
FilePath, RedditClientID, RedditClientSecret, RedditRedirectURI, TestUserName, TestUserPassword

# Project Details
There are 2 projects within the solution.
1. RedditSharp.API which is .NET 6 Web API
2. RedditSharp.API.Test which is xUnit project to Unit testing Controller and Business logic layer

I have created various layers of a the api as a folders within RedditSharp.API to keep it simple and easy to read, ideally many folders will become individual class libraries in a real life project. 
We can certainly secure our API with jWT Token auth or any other mechanism.

I have utilized Redditsharp library for consuming Reddit posts as a stream, after studying it in details, I found that it already fulfills the needs of throttling (Reqeusts per second/ per user) requirement based of the Response headers. 

# Usage
API Exposes below 4 endpoints:

**1. /ReadRedditPostsAsStream**
This end-point is the starting point of running/testing the application, it starts listening for no. of subreddit's you decided to listen on. I tried with **"funny", "askreddit", "memes", "worldnews"** for my testing. by default it download "New Posts" (100) and keep adding new ones being posted in real time. This is a void Task which will trigger Reddit Posts as stream and keep listening until you kill the API. It will store consumed posts in in-memory data store and do console log to post the performance and also creates a log file where we can see the same details for troubleshooting / verification purposes.
![image](https://github.com/shirinp/reddit-api/assets/2934881/afe5b2f9-cda0-4ed7-a434-96bb0baf7627)


**2. **/api/v1/Reddit/posts/{subRedditName}****
This end-point returns "Posts With Most Up votes" within a given subreddit, you can vary by top 5, 10, 15 etc.. default is 5
![image](https://github.com/shirinp/reddit-api/assets/2934881/713e70c9-0a89-4d1f-b305-1a9bfeb5a87b)



**3. /api/v1/Reddit/users/{subRedditName}**
This end-point returns "Users With most posts" within a given subreddit, you can vary by top 5, 10, 15 etc.. default is 5
![image](https://github.com/shirinp/reddit-api/assets/2934881/3f77385c-e506-4cac-ad8f-f4809a8a5dc4)



**4. /api/v1/Reddit/posts/total/{subRedditName}**
This end-point returns "Total posts downloaded within subreddit so far."
![image](https://github.com/shirinp/reddit-api/assets/2934881/21619b64-e3a3-44c9-a591-e83b8d175f02)




![image](https://github.com/shirinp/reddit-api/assets/2934881/d485c726-170b-4522-8d82-b4a6b56502e8)



