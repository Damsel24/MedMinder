using AutoMapper;
using MedMinder_Api.Dtos;
using MedMinder_Api.Models;

namespace MedMinder_Api.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            // Source -> Target
            CreateMap<Patient, PatientReadDto>();
            CreateMap<PatientCreateDto, Patient>();
            CreateMap<PatientUpdateDto, Patient>();
            CreateMap<Patient, PatientUpdateDto>();
        }
    }
}
