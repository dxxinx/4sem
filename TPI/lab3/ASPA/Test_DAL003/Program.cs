using DAL003;

Repository.JSONFileName = "Celebrities.json";
using (IRepository repository = Repository.Create("Celebrities"))
{
    // 1. Все знаменитости
    foreach (var c in repository.getAllCelebrities())
        Console.WriteLine($"Id = {c.Id}, Firstname = {c.Firstname}, Surname = {c.Surname}, PhotoPath = {c.PhotoPath}");

    // 2. По Id
    var c1 = repository.GetCelebrityById(1);
    if (c1 != null) Console.WriteLine($"Id = {c1.Id}, Firstname = {c1.Firstname}, Surname = {c1.Surname}, PhotoPath = {c1.PhotoPath}");

    var c3 = repository.GetCelebrityById(3);
    if (c3 != null) Console.WriteLine($"Id = {c3.Id}, Firstname = {c3.Firstname}, Surname = {c3.Surname}, PhotoPath = {c3.PhotoPath}");

    // 3. Несуществующий Id
    var c222 = repository.GetCelebrityById(222);
    if (c222 == null) Console.WriteLine("Not Found 222");

    // 4. По фамилии
    foreach (var c in repository.GetCelebritiesBySurname("Chomsky"))
        Console.WriteLine($"Id = {c.Id}, Firstname = {c.Firstname}, Surname = {c.Surname}, PhotoPath = {c.PhotoPath}");

    // 5. Пути к фото
    Console.WriteLine($"PhotoPathById = {repository.getPhotoPathById(4)}");
    Console.WriteLine($"PhotoPathById = {repository.getPhotoPathById(6)}");
    Console.WriteLine($"PhotoPathById = {repository.getPhotoPathById(222)}");
}