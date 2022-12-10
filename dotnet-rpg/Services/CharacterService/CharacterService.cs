using AutoMapper;
using dotnet_rpg.Dtos.Character;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;

        private static List<Character> characters = new List<Character>
        {
            new Character { Id = 0 },
            new Character { Id = 1, Name = "Sam", Class = RpgClass.Mage }
        };

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            Character newCharacter = _mapper.Map<Character>(character);
            newCharacter.Id = characters.Max(c => c.Id) + 1;

            characters.Add(newCharacter);

            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = _mapper.Map<List<GetCharacterDto>>(characters),
                Success = true
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            try
            {
                GetCharacterDto character = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
                if (character == null) throw new ArgumentException("character not found by id " + id);

                return new ServiceResponse<GetCharacterDto>
                {
                    Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id)),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<GetCharacterDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacterList()
        {
            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = _mapper.Map<List<GetCharacterDto>>(characters),
                Success = true
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto, long id)
        {

            try
            {

                Character toUpdate = characters.FirstOrDefault(c => c.Id == id);
                if (toUpdate == null) throw new ArgumentException("Character not found. Bad ID.");

                toUpdate.Name = updateCharacterDto.Name == null ?
                    toUpdate.Name : updateCharacterDto.Name;

                toUpdate.HitPoints = updateCharacterDto.HitPoints != null ?
                    updateCharacterDto.HitPoints.Value : toUpdate.HitPoints;

                toUpdate.Strength = updateCharacterDto.Strength != null ?
                    updateCharacterDto.Strength.Value : toUpdate.Strength;

                toUpdate.Defence = updateCharacterDto.Defence != null ?
                    updateCharacterDto.Defence.Value : toUpdate.Defence;

                toUpdate.Intelligence = updateCharacterDto.Intelligence != null ?
                    updateCharacterDto.Intelligence.Value : toUpdate.Intelligence;

                toUpdate.Class = updateCharacterDto.Class != null ?
                    updateCharacterDto.Class.Value : toUpdate.Class;

                toUpdate = _mapper.Map<Character>(updateCharacterDto);
                GetCharacterDto response = _mapper.Map<GetCharacterDto>(characters.Find(c => c.Id == id));
                return new ServiceResponse<GetCharacterDto>
                {
                    Data = response,
                    Success = true,
                    Message = "character was successfully updated"
                };

            }
            catch (Exception ex)
            {

                return new ServiceResponse<GetCharacterDto>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }


        }

        public async Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(int id)
        {
            try
            {
                Character toDelete = characters.FirstOrDefault(c => c.Id == id);
                if (toDelete == null) throw new ArgumentException("Character not found. Bad ID.");

                characters.Remove(toDelete);

                return new ServiceResponse<GetCharacterDto>
                {
                    Data = _mapper.Map<GetCharacterDto>(toDelete),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<GetCharacterDto>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }

        }
    }
}
