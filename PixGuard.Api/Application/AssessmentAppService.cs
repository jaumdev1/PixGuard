using PixGuard.Api.Application.Contracts;
using Domain.Entities;
using Domain.DTOs;
using Domain.Contracts;
using Domain.Exceptions;
using PixGuard.Api.Application.Contracts.Mappers;

namespace PixGuard.Api.Application;

public class AssessmentAppService:  IAppService<AssessmentDto, CreateAssessmentDto>
{
    private readonly IRepository<Assessment> _assessmentRepository;
    private readonly AssessmentMapper _assessmentMapper;
    public AssessmentAppService(IRepository<Assessment> assessmentRepository, AssessmentMapper assessmentMapper)
    {
        _assessmentRepository = assessmentRepository;
        _assessmentMapper = assessmentMapper;
      
    }
    public async Task<AssessmentDto> GetById(Guid id)
    {
        var assessment = await _assessmentRepository.GetById(id);
        
        var assessmentDto =  _assessmentMapper.ToDto(assessment);
       
        return assessmentDto;
    }

    public async Task<List<AssessmentDto>> GetAll()
    {
        var assessmentList = await _assessmentRepository.GetAll();

        if (assessmentList == null)
        {
            throw new Exception("Could not  get all AssessmentDto objects from AssessmentRepository");
        }
        
        var assessmentsDto = assessmentList.Select(assessment => _assessmentMapper.ToDto(assessment)).ToList();
        return assessmentsDto;
    }

    public async Task<Guid> Add(CreateAssessmentDto createDto)
    {
      
            var assessment = _assessmentMapper.ToEntity(createDto);

            await _assessmentRepository.Add(assessment);

            return assessment.Id;
            


        
            
      
       
    }
    
  
}