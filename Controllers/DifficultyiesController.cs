using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Data;
using RESTAPI.Models.Domain;
using RESTAPI.Models.DTO;
using RESTAPI.Repositories;


namespace RESTAPI.Controllers
{
    [EnableCors("AllowReactApp")]
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyiesController : ControllerBase
    {

        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IMapper _mapper;
        public DifficultyiesController(IDifficultyRepository difficultyRepository, IMapper mapper )
        {
   
            _difficultyRepository= difficultyRepository;
            _mapper= mapper;
        }


        //--- Get Difficulty
        [HttpGet]
        [Route("Difficulties")]
        public async Task<IActionResult> Difficulties()
        {
            var difficultyData = await _difficultyRepository.GetDifficultyAsync(); 

            return Ok(new{
                Data = difficultyData});
        }

        //--- Create Difficulty
        [HttpPost]
        [Route("AddDifficulty")]

        public async Task<IActionResult> AddDifficulty([FromBody] DifficultyDTO difficultydto )
        {
         
                var modelData = _mapper.Map<Difficulty>(difficultydto);
            var result = await _difficultyRepository.AddDifficultyAsync(modelData);

            if(result != null)
            {
                return Ok(new
                {
                   Message="Difficulty Added!",
                   Data=result

                });
            }

            return BadRequest();
        }

        //--- Delete Difficulty

        [HttpDelete]
        [Route("DeleteDifficulty/{id}")]

        public async Task<IActionResult> DeleteDifficulty(Guid id)
        {
             var result = await _difficultyRepository.RemoveDifficultyAsync(id);
          if(result == null)
            {
                     return BadRequest();
            }

            return Ok(new
            {
                Message = "Difficulty Deleted Successfully!",
                Data = result,
            });
        }


        //--- Updated Difficulty

        [HttpPut]
        [Route("UpdateDifficulty/{id}")]

        public async Task<IActionResult> UpdateDifficulty([FromRoute] Guid id,[FromBody] DifficultyDTO difficulty)
        {
             var DominData = _mapper.Map<Difficulty>(difficulty);
            var result= await _difficultyRepository.UpdateDifficultyAsync(id, DominData);
        
            if(result != null)
            {
                return Ok(new
                {
                    Message = "Difficulty Updated Successfully!",
                    Data = result,
                });
            }
            return BadRequest();
        }


        //--> Get Single  Difficulty

        [HttpGet]
        [Route("Difficulty/{id:Guid}")]

        public async Task<IActionResult> GetDifficulty(Guid id)
        {
           var result =  await _difficultyRepository.GetDifficultyAsync(id);
          
            if(result == null)
            {
                return NotFound();
              
            }
            return Ok(new
            {
                Data = result,
            });

        }
    }
}
