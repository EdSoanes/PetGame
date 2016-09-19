# PetGame
Pet Game Server

##Security
* Security is a no-brainer. Not added to keep demo of API simple. Would likely implement OAuth2/OpenID connect
* I would NEVER normally include credentials in the URL! Just to make the demo easier.

##Tests
* Install the mspec test adapter from https://visualstudiogallery.msdn.microsoft.com/4abcb54b-53b5-4c44-877f-0397556c5c44
* Not complete code coverage but demonstrates that everything but DB repositories testable
 

##API Calls
Use Postman, Fiddler or similar to make API calls.
* Available in Azure 
* Responses in JSON 
* 400 Bad Request with Reason for not permitted operations

1. GET http://petgame.azurewebsites.net/animaltypes - Fetches all animal types available. Can keep calling to see health and happiness change over time

2. PUT http://petgame.azurewebsites.net/users - Create a user with following JSON

    {
      UserName: "ed",
      FullName: "Ed Soanes"
    }
	
3. GET http://petgame.azurewebsites.net/users/{username} - Get your user and their list of animals

4. PUT http://petgame.azurewebsites.net/users/{username}/animals - Create a new animal for a given user

    {
      UserName: "ed",
      AnimalTypeId: 1,
      Name: "Anthony"
    }
	
5. POST http://petgame.azurewebsites.net/users/{username}/animals/{animalId}/feed - To feed your selected animal

6. POST http://petgame.azurewebsites.net/users/{username}/animals/{animalId}/pet - To pet your selected animal
