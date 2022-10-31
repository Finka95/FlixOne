

using TestMsSQL;

var context = new UserContext();

string[] names = { "Jo", "nataly", "vlad", "DEN", "Roger", "Ko", "Mamasita" };

var users = Enumerable.Range(1,7).Select(index => new User 
{
    age = 17 + index,
    name = names[Random.Shared.Next(0,names.Length)],
    bornYear = new DateTime(1970 + Random.Shared.Next(1,40), index, Random.Shared.Next(1,30))

}).ToList();

context.User.AddRange(users);
context.SaveChanges();

foreach (var u in context.User.ToArray())
{
    Console.WriteLine($"Id: {u.id}, Name: {u.name}, was Burn: {u.bornYear}, age: {u.age}");
}
Console.ReadLine();
