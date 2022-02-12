using Customer.Contact.Core.Dtos;
using Customer.Contact.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customer.Contact.Api.Controllers
{
    [Route("api/customer/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ContactResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var contactDetails = await _contactService.GetAllContactDetails();
                if (contactDetails.Count() == 0)
                {
                    _logger.LogInformation("Couldn't found customer contact details");
                    return NotFound();
                }

                _logger.LogInformation("Found customer contact details");
                return Ok(contactDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while processing request {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error retrieving contact info");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var contact = await _contactService.GetContactDetailsById(id);
                if (contact == null)
                {
                    _logger.LogInformation("Couldn't found customer contact details for id {0}", id);
                    return NotFound();
                }

                _logger.LogInformation("Found customer contact details for id{0}", id);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while processing request {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error retrieving contact info");
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ContactRequestDto contactRequest)
        {
            try
            {
                if (contactRequest == null)
                {
                    return BadRequest();
                }
                int id = await _contactService.CreateContact(contactRequest);

                _logger.LogInformation("Customer contact details are saved successfully");
                return CreatedAtAction(nameof(Get), new { Id = id }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while processing request {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error storing contact info");
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ContactResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Put(int id, [FromBody] ContactRequestDto contactDetails)
        {
            try
            {
                var contactToUpdate = await _contactService.GetContactDetailsById(id);

                if (contactToUpdate == null)
                {
                    return NotFound("Contact details not found to update");
                }
                await _contactService.UpdateContactDetails(contactDetails, id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while processing request {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating contact info");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var contactDetails = await _contactService.GetContactDetailsById(id);

                if (contactDetails == null)
                {
                    _logger.LogInformation("Contact details is not found for id {0}", id);
                    return NotFound("Contact details not found to delete");
                }

                await _contactService.DeleteContactDetails(id);
                _logger.LogInformation("Contact details is removed successfully for id {0}", id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while processing request {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error deleting contact info");
            }
        }
    }
}
