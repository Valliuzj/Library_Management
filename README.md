# Library Management System

This is a Library Management System built with C# and .NET.

## Prerequisites
- .NET SDK
- Entity Framework Core Tools
- Google Developer Account for API keys
- Twitter Developer Account for API keys

## Setting Up Google Authentication
1. Register your application on the [Google Developer Console](https://console.developers.google.com/) to obtain your Client ID and Client Secret.
2. Use the .NET Secret Manager to store your Google API keys securely:
    ```sh
    dotnet user-secrets set "Authentication:Google:ClientId" "your-google-client-id"
    dotnet user-secrets set "Authentication:Google:ClientSecret" "your-google-client-secret"
    ```

## Setting Up Twitter Authentication
1. Register your application on the [Twitter Developer Portal](https://developer.twitter.com/en/apps) to obtain your Consumer Key and Consumer Secret.
2. Use the .NET Secret Manager to store your Twitter API keys securely:
    ```sh
    dotnet user-secrets set "Authentication:Twitter:ConsumerKey" "your-twitter-consumer-key"
    dotnet user-secrets set "Authentication:Twitter:ConsumerSecret" "your-twitter-consumer-secret"
    ```

## Run the Following Commands to Start the Project

1. Navigate to the LibraryManagement folder:
    ```sh
    cd LibraryManagement
    ```

2. If you are setting up a new database, run the following commands:
    ```sh
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

3. Run the project:
    ```sh
    dotnet run
    ```

4. Open [http://localhost:5170](http://localhost:5170) or any port specified in the terminal.

5. Navigate around the website.