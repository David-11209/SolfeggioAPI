namespace SolfeggioAPI.Models;

public class UserList
{
    public List<User> Users { get; set; }
}

public class User
{
    public String? Name { get; set; }
    public String Login { get; set; }
    public String Password { get; set; }
    public String? Image { get; set; }
    
    public String? NotesStat { get; set; }
    public String? IntervalsStat { get; set; }
    public String? MoodsStat { get; set; }
    public String? ChordsStat { get; set; }
    
    public String? NumberNotesListened { get; set; }
    public String? NumberIntervalsListened { get; set; }
    public String? NumberMoodsListened { get; set; }
    public String? NumberChordsListened { get; set; }
    public String? NumberNotesSuccessListened { get; set; }
    public String? NumberIntervalsSuccessListened { get; set; }
    public String? NumberMoodsSuccessListened { get; set; }
    public String? NumberChordsSuccessListened { get; set; }
    public List<CompletedLevel> CompletedLevels { get; set; }
}

public class CompletedLevel
{
    public string Id { get; set; }
}

public class UserModel
{
    public String? Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public String? Image { get; set; }
    
    public string NotesStat { get; set; }
    public string IntervalsStat { get; set; }
    public string MoodsStat { get; set; }
    public string ChordsStat { get; set; }
    public String? NumberNotesListened { get; set; }
    public String? NumberIntervalsListened { get; set; }
    public String? NumberMoodsListened { get; set; }
    public String? NumberChordsListened { get; set; }
    public String? NumberNotesSuccessListened { get; set; }
    public String? NumberIntervalsSuccessListened { get; set; }
    public String? NumberMoodsSuccessListened { get; set; }
    public String? NumberChordsSuccessListened { get; set; }
}