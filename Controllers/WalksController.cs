using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.CustomeActionFilter;
using RESTAPI.Data;
using RESTAPI.Models.Domain;
using RESTAPI.Models.DTO;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers
{
    [EnableCors("AllowReactApp")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
  
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext _context;
        private readonly IAddWalkRepository _walkRepo;
        private readonly IMapper _mapper;
        public WalksController(NZWalksDbContext context ,IAddWalkRepository walkRepo , IMapper mapper )
        {
            _context = context;
            _walkRepo=walkRepo;
            _mapper = mapper;
        }


        //----------  Create Walk
      
        [HttpPost]
        [Route("CreateWalk")]
        [Authorize(Roles = "Writer")]
        //[ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkDTO WalkDTOData)
        {
                if (WalkDTOData == null)
                {
                    return BadRequest();
                }
                // MAP data from DTO into Domin Model Walk
                var walkDominModelData = _mapper.Map<Walk>(WalkDTOData);

                var walkAddedData = await _walkRepo.AddWalkAsync(walkDominModelData);



                 var walkWithRelatedData = _context.Walks
                  .Include(w => w.Difficulty)
                  .Include(w => w.Region)
                  .FirstOrDefault(w => w.Id == walkAddedData.Id);



            // Map Back addedWalk Data into AddWalkDTO

            var WalkDTOMappedData = _mapper.Map<WalksDTO>(walkWithRelatedData);

                return Ok(new
                {
                    Data = WalkDTOMappedData,
                    Message = "Walk Added Successfully!!"
                });
          }




        //----------  Get ALL Walks
        [HttpGet]
        [Route("GetAll")]
       
        //localhost:2023/api/walks/?fileterOn=name & filterQuerry=text
        // for the pagination our endPoint url will be    //localhost:2023/api/walks/?fileterOn=name & filterQuerry=text & pageNumber = &pageSize
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn = null, [FromQuery] string? filterQuerry = null ,
            [FromQuery] string? SortBy=null, [FromQuery]  bool? isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walkDominModelData = await _walkRepo.GetAllWalksAsync(filterOn, filterQuerry, SortBy, isAscending,pageNumber , pageSize);

            // Map Data from DominModel into DTO
            // But yaha hame DTO mai b Navigation Property add krna zarooree hai
            var mappedDTOData = _mapper.Map<List<WalksDTO>>(walkDominModelData);
            return Ok(new
            {
                Data= mappedDTOData,                                                                                                                      
            });
        }


        //---------- Get Walk BY ID
        [HttpGet]
        [Route("GetWalkById/{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult>  GetWalkById([FromRoute] Guid id)
        {
           // Get Data from Domin Model
            var WalkDominModelData = await _walkRepo.GetWalkByIdAsync(id);

            //Map Data from Domin Model into DTO
            var WalkDTOMappedData = _mapper.Map<WalksDTO>(WalkDominModelData);

            return Ok(new
            {
                Data= WalkDTOMappedData, 
            });
        }



        //-----------  Delete Walk 

        [HttpDelete]
        [Route("DeleteWalk/{id:Guid}")]
        //[Authorize(Roles="Writer")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
           
            // Get Data from Database Domin Model
            var DeletedDominWalkData = await _walkRepo.DeleteWalkByIdAsync(id);

            // Map Data from Domin Model into DTO

            var WalkDTOMappedData = _mapper.Map<WalksDTO>(DeletedDominWalkData);
            return Ok(new
            {
                Data = WalkDTOMappedData,
                Message="Deleted Walk Successfully!!"
            });

        }


        //---------- Update Walk

        [HttpPut]
        [Route("UpdateWalk/{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        //[ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id ,[FromBody] UpdateWalkDTO UpdateWalkDTOData )
        {
           
                //First Map Data from DTO into Domin Model

                var walkDTOMappedData = _mapper.Map<Walk>(UpdateWalkDTOData);

                //put in the Database

                var walkUpdatedData = await _walkRepo.UpdateWalkAsync(id, walkDTOMappedData);

                // Again Map back Domin Model Data into DTO

                var walkUpdatedMappedData = _mapper.Map<WalksDTO>(walkUpdatedData);


                return Ok(new
                {
                    Data = walkUpdatedMappedData,
                    Messsage = "Walk Updated Sucessfully!!"

                });
            }
         
           
        }


    };





