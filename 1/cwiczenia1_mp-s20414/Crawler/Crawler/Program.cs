using System.Text.RegularExpressions;

/* STEP 1:
 * Program otrzymuje pojedynczy argument, który jest adresem URL strony, która będzie celem skanu "crawlera" 
 */

if (args.Length < 1) throw new ArgumentNullException("Argument nie został przekazany"); //Exception do sytuacji w której argument nie został przekazany

string url = args[0]; //Pierwszym argumentem będzie URL

/*STEP 2:
 * Za pomocą klasy HttpClient wykonuje żądanie HTTP GET i pobiera kod źródłowy strony internetowej
 */

var httpClient = new HttpClient(); //HTTP Client, czyli umożliwienie dalszego procesu obsługi rządań

var a = "\r";
var b = @"\r";
var c = $"{a} {b}";

//Znajdujący email regex
var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");
var regexUrl = new Regex(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");

if (!regexUrl.IsMatch(url)) throw new ArgumentException("Podany adres URL jest nieprawidłowy"); //Exception dla sytuacji gdy został przekazany argument, który nie jest poprawnym adresem URL

//Obsługa response'a 
HttpResponseMessage response = await httpClient.GetAsync(url);

if (!response.IsSuccessStatusCode) throw new Exception("Błąd w czasie pobierania strony"); //Exception dla sytuacji, gdy podczas pobierania strony wystąpi błąd

/*STEP 3: 
 *Przeszukuje zawartość strony i wypisuje na konsoli wszystkie adresy email, które zostały znalezione na stronie
 */

//Zawartość strony
string content = await response.Content.ReadAsStringAsync();

//Znalezienie w zawartości emaila/emaili regexem
MatchCollection matches = regex.Matches(content);

if (matches.Count > 0)
{

    //wydrukowanie ich na konsoli
    foreach (Match match in matches)
    {
        Console.WriteLine(match);
    }
} else
{
    throw new Exception("Nie znaleziono adresów email"); //Exception dla sytuacji, gdy nie znaleziono żadnych adresów email
}