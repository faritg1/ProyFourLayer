using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
public class StateController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StateController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<StateDto>>> Get()
    {
        var con = await _unitOfWork.States.GetAllAsync();

        return _mapper.Map<List<StateDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StateDto>> Get(int id){
        var con = await _unitOfWork.States.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<StateDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StateDto>> Post(StateDto StateDto){
        var con = _mapper.Map<State>(StateDto);
        _unitOfWork.States.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        StateDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = StateDto.Id}, StateDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StateDto>> Put(int id, [FromBody]StateDto StateDto){
        if(StateDto.Id == 0){
            StateDto.Id = id;
        }

        if(StateDto.Id != id){
            return BadRequest();
        }

        if(StateDto == null){
            return NotFound();
        }
        var con = _mapper.Map<State>(StateDto);
        _unitOfWork.States.Update(con);
        await _unitOfWork.SaveAsync();
        return StateDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var con = await _unitOfWork.States.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.States.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
