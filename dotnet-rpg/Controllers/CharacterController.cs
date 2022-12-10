using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
        {
            return Ok(await _characterService.GetCharacterList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetOne(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost("New")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Add(AddCharacterDto character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Update(UpdateCharacterDto character, long id)
        {

            var response = await _characterService.UpdateCharacter(character, id);
            if (response.Success == false)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteOne(int id)
        {
            return Ok(await _characterService.DeleteCharacter(id));
        }

    }
}
