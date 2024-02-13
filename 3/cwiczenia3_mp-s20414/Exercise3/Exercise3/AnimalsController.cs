using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsController(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    //GET zwr�� liste zwierz�t
    [HttpGet]
    public async Task<IActionResult> getAnimals(string? orderBy)
    {
        if (orderBy == null) orderBy = "name";

        //pobranie zwierz�t
        var animals = await _animalsRepository.GetAnimals(orderBy);

        return Ok(animals);
    }

    //POST dodaj nowe zwierz�
    [HttpPost]
    public async Task<IActionResult> AddAnimal(AnimalPOST animalPost)
    {
        //sprawdzenie czy takie samo Id jest w bazie
        /*if(await _animalsRepository.DoesAnimalExist(animalPost.ID)) 
        {
            return Conflict();
        }*/

        //zapisz zwierze do bazy
        await _animalsRepository.AddAnimal(animalPost);

        return Created("api/animals", animalPost);
    }

    [HttpPut("{animalID}")]
    public async Task<IActionResult> UpdateAnimal(int animalID, [FromBody] Animal animal)
    {
        // czy zwierz� o podanym ID istnieje w bazie danych
        bool animalExists = await _animalsRepository.DoesAnimalExist(animalID);
        if (!animalExists)
        {
            return NotFound();
        }

        // aktualizacja danych zwierz�cia w bazie danych
        animal.ID = animalID;
        bool success = await _animalsRepository.UpdateAnimal(animal);

        if (!success)
        {
            return BadRequest();
        }

        // zwr�cenie danych zaktualizowanego zwierz�cia
        Animal updatedAnimal = await _animalsRepository.GetAnimal(animalID);
        return Ok(updatedAnimal);
    }

    [HttpDelete("{animalID}")]
    public async Task<IActionResult> DeleteAnimal(int animalID)
    {
        // sprawdzenie, czy zwierz� o podanym ID istnieje w bazie danych
        bool animalExists = await _animalsRepository.DoesAnimalExist(animalID);
        if (!animalExists)
        {
            return NotFound();
        }

        // usuni�cie zwierz�cia z bazy danych
        bool success = await _animalsRepository.DeleteAnimal(animalID);

        if (!success)
        {
            return BadRequest();
        }

        // zwr�cenie komunikatu o sukcesie
        return Ok("Animal deleted successfully");
    }

}