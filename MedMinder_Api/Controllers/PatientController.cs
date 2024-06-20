using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MedMinder_Api.Models;
using MedMinder_Api.Dtos;
using MedMinder_Api.Data;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq; // Added System.Linq for LINQ operations

namespace MedMinder_Api.Controllers
{
    [ServiceFilter(typeof(TestAsyncActionFilter))]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepo _repo;
        private readonly IMapper _mapper;

        public PatientController(IPatientRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("SortPatients")]
        public async Task<ActionResult<IEnumerable<PatientReadDto>>> GetPatients(
            string? sort_by = null,
            string? firstName = null,
            string? lastName = null,
            string? active = null,
            string? city = null)
        {
            var patients = await _repo.GetAllPatients();

            if (patients == null)
            {
                return NotFound("No patients found");
            }

            // Filtering
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                patients = patients.Where(p => p.FirstName != null && p.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                patients = patients.Where(p => p.LastName != null && p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(active))
            {
                patients = patients.Where(p => p.Active != null && p.Active.Equals(active, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                patients = patients.Where(p => p.City != null && p.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sorting
            patients = sort_by?.ToLower() switch
            {
                "firstname" => patients.OrderBy(p => p.FirstName).ToList(),
                "lastname" => patients.OrderBy(p => p.LastName).ToList(),
                "active" => patients.OrderBy(p => p.Active).ToList(),
                "city" => patients.OrderBy(p => p.City).ToList(),
                _ => patients
            };

            return Ok(_mapper.Map<IEnumerable<PatientReadDto>>(patients));
        }

        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<IEnumerable<PatientReadDto>>> GetAllPatients()
        {
            var patients = await _repo.GetAllPatients();
            return Ok(_mapper.Map<IEnumerable<PatientReadDto>>(patients));
        }

        [HttpGet("{id}", Name = "GetPatientById")]
        public async Task<ActionResult<PatientReadDto>> GetPatientById(int id)
        {
            var patientModel = await _repo.GetPatientById(id);
            if (patientModel != null)
            {
                return Ok(_mapper.Map<PatientReadDto>(patientModel));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PatientReadDto>> CreatePatient([FromBody] PatientCreateDto cmdCreateDto)
        {
            if (cmdCreateDto == null)
            {
                return BadRequest("Patient data is null");
            }

            var patientModel = _mapper.Map<Patient>(cmdCreateDto);
            await _repo.CreatePatient(patientModel);
            await _repo.SaveChanges();

            var cmdReadDto = _mapper.Map<PatientReadDto>(patientModel);

            return CreatedAtRoute(nameof(GetPatientById), new { Id = cmdReadDto.PatientId }, cmdReadDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdatePatient(int id, [FromBody] JsonPatchDocument<PatientUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document is null");
            }

            var patientModelFromRepo = await _repo.GetPatientById(id);
            if (patientModelFromRepo == null)
            {
                return NotFound();
            }

            var patientToPatch = _mapper.Map<PatientUpdateDto>(patientModelFromRepo);

            patchDoc.ApplyTo(patientToPatch, ModelState);

            if (!TryValidateModel(patientToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(patientToPatch, patientModelFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] PatientUpdateDto patientUpdateDto)
        {
            if (patientUpdateDto == null)
            {
                return BadRequest("Patient data is null");
            }

            var patientModelFromRepo = await _repo.GetPatientById(id);
            if (patientModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(patientUpdateDto, patientModelFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patientModelFromRepo = await _repo.GetPatientById(id);
            if (patientModelFromRepo == null)
            {
                return NotFound();
            }

            _repo.DeletePatient(patientModelFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }
    }
}
