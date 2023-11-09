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
public class PersonTypeController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonTypeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonTypeDto>>> Get()
    {
        var con = await _unitOfWork.PersonsTypes.GetAllAsync();

        return _mapper.Map<List<PersonTypeDto>>(con);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonTypeDto>> Get(int id){
        var con = await _unitOfWork.PersonsTypes.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<PersonTypeDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonTypeDto>> Post(PersonTypeDto PersonTypeDto){
        var con = _mapper.Map<PersonType>(PersonTypeDto);
        _unitOfWork.PersonsTypes.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        PersonTypeDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = PersonTypeDto.Id}, PersonTypeDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonTypeDto>> Put(int id, [FromBody]PersonTypeDto PersonTypeDto){
        if(PersonTypeDto.Id == 0){
            PersonTypeDto.Id = id;
        }

        if(PersonTypeDto.Id != id){
            return BadRequest();
        }

        if(PersonTypeDto == null){
            return NotFound();
        }
        var con = _mapper.Map<PersonType>(PersonTypeDto);
        _unitOfWork.PersonsTypes.Update(con);
        await _unitOfWork.SaveAsync();
        return PersonTypeDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var con = await _unitOfWork.PersonsTypes.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.PersonsTypes.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
