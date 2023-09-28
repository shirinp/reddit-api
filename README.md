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

# Usage
API Exposes below 4 endpoints:
**1. /ReadRedditPostsAsStream**
This end-point is the starting point of running/testing the application, it starts listening for no. of subreddit's you decided to listen on. I tried with **"funny", "askreddit", "memes", "worldnews"** for my testing. by default it download "New Posts" (100) and keep adding new ones being posted in real time. This is a void Task which will trigger Reddit Posts as stream and keep listening until you kill the API. It will store consumed posts in in-memory data store and do console log to post the performance and also creates a log file where we can see the same details for troubleshooting / verification purposes.
![image](https://github.com/shirinp/reddit-api/assets/2934881/aa31eb56-b612-4c54-87b1-d55a1bc70ed8)

**2. **/GetPostsByUpvoteAsync****
This end-point returns "Posts With Most Up votes" within a given subreddit, you can vary by top 5, 10, 15 etc.. default is 5
![image](https://github.com/shirinp/reddit-api/assets/2934881/2e7be830-fd49-4836-98db-e1ce87f23c1c)


**3. /UsersWithMostPosts**
This end-point returns "Users With most posts" within a given subreddit, you can vary by top 5, 10, 15 etc.. default is 5
![image](https://github.com/shirinp/reddit-api/assets/2934881/9d289956-cb72-4690-9035-3f2ac2281830)


**4. /GetTotalPostCount**
This end-point returns "Total posts downloaded within subreddit so far."
![image](https://github.com/shirinp/reddit-api/assets/2934881/71805f40-8378-4bb4-9946-3237fc9b9a9f)



![image](https://github.com/shirinp/reddit-api/assets/2934881/d485c726-170b-4522-8d82-b4a6b56502e8)



