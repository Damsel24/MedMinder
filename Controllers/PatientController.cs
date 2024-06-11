using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MedMinder_Api.Models;
using MedMinder_Api.Dtos;
using MedMinder_Api.Data;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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
            string sort_by = null,
            string firstName = null,
            string lastName = null,
            bool? active = null,
            string city = null)
        {
            var patients = await _repo.GetAllPatients();

            // Filtering
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                patients = patients.Where(p => p.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                patients = patients.Where(p => p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
            }

            if (active.HasValue)
            {
                patients = patients.Where(p => p.Active == active.Value);
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                patients = patients.Where(p => p.City.Equals(city, StringComparison.OrdinalIgnoreCase));
            }

            // Sorting
            patients = sort_by?.ToLower() switch
            {
                "firstname" => patients.OrderBy(p => p.FirstName),
                "lastname" => patients.OrderBy(p => p.LastName),
                "active" => patients.OrderBy(p => p.Active),
                "city" => patients.OrderBy(p => p.City),
                _ => patients // No sorting if the sort_by parameter is invalid or not provided
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
        public async Task<ActionResult<PatientReadDto>> CreatePatient(PatientCreateDto cmdCreateDto)
        {
            var patientModel = _mapper.Map<Patient>(cmdCreateDto);
            await _repo.CreatePatient(patientModel);
            await _repo.SaveChanges();

            var cmdReadDto = _mapper.Map<PatientReadDto>(patientModel);
            
            Console.WriteLine($"Model State is: {ModelState.IsValid}");

            return CreatedAtRoute(nameof(GetPatientById), new { Id = cmdReadDto.PatientId }, cmdReadDto);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdatePatient(int id, [FromBody] JsonPatchDocument<PatientUpdateDto> patchDoc)
        {
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



        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient(int id, PatientUpdateDto patientUpdateDto)
        {
            var patientModelFromRepo = await _repo.GetPatientById(id);
            if(patientModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(patientUpdateDto, patientModelFromRepo);

            await _repo.SaveChanges();

            return NoContent();
        }

         //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patientModelFromRepo = await _repo.GetPatientById(id);
            if(patientModelFromRepo == null)
            {
                return NotFound();
            }
            _repo.DeletePatient(patientModelFromRepo);
            await _repo.SaveChanges();

            return NoContent();
        }
    }
}
