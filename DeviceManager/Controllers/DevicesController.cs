using AutoMapper;
using DeviceManager.API.AutoMapperApi;
using DeviceManager.API.ViewModels;
using DeviceManager.Busniess.Commands.DevicesCommands;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Exceptions.BaseException;
using DeviceManager.Busniess.Exceptions.DevicesExceptions;
using DeviceManager.Busniess.Queries;
using DeviceManager.Busniess.Queries.DevicesQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {

        private readonly ILogger<DevicesController> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public DevicesController(ILogger<DevicesController> logger, IMediator mediator, IMapper mapper)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DeviceVM>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Devices()
        {

            try
            {
                IEnumerable<DeviceDTO> deviceListOfDTOs = await mediator.Send(new GetDevicesListQuery());

                IEnumerable<DeviceVM> deviceListOfViewModels = mapper.Map<IEnumerable<DeviceVM>>(deviceListOfDTOs);

                return Ok(deviceListOfViewModels);
            }
            catch (AplicationBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return StatusCode(500);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Device(int id)
        {
            try
            {
                DeviceDTO deviceWithGivenId = await mediator.Send(new GetDeviceByIdQuery(id));

                DeviceVM deviceWithGivenId_AsViewModel = mapper.Map<DeviceVM>(deviceWithGivenId);
                return Ok(deviceWithGivenId_AsViewModel);
            }
            catch (DeviceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AplicationBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return StatusCode(500);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDevice(int id)
        {
            try
            {
                var success = await mediator.Send(new DeleteDeviceByIdCommand(id));

                return Ok();
            }
            catch (DeviceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AplicationBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return StatusCode(500);
            }
        }


        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddDevice(AddDeviceVM deviceToBeAdded)
        {
            try
            {
                AddDeviceDTO deviceToBeAdded_AsDTO = mapper.Map<AddDeviceDTO>(deviceToBeAdded);
                DeviceDTO deviceCreated_AsDTO = await mediator.Send(new AddDeviceCommand(deviceToBeAdded_AsDTO));

                DeviceVM deviceCreated_AsViewModel = mapper.MapDeviceDTO_To_DeviceVM(deviceCreated_AsDTO);
                return Ok(deviceCreated_AsViewModel);
            }
            catch(DeviceAlreadyExistsException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (AplicationBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DeviceVM))]
        public async Task<ActionResult> EditDevice(int id, EditDeviceVM deviceToEddited)
        {
            try
            {
                EditDeviceDTO deviceToBeEddited_AsDTO = mapper.Map<EditDeviceDTO>(deviceToEddited);
                DeviceDTO deviceModified = await mediator.Send(new EditDeviceCommand(id, deviceToBeEddited_AsDTO));

                return Ok(mapper.MapDeviceDTO_To_DeviceVM(deviceModified));
            }
            catch (DeviceNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (AplicationBaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return StatusCode(500);
            }
        }


        [HttpGet("AvailableOperatingSystems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DeviceDTO))]
        public async Task<ActionResult> AvailableOperatingSystems()
        {
            try
            {
                var result = await mediator.Send(new GetAvailableOperatingSystemsQuery());

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return StatusCode(500);
            }
        }

    }
}
