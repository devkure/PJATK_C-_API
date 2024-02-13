/*
 * Author : Jan Brzeziński, s20414
 * 
1)Program otrzymuje cztery argumenty:
    -Ścieżka do pliku z danymi np. "C:\Users\Jan\Desktop\csvData.csv"
    -Ścieżka do folderu, gdzie zostanie wyeksportowany plik wynikowy np. output
    -Ścieżka do pliku z logami logs.txt
    -Format danych w jakich plik ma zostać wyeksportowany np. json
---DONE---

2) Program odczytuje plik z danymi, gdzie każdy wiersz reprezentuje pojedynczego studenta. 
Każda kolumna jest oddzielona znakiem ,. Każdy student powinien być opisywany przez 9 kolumn.
Poniżej zaprezentowany jest pojedynczy wpis w pliku CSV.
Paweł, Nowak1, Informatyka dzienne, Dzienne,459,2000-02-12,1@pjwstk.edu.pl, Alina, Adam
---DONE---

3)Program przetwarza dane w celu usunięcie błędów i brakujących danych.
---DONE---

4)Dane są odpowiednio agregowane.
---DONE---

5)Zapisujemy dane odpowiednio sformatowane zgodnie z rozszerzeniem przekazanym w 4 argumencie do pliku.
---DONE---
*/


// Zaczynamy od odczytania pliku z surowymi danymi
// Wyciągamy z listy argumentów ścieżkę do pliku, pierwszy argument
using System.Text;
using System.Text.Json;

string path = args[0];

// Następnie, asynchronicznie odczytujemy cały plik tekstowy zapisując go do tablicy
string[] lines = await File.ReadAllLinesAsync(path);

// Lista reprezentująca studenta, każdy element jest osobną tablicą imion, nazwisk i innych danych studentów
List<string[]> studentsAsArray = new List<string[]>();

// Lista obiektów student (agregacja)
List<Student> students = new List<Student>();

Log logger = new Log(args[2]);

// Przeiterujmy po całej tablicy:
foreach (string line in lines)
{
    string[] data = line.Split(',');
    

    // Sprawdzamy długość każdej linii, jeśli jest mniejsze niż 9, skipujemy tę linię:
    bool isValid = true;
    
    foreach(string d in data)
    {
        if (d.Length == 0)
        {
            isValid = false;
            logger.log("Wartość w danych studenta była pusta, więc pominięto linię:" + "\n" + line + "\n");
            break;
        }
    }

    if (!isValid) continue;

    // Dodaj do listy
    studentsAsArray.Add(data);
} 

// Utworzenie z tych danych obiektów i dodanie ich do listy obiektów student

foreach(string[] arr in studentsAsArray)
{
    Student student = new Student();
    student.FirstName = arr[0];
    student.LastName = arr[1];
    student.FieldOfStudy = arr[2];
    student.StudyMode = arr[3];
    student.IndexNumber = arr[4];
    student.BirthDate = arr[5];
    student.Email = arr[6];
    student.MotherFirstName = arr[7];
    student.FatherFirstName = arr[8];

    students.Add(student);
}

// Wydrukowanie listy studentów na konsolę

foreach(Student s in students)
{
    Console.WriteLine(s.FirstName + " " + s.LastName + " " + s.FieldOfStudy + " " + s.StudyMode + " " + s.IndexNumber + 
        " " + s.BirthDate + " " + s.Email + " " + s.MotherFirstName + " " + s.FatherFirstName);
}

// Zapis do pliku o odpowiednim formacie, w naszym przypadku będzie to JSON

// Obiekt klasy reprezentującej uczelnię, zawierającą studentów 
University uczelnia = new University()
{
    name = "PJWSTK",
    createdAt = "28.03.2023",
    author = "Jan Brzeziński, s20414",
    students = students
};

// Odczytanie ścieżki do pliku wyjściowego z argumentów
string outputPath = args[1];

// Odczytanie formatu pliku wyjściowego z argumentów
string outputFormat = args[3];

// Serializacja danych do JSON string
string jsonString = JsonSerializer.Serialize(new { uczelnia = uczelnia }, new JsonSerializerOptions { WriteIndented = true });

// Zapisanie JSON string do pliku
File.WriteAllText($"{outputPath}\\output.{outputFormat}", jsonString, Encoding.UTF8);

class Log
{
    private string path;

    public Log(string path)
    {
        this.path = path;
    }

    public void log(string msg)
    {
        File.AppendAllText(path, msg);
    }
}

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FieldOfStudy { get; set; }
    public string StudyMode { get; set; }
    public string IndexNumber { get; set; }
    public string BirthDate { get; set; }
    public string Email { get; set; }
    public string MotherFirstName { get; set; }
    public string FatherFirstName { get; set; }
}

class University
{
    public string name { get; set; }
    public string createdAt { get; set; }
    public string author { get; set; }
    public List<Student> students { get; set; }
}

