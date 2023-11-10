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
public class CountryController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CountryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CountryDto>>> Get()
    {
        var con = await _unitOfWork.Countries.GetAllAsync();

        return _mapper.Map<List<CountryDto>>(con);
    }

    [HttpGet("getCountriesAndStatesAndCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetCoutriesStateCities()
    {
        var countries = await _unitOfWork.Countries.getCountriesAndStatesAndCities();
        return _mapper.Map<List<CountryDto>>(countries);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDto>> Get(int id){
        var con = await _unitOfWork.Countries.GetByIdAsync(id);
        if (con == null){
            return NotFound();
        }
        return _mapper.Map<CountryDto>(con);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CountryDto>> Post(CountryDto CountryDto){
        var con = _mapper.Map<Country>(CountryDto);
        _unitOfWork.Countries.Add(con);
        await _unitOfWork.SaveAsync();
        if(con == null){
            return BadRequest();
        }
        CountryDto.Id = con.Id;
        return CreatedAtAction(nameof(Post), new {id = CountryDto.Id}, CountryDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDto>> Put(int id, [FromBody]CountryDto CountryDto){
        if(CountryDto.Id == 0){
            CountryDto.Id = id;
        }

        if(CountryDto.Id != id){
            return BadRequest();
        }

        if(CountryDto == null){
            return NotFound();
        }
        var con = _mapper.Map<Country>(CountryDto);
        _unitOfWork.Countries.Update(con);
        await _unitOfWork.SaveAsync();
        return CountryDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var con = await _unitOfWork.Countries.GetByIdAsync(id);
        if(con == null){
            return NotFound();
        }
        _unitOfWork.Countries.Remove(con);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
