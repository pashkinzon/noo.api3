using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Core.Utils.Json;
using Noo.Api.Polls.DTO;
using Noo.Api.Polls.Exceptions;
using Noo.Api.Polls.Filters;
using Noo.Api.Polls.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Polls.Services;

[RegisterScoped(typeof(IPollService))]
public class PollService : IPollService
{
    private readonly IMapper _mapper;

    private readonly IPollRepository _pollRepository;

    private readonly IPollParticipationRepository _pollParticipationRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IPollAnswerRepository _pollAnswerRepository;

    public PollService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _pollRepository = unitOfWork.PollRepository();
        _pollParticipationRepository = unitOfWork.PollParticipationRepository();
        _pollAnswerRepository = unitOfWork.PollAnswerRepository();
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

    public Task<PollModel?> GetPollAsync(Ulid id)
    {
        return _pollRepository.GetByIdAsync(id);
    }

    public Task<PollParticipationModel?> GetPollParticipationAsync(Ulid participationId)
    {
        return _pollParticipationRepository.GetByIdAsync(participationId);
    }

    public Task<SearchResult<PollParticipationModel>> GetPollParticipationsAsync(Ulid pollId, PollParticipationFilter filter)
    {
        filter.PollId = pollId;
        return _pollParticipationRepository.SearchAsync(filter);
    }

    public Task<SearchResult<PollModel>> GetPollsAsync(PollFilter filter)
    {
        return _pollRepository.SearchAsync(filter);
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
