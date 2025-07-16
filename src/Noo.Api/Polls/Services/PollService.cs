using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Criteria.Filters;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Core.Utils.Json;
using Noo.Api.Polls.DTO;
using Noo.Api.Polls.Exceptions;
using Noo.Api.Polls.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Polls.Services;

[RegisterScoped(typeof(IPollService))]
public class PollService : IPollService
{
    private readonly Mapper _mapper;

    private readonly IPollRepository _pollRepository;

    private readonly IPollParticipationRepository _pollParticipationRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly ISearchStrategy<PollModel> _pollSearchStrategy;

    private readonly ISearchStrategy<PollParticipationModel> _pollParticipationSearchStrategy;

    private readonly IPollAnswerRepository _pollAnswerRepository;

    public PollService(Mapper mapper, IUnitOfWork unitOfWork, PollSearchStrategy pollSearchStrategy, PollParticipationSearchStrategy pollParticipationSearchStrategy)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _pollRepository = unitOfWork.PollRepository();
        _pollParticipationRepository = unitOfWork.PollParticipationRepository();
        _pollAnswerRepository = unitOfWork.PollAnswerRepository();
        _pollSearchStrategy = pollSearchStrategy;
        _pollParticipationSearchStrategy = pollParticipationSearchStrategy;
        _unitOfWork = unitOfWork;
    }

    public Task<Ulid> CreatePollAsync(CreatePollDTO createPollDto)
    {
        var pollModel = _mapper.Map<PollModel>(createPollDto);
        _pollRepository.Add(pollModel);

        return _unitOfWork.CommitAsync().ContinueWith(_ => pollModel.Id);
    }

    public Task DeletePollAsync(Ulid id)
    {
        _pollRepository.DeleteById(id);
        return _unitOfWork.CommitAsync();
    }

    public Task<PollDTO?> GetPollAsync(Ulid id)
    {
        return _pollRepository.GetByIdAsync<PollDTO>(id, _mapper.ConfigurationProvider);
    }

    public Task<PollParticipationDTO?> GetPollParticipationAsync(Ulid participationId)
    {
        return _pollParticipationRepository.GetByIdAsync<PollParticipationDTO>(participationId, _mapper.ConfigurationProvider);
    }

    public Task<SearchResult<PollParticipationDTO>> GetPollParticipationsAsync(Ulid pollId, Criteria<PollParticipationModel> criteria)
    {
        criteria.AddFilter("pollId", FilterType.Equals, pollId);
        return _pollParticipationRepository.SearchAsync<PollParticipationDTO>(criteria, _pollParticipationSearchStrategy, _mapper.ConfigurationProvider);
    }

    public Task<SearchResult<PollDTO>> GetPollsAsync(Criteria<PollModel> criteria)
    {
        return _pollRepository.SearchAsync<PollDTO>(criteria, _pollSearchStrategy, _mapper.ConfigurationProvider);
    }

    public async Task ParticipateAsync(Ulid pollId, PollParticipationDTO participationDto)
    {
        if (await UserAlreadyParticipatedAsync(pollId, participationDto.UserId, participationDto.UserExternalIdentifier))
        {
            throw new UserAlreadyVotedException();
        }

        var participationModel = _mapper.Map<PollParticipationModel>(participationDto);
        participationModel.PollId = pollId;
        _pollParticipationRepository.Add(participationModel);

        await _unitOfWork.CommitAsync();
    }

    public async Task UpdatePollAnswerAsync(Ulid answerId, JsonPatchDocument<UpdatePollAnswerDTO> updateAnswerDto, ModelStateDictionary modelState)
    {
        var repository = _unitOfWork.PollAnswerRepository();
        var model = await repository.GetByIdAsync(answerId) ?? throw new NotFoundException();

        if (model == null)
        {
            throw new NotFoundException();
        }

        var dto = _mapper.Map<UpdatePollAnswerDTO>(model);

        modelState ??= new ModelStateDictionary();

        updateAnswerDto.ApplyToAndValidate(dto, modelState);

        if (!modelState.IsValid)
        {
            throw new BadRequestException();
        }

        _mapper.Map(dto, model);

        repository.Update(model);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdatePollAsync(Ulid id, JsonPatchDocument<UpdatePollDTO> updatePollDto, ModelStateDictionary? modelState = null)
    {
        var repository = _unitOfWork.PollRepository();
        var model = await repository.GetByIdAsync(id) ?? throw new NotFoundException();

        if (model == null)
        {
            throw new NotFoundException();
        }

        var dto = _mapper.Map<UpdatePollDTO>(model);

        modelState ??= new ModelStateDictionary();

        updatePollDto.ApplyToAndValidate(dto, modelState);

        if (!modelState.IsValid)
        {
            throw new BadRequestException();
        }

        _mapper.Map(dto, model);

        repository.Update(model);
        await _unitOfWork.CommitAsync();
    }

    private Task<bool> UserAlreadyParticipatedAsync(Ulid pollId, Ulid? userId, string? userExternalIdentifier)
    {
        return _pollParticipationRepository
            .ParticipationExistsAsync(pollId, userId, userExternalIdentifier);
    }
}
