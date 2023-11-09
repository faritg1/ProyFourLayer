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
public class CityController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CityController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CityDto>>> Get()
    {
        var city = await _unitOfWork.Cities.GetAllAsync();

        return _mapper.Map<List<CityDto>>(city);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDto>> Get(int id){
        var city = await _unitOfWork.Cities.GetByIdAsync(id);
        if (city == null){
            return NotFound();
        }
        return _mapper.Map<CityDto>(city);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CityDto>> Post(CityDto CityDto){
        var city = _mapper.Map<City>(CityDto);
        _unitOfWork.Cities.Add(city);
        await _unitOfWork.SaveAsync();
        if(city == null){
            return BadRequest();
        }
        CityDto.Id = city.Id;
        return CreatedAtAction(nameof(Post), new {id = CityDto.Id}, CityDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDto>> Put(int id, [FromBody]CityDto CityDto){
        if(CityDto.Id == 0){
            CityDto.Id = id;
        }

        if(CityDto.Id != id){
            return BadRequest();
        }

        if(CityDto == null){
            return NotFound();
        }
        var city = _mapper.Map<City>(CityDto);
        _unitOfWork.Cities.Update(city);
        await _unitOfWork.SaveAsync();
        return CityDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var city = await _unitOfWork.Cities.GetByIdAsync(id);
        if(city == null){
            return NotFound();
        }
        _unitOfWork.Cities.Remove(city);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
