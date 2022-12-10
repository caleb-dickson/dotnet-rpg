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
            return new ServiceResponse<GetCharacterDto>
            {
                Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id)),
                Success = true
            };
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacterList()
        {
            return new ServiceResponse<List<GetCharacterDto>>
            {
                Data = _mapper.Map<List<GetCharacterDto>>(characters),
                Success = true
            };
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            Character toUpdate = characters.FirstOrDefault(c => c.Id == updateCharacterDto.Id);

            if (toUpdate != null)
            {
                toUpdate.Name = updateCharacterDto.Name != null ?
                    updateCharacterDto.Name : toUpdate.Name;

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
                GetCharacterDto response = _mapper.Map<GetCharacterDto>(characters.Find(c => c.Id == updateCharacterDto.Id));
                return new ServiceResponse<GetCharacterDto>
                {
                    Data = response,
                    Success = true
                };
            }
            else
            {
                throw new Exception("Not Found");
            }
        }
    }
}
