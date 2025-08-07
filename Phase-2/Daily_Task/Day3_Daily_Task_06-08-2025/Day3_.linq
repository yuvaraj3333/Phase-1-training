<Query Kind="Program" />

void Main()
{
    var conferenceRooms = new List<ConferenceRoom>
    {
        new ConferenceRoom { Id = 1, Name = "Ocean View", SeatingCapacity = 50 },
        new ConferenceRoom { Id = 2, Name = "Sky Lounge", SeatingCapacity = 30 },
        new ConferenceRoom { Id = 3, Name = "Green Room", SeatingCapacity = 20 }
    };
 conferenceRooms.Dump("Conference Rooms List");
    var trees = new List<Tree>
    {
        new Tree { Id = 1, Name = "Banyan", Detail = "Large shade tree", ConferenceRoomId = 1 },
        new Tree { Id = 2, Name = "Neem", Detail = "Medicinal tree", ConferenceRoomId = 2 },
        new Tree { Id = 3, Name = "Peepal", Detail = "Sacred tree", ConferenceRoomId = 1 },
        new Tree { Id = 4, Name = "Mango", Detail = "Fruit tree", ConferenceRoomId = 3 }
    };
	trees.Dump("Trees List");

    var result = from t in trees
                 join c in conferenceRooms on t.ConferenceRoomId equals c.Id
                 select new
                 {
                     TreeName = t.Name,
                     TreeDetail = t.Detail,
                     RoomName = c.Name,
                     SeatingCapacity = c.SeatingCapacity
                 };

    result.Dump("Tree + Conference Room Info");
}

public class ConferenceRoom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SeatingCapacity { get; set; }
}

public class Tree
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Detail { get; set; }
    public int ConferenceRoomId { get; set; }
}
