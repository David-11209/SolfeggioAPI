using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SolfeggioAPI.Models;
using SolfeggioAPI.Services;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SolfeggioAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SolfeggioController : ControllerBase
{
    public static String items;
    public static String users;

    public SolfeggioController()
    {
        items = JsonService.GetJson();
        users = JsonService.GetUsers();
    }

    /// <summary>
    /// Метод для получения всех заданий
    /// </summary>
    /// <returns> Возвращает список всех заданий </returns>
    [HttpGet]
    public String GetAll()
    {
        return items;
    }

    [HttpPost("getUser")]
    public async Task<User> GetUser()
    {
        var model = await DeserializeRequestBody<UserModel>();
        var user = AuthenticateUser(model.Login, model.Password);
        if (user == null)
        {
            return user;
        }
        else
        {
            return user;
        }
    }
    
    [HttpPost("addUser")]
    public async Task<IActionResult> AddNewUser()
    {
        var model = await DeserializeRequestBody<UserModel>();
        var result = RegisterUser(model.Name, model.Login, model.Password, model.Image);
        if (result == false) 
        {
            return BadRequest();
        }
        return Ok();
    }
    
    [HttpPost("updateUserInfo")]
    public async Task<IActionResult> UpdateUser()
    {
        var model = await DeserializeRequestBody<User>();

        if (UpdateUser(model))
        {
            return Ok();
        }
        return BadRequest();
    }

    private async Task<User> DeserializeRequestBody<User>()
    {
        var requestBody = string.Empty;
        using (var reader = new StreamReader(Request.Body))
        {
            requestBody = await reader.ReadToEndAsync();
        }
        var model = JsonConvert.DeserializeObject<User>(requestBody); 
        
        return model;
    }

    private User AuthenticateUser(string login, string password)
    {   
        string jsonUsers = System.IO.File.ReadAllText("users.json");
        UserList userList = JsonConvert.DeserializeObject<UserList>(jsonUsers);
        User user = userList.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        return user;
    }

    private bool RegisterUser(string name, string login, string password, string image)
    {
        string jsonUsers = System.IO.File.ReadAllText("users.json");
        UserList userList = JsonConvert.DeserializeObject<UserList>(jsonUsers);
        User user = userList.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        if (user == null)
        {
            User newUser = new User
            {
                Name = name,
                Login = login,
                Password = password,
                Image = image,
                NotesStat = "0",
                IntervalsStat = "0",
                MoodsStat = "0",
                ChordsStat = "0",
                NumberNotesListened = "0",
                NumberIntervalsListened = "0",
                NumberMoodsListened = "0",
                NumberChordsListened = "0",
                NumberNotesSuccessListened = "0",
                NumberIntervalsSuccessListened = "0",
                NumberMoodsSuccessListened = "0",
                NumberChordsSuccessListened = "0",
                CompletedLevels = new List<CompletedLevel>()
            };

            userList.Users.Add(newUser);
            string updatedJsonUsers = JsonConvert.SerializeObject(userList, Formatting.Indented);
            System.IO.File.WriteAllText("users.json", updatedJsonUsers);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool UpdateUser(User user)
    {
        string jsonUsers = System.IO.File.ReadAllText("users.json");
        UserList userList = JsonConvert.DeserializeObject<UserList>(jsonUsers);
        User userModel = userList.Users.Find(u => u.Login == user.Login && u.Password == user.Password);
        if (userModel != null)
        {
            userModel.NotesStat = user.NotesStat;
            userModel.IntervalsStat = user.IntervalsStat;
            userModel.MoodsStat = user.MoodsStat;
            userModel.ChordsStat = user.ChordsStat;
            
            userModel.NumberNotesListened = user.NumberNotesListened;
            userModel.NumberIntervalsListened = user.NumberIntervalsListened;
            userModel.NumberMoodsListened = user.NumberMoodsListened;
            userModel.NumberChordsListened = user.NumberChordsListened;

            userModel.NumberNotesSuccessListened = user.NumberNotesSuccessListened;
            userModel.NumberIntervalsSuccessListened = user.NumberIntervalsSuccessListened;
            userModel.NumberMoodsSuccessListened = user.NumberMoodsSuccessListened;
            userModel.NumberChordsSuccessListened = user.NumberChordsSuccessListened;
            
            userModel.CompletedLevels = new List<CompletedLevel> { new CompletedLevel{ Id = "1" } };
            SaveUsers(user);
            return true;
        }

        return false;
    }
    private void SaveUsers(User user)
    {
        string json = System.IO.File.ReadAllText("users.json");
        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

        foreach (var u in jsonObj.Users)
        {
            if (u.Login == user.Login && u.Password == user.Password)
            {
                u.NotesStat = JToken.FromObject(user.NotesStat);
                u.NumberNotesListened = JToken.FromObject(user.NumberNotesListened);
                u.NumberNotesSuccessListened = JToken.FromObject(user.NumberNotesSuccessListened);
                
                u.IntervalsStat = JToken.FromObject(user.IntervalsStat);
                u.NumberIntervalsListened = JToken.FromObject(user.NumberIntervalsListened);
                u.NumberIntervalsSuccessListened = JToken.FromObject(user.NumberIntervalsSuccessListened);
                
                u.MoodsStat = JToken.FromObject(user.MoodsStat);
                u.NumberMoodsListened = JToken.FromObject(user.NumberMoodsListened);
                u.NumberMoodsSuccessListened = JToken.FromObject(user.NumberMoodsSuccessListened);
                
                u.ChordsStat = JToken.FromObject(user.ChordsStat);
                u.NumberChordsListened = JToken.FromObject(user.NumberChordsListened);
                u.NumberChordsSuccessListened = JToken.FromObject(user.NumberChordsSuccessListened);
                u.CompletedLevels = JToken.FromObject(user.CompletedLevels);
                break;
            }
        }

        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
        System.IO.File.WriteAllText("users.json", output);
    }

}