using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechBridgeDonation.API.CustomActionFilters;
using TechBridgeDonation.API.Data;
using TechBridgeDonation.API.Models.Domain;
using TechBridgeDonation.API.Models.DTO;
using TechBridgeDonation.API.Repositories;

namespace TechBridgeDonation.API.Controllers
{
    // https://localhost:5001/api/devices
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly TechBridgeDonationDbContext dbContext;
        private readonly IDeviceRepository deviceRepository;
        private readonly IMapper mapper;

        public DevicesController(
            TechBridgeDonationDbContext dbContext,
            IDeviceRepository deviceRepository,
            IMapper mapper
            )
        {
            this.dbContext = dbContext;
            this.deviceRepository = deviceRepository;
            this.mapper = mapper;
        }

        // GET ALL Devices
        // GET: https://localhost:portnumber/api/devices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get a list of Device Domain Models from database
            var devicesDomainModel = await deviceRepository.GetAllAsync();

            // Map domain models to DTOs (with AutoMapper)
            var deviceDto = mapper.Map<List<DeviceDto>>(devicesDomainModel);

            // Return DTOs
            return Ok(deviceDto);
        }

        // GET SINGLE Devices BY ID
        // GET: https://localhost:portnumber/api/devices/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var deviceDomain = await deviceRepository.GetByIdAsync(id);

            if (deviceDomain == null)
            {
                return NotFound();
            }

            // Map domain model to DTO (with AutoMapper)
            var deviceDto = mapper.Map<DeviceDto>(deviceDomain);

            // Return DTO
            return Ok(deviceDto);
        }

        // POST to Create New Device
        // POST: https://localhost:portnumber/api/organisations
        [HttpPost]
        [ValidateModel]  // Rules in AddDeviceRequestDto
        public async Task<IActionResult> Create([FromBody] AddDeviceRequestDto addDeviceRequestDto)
        {
            // Map DTO to Domain Model (with AutoMapper)
            var deviceDomainModel = mapper.Map<Device>(addDeviceRequestDto);

            // Use Domain Model to create Device
            deviceDomainModel = await deviceRepository.CreateAsync(deviceDomainModel);

            // Map Domain Model back to DTO to send the result back to frontend (with AutoMapper)
            var deviceDto = mapper.Map<DeviceDto>(deviceDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = deviceDto.Id }, deviceDto);

        }


        // PUT to Update Device
        // PUT: https://localhost:portnumber/api/devices/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]  // Rules in UpdateDeviceRequestDto
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDeviceRequestDto updateDeviceRequestDto)
        {
            // Map DTO to Domain Model
            var updateDeviceDomainModel = mapper.Map<Device>(updateDeviceRequestDto);

            var updatedDeviceDomainModel = await deviceRepository.UpdateAsync(id, updateDeviceDomainModel);

            if (updatedDeviceDomainModel == null)
            {
                return NotFound();
            }

            // Return the updated Device back to client
            // Map Domain Model to DTO (with AutoMapper)
            var deviceDto = mapper.Map<DeviceDto>(updatedDeviceDomainModel);

            // Return DTOs
            return Ok(deviceDto);

        }

        // Delete one Device
        // DELETE: https://localhost:portnumber/api/devices/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedDeviceDomainModel = await deviceRepository.DeleteAsync(id);

            if (deletedDeviceDomainModel == null)
            {
                return NotFound();
            }

            // Return the deleted Device back to client
            // Map Domain Model to DTO
            var deviceDto = mapper.Map<DeviceDto>(deletedDeviceDomainModel);

            return Ok(deviceDto);
        }

    }
}
